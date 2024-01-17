using YBOInvestigation.Classes;
using YBOInvestigation.Data;
using YBOInvestigation.Paging;
using Microsoft.EntityFrameworkCore;
using YBOInvestigation.Models;

namespace YBOInvestigation.Services.Impl
{
    public class VehicleDataServiceImpl : AbstractServiceImpl<VehicleData>, VehicleDataService
    {
        public VehicleDataServiceImpl(YBOInvestigationDBContext context) : base(context)
        {
        }

        public List<VehicleData> GetAllVehicles()
        {
            return GetAll().Where(vehicle => vehicle.IsDeleted == false).ToList();
        }

        public List<VehicleData> GetAllVehiclesEgerLoad()
        {
            return _context.VehicleDatas.Where(vehicle => vehicle.IsDeleted == false).Include(vehicle => vehicle.YBSCompany).Include(vehicle => vehicle.YBSType).Include(vehicle => vehicle.Manufacturer).ToList();
        }
        public PagingList<VehicleData> GetAllVehiclesWithPagin(string searchString, AdvanceSearch advanceSearch, int? pageNo, int PageSize, string searchOption = null)
        {
            List<VehicleData> vehicleDatas = GetAllVehiclesEgerLoad();
            List<VehicleData> resultList = new List<VehicleData>();
            if (searchString != null && !String.IsNullOrEmpty(searchString))
            {
                resultList = vehicleDatas.Where(vehicle => IsSearchDataContained(vehicle, searchString, searchOption) || IsSearchDataContained(vehicle.YBSType, searchString, searchOption))
                            .AsQueryable()
                            .ToList();
            }
            else if (advanceSearch.CngQty != null || advanceSearch.CctvInstalled != null || advanceSearch.TotalBusStop != null)
            {
                resultList = AdvanceSearch(advanceSearch, _context.VehicleDatas).Where(vehicleData => !vehicleData.IsDeleted).ToList();
            }
            else
            {
                resultList = vehicleDatas.AsQueryable().Include(vehicle => vehicle.YBSCompany).Include(vehicle => vehicle.YBSType).Include(vehicle => vehicle.Manufacturer).ToList();
                foreach(VehicleData v in resultList)
                {
                    Console.WriteLine("c name" + v.YBSType.YBSTypeName);
                    Console.WriteLine("c name" + v.Manufacturer.ManufacturerName);
                }
            }
            return GetAllWithPagin(resultList, pageNo, PageSize);
        }

        public bool CreateVehicle(VehicleData vehicleData)
        {
            vehicleData.IsDeleted = false;
            vehicleData.CreatedDate = DateTime.Now;
            vehicleData.RegistrationDate = DateTime.Now;
            return Create(vehicleData);
        }

        public bool EditVehicle(VehicleData vehicleData)
        {
            return Update(vehicleData);
        }

        public bool DeleteVehicle(VehicleData vehicleData)
        {
            vehicleData.IsDeleted = true;
            return Update(vehicleData);
        }

        public VehicleData FindVehicleDataById(int id)
        {
            return FindById(id);
        }

        public VehicleData FindVehicleDataByIdEgerLoad(int id)
        {
            return _context.VehicleDatas.Where(VehicleData => !VehicleData.IsDeleted)
                           .Include(vehicle => vehicle.YBSCompany)
                           .Include(vehicle => vehicle.YBSType)
                           .Include(vehicle => vehicle.Manufacturer)
                           .Include(vehicle => vehicle.FuelType)
                           .FirstOrDefault(vehicle => vehicle.VehicleDataPkid == id);
        }

        public VehicleData FindVehicleDataByIdYBSTableEgerLoad(int id)
        {
            return _context.VehicleDatas.Where(VehicleData => !VehicleData.IsDeleted)
                           .Include(vehicle => vehicle.YBSCompany)
                           .Include(vehicle => vehicle.YBSType)
                           .FirstOrDefault(vehicle => vehicle.VehicleDataPkid == id);
        }

        public VehicleData FindVehicleByVehicleNumber(string vehicleNumer)
        {
            return FindByString("VehicleNumber", vehicleNumer);
        }
    }
}
