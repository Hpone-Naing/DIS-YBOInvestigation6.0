using YBOInvestigation.Classes;
using YBOInvestigation.Data;
using YBOInvestigation.Paging;
using Microsoft.EntityFrameworkCore;
using YBOInvestigation.Models;

namespace YBOInvestigation.Services.Impl
{
    public class VehicleDataServiceImpl : AbstractServiceImpl<VehicleData>, VehicleDataService
    {
        private readonly ILogger<VehicleDataServiceImpl> _logger;

        public VehicleDataServiceImpl(YBOInvestigationDBContext context, ILogger<VehicleDataServiceImpl> logger) : base(context, logger)
        {
            _logger = logger;
        }

        public List<VehicleData> GetAllVehicles()
        {
            _logger.LogInformation(">>>>>>>>>> [VehicleDataServiceImpl][GetAllVehicles] Get VehicleData list. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Get VehicleData list. <<<<<<<<<<");
                return GetAll().Where(vehicle => vehicle.IsDeleted == false).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when retrieving VehicleData list. <<<<<<<<<<" + e);
                throw;
            }
        }

        public List<VehicleData> GetAllVehiclesEgerLoad()
        {
            _logger.LogInformation(">>>>>>>>>> [VehicleDataServiceImpl][GetAllVehiclesEgerLoad] Get VehicleData eger load list. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Get VehicleData eger load list. <<<<<<<<<<");
                return _context.VehicleDatas.Where(vehicle => vehicle.IsDeleted == false).Include(vehicle => vehicle.YBSCompany).Include(vehicle => vehicle.YBSType).Include(vehicle => vehicle.Manufacturer).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when retrieving VehicleData eger load list. <<<<<<<<<<" + e);
                throw;
            }
        }
        public PagingList<VehicleData> GetAllVehiclesWithPagin(string searchString, AdvanceSearch advanceSearch, int? pageNo, int PageSize, string searchOption = null)
        {
            _logger.LogInformation(">>>>>>>>>> [VehicleDataServiceImpl][GetAllVehiclesWithPagin] SearchAll or GetAll VehicleData paginate eger load list. <<<<<<<<<<");
            try
            {
                List<VehicleData> vehicleDatas = GetAllVehiclesEgerLoad();
            List<VehicleData> resultList = new List<VehicleData>();
            if (searchString != null && !String.IsNullOrEmpty(searchString))
            {
                    _logger.LogInformation($">>>>>>>>>> Get searchAll result VehicleData paginate eger load list. <<<<<<<<<<");
                    try
                    {
                        _logger.LogInformation($">>>>>>>>>> Success. Get searchAll result VehicleData paginate eger load list. <<<<<<<<<<");
                        resultList = vehicleDatas.Where(vehicle => IsSearchDataContained(vehicle, searchString, searchOption) || IsSearchDataContained(vehicle.YBSType, searchString, searchOption))
                            .AsQueryable()
                            .ToList();
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(">>>>>>>>>> Error occur. Get searchAll result VehicleData paginate eger load list. <<<<<<<<<<" + e);
                        throw;
                    }
                }
            else
            {
                    _logger.LogInformation($">>>>>>>>>> GetAll VehicleData paginate eger load list. <<<<<<<<<<");
                    try
                    {
                        _logger.LogInformation($">>>>>>>>>> Success. GetAll VehicleData paginate eger load list. <<<<<<<<<<");
                        resultList = vehicleDatas.AsQueryable().Include(vehicle => vehicle.YBSCompany).Include(vehicle => vehicle.YBSType).Include(vehicle => vehicle.Manufacturer).ToList();
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(">>>>>>>>>> Error occur. GetAll VehicleData paginate eger load list. <<<<<<<<<<" + e);
                        throw;
                    }
                }
                _logger.LogInformation($">>>>>>>>>> Success. SearchAll or GetAll SpecialEventInvestigationDept paginate eger load list. <<<<<<<<<<");
                return GetAllWithPagin(resultList, pageNo, PageSize);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur. SearchAll or GetAll VehicleData paginate eger load list. <<<<<<<<<<" + e);
                throw;
            }
        }

        /*public bool CreateVehicle(VehicleData vehicleData)
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
        }*/

        public VehicleData FindVehicleDataById(int id)
        {
            _logger.LogInformation(">>>>>>>>>> [VehicleDataServiceImpl][FindVehicleDataById] Find VehicleData by pkId. <<<<<<<<<<");
            try
            {
                _logger.LogInformation(">>>>>>>>>> Success. Find VehicleData by pkId. <<<<<<<<<<");
                return FindById(id);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when finding VehicleData by pkId. <<<<<<<<<<" + e);
                throw;
            }
        }

        public VehicleData FindVehicleDataByIdEgerLoad(int id)
        {
            _logger.LogInformation(">>>>>>>>>> [VehicleDataServiceImpl][FindVehicleDataByIdEgerLoad] Find VehicleData by pkId with eger load. <<<<<<<<<<");
            try
            {
                _logger.LogInformation(">>>>>>>>>> Success. Find VehicleData by pkId with eger load. <<<<<<<<<<");
                return _context.VehicleDatas.Where(VehicleData => !VehicleData.IsDeleted)
                           .Include(vehicle => vehicle.YBSCompany)
                           .Include(vehicle => vehicle.YBSType)
                           .Include(vehicle => vehicle.Manufacturer)
                           .Include(vehicle => vehicle.FuelType)
                           .FirstOrDefault(vehicle => vehicle.VehicleDataPkid == id);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when finding VehicleData by pkId with eger load. <<<<<<<<<<" + e);
                throw;
            }
        }

        public VehicleData FindVehicleDataByIdYBSTableEgerLoad(int id)
        {
            _logger.LogInformation(">>>>>>>>>> [VehicleDataServiceImpl][FindVehicleDataByIdYBSTableEgerLoad] Find VehicleData by pkId with YBSTable eger load. <<<<<<<<<<");
            try
            {
                _logger.LogInformation(">>>>>>>>>> Success. Find VehicleData by pkId with YBSTable eger load. <<<<<<<<<<");
                return _context.VehicleDatas.Where(VehicleData => !VehicleData.IsDeleted)
                           .Include(vehicle => vehicle.YBSCompany)
                           .Include(vehicle => vehicle.YBSType)
                           .FirstOrDefault(vehicle => vehicle.VehicleDataPkid == id);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when finding VehicleData by pkId with YBSTable eger load. <<<<<<<<<<" + e);
                throw;
            }
        }

        public VehicleData FindVehicleByVehicleNumber(string vehicleNumer)
        {
            _logger.LogInformation(">>>>>>>>>> [VehicleDataServiceImpl][FindVehicleByVehicleNumber] Find VehicleData by vehicleNumber. <<<<<<<<<<");
            try
            {
                _logger.LogInformation(">>>>>>>>>> Success. Find VehicleData by vehicleNumber. <<<<<<<<<<");
                return FindByString("VehicleNumber", vehicleNumer);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when finding VehicleData by vehicleNumber. <<<<<<<<<<" + e);
                throw;
            }
        }
    }
}
