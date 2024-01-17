using YBOInvestigation.Classes;
using YBOInvestigation.Factories;
using YBOInvestigation.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using YBOInvestigation.Models;

namespace YBOInvestigation.Controllers.VechicleData
{
    public class VehicleDataController : Controller
    {
        private readonly ServiceFactory _serviceFactory;
        public VehicleDataController(ServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        private void AddSearchDatasToViewBag(string searchString, string searchOption = null)
        {
            ViewBag.SearchString = searchString;
            ViewBag.SearchOption = searchOption;
        }

        public IActionResult SearchVehicle(int? pageNo)
        {
            if (!SessionUtil.IsActiveSession(HttpContext))
                return RedirectToAction("Index", "Login");

            string searchString = Request.Query["SearchString"];
            string searchOption = Request.Query["SearchOption"];
            AdvanceSearch advanceSearch = Utility.MakeAdvanceSearch(HttpContext);

            int pageSize = Utility.DEFAULT_PAGINATION_NUMBER;
            AddSearchDatasToViewBag(searchString, searchOption);
            List<VehicleData> vehicleDatas = _serviceFactory.CreateVehicleDataService().GetAllVehiclesWithPagin(searchString, advanceSearch, pageNo, pageSize, searchOption);
            ViewBag.AutoComplete = _serviceFactory.CreateVehicleDataService().GetAllVehicles()
                .Select(vehicle => new { VehicleNumber = vehicle.VehicleNumber, YBSTypeName = vehicle.YBSType.YBSTypeName })
                .ToList();
            if (string.IsNullOrEmpty(searchString))
                return View();
            return View(vehicleDatas);
        }

        
        private List<SelectListItem> GetItemsFromList<T>(List<T> list, string valuePropertyName, string textPropertyName)
        {
            var lstItems = new List<SelectListItem>();

            foreach (T item in list)
            {
                var itemType = item.GetType();
                var valueProperty = itemType.GetProperty(valuePropertyName);
                var textProperty = itemType.GetProperty(textPropertyName);

                if (valueProperty != null && textProperty != null)
                {
                    var value = valueProperty.GetValue(item)?.ToString();
                    var text = textProperty.GetValue(item)?.ToString();

                    lstItems.Add(new SelectListItem
                    {
                        Value = value,
                        Text = text
                    });
                }
            }

            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "ရွေးချယ်ရန်"
            };

            lstItems.Insert(0, defItem);
            return lstItems;
        }
        private void AddViewBag()
        {
            List<FuelType> uniqueFuelTypes = _serviceFactory.CreateFuelTypeService().GetUniqueFuelTypes();
            List<Manufacturer> uniqueManufacturers = _serviceFactory.CreateManufacturerService().GetUniqueManufacturers();
            //List<YBSCompany> uniqueYBSCompanies = _serviceFactory.CreateYBSCompanyService().GetUniqueYBSCompanys();
            //List<YBSType> uniqueYBSTypes = _serviceFactory.CreateYBSTypeService().GetUniqueYBSTypes();

            ViewBag.FuelTypes = GetItemsFromList(uniqueFuelTypes, "FuelTypePkid", "FuelTypeName");
            ViewBag.Manufacturers = GetItemsFromList(uniqueManufacturers, "ManufacturerPkid", "ManufacturerName");
            ViewBag.YBSCompanies = _serviceFactory.CreateYBSCompanyService().GetSelectListYBSCompanys();//GetItemsFromList(uniqueYBSCompanies, "YBSCompanyPkid", "YBSCompanyName");
            ViewBag.YBSTypes = _serviceFactory.CreateYBSTypeService().GetSelectListYBSTypesByYBSCompanyId();//GetItemsFromList(uniqueYBSTypes, "YBSTypePkid", "YBSTypeName");
        }
        
        public IActionResult Details(int Id)
        {
            if (!SessionUtil.IsActiveSession(HttpContext))
                return RedirectToAction("Index", "Login");

            VehicleData vehicleData = _serviceFactory.CreateVehicleDataService().FindVehicleDataByIdEgerLoad(Id);
            return View(vehicleData);
        }

        public JsonResult GetYBSTypeByYBSCompanyId(int ybsCompanyId)
        {
            List<SelectListItem> ybsTypes = _serviceFactory.CreateYBSTypeService().GetSelectListYBSTypesByYBSCompanyId(ybsCompanyId);
            return Json(ybsTypes);
        }

    }
}
