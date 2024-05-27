using YBOInvestigation.Classes;
using YBOInvestigation.Data;
using YBOInvestigation.Paging;
using Microsoft.EntityFrameworkCore;
using YBOInvestigation.Models;
using DocumentFormat.OpenXml.Wordprocessing;

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
                        
                        if(searchOption == "idNumber")
                        {
                            Driver driver = _context.Drivers.Where(driver => driver.IDNumber.Contains(searchString)).Include(driver => driver.VehicleData).FirstOrDefault();
                            if(driver != null)
                            {
                                resultList.Add(driver.VehicleData);

                            }
                        }
                        else if(searchOption == "driverName")
                        {
                            Driver driver = _context.Drivers.Where(driver => driver.DriverName.Contains(searchString)).Include(driver => driver.VehicleData).FirstOrDefault();
                            if(driver != null)
                            {
                                resultList.Add(driver.VehicleData);

                            }
                        }
                        else if (searchOption == "licenseNumber")
                        {
                            /*Driver driver = _context.Drivers.Where(driver => driver.DriverLicense.Contains(searchString)).Include(driver => driver.VehicleData).FirstOrDefault();
                            if (driver != null)
                            {
                                resultList.Add(driver.VehicleData);

                            }*/
                            resultList = _context.Drivers.Where(driver => driver.DriverLicense.Contains(searchString)).Include(driver => driver.VehicleData).Select(driver => driver.VehicleData).ToList();
                        }
                        else
                        {
                            resultList = vehicleDatas.Where(vehicle => IsSearchDataContained(vehicle, searchString, searchOption) || IsSearchDataContained(vehicle.YBSType, searchString, searchOption))
                            .AsQueryable()
                            .ToList();
                        }

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

        public PagingList<DriverPunishmentInfo> GetAllDriverPunishmentInfoWithPagin(string searchString, AdvanceSearch advanceSearch, int? pageNo, int PageSize, string searchOption = null)
        {
            _logger.LogInformation(">>>>>>>>>> [VehicleDataServiceImpl][GetAllVehiclesWithPagin] SearchAll or GetAll VehicleData paginate eger load list. <<<<<<<<<<");
            try
            {
                List<VehicleData> vehicleDatas = GetAllVehiclesEgerLoad();
                if (searchString != null && !String.IsNullOrEmpty(searchString))
                {
                    _logger.LogInformation($">>>>>>>>>> Get searchAll result VehicleData paginate eger load list. <<<<<<<<<<");
                    try
                    {
                        _logger.LogInformation($">>>>>>>>>> Success. Get searchAll result VehicleData paginate eger load list. <<<<<<<<<<");

                        if (searchOption == "idNumber")
                        {
                            List<Driver> drivers = _context.Drivers.Where(driver => driver.IDNumber == searchString).Include(driver => driver.VehicleData).ToList();
                            if (drivers.Count > 0)
                            {
                                List<DriverPunishmentInfo> driverPunishmentInfos = new List<DriverPunishmentInfo>();
                                foreach(Driver driver in drivers)
                                {
                                    DriverPunishmentInfo driverPunishmentInfo = new DriverPunishmentInfo()
                                    {
                                        VehicleData = driver.VehicleData,
                                        Driver = driver
                                    };
                                    driverPunishmentInfos.Add(driverPunishmentInfo);
                                }
                                return PagingList<DriverPunishmentInfo>.CreateAsync(driverPunishmentInfos.AsQueryable(), pageNo ?? 1, PageSize);
                            }
                        }
                        else if (searchOption == "driverName")
                        {
                            List<Driver> drivers = _context.Drivers.Where(driver => driver.DriverName.Contains(searchString)).Include(driver => driver.VehicleData).ToList();
                            Console.WriteLine("drive count: /////////////" + drivers.Count);
                            if (drivers.Count > 0)
                            {
                                List<DriverPunishmentInfo> driverPunishmentInfos = new List<DriverPunishmentInfo>();
                                foreach (Driver driver in drivers)
                                {
                                    Console.WriteLine("DName / license" + driver.DriverName + " / " + driver.DriverLicense);
                                    DriverPunishmentInfo driverPunishmentInfo = new DriverPunishmentInfo()
                                    {
                                        VehicleData = driver.VehicleData,
                                        Driver = driver
                                    };
                                    driverPunishmentInfos.Add(driverPunishmentInfo);
                                }
                                return PagingList<DriverPunishmentInfo>.CreateAsync(driverPunishmentInfos.AsQueryable(), pageNo ?? 1, PageSize);

                            }
                        }
                        else if (searchOption == "licenseNumber")
                        {
                            List<Driver> drivers = _context.Drivers.Where(driver => driver.DriverLicense == searchString).ToList();
                            if (drivers.Count > 0)
                            {
                                List<DriverPunishmentInfo> driverPunishmentInfos = new List<DriverPunishmentInfo>();
                                foreach (Driver driver in drivers)
                                {
                                    DriverPunishmentInfo driverPunishmentInfo = new DriverPunishmentInfo()
                                    {
                                        VehicleData = driver.VehicleData,
                                        Driver = driver
                                    };
                                    driverPunishmentInfos.Add(driverPunishmentInfo);
                                }
                                return PagingList<DriverPunishmentInfo>.CreateAsync(driverPunishmentInfos.AsQueryable(), pageNo ?? 1, PageSize);

                            }
                        }
                        else
                        {
                            List<VehicleData> vehicles = vehicleDatas.Where(vehicle => IsSearchDataContained(vehicle, searchString, searchOption) || IsSearchDataContained(vehicle.YBSType, searchString, searchOption)).ToList();
                            if (vehicles.Count > 0)
                            {
                                List<DriverPunishmentInfo> driverPunishmentInfos = new List<DriverPunishmentInfo>();
                                foreach (VehicleData vehicleData in vehicles)
                                {
                                    DriverPunishmentInfo driverPunishmentInfo = new DriverPunishmentInfo()
                                    {
                                        VehicleData = vehicleData,
                                    };
                                    driverPunishmentInfos.Add(driverPunishmentInfo);
                                }
                                return PagingList<DriverPunishmentInfo>.CreateAsync(driverPunishmentInfos.AsQueryable(), pageNo ?? 1, PageSize);

                            }
                        }

                    }
                    catch (Exception e)
                    {
                        _logger.LogError(">>>>>>>>>> Error occur. Get searchAll result VehicleData paginate eger load list. <<<<<<<<<<" + e);
                        throw;
                    }
                }
                else
                {
                    return null; 
                }
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur. SearchAll or GetAll VehicleData paginate eger load list. <<<<<<<<<<" + e);
                throw;
            }
        }

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
                return _context.VehicleDatas.Where(VehicleData => VehicleData.IsDeleted == false)
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

        public DriverPunishmentInfo FindDriverPunshmentInfoByIdEgerLoad(int vehicleId, int driverId)
        {
            
            _logger.LogInformation(">>>>>>>>>> [VehicleDataServiceImpl][FindVehicleDataByIdEgerLoad] Find VehicleData by pkId with eger load. <<<<<<<<<<");
            try
            {
                if (driverId != 0)
                {
                    Driver driver = _context.Drivers.Where(driver => driver.DriverPkid == driverId).Include(driver => driver.VehicleData).Include(driver => driver.VehicleData.YBSCompany).Include(driver => driver.VehicleData.YBSType).Include(driver => driver.VehicleData.FuelType).Include(driver => driver.VehicleData.Manufacturer).FirstOrDefault();
                    Console.WriteLine("driver pkid: " + driver.DriverPkid);
                    List<CallCenterInvestigationDept> callCenterInvestigationDepts = _context.CallCenterInvestigationDepts.Where(callCenter => callCenter.DriverPkid == driver.DriverPkid).ToList();
                    List<YboRecord> yboRecords = _context.YboRecords.Where(yboRecord => yboRecord.DriverPkid == driver.DriverPkid).ToList();
                    List<YBOInvestigationDept> yBOInvestigationDepts = _context.YBOInvestigationDepts.Where(yBOInvestigationDept => yBOInvestigationDept.DriverPkid == driver.DriverPkid).Include(yboInvestigation => yboInvestigation.PunishmentType).ToList();
                    List<TrafficControlCenterInvestigationDept> trafficControlCenterInvestigationDepts = _context.TrafficControlCenterInvestigationDepts.Where(trafficControlCenterInvestigationDept => trafficControlCenterInvestigationDept.DriverPkid == driver.DriverPkid).Include(traffic => traffic.PunishmentType).ToList();
                    List<SpecialEventInvestigationDept> specialEventInvestigationDepts = _context.SpecialEventInvestigationDepts.Where(specialEventInvestigationDept => specialEventInvestigationDept.DriverPkid == driver.DriverPkid).ToList();
                    DriverPunishmentInfo driverPunishmentInfo = new DriverPunishmentInfo()
                    {
                       VehicleData = driver.VehicleData,
                       Driver = driver                        
                    };
                    CallCenterPunishmentDriverInfo callCenterPunishmentDriverInfo = new CallCenterPunishmentDriverInfo();
                    callCenterPunishmentDriverInfo.TotalRecord = callCenterInvestigationDepts.Count;
                    if (callCenterInvestigationDepts.Count > 0)
                    {
                        List<string> callCenterPunishmentRecordList = new List<string>();
                        foreach(CallCenterInvestigationDept callCenterInvestigationDept in callCenterInvestigationDepts)
                        {
                            if(callCenterInvestigationDept.RecordDescription != null)
                            {
                                callCenterPunishmentRecordList.Add(callCenterInvestigationDept.RecordDescription);
                            }
                            else
                            {
                                callCenterPunishmentRecordList.Add("စုံစမ်းဆဲ");
                            }
                        }
                        callCenterPunishmentDriverInfo.Punishments = callCenterPunishmentRecordList;
                    }
                    driverPunishmentInfo.CallCenterPunishmentDriverInfo = callCenterPunishmentDriverInfo;

                    YBOPunishmentDriverInfo yBOPunishmentDriverInfo = new YBOPunishmentDriverInfo();
                    yBOPunishmentDriverInfo.TotalRecord = yboRecords.Count;
                    if (yboRecords.Count > 0)
                    {
                        List<string> yboRecordPunishmentRecordList = new List<string>();
                        foreach (YboRecord yboRecord in yboRecords)
                        {
                            if (yboRecord.RecordDescription != null)
                            {
                                yboRecordPunishmentRecordList.Add(yboRecord.RecordDescription);
                            }
                            else
                            {
                                yboRecordPunishmentRecordList.Add("စုံစမ်းဆဲ");
                            }
                        }
                        yBOPunishmentDriverInfo.Punishments = yboRecordPunishmentRecordList;
                    }
                    driverPunishmentInfo.YBOPunishmentDriverInfo = yBOPunishmentDriverInfo;

                    YBOInvestigationPunishmentDriverInfo yBOInvestigationPunishmentDriverInfo = new YBOInvestigationPunishmentDriverInfo();
                    yBOInvestigationPunishmentDriverInfo.TotalRecord = yBOInvestigationDepts.Count;
                    if (yBOInvestigationDepts.Count > 0)
                    {
                        List<string> yboInvestigationDeptPunishmentRecordList = new List<string>();
                        foreach (YBOInvestigationDept yBOInvestigationDept in yBOInvestigationDepts)
                        {
                            if (yBOInvestigationDept.PunishmentType != null)
                            {
                                if(yBOInvestigationDept.PunishmentType.Punishment != null)
                                {
                                    yboInvestigationDeptPunishmentRecordList.Add(yBOInvestigationDept.PunishmentType.Punishment);
                                }
                            }
                            else
                            {
                                yboInvestigationDeptPunishmentRecordList.Add("စုံစမ်းဆဲ");
                            }
                        }
                        yBOInvestigationPunishmentDriverInfo.Punishments = yboInvestigationDeptPunishmentRecordList;
                    }
                    driverPunishmentInfo.YBOInvestigationPunishmentDriverInfo = yBOInvestigationPunishmentDriverInfo;

                    TrafficControlCenterPunishmentDriverInfo trafficControlCenterPunishmentDriverInfo = new TrafficControlCenterPunishmentDriverInfo();
                    trafficControlCenterPunishmentDriverInfo.TotalRecord = trafficControlCenterInvestigationDepts.Count;
                    if (trafficControlCenterInvestigationDepts.Count > 0)
                    {
                        List<string> trafficControlCenterPunishmentRecordList = new List<string>();
                        foreach (TrafficControlCenterInvestigationDept trafficControlCenterInvestigationDept in trafficControlCenterInvestigationDepts)
                        {
                            if (trafficControlCenterInvestigationDept.PunishmentType != null)
                            {
                                if (trafficControlCenterInvestigationDept.PunishmentType.Punishment != null)
                                {
                                    trafficControlCenterPunishmentRecordList.Add(trafficControlCenterInvestigationDept.PunishmentType.Punishment);
                                }
                                else
                                {
                                    trafficControlCenterPunishmentRecordList.Add("စုံစမ်းဆဲ");
                                }
                            }
                        }
                        trafficControlCenterPunishmentDriverInfo.Punishments = trafficControlCenterPunishmentRecordList;
                    }
                    driverPunishmentInfo.TrafficControlCenterPunishmentDriverInfo = trafficControlCenterPunishmentDriverInfo;

                    SpecialEventInvestigationPunishmentDriverInfo specialEventInvestigationPunishmentDriverInfo = new SpecialEventInvestigationPunishmentDriverInfo();
                    specialEventInvestigationPunishmentDriverInfo.TotalRecord = specialEventInvestigationDepts.Count;
                    if (specialEventInvestigationDepts.Count > 0)
                    {
                        List<string> specialEventInvestigationPunishmentRecordList = new List<string>();
                        foreach (SpecialEventInvestigationDept specialEventInvestigationDept in specialEventInvestigationDepts)
                        {
                            if (specialEventInvestigationDept.RecordDescription != null)
                            {
                                specialEventInvestigationPunishmentRecordList.Add(specialEventInvestigationDept.RecordDescription);
                            }
                            else
                            {
                                specialEventInvestigationPunishmentRecordList.Add("စုံစမ်းဆဲ");
                            }
                        }
                        specialEventInvestigationPunishmentDriverInfo.Punishments = specialEventInvestigationPunishmentRecordList;
                    }
                    driverPunishmentInfo.SpecialEventInvestigationPunishmentDriverInfo = specialEventInvestigationPunishmentDriverInfo;
                    return driverPunishmentInfo;
                }
                _logger.LogInformation(">>>>>>>>>> Success. Find VehicleData by pkId with eger load. <<<<<<<<<<");

                return new DriverPunishmentInfo()
                {
                    VehicleData = _context.VehicleDatas.Where(vehicleData => vehicleData.VehicleDataPkid == vehicleId)
                    .Include(vehicle => vehicle.YBSCompany)
                    .Include(vehicle => vehicle.YBSType)
                    .Include(vehicle => vehicle.FuelType)
                    .Include(vehicle => vehicle.Manufacturer)
                    .FirstOrDefault()
                };
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when finding VehicleData by pkId with eger load. <<<<<<<<<<" + e);
                throw;
            }
        }

        public VehicleData FindVehicleDataByIdContainSoftDeleteEgerLoad(int id)
        {
            _logger.LogInformation(">>>>>>>>>> [VehicleDataServiceImpl][FindVehicleDataByIdContainSoftDeleteEgerLoad] Find VehicleData by pkId with eger load. <<<<<<<<<<");
            try
            {
                _logger.LogInformation(">>>>>>>>>> Success. Find VehicleData by pkId with eger load. <<<<<<<<<<");
                return _context.VehicleDatas
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
                return _context.VehicleDatas.Where(VehicleData => VehicleData.IsDeleted == false)
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
