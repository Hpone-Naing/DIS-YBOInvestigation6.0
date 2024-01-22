using YBOInvestigation.Classes;
using YBOInvestigation.Factories;
using YBOInvestigation.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using YBOInvestigation.Models;
using YBOInvestigation.Paging;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace YBOInvestigation.Controllers.YBSDriverCourseDeliveryController
{
    public class YBSDriverCourseDeliveryController : Controller
    {
        private readonly ServiceFactory _serviceFactory;
        public YBSDriverCourseDeliveryController(ServiceFactory serviceFactory)
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
                return "YBSယာဥ်‌မောင်းများအားသင်တန်းပေးမှုမှတ်တမ်းအားလုံး" + DateTime.Now + ".xlsx";
            }
            else
            {
                if (searchString != null && !string.IsNullOrEmpty(searchString))
                    return "YBSယာဥ်‌မောင်းများအားသင်တန်းပေးမှုမှတ်တမ်းရှာဖွေမှု(" + searchString + ")" + DateTime.Now + ".xlsx";
                else
                    return "YBSယာဥ်‌မောင်းများအားသင်တန်းပေးမှုမှတ်တမ်း PageNumber( " + pageNo + " )" + DateTime.Now + ".xlsx";
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
                PagingList<YBSDriverCourseDelivery> ybsDriverCourseDeliverys = _serviceFactory.CreateYBSDriverCourseDeliveryService().GetAllYBSDriverCourseDeliveriesWithPagin(searchString, pageNo, pageSize);
                if (Request.Query["export"] == "excel")
                {
                    bool ExportAll = Request.Query["ExportAll"] == "true";
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        var ws = wb.Worksheets.Add(_serviceFactory.CreateYBSDriverCourseDeliveryService().MakeYBSDriverCourseDeliveriesExcelData(ybsDriverCourseDeliverys, ExportAll));
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
                return View(ybsDriverCourseDeliverys);
            }
            catch (NullReferenceException ne)
            {
                Utility.AlertMessage(this, "Data Issue. Please fill YBSDriverCourseDeliveries in database", "alert-danger");
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
            VehicleData vehicleData = _serviceFactory.CreateVehicleDataService().FindVehicleDataByIdYBSTableEgerLoad(vehicleId);
            List<Driver> drivers = _serviceFactory.CreateDriverService().GetDriversByVehicleDataId(vehicleData.VehicleDataPkid).Where(driver => driver.VehicleData.VehicleNumber == vehicleData.VehicleNumber).ToList();
            List<PunishmentType> punishmentTypes = _serviceFactory.CreatePunishmentTypeService().GetUniquePunishmentTypes();
            ViewBag.YBSCompany = _serviceFactory.CreateYBSCompanyService().FindYBSCompanyById(vehicleData.YBSCompany.YBSCompanyPkid);//.GetSelectListYBSCompanys();
            ViewBag.YBSType = _serviceFactory.CreateYBSTypeService().FindYBSTypeById(vehicleData.YBSType.YBSTypePkid);
            ViewBag.PunishmentTypes = _serviceFactory.CreatePunishmentTypeService().GetPunishmentTypeSelectList(punishmentTypes, "PunishmentTypePkid", "Punishment");
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
        public IActionResult Create(YBSDriverCourseDelivery ybsDriverCourseDelivery)
        {
            if (!SessionUtil.IsActiveSession(HttpContext))
                return RedirectToAction("Index", "Login");

            string selectedOldDriverId = Request.Form["selectedDriverName"].FirstOrDefault() ?? "";
            string newDriverName = Request.Form["newDriverName"].FirstOrDefault() ?? "";
            ybsDriverCourseDelivery.DriverName = !string.IsNullOrEmpty(selectedOldDriverId) ? selectedOldDriverId : newDriverName;
            try
            {
                if (_serviceFactory.CreateYBSDriverCourseDeliveryService().CreateYBSDriverCourseDeliveries(ybsDriverCourseDelivery))
                {
                    Utility.AlertMessage(this, "Save Success", "alert-success");
                    return RedirectToAction(nameof(List));

                }
                else
                {
                    Utility.AlertMessage(this, "Save Fail. Server Error encounter.", "alert-danger");
                    return RedirectToAction(nameof(List));
                }
            }
            catch (Exception e)
            {
                Utility.AlertMessage(this, "Save Fail. Server Error encounter.", "alert-danger");
                return RedirectToAction(nameof(List));
            }
        }

        public IActionResult Edit(int Id)
        {

            if (!SessionUtil.IsActiveSession(HttpContext))
                return RedirectToAction("Index", "Login");
            try
            {
                YBSDriverCourseDelivery ybsDriverCourseDelivery = _serviceFactory.CreateYBSDriverCourseDeliveryService().FindYBSDriverCourseDeliveriesByIdEgerLoad(Id);
                AddViewBag(ybsDriverCourseDelivery.TrainedYBSDriverInfo.Driver.VehicleData.VehicleDataPkid);
                return View(ybsDriverCourseDelivery);
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

            if (_serviceFactory.CreateYBSDriverCourseDeliveryService().FindYBSDriverCourseDeliveriesById(Id) == null)
            {
                Utility.AlertMessage(this, "YBSDriverCourseDelivery record doesn't exit!", "alert-primary");
                return RedirectToAction(nameof(List));
            }

            try
            {
                YBSDriverCourseDelivery ybsDriverCourseDelivery = _serviceFactory.CreateYBSDriverCourseDeliveryService().FindYBSDriverCourseDeliveriesByIdEgerLoad(Id);
                return View(ybsDriverCourseDelivery);
            }
            catch (Exception e)
            {
                Utility.AlertMessage(this, "Server Error encounter. Fail to view detail page.", "alert-danger");
                return RedirectToAction(nameof(List));
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(YBSDriverCourseDelivery ybsDriverCourseDelivery)
        {
            if (!SessionUtil.IsActiveSession(HttpContext))
                return RedirectToAction("Index", "Login");

            if (_serviceFactory.CreateYBSDriverCourseDeliveryService().FindYBSDriverCourseDeliveriesById(ybsDriverCourseDelivery.YBSDriverCourseDeliveryPkid) == null)
            {
                Utility.AlertMessage(this, "YBSDriverCourseDelivery record doesn't exit!", "alert-primary");
                return RedirectToAction(nameof(List));
            }

            string selectedOldDriverId = Request.Form["selectedDriverName"].FirstOrDefault() ?? "";
            string newDriverName = Request.Form["newDriverName"].FirstOrDefault() ?? "";
            ybsDriverCourseDelivery.DriverName = !string.IsNullOrEmpty(selectedOldDriverId) ? selectedOldDriverId : newDriverName;
            try
            {
                if (_serviceFactory.CreateYBSDriverCourseDeliveryService().EditYBSDriverCourseDeliveries(ybsDriverCourseDelivery))
                {

                    Utility.AlertMessage(this, "Edit Success", "alert-success");
                    return RedirectToAction(nameof(List));
                }
                else
                {
                    YBSDriverCourseDelivery oldYbsDriverCourseDelivery = _serviceFactory.CreateYBSDriverCourseDeliveryService().FindYBSDriverCourseDeliveriesByIdEgerLoad(ybsDriverCourseDelivery.YBSDriverCourseDeliveryPkid);
                    AddViewBag(oldYbsDriverCourseDelivery.TrainedYBSDriverInfo.Driver.VehicleData.VehicleDataPkid);
                    Utility.AlertMessage(this, "Edit Fail.Internal Server Error", "alert-danger");
                    return View(oldYbsDriverCourseDelivery);
                }
            }
            catch (Exception e)
            {
                YBSDriverCourseDelivery oldYbsDriverCourseDelivery = _serviceFactory.CreateYBSDriverCourseDeliveryService().FindYBSDriverCourseDeliveriesByIdEgerLoad(ybsDriverCourseDelivery.YBSDriverCourseDeliveryPkid);
                AddViewBag(oldYbsDriverCourseDelivery.TrainedYBSDriverInfo.Driver.VehicleData.VehicleDataPkid);
                Utility.AlertMessage(this, "Edit Fail.Internal Server Error", "alert-danger");
                return View(oldYbsDriverCourseDelivery);
            }
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Delete(int Id)
        {
            if (!SessionUtil.IsActiveSession(HttpContext))
                return RedirectToAction("Index", "Login");
            try
            {
                YBSDriverCourseDelivery ybsDriverCourseDelivery = _serviceFactory.CreateYBSDriverCourseDeliveryService().FindYBSDriverCourseDeliveriesById(Id);
                if (_serviceFactory.CreateYBSDriverCourseDeliveryService().DeleteYBSDriverCourseDeliveries(ybsDriverCourseDelivery))
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

        public JsonResult GetDriverInfoByDriverId(int driverPkId)
        {
            TrainedYBSDriverInfo trainedDriverInfo = _serviceFactory.CreateTrainedYBSDriverInfoService().GetTrainedYBSDriverInfoByDriverIdEgerLoad(driverPkId);
            string license = _serviceFactory.CreateDriverService().FindDriverById(driverPkId).DriverLicense;
            var result = new
            {
                license = license,
                trainedDriverInfo = trainedDriverInfo
            };
            return Json(result);
        }
    }
}
