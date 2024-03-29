﻿using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using YBOInvestigation.Factories;
using YBOInvestigation.Models;
using YBOInvestigation.Util;

namespace YBOInvestigation.Controllers.YBORecord
{
    public class YboRecordController : Controller
    {
        private readonly ServiceFactory _serviceFactory;
        public YboRecordController(ServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        private void AddSearchDatasToViewBag(string searchString)
        {
            ViewBag.SearchString = searchString;
        }

        private string MakeExcelFileName(string searchString, bool ExportAll, int? pageNo)
        {
            if (ExportAll)
            {
                return "YBOဖမ်းစီးမှုမှတ်တမ်းအားလုံး" + DateTime.Now + ".xlsx";
            }
            else
            {
                if (searchString != null && !string.IsNullOrEmpty(searchString))
                    return "YBOဖမ်းစီးမှုမှတ်တမ်းရှာဖွေမှု(" + searchString + ")" + DateTime.Now + ".xlsx";
                else
                    return "YBOဖမ်းစီးမှုမှတ်တမ်း PageNumber( " + pageNo + " )" + DateTime.Now + ".xlsx";
            }

        }
        public IActionResult List(int? pageNo)
        {
            try
            {
                if (!SessionUtil.IsActiveSession(HttpContext))
                    return RedirectToAction("Index", "Login");

                string searchString = Request.Query["SearchString"];

                int pageSize = Utility.DEFAULT_PAGINATION_NUMBER;
                AddSearchDatasToViewBag(searchString);
                PagingList<YboRecord> yboRecords = _serviceFactory.CreateYBORecordService().GetAllYboRecordsWithPagin(searchString, pageNo, pageSize);
                if (Request.Query["export"] == "excel")
                {
                    bool ExportAll = Request.Query["ExportAll"] == "true";
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        var ws = wb.Worksheets.Add(_serviceFactory.CreateYBORecordService().MakeYboRecordExcelData(yboRecords, ExportAll));
                        ws.Rows().AdjustToContents();
                        ws.Rows().Height = 20;
                        ws.Columns().AdjustToContents();
                        ws.Columns().Style.Font.FontSize = 12;
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", MakeExcelFileName(searchString, ExportAll, pageNo));
                        }
                    }


                }
                return View(yboRecords);
            }
            catch (NullReferenceException ne)
            {
                Utility.AlertMessage(this, "Data Issue. Please fill YboRecord in database", "alert-danger");
                return View();
            }
            catch (SqlException se)
            {
                Utility.AlertMessage(this, "Internal Server Error", "alert-danger");
                return View();
            }
        }


        private void AddViewBag(int vehicleId = 0)
        {
            VehicleData vehicleData = _serviceFactory.CreateVehicleDataService().FindVehicleDataByIdContainSoftDeleteEgerLoad(vehicleId);
            List<Driver> drivers = _serviceFactory.CreateDriverService().GetDriversByVehicleDataId(vehicleData.VehicleDataPkid).Where(driver => driver.VehicleData.VehicleNumber == vehicleData.VehicleNumber).ToList();
            ViewBag.YBSCompany = _serviceFactory.CreateYBSCompanyService().FindYBSCompanyById(vehicleData.YBSCompany.YBSCompanyPkid);//.GetSelectListYBSCompanys();
            ViewBag.YBSType = _serviceFactory.CreateYBSTypeService().FindYBSTypeById(vehicleData.YBSType.YBSTypePkid);//.GetSelectListYBSTypesByYBSCompanyId(vehicleData.YBSCompany.YBSCompanyPkid);
            ViewBag.VehicleNumber = vehicleData.VehicleNumber;
            ViewBag.AutoComplete = drivers
                .Select(driver => new { DriverPkId = driver.DriverPkid, DriverName = driver.DriverName, DriverLicense = driver.DriverLicense })
                .ToList();
        }
        public IActionResult Create(int vehicleId)
        {
            if (!SessionUtil.IsActiveSession(HttpContext))
                return RedirectToAction("Index", "Login");
            try
            {
                AddViewBag(vehicleId);
                return View();
            }
            catch (Exception e)
            {
                Utility.AlertMessage(this, "Server Error encounter. Fail to view create page.", "alert-danger");
                return RedirectToAction(nameof(List));
            }
        }


        [ValidateAntiForgeryToken, HttpPost]
        public IActionResult Create(YboRecord yboRecord)
        {
            if (!SessionUtil.IsActiveSession(HttpContext))
                return RedirectToAction("Index", "Login");

            string selectedOldDriverId = Request.Form["selectedDriverName"].FirstOrDefault() ?? "";
            string newDriverName = Request.Form["newDriverName"].FirstOrDefault() ?? "";
            yboRecord.DriverName = !string.IsNullOrEmpty(selectedOldDriverId) ? selectedOldDriverId : newDriverName;
            try
            {

                if (_serviceFactory.CreateYBORecordService().CreateYboRecord(yboRecord))
                {
                    Utility.AlertMessage(this, "Save Success", "alert-success");
                    return RedirectToAction(nameof(List));
                }
                else
                {
                    Utility.AlertMessage(this, "Save Fail.Internal Server Error", "alert-danger");
                    return RedirectToAction(nameof(List));
                }
            }
            catch (Exception e)
            {
                Utility.AlertMessage(this, "Save Fail.Internal Server Error", "alert-danger");
                return RedirectToAction(nameof(List));
            }
        }

        public IActionResult Edit(int Id)
        {
            if (!SessionUtil.IsActiveSession(HttpContext))
                return RedirectToAction("Index", "Login");

            try
            {
                YboRecord yboRecord = _serviceFactory.CreateYBORecordService().FindYboRecordByIdEgerLoad(Id);
                AddViewBag(yboRecord.Driver.VehicleData.VehicleDataPkid);
                return View(yboRecord);
            }
            catch (Exception e)
            {
                Utility.AlertMessage(this, "Server Error encounter. Fail to view edit page.", "alert-danger");
                return RedirectToAction(nameof(List));
            }
        }

        public IActionResult Details(int Id)
        {
            if (!SessionUtil.IsActiveSession(HttpContext))
                return RedirectToAction("Index", "Login");

            try
            {
                YboRecord yboRecord = _serviceFactory.CreateYBORecordService().FindYboRecordByIdEgerLoad(Id);
                return View(yboRecord);
            }
            catch (Exception e)
            {
                Utility.AlertMessage(this, "Server Error encounter. Fail to view detail page.", "alert-danger");
                return RedirectToAction(nameof(List));
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(YboRecord yboRecord)
        {
            if (!SessionUtil.IsActiveSession(HttpContext))
                return RedirectToAction("Index", "Login");

            string selectedOldDriverId = Request.Form["selectedDriverName"].FirstOrDefault() ?? "";
            string newDriverName = Request.Form["newDriverName"].FirstOrDefault() ?? "";
            yboRecord.DriverName = !string.IsNullOrEmpty(selectedOldDriverId) ? selectedOldDriverId : newDriverName;
            try
            {
                if (_serviceFactory.CreateYBORecordService().EditYboRecord(yboRecord))
                {

                    Utility.AlertMessage(this, "Edit Success", "alert-success");
                    return RedirectToAction(nameof(List));
                }
                else
                {
                    YboRecord oldYboRecord = _serviceFactory.CreateYBORecordService().FindYboRecordByIdEgerLoad(yboRecord.YboRecordPkid);
                    AddViewBag(oldYboRecord.Driver.VehicleData.VehicleDataPkid);
                    Utility.AlertMessage(this, "Edit Fail.Internal Server Error", "alert-danger");
                    return View(oldYboRecord);
                }
            }
            catch (Exception e)
            {
                YboRecord oldYboRecord = _serviceFactory.CreateYBORecordService().FindYboRecordByIdEgerLoad(yboRecord.YboRecordPkid);
                AddViewBag(oldYboRecord.Driver.VehicleData.VehicleDataPkid);
                Utility.AlertMessage(this, "Edit Fail.Internal Server Error", "alert-danger");
                return View(oldYboRecord);
            }
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Delete(int Id)
        {
            try
            {
                if (!SessionUtil.IsActiveSession(HttpContext))
                    return RedirectToAction("Index", "Login");

                YboRecord yboRecord = _serviceFactory.CreateYBORecordService().FindYboRecordById(Id);
                if (_serviceFactory.CreateYBORecordService().DeleteYboRecord(yboRecord))
                {
                    Utility.AlertMessage(this, "Delete Success", "alert-success");
                    return RedirectToAction(nameof(List));
                }
                else
                {
                    Utility.AlertMessage(this, "Delete Fail.Internal Server Error", "alert-danger");
                    return RedirectToAction(nameof(List));
                }
            }
            catch (Exception e)
            {
                Utility.AlertMessage(this, "Delete Fail.Internal Server Error", "alert-danger");
                return RedirectToAction(nameof(List));
            }
        }

        public JsonResult GetYBSTypeByYBSCompanyId(int ybsCompanyId)
        {
            List<SelectListItem> ybsTypes = _serviceFactory.CreateYBSTypeService().GetSelectListYBSTypesByYBSCompanyId(ybsCompanyId);
            return Json(ybsTypes);
        }

        public JsonResult GetDriverLicenseByDriverId(int driverPkId)
        {
            string license = _serviceFactory.CreateDriverService().FindDriverById(driverPkId).DriverLicense;
            return Json(license);
        }
    }
}
