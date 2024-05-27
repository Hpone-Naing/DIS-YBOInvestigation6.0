using YBOInvestigation.Classes;
using YBOInvestigation.Factories;
using YBOInvestigation.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using ClosedXML.Excel;
using YBOInvestigation.Models;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace YBOInvestigation.Controllers.SpecialEventInvestigationDeptController
{
    public class SpecialEventInvestigationDeptController : Controller
    {
        private readonly ServiceFactory _serviceFactory;
        public SpecialEventInvestigationDeptController(ServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        private string MakeExcelFileName(string searchString, bool ExportAll, int? pageNo)
        {
            if (ExportAll)
            {
                return "SpecialEventInvestigationDeptမှတ်တမ်းအားလုံး" + DateTime.Now + ".xlsx";
            }
            else
            {
                if (searchString != null && !string.IsNullOrEmpty(searchString))
                    return "SpecialEventInvestigationDeptမှတ်တမ်းရှာဖွေမှု(" + searchString + ")" + DateTime.Now + ".xlsx";
                else
                    return "SpecialEventInvestigationDeptမှတ်တမ်း PageNumber( " + pageNo + " )" + DateTime.Now + ".xlsx";
            }

        }

        private void AddSearchDatasToViewBag(string searchString)
        {
            ViewBag.SearchString = searchString;
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
                PagingList<SpecialEventInvestigationDept> specialEventInvestigationDepts = _serviceFactory.CreateSpecialEventInvestigationDeptService().GetAllSpecialEventInvestigationDeptsWithPagin(searchString, pageNo, pageSize);
                if (Request.Query["export"] == "excel")
                {
                    bool ExportAll = Request.Query["ExportAll"] == "true";
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        var ws = wb.Worksheets.Add(_serviceFactory.CreateSpecialEventInvestigationDeptService().MakeSpecialEventInvestigationDeptExcelData(specialEventInvestigationDepts, ExportAll));
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
                return View(specialEventInvestigationDepts);
            }
            catch (NullReferenceException ne)
            {
                Utility.AlertMessage(this, "Data Issue. Please fill SpecialEventInvestigationDept in database", "alert-danger");
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

        private void AddViewBag(int vehicleId = 0, int driverId = 0)
        {
            VehicleData vehicleData = _serviceFactory.CreateVehicleDataService().FindVehicleDataByIdContainSoftDeleteEgerLoad(vehicleId);
            List<Driver> drivers = _serviceFactory.CreateDriverService().GetDriversByVehicleDataId(vehicleData.VehicleDataPkid).Where(driver => driver.VehicleData.VehicleNumber == vehicleData.VehicleNumber).ToList();
            if (driverId > 0)
            {
                //ViewBag.TotalRecord = _serviceFactory.CreateSpecialEventInvestigationDeptService().GetTotalRecordByDriver(driverId);
                ViewBag.ChooseDriver = drivers.Where(driver => driver.DriverPkid == driverId).FirstOrDefault();
            }
            ViewBag.YBSCompany = _serviceFactory.CreateYBSCompanyService().FindYBSCompanyById(vehicleData.YBSCompany.YBSCompanyPkid);//.GetSelectListYBSCompanys();
            ViewBag.YBSType = _serviceFactory.CreateYBSTypeService().FindYBSTypeById(vehicleData.YBSType.YBSTypePkid);//.GetSelectListYBSTypesByYBSCompanyId(vehicleData.YBSCompany.YBSCompanyPkid);
            ViewBag.VehicleNumber = vehicleData.VehicleNumber;
            ViewBag.AutoComplete = drivers
                .Select(driver => new { DriverPkId = driver.DriverPkid, DriverName = driver.DriverName, DriverLicense = driver.DriverLicense })
                .ToList();
        }

        public IActionResult Create(int vehicleId, int driverId)
        {
            if (!SessionUtil.IsActiveSession(HttpContext))
                return RedirectToAction("Index", "Login");
            try
            {
                AddViewBag(vehicleId, driverId);
                return View();
            }
            catch (Exception e)
            {
                Utility.AlertMessage(this, "Server Error encounter. Fail to view create page.", "alert-danger");
                return RedirectToAction(nameof(List));
            }
        }


        [ValidateAntiForgeryToken, HttpPost]
        public IActionResult Create(SpecialEventInvestigationDept specialEventInvestigationDept)
        {
            if (!SessionUtil.IsActiveSession(HttpContext))
                return RedirectToAction("Index", "Login");

            Driver driver = _serviceFactory.CreateDriverService().IsExistingDriver(specialEventInvestigationDept.IDNumber, specialEventInvestigationDept.DriverLicense);
            if (driver != null)
            {
                String message = "Driver  " + driver.DriverName + " License " + driver.DriverLicense + "  ID Number " + driver.IDNumber + " exit in database.\n but your data entry is ID Number: " + specialEventInvestigationDept.IDNumber + " License Number: " + specialEventInvestigationDept.DriverLicense + ". Are you wrong data entry?";
                Utility.AlertMessage(this, message, "alert-danger");
                return RedirectToAction(nameof(List));
            }

            string selectedDriverName = Request.Form["selectedDriverName"].FirstOrDefault() ?? "";
            string newDriverName = Request.Form["newDriverName"].FirstOrDefault() ?? "";
            specialEventInvestigationDept.DriverName = (!string.IsNullOrEmpty(selectedDriverName) && selectedDriverName != "0") ? selectedDriverName : newDriverName;
            try
            {
                if (_serviceFactory.CreateSpecialEventInvestigationDeptService().CreateSpecialEventInvestigationDept(specialEventInvestigationDept))
                {
                    Utility.AlertMessage(this, "Save Success", "alert-success");
                    return RedirectToAction(nameof(List));
                }
                else
                {
                    Utility.AlertMessage(this, "Save Fail.Internal Server Error", "alert-danger");
                    return View();
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
                SpecialEventInvestigationDept specialEventInvestigationDept = _serviceFactory.CreateSpecialEventInvestigationDeptService().FindSpecialEventInvestigationDeptByIdEgerLoad(Id);
                //VehicleData vehicleData = _serviceFactory.CreateVehicleDataService().FindVehicleByVehicleNumber(specialEventInvestigationDept.VehicleNumber);
                //AddViewBag(vehicleData.VehicleDataPkid);
                AddViewBag(specialEventInvestigationDept.Driver.VehicleData.VehicleDataPkid);
                return View(specialEventInvestigationDept);
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
                SpecialEventInvestigationDept specialEventInvestigationDept = _serviceFactory.CreateSpecialEventInvestigationDeptService().FindSpecialEventInvestigationDeptByIdEgerLoad(Id);
                return View(specialEventInvestigationDept);
            }
            catch (Exception e)
            {
                Utility.AlertMessage(this, "Server Error encounter. Fail to view detail page.", "alert-danger");
                return RedirectToAction(nameof(List));
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(SpecialEventInvestigationDept specialEventInvestigationDept)
        {
            if (!SessionUtil.IsActiveSession(HttpContext))
                return RedirectToAction("Index", "Login");
            Driver driver = _serviceFactory.CreateDriverService().IsExistingDriver(specialEventInvestigationDept.IDNumber, specialEventInvestigationDept.DriverLicense);
            if (driver != null)
            {
                String message = "Driver  " + driver.DriverName + " License " + driver.DriverLicense + "  ID Number " + driver.IDNumber + " exit in database.\n but your data entry is ID Number: " + specialEventInvestigationDept.IDNumber + " License Number: " + specialEventInvestigationDept.DriverLicense + ". Are you wrong data entry?";
                Utility.AlertMessage(this, message, "alert-danger");
                return RedirectToAction(nameof(List));
            }
            string selectedDriverName = Request.Form["selectedDriverName"].FirstOrDefault() ?? "";
            string newDriverName = Request.Form["newDriverName"].FirstOrDefault() ?? "";
            specialEventInvestigationDept.DriverName = !string.IsNullOrEmpty(selectedDriverName) ? selectedDriverName : newDriverName;
            try
            {
                if (_serviceFactory.CreateSpecialEventInvestigationDeptService().EditSpecialEventInvestigationDept(specialEventInvestigationDept))
                {
                    Utility.AlertMessage(this, "Edit Success", "alert-success");
                    return RedirectToAction(nameof(List));
                }
                else
                {
                    Utility.AlertMessage(this, "Edit Fail.Internal Server Error", "alert-danger");
                    SpecialEventInvestigationDept record = _serviceFactory.CreateSpecialEventInvestigationDeptService().FindSpecialEventInvestigationDeptByIdEgerLoad(specialEventInvestigationDept.SpecialEventInvestigationDeptDeptPkid);
                    AddViewBag(record.Driver.VehicleData.VehicleDataPkid);
                    return View(record);
                }
            }
            catch (Exception e)
            {
                Utility.AlertMessage(this, "Edit Fail.Internal Server Error", "alert-danger");
                SpecialEventInvestigationDept oldSpecialEventInvestigationDept = _serviceFactory.CreateSpecialEventInvestigationDeptService().FindSpecialEventInvestigationDeptByIdEgerLoad(specialEventInvestigationDept.SpecialEventInvestigationDeptDeptPkid);
                AddViewBag(oldSpecialEventInvestigationDept.Driver.VehicleData.VehicleDataPkid);
                return View(oldSpecialEventInvestigationDept);
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
                SpecialEventInvestigationDept specialEventInvestigationDept = _serviceFactory.CreateSpecialEventInvestigationDeptService().FindSpecialEventInvestigationDeptById(Id);
                if (_serviceFactory.CreateSpecialEventInvestigationDeptService().DeleteSpecialEventInvestigationDept(specialEventInvestigationDept))
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

        public JsonResult GetDriverLicenseByDriverName(string driverName)
        {
            string license = _serviceFactory.CreateDriverService().FindDriverLicenseByDriverName(driverName);
            return Json(license);
        }

        public JsonResult GetDriverLicenseByDriverId(int driverPkId)
        {
            Driver driver = _serviceFactory.CreateDriverService().FindDriverById(driverPkId);
            string license = driver.DriverLicense;
            string idNumber = driver.IDNumber;
            //int totalRecord = _serviceFactory.CreateCallCenterInvestigationDeptService().GetTotalRecordByDriver(driverPkId);
            var result = new
            {
                license = license,
                idNumber = idNumber,
                totalRecord = 0
            };
            return Json(result);
        }
    }
}
