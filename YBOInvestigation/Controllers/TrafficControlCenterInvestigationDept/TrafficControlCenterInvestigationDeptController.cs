using YBOInvestigation.Factories;
using YBOInvestigation.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.Excel;
using YBOInvestigation.Models;

namespace YBOInvestigation.Controllers.TrafficControlCenterInvestigationDeptController
{
    public class TrafficControlCenterInvestigationDeptController : Controller
    {
        private readonly ServiceFactory _serviceFactory;
        public TrafficControlCenterInvestigationDeptController(ServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }



        private string MakeExcelFileName(string searchString, bool ExportAll, int? pageNo)
        {
            if (ExportAll)
            {
                return "TrafficControlCenterInvestigationDeptမှတ်တမ်းအားလုံး" + DateTime.Now + ".xlsx";
            }
            else
            {
                if (searchString != null && !string.IsNullOrEmpty(searchString))
                    return "TrafficControlCenterInvestigationDeptမှတ်တမ်းရှာဖွေမှု(" + searchString + ")" + DateTime.Now + ".xlsx";
                else
                    return "TrafficControlCenterInvestigationDeptမှတ်တမ်း PageNumber( " + pageNo + " )" + DateTime.Now + ".xlsx";
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
                PagingList<TrafficControlCenterInvestigationDept> trafficControlCenterInvestigationDepts = _serviceFactory.CreateTrafficControlCenterInvestigationDeptService().GetAllTrafficControlCenterInvestigationDeptsWithPagin(searchString, pageNo, pageSize);
                if (Request.Query["export"] == "excel")
                {
                    bool ExportAll = Request.Query["ExportAll"] == "true";
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        var ws = wb.Worksheets.Add(_serviceFactory.CreateTrafficControlCenterInvestigationDeptService().MakeTrafficControlCenterInvestigationDeptExcelData(trafficControlCenterInvestigationDepts, ExportAll));
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
                return View(trafficControlCenterInvestigationDepts);
            }
            catch (NullReferenceException ne)
            {
                Utility.AlertMessage(this, "Data Issue. Please fill TrafficControlCenterInvestigationDept in database", "alert-danger");
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
            List<PunishmentType> punishmentTypes = _serviceFactory.CreatePunishmentTypeService().GetUniquePunishmentTypes();
            ViewBag.YBSCompany = _serviceFactory.CreateYBSCompanyService().FindYBSCompanyById(vehicleData.YBSCompany.YBSCompanyPkid);//.GetSelectListYBSCompanys();
            ViewBag.YBSType = _serviceFactory.CreateYBSTypeService().FindYBSTypeById(vehicleData.YBSType.YBSTypePkid);//.GetSelectListYBSTypesByYBSCompanyId(vehicleData.YBSCompany.YBSCompanyPkid);
            ViewBag.PunishmentTypes = _serviceFactory.CreatePunishmentTypeService().GetPunishmentTypeSelectList(punishmentTypes, "PunishmentTypePkid", "Punishment");
            ViewBag.VehicleNumber = vehicleData.VehicleNumber;
            ViewBag.AutoComplete = drivers
                .Select(driver => new { DriverPkId = driver.DriverPkid, DriverName = driver.DriverName, DriverLicense = driver.DriverLicense })
                .ToList();
        }

        private void AddViewBag(int vehicleId = 0, int driverId=0)
        {
            VehicleData vehicleData = _serviceFactory.CreateVehicleDataService().FindVehicleDataByIdContainSoftDeleteEgerLoad(vehicleId);
            List<Driver> drivers = _serviceFactory.CreateDriverService().GetDriversByVehicleDataId(vehicleData.VehicleDataPkid).Where(driver => driver.VehicleData.VehicleNumber == vehicleData.VehicleNumber).ToList();
            if (driverId > 0)
            {
                ViewBag.TotalRecord = _serviceFactory.CreateTrafficControlCenterInvestigationDeptService().GetTotalRecordByDriver(driverId);
                ViewBag.ChooseDriver = drivers.Where(driver => driver.DriverPkid == driverId).FirstOrDefault();
            }
            List<PunishmentType> punishmentTypes = _serviceFactory.CreatePunishmentTypeService().GetUniquePunishmentTypes();
            ViewBag.YBSCompany = _serviceFactory.CreateYBSCompanyService().FindYBSCompanyById(vehicleData.YBSCompany.YBSCompanyPkid);//.GetSelectListYBSCompanys();
            ViewBag.YBSType = _serviceFactory.CreateYBSTypeService().FindYBSTypeById(vehicleData.YBSType.YBSTypePkid);//.GetSelectListYBSTypesByYBSCompanyId(vehicleData.YBSCompany.YBSCompanyPkid);
            ViewBag.PunishmentTypes = _serviceFactory.CreatePunishmentTypeService().GetPunishmentTypeSelectList(punishmentTypes, "PunishmentTypePkid", "Punishment");
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
        public IActionResult Create(TrafficControlCenterInvestigationDept trafficControlCenterInvestigationDept)
        {
            if (!SessionUtil.IsActiveSession(HttpContext))
                return RedirectToAction("Index", "Login");

            Driver driver = _serviceFactory.CreateDriverService().IsExistingDriver(trafficControlCenterInvestigationDept.IDNumber, trafficControlCenterInvestigationDept.DriverLicense);
            if (driver != null)
            {
                String message = "Driver  " + driver.DriverName + " License " + driver.DriverLicense + "  ID Number " + driver.IDNumber + " exit in database.\n but your data entry is ID Number: " + trafficControlCenterInvestigationDept.IDNumber + " License Number: " + trafficControlCenterInvestigationDept.DriverLicense + ". Are you wrong data entry?";
                Utility.AlertMessage(this, message, "alert-danger");
                return RedirectToAction(nameof(List));
            }
            string selectedDriverName = Request.Form["selectedDriverName"].FirstOrDefault() ?? "";
            string newDriverName = Request.Form["newDriverName"].FirstOrDefault() ?? "";
            trafficControlCenterInvestigationDept.DriverName = (!string.IsNullOrEmpty(selectedDriverName) && selectedDriverName != "0") ? selectedDriverName : newDriverName;
            try
            {
                if (_serviceFactory.CreateTrafficControlCenterInvestigationDeptService().CreateTrafficControlCenterInvestigationDept(trafficControlCenterInvestigationDept))
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
            catch(Exception e)
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
                TrafficControlCenterInvestigationDept trafficControlCenterInvestigationDept = _serviceFactory.CreateTrafficControlCenterInvestigationDeptService().FindTrafficControlCenterInvestigationDeptByIdEgerLoad(Id);
                AddViewBag(trafficControlCenterInvestigationDept.Driver.VehicleData.VehicleDataPkid);
                ViewBag.TotalRecord = _serviceFactory.CreateTrafficControlCenterInvestigationDeptService().GetTotalRecordByDriver(trafficControlCenterInvestigationDept.Driver.DriverPkid);
                return View(trafficControlCenterInvestigationDept);
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
                TrafficControlCenterInvestigationDept trafficControlCenterInvestigationDept = _serviceFactory.CreateTrafficControlCenterInvestigationDeptService().FindTrafficControlCenterInvestigationDeptByIdEgerLoad(Id);
                trafficControlCenterInvestigationDept.TotalRecord = _serviceFactory.CreateTrafficControlCenterInvestigationDeptService().GetTotalRecordByDriver(trafficControlCenterInvestigationDept.Driver.DriverPkid);

                return View(trafficControlCenterInvestigationDept);
            }
            catch (Exception e)
            {
                Utility.AlertMessage(this, "Server Error encounter. Fail to view detail page.", "alert-danger");
                return RedirectToAction(nameof(List));
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(TrafficControlCenterInvestigationDept trafficControlCenterInvestigationDept)
        {
            if (!SessionUtil.IsActiveSession(HttpContext))
                return RedirectToAction("Index", "Login");
            Driver driver = _serviceFactory.CreateDriverService().IsExistingDriver(trafficControlCenterInvestigationDept.IDNumber, trafficControlCenterInvestigationDept.DriverLicense);
            if (driver != null)
            {
                String message = "Driver  " + driver.DriverName + " License " + driver.DriverLicense + "  ID Number " + driver.IDNumber + " exit in database.\n but your data entry is ID Number: " + trafficControlCenterInvestigationDept.IDNumber + " License Number: " + trafficControlCenterInvestigationDept.DriverLicense + ". Are you wrong data entry?";
                Utility.AlertMessage(this, message, "alert-danger");
                return RedirectToAction(nameof(List));
            }
            string selectedDriverName = Request.Form["selectedDriverName"].FirstOrDefault() ?? "";
            string newDriverName = Request.Form["newDriverName"].FirstOrDefault() ?? "";
            trafficControlCenterInvestigationDept.DriverName = !string.IsNullOrEmpty(selectedDriverName) ? selectedDriverName : newDriverName;
            try
            {
                if (_serviceFactory.CreateTrafficControlCenterInvestigationDeptService().EditTrafficControlCenterInvestigationDept(trafficControlCenterInvestigationDept))
                {

                    Utility.AlertMessage(this, "Edit Success", "alert-success");
                    return RedirectToAction(nameof(List));
                }
                else
                {
                    Utility.AlertMessage(this, "Edit Fail.Internal Server Error", "alert-danger");
                    TrafficControlCenterInvestigationDept oldTrafficControlCenterInvestigationDept = _serviceFactory.CreateTrafficControlCenterInvestigationDeptService().FindTrafficControlCenterInvestigationDeptByIdEgerLoad(trafficControlCenterInvestigationDept.TrafficControlCenterInvestigationDeptPkid);
                    AddViewBag(oldTrafficControlCenterInvestigationDept.Driver.VehicleData.VehicleDataPkid);
                    ViewBag.TotalRecord = _serviceFactory.CreateTrafficControlCenterInvestigationDeptService().GetTotalRecordByDriver(trafficControlCenterInvestigationDept.Driver.DriverPkid);
                    return View(trafficControlCenterInvestigationDept);
                }
            }
            catch(Exception e)
            {
                Utility.AlertMessage(this, "Edit Fail.Internal Server Error", "alert-danger");
                TrafficControlCenterInvestigationDept oldTrafficControlCenterInvestigationDept = _serviceFactory.CreateTrafficControlCenterInvestigationDeptService().FindTrafficControlCenterInvestigationDeptByIdEgerLoad(trafficControlCenterInvestigationDept.TrafficControlCenterInvestigationDeptPkid);
                AddViewBag(oldTrafficControlCenterInvestigationDept.Driver.VehicleData.VehicleDataPkid);
                ViewBag.TotalRecord = _serviceFactory.CreateTrafficControlCenterInvestigationDeptService().GetTotalRecordByDriver(trafficControlCenterInvestigationDept.Driver.DriverPkid);
                return View(oldTrafficControlCenterInvestigationDept);
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
                TrafficControlCenterInvestigationDept trafficControlCenterInvestigationDept = _serviceFactory.CreateTrafficControlCenterInvestigationDeptService().FindTrafficControlCenterInvestigationDeptById(Id);
                if (_serviceFactory.CreateTrafficControlCenterInvestigationDeptService().DeleteTrafficControlCenterInvestigationDept(trafficControlCenterInvestigationDept))
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
            int totalRecord = _serviceFactory.CreateTrafficControlCenterInvestigationDeptService().GetTotalRecordByDriver(driverPkId);
            var result = new
            {
                license = license,
                idNumber = idNumber,
                totalRecord = totalRecord
            };
            return Json(result);
        }
    }
}
