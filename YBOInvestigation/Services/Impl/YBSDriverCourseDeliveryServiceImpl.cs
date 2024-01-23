using YBOInvestigation.Data;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace YBOInvestigation.Services.Impl
{
    public class YBSDriverCourseDeliveryServiceImpl : AbstractServiceImpl<YBSDriverCourseDelivery>, YBSDriverCourseDeliveryService
    {
        private readonly ILogger<YBSDriverCourseDeliveryServiceImpl> _logger;

        private readonly DriverService _driverService;
        private readonly TrainedYBSDriverInfoService _trainedDriverInfoService;
        private readonly VehicleDataService _vehicleDataService;
        public YBSDriverCourseDeliveryServiceImpl(YBOInvestigationDBContext context, ILogger<YBSDriverCourseDeliveryServiceImpl> logger, DriverService driverService, TrainedYBSDriverInfoService trainedDriverInfoService, VehicleDataService vehicleDataService) : base(context, logger)
        {
            _logger = logger;

            _driverService = driverService;
            _trainedDriverInfoService = trainedDriverInfoService;
            _vehicleDataService = vehicleDataService;
        }

        public List<YBSDriverCourseDelivery> GetAllYBSDriverCourseDeliveries()
        {
            _logger.LogInformation(">>>>>>>>>> [YBSDriverCourseDeliveryServiceImpl][GetAllYBSDriverCourseDeliveries] Retrieve YBSDriverCourseDelivery List <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Retrieve YBSDriverCourseDelivery List success. <<<<<<<<<<");
                return GetAll().Where(yBSDriverCourseDelivery => yBSDriverCourseDelivery.IsDeleted == false).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when retrieveing YBSDriverCourseDelivery List. <<<<<<<<<<" + e);
                throw;
            }
        }

        public List<YBSDriverCourseDelivery> GetAllYBSDriverCourseDeliveriesEgerLoad()
        {
            _logger.LogInformation(">>>>>>>>>> [YBSDriverCourseDeliveryServiceImpl][GetAllYBSDriverCourseDeliveriesEgerLoad] Retrieve YBSDriverCourseDelivery eger load list <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Retrieve YBSDriverCourseDelivery eger laod list success. <<<<<<<<<<");
                return _context.YBSDriverCourseDeliveries
                    .Where(yBSDriverCourseDelivery => yBSDriverCourseDelivery.IsDeleted == false)
                            .Include(yBSDriverCourseDelivery => yBSDriverCourseDelivery.YBSCompany)
                            .Include(yBSDriverCourseDelivery => yBSDriverCourseDelivery.YBSType)
                            .Include(yBSDriverCourseDelivery => yBSDriverCourseDelivery.TrainedYBSDriverInfo)
                            .Include(yBSDriverCourseDelivery => yBSDriverCourseDelivery.TrainedYBSDriverInfo.Driver)
                            .Include(yBSDriverCourseDelivery => yBSDriverCourseDelivery.TrainedYBSDriverInfo.Driver.VehicleData)
                            .ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when retrieveing YBSDriverCourseDelivery eger load list. <<<<<<<<<<" + e);
                throw;
            }
        }

        public PagingList<YBSDriverCourseDelivery> GetAllYBSDriverCourseDeliveriesWithPagin(string searchString, int? pageNo, int PageSize)
        {
            _logger.LogInformation(">>>>>>>>>> [YBSDriverCourseDeliveryServiceImpl][GetAllYBSDriverCourseDeliveriesWithPagin] SearchAll or GetAll YBSDriverCourseDelivery list and make pagination <<<<<<<<<<");
            try
            {
                List<YBSDriverCourseDelivery> resultList = new List<YBSDriverCourseDelivery>();
                if (searchString != null && !String.IsNullOrEmpty(searchString))
                {
                    _logger.LogInformation($">>>>>>>>>> Get searchAll result YBSDriverCourseDelivery paginate eger load list. <<<<<<<<<<");
                    try
                    {
                        _logger.LogInformation($">>>>>>>>>> Success. Get searchAll result YBSDriverCourseDelivery paginate eger load list. <<<<<<<<<<");
                        resultList = _context.YBSDriverCourseDeliveries
                            .Where(yBSDriverCourseDelivery => yBSDriverCourseDelivery.IsDeleted == false)
                            .Include(yBSDriverCourseDelivery => yBSDriverCourseDelivery.YBSCompany)
                            .Include(yBSDriverCourseDelivery => yBSDriverCourseDelivery.YBSType)
                            .Include(yBSDriverCourseDelivery => yBSDriverCourseDelivery.PunishmentType)
                            .Include(yBSDriverCourseDelivery => yBSDriverCourseDelivery.TrainedYBSDriverInfo)
                            .Include(yBSDriverCourseDelivery => yBSDriverCourseDelivery.TrainedYBSDriverInfo.Driver)
                            .Include(yBSDriverCourseDelivery => yBSDriverCourseDelivery.TrainedYBSDriverInfo.Driver.VehicleData)
                            .ToList()
                            .Where(yBSDriverCourseDelivery => yBSDriverCourseDelivery.YBSCompany.IsDeleted == false)
                            .Where(yBSDriverCourseDelivery => yBSDriverCourseDelivery.YBSType.IsDeleted == false)
                            .Where(yBSDriverCourseDelivery => yBSDriverCourseDelivery.PunishmentType.IsDeleted == false)

                            .Where(yBSDriverCourseDelivery => IsSearchDataContained(yBSDriverCourseDelivery, searchString)
                            || IsSearchDataContained(yBSDriverCourseDelivery.YBSCompany, searchString)
                            || IsSearchDataContained(yBSDriverCourseDelivery.YBSType, searchString)
                            || IsSearchDataContained(yBSDriverCourseDelivery.TrainedYBSDriverInfo, searchString)
                            || IsSearchDataContained(yBSDriverCourseDelivery.TrainedYBSDriverInfo.Driver, searchString)
                            || IsSearchDataContained(yBSDriverCourseDelivery.TrainedYBSDriverInfo.Driver.VehicleData, searchString)
                            || IsSearchDataContained(yBSDriverCourseDelivery.PunishmentType, searchString)
                            ).AsQueryable().ToList();
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(">>>>>>>>>> Error occur. Get searchAll result YBSDriverCourseDelivery paginate eger load list. <<<<<<<<<<" + e);
                        throw;
                    }
                }
                else
                {
                    _logger.LogInformation($">>>>>>>>>> GetAll YBSDriverCourseDelivery paginate eger load list. <<<<<<<<<<");
                    try
                    {
                        _logger.LogInformation($">>>>>>>>>> Success. GetAll YBSDriverCourseDelivery paginate eger load list. <<<<<<<<<<");
                        resultList = _context.YBSDriverCourseDeliveries
                    .Where(yBSDriverCourseDelivery => yBSDriverCourseDelivery.IsDeleted == false)
                            .Include(yBSDriverCourseDelivery => yBSDriverCourseDelivery.YBSCompany)
                            .Include(yBSDriverCourseDelivery => yBSDriverCourseDelivery.YBSType)
                            .Include(yBSDriverCourseDelivery => yBSDriverCourseDelivery.PunishmentType)
                            .Include(yBSDriverCourseDelivery => yBSDriverCourseDelivery.TrainedYBSDriverInfo)
                            .Include(yBSDriverCourseDelivery => yBSDriverCourseDelivery.TrainedYBSDriverInfo.Driver)
                            .Include(yBSDriverCourseDelivery => yBSDriverCourseDelivery.TrainedYBSDriverInfo.Driver.VehicleData)
                            .ToList();
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(">>>>>>>>>> Error occur. GetAll YBSDriverCourseDelivery paginate eger load list. <<<<<<<<<<" + e);
                        throw;
                    }
                }
                _logger.LogInformation($">>>>>>>>>> SearchAll or GetAll YBSDriverCourseDelivery list and make pagination. <<<<<<<<<<");
                return GetAllWithPagin(resultList, pageNo, PageSize);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur. SearchAll or GetAll YBSDriverCourseDelivery list and make pagination. <<<<<<<<<<" + e);
                throw;
            }
        }


        public bool CreateYBSDriverCourseDeliveries(YBSDriverCourseDelivery yBSDriverCourseDelivery)
        {
            _logger.LogInformation(">>>>>>>>>> [YBSDriverCourseDeliveryServiceImpl][CreateYBSDriverCourseDeliveries] Create YBSDriverCourseDelivery <<<<<<<<<<");
            yBSDriverCourseDelivery.IsDeleted = false;
            yBSDriverCourseDelivery.CreatedDate = DateTime.Now;
            yBSDriverCourseDelivery.CreatedBy = "admin";
            _logger.LogInformation(">>>>>>>>>> Parse integer driverPkId. <<<<<<<<<<");
            try
            {
                _logger.LogInformation(">>>>>>>>>> Success parse integer driverPkId. <<<<<<<<<<");
                try
                {
                    _logger.LogInformation(">>>>>>>>>> Find driver by driverPkId and assign old drivername. <<<<<<<<<<");
                    if (!string.IsNullOrEmpty(yBSDriverCourseDelivery.DriverName) && int.TryParse(yBSDriverCourseDelivery.DriverName, out int oldDriverPkId))
                    {
                        Driver oldDriver = _driverService.FindDriverById(oldDriverPkId);
                        if (oldDriver != null)
                        {
                            yBSDriverCourseDelivery.DriverName = oldDriver.DriverName;
                        }
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(">>>>>>>>>> Error occur when finding driver by driverPkId and assign old drivername. <<<<<<<<<<" + e);
                    throw;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when parseing driver. <<<<<<<<<<" + e);
                throw;
            }
            Driver existingDriver = null;
            _logger.LogInformation(">>>>>>>>>> Get driver by driverLicense. <<<<<<<<<<");
            try
            {
                _logger.LogInformation(">>>>>>>>>> Success Get driver by driverLicense. <<<<<<<<<<");
                existingDriver = _driverService.FindDriverByLicense(yBSDriverCourseDelivery.DriverLicense);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when getting driver by driverLicense <<<<<<<<<<" + e);
                throw;
            }
            try
            {
                _logger.LogInformation(">>>>>>>>>> Create new driver or edit old driver and create YBSDriverCourseDelivery. <<<<<<<<<<");
                TrainedYBSDriverInfo trainedDriverInfo = new TrainedYBSDriverInfo
                {
                    Age = yBSDriverCourseDelivery.Age,
                    FatherName = yBSDriverCourseDelivery.FatherName,
                    Address = yBSDriverCourseDelivery.Address,
                    EducationLevel = yBSDriverCourseDelivery.EducationLevel,
                    Phone = yBSDriverCourseDelivery.Phone
                };
                if (existingDriver == null)
                {
                    VehicleData vehicleData = _vehicleDataService.FindVehicleByVehicleNumber(yBSDriverCourseDelivery.VehicleNumber);

                    Driver driver = new Driver
                    {
                        DriverName = yBSDriverCourseDelivery.DriverName,
                        DriverLicense = yBSDriverCourseDelivery.DriverLicense,
                    };
                    driver.VehicleData = vehicleData;
                    trainedDriverInfo.Driver = driver;
                    //_driverService.CreateDriver(driver);
                    yBSDriverCourseDelivery.TrainedYBSDriverInfo = trainedDriverInfo;
                    _logger.LogInformation(">>>>>>>>>> Create success YBSDriverCourseDelivery with new driver. <<<<<<<<<<");
                    return Create(yBSDriverCourseDelivery);
                }
                else
                {
                    TrainedYBSDriverInfo existingTrainedDriverInfo = _trainedDriverInfoService.GetTrainedYBSDriverInfoByDriverId(existingDriver.DriverPkid);
                    if (existingTrainedDriverInfo == null)
                    {
                        trainedDriverInfo.Driver = existingDriver;
                        yBSDriverCourseDelivery.TrainedYBSDriverInfo = trainedDriverInfo;
                    }
                    else
                    {
                        existingTrainedDriverInfo.Age = yBSDriverCourseDelivery.Age;
                        existingTrainedDriverInfo.Address = yBSDriverCourseDelivery.Address;
                        existingTrainedDriverInfo.Phone = yBSDriverCourseDelivery.Phone;
                        existingTrainedDriverInfo.FatherName = yBSDriverCourseDelivery.FatherName;
                        existingTrainedDriverInfo.EducationLevel = yBSDriverCourseDelivery.EducationLevel;
                        yBSDriverCourseDelivery.TrainedYBSDriverInfo = existingTrainedDriverInfo;
                    }
                    _logger.LogInformation(">>>>>>>>>> Create success YBSDriverCourseDelivery with existing driver.<<<<<<<<<<");
                    return Create(yBSDriverCourseDelivery);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when creating new driver or edit old driver and create YBSDriverCourseDelivery. <<<<<<<<<<" + e);
                throw;
            }
        }

        public bool EditYBSDriverCourseDeliveries(YBSDriverCourseDelivery yBSDriverCourseDelivery)
        {
            _logger.LogInformation(">>>>>>>>>> [YBSDriverCourseDeliveryServiceImpl][EditYBSDriverCourseDeliveries] Edit YBSDriverCourseDelivery <<<<<<<<<<");
            yBSDriverCourseDelivery.IsDeleted = false;
            yBSDriverCourseDelivery.CreatedDate = DateTime.Now;
            yBSDriverCourseDelivery.CreatedBy = "admin";
            _logger.LogInformation(">>>>>>>>>> Parse integer driverPkId. <<<<<<<<<<");
            try
            {
                if (int.TryParse(yBSDriverCourseDelivery.DriverName, out int oldDriverPkId))
                {
                    _logger.LogInformation(">>>>>>>>>> Success parse integer driverPkId. <<<<<<<<<<");
                    try
                    {
                        _logger.LogInformation(">>>>>>>>>> Find driver by driverPkId and assign old drivername. <<<<<<<<<<");
                        Driver oldDriver = _driverService.FindDriverById(oldDriverPkId);
                        if (oldDriver != null)
                        {
                            yBSDriverCourseDelivery.DriverName = oldDriver.DriverName;
                        }
                        _logger.LogInformation(">>>>>>>>>> Success find driver by driverPkId and assign old drivername. <<<<<<<<<<");
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(">>>>>>>>>> Error occur when finding driver by driverPkId and assign old drivername. <<<<<<<<<<" + e);
                        throw;
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when parseing driver. <<<<<<<<<<" + e);
                throw;
            }
            TrainedYBSDriverInfo trainedDriverInfo = new TrainedYBSDriverInfo
            {
                Age = yBSDriverCourseDelivery.Age,
                FatherName = yBSDriverCourseDelivery.FatherName,
                Address = yBSDriverCourseDelivery.Address,
                EducationLevel = yBSDriverCourseDelivery.EducationLevel,
                Phone = yBSDriverCourseDelivery.Phone
            };
            Driver existingDriver = null;
            _logger.LogInformation(">>>>>>>>>> Get driver by driverLicense. <<<<<<<<<<");
            try
            {
                _logger.LogInformation(">>>>>>>>>> Success Get driver by driverLicense. <<<<<<<<<<");

                existingDriver = _driverService.FindDriverByLicense(yBSDriverCourseDelivery.DriverLicense);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when getting driver by driverLicense <<<<<<<<<<" + e);
                throw;
            }
            try
            {
                _logger.LogInformation(">>>>>>>>>> Create new driver or edit old driver and edit YBSDriverCourseDelivery. <<<<<<<<<<");

                if (existingDriver == null)
                {
                    VehicleData vehicleData = _vehicleDataService.FindVehicleByVehicleNumber(yBSDriverCourseDelivery.VehicleNumber);
                    Driver driver = new Driver
                    {
                        DriverName = yBSDriverCourseDelivery.DriverName,
                        DriverLicense = yBSDriverCourseDelivery.DriverLicense,
                    };

                    driver.VehicleData = vehicleData;
                    _driverService.CreateDriver(driver);
                    trainedDriverInfo.Driver = driver;
                    _trainedDriverInfoService.CreateTrainedYBSDriverInfo(trainedDriverInfo);
                    yBSDriverCourseDelivery.TrainedYBSDriverInfo = trainedDriverInfo;
                    return Update(yBSDriverCourseDelivery);

                }
                else
                {
                    existingDriver.DriverName = yBSDriverCourseDelivery.DriverName;
                    existingDriver.DriverLicense = yBSDriverCourseDelivery.DriverLicense;
                    _driverService.EditDriver(existingDriver);

                    TrainedYBSDriverInfo existingTrainedDriverInfo = _trainedDriverInfoService.GetTrainedYBSDriverInfoByDriverId(existingDriver.DriverPkid);
                    if (existingTrainedDriverInfo == null)
                    {
                        trainedDriverInfo.Driver = existingDriver;
                        _trainedDriverInfoService.CreateTrainedYBSDriverInfo(trainedDriverInfo);
                        yBSDriverCourseDelivery.TrainedYBSDriverInfo = trainedDriverInfo;
                        _logger.LogInformation(">>>>>>>>>> Edit success YBSDriverCourseDelivery with new driver. <<<<<<<<<<");
                        return Update(yBSDriverCourseDelivery);
                    }
                    else
                    {
                        existingTrainedDriverInfo.Age = yBSDriverCourseDelivery.Age;
                        existingTrainedDriverInfo.Address = yBSDriverCourseDelivery.Address;
                        existingTrainedDriverInfo.Phone = yBSDriverCourseDelivery.Phone;
                        existingTrainedDriverInfo.FatherName = yBSDriverCourseDelivery.FatherName;
                        existingTrainedDriverInfo.EducationLevel = yBSDriverCourseDelivery.EducationLevel;
                        yBSDriverCourseDelivery.TrainedYBSDriverInfo = existingTrainedDriverInfo;
                        _trainedDriverInfoService.EditTrainedYBSDriverInfo(existingTrainedDriverInfo);
                        yBSDriverCourseDelivery.TrainedYBSDriverInfo = existingTrainedDriverInfo;
                        _logger.LogInformation(">>>>>>>>>> Edit success YBSDriverCourseDelivery with existing driver.<<<<<<<<<<");
                        return Update(yBSDriverCourseDelivery);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when creating new driver or edit old driver and create YBSDriverCourseDelivery. <<<<<<<<<<" + e);
                throw;
            }
        }

        public bool DeleteYBSDriverCourseDeliveries(YBSDriverCourseDelivery yBSDriverCourseDelivery)
        {
            _logger.LogInformation(">>>>>>>>>> [YBSDriverCourseDeliveryServiceImpl][DeleteYBSDriverCourseDeliveries] Soft delete YBSDriverCourseDelivery <<<<<<<<<<");
            try
            {
                yBSDriverCourseDelivery.IsDeleted = true;
                _logger.LogInformation(">>>>>>>>>> Success soft delete YBSDriverCourseDelivery. <<<<<<<<<<");
                return Update(yBSDriverCourseDelivery);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when soft deleting YBSDriverCourseDelivery. <<<<<<<<<<" + e);
                throw;
            }
        }

        public YBSDriverCourseDelivery FindYBSDriverCourseDeliveriesById(int id)
        {
            _logger.LogInformation(">>>>>>>>>> [YBSDriverCourseDeliveryServiceImpl][FindYBSDriverCourseDeliveriesById] Find YBSDriverCourseDelivery by pkId <<<<<<<<<<");
            try
            {
                _logger.LogInformation(">>>>>>>>>> Success find YBSDriverCourseDelivery by pkId.<<<<<<<<<<");
                return FindById(id);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when finding YBSDriverCourseDelivery by pkId. <<<<<<<<<<" + e);
                throw;
            }
        }

        public YBSDriverCourseDelivery FindYBSDriverCourseDeliveriesByIdEgerLoad(int id)
        {
            _logger.LogInformation(">>>>>>>>>> [YBSDriverCourseDeliveryServiceImpl][FindYBSDriverCourseDeliveriesByIdEgerLoad] Find YBSDriverCourseDelivery by pkId with eger load <<<<<<<<<<");
            try
            {
                _logger.LogInformation(">>>>>>>>>> Success find YBSDriverCourseDelivery by pkId with eger load.<<<<<<<<<<");
                return _context.YBSDriverCourseDeliveries.Where(record => record.IsDeleted == false)
                           .Include(yBSDriverCourseDelivery => yBSDriverCourseDelivery.YBSCompany)
                           .Include(yBSDriverCourseDelivery => yBSDriverCourseDelivery.YBSType)
                           .Include(yBSDriverCourseDelivery => yBSDriverCourseDelivery.TrainedYBSDriverInfo)
                            .Include(yBSDriverCourseDelivery => yBSDriverCourseDelivery.TrainedYBSDriverInfo.Driver)
                            .Include(yBSDriverCourseDelivery => yBSDriverCourseDelivery.TrainedYBSDriverInfo.Driver.VehicleData)
                           .FirstOrDefault(yBSDriverCourseDelivery => yBSDriverCourseDelivery.YBSDriverCourseDeliveryPkid == id);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when finding YBSDriverCourseDelivery by pkId with eger load. <<<<<<<<<<" + e);
                throw;
            }
        }

        public DataTable MakeYBSDriverCourseDeliveriesExcelData(PagingList<YBSDriverCourseDelivery> yBSDriverCourseDeliverys, bool exportAll)
        {
            _logger.LogInformation(">>>>>>>>>> [YBSDriverCourseDeliveryServiceImpl][MakeYBSDriverCourseDeliveriesExcelData] Assign SearchAll or GetAll YBSDriverCourseDelivery list to dataTable. <<<<<<<<<<");
            DataTable dt = new DataTable("သင်တန်းပေးမှုစာရင်း");
            dt.Columns.AddRange(new DataColumn[13] {
                                        new DataColumn("ပြုလုပ်ရက်စွဲ"),
                                        new DataColumn("အမှုအမျိုးအစား"),
                                        new DataColumn("အကြိမ်အရေ"),
                                        new DataColumn("သင်တန်းသားအမည်"),
                                        new DataColumn("အသက်"),
                                        new DataColumn("အဖအမည်"),
                                        new DataColumn("ပညာအရည်အချင်း"),
                                        new DataColumn("နေရပ်လိပ်စာ"),
                                        new DataColumn("ဖုန်းနံပါတ်"),
                                        new DataColumn("ယာဥ်အမှတ်"),
                                        new DataColumn("လိုင်စင်အမှတ်"),
                                        new DataColumn("YBS Company Name"),
                                        new DataColumn("ယာဥ်လိုင်း"),
                                        });
            var list = new List<YBSDriverCourseDelivery>();
            if (exportAll)
            {
                _logger.LogInformation(">>>>>>>>>> For export all datas. <<<<<<<<<<");
                _logger.LogInformation(">>>>>>>>>> Get all YboRecord eger load list. <<<<<<<<<<");
                try
                {
                    _logger.LogInformation(">>>>>>>>>> ှSuccess. Get all YBSDriverCourseDelivery eger load list. <<<<<<<<<<");
                    list = GetAllYBSDriverCourseDeliveriesEgerLoad();
                }
                catch (Exception e)
                {
                    _logger.LogError(">>>>>>>>>> Error occur when getting all YBSDriverCourseDelivery eger load list. <<<<<<<<<<" + e);
                    throw;
                }
            }
            else
            {
                _logger.LogInformation(">>>>>>>>>> For export paginate or searchResult YBSDriverCourseDelivery list. <<<<<<<<<<");
                _logger.LogInformation(">>>>>>>>>> Get all paginate or searchResult YBSDriverCourseDelivery eger load list. <<<<<<<<<<");
                try
                {
                    _logger.LogInformation(">>>>>>>>>> Success. Get all paginate or searchResult YBSDriverCourseDelivery eger load list. <<<<<<<<<<");
                    list = yBSDriverCourseDeliverys;
                }
                catch (Exception e)
                {
                    _logger.LogError(">>>>>>>>>> Error occur when getting all paginate or searchResult YBSDriverCourseDelivery eger load list. <<<<<<<<<<" + e);
                    throw;
                }
            }
            try
            {
                _logger.LogInformation(">>>>>>>>>> Assign list to dataTable. <<<<<<<<<<");
                if (list.Count() > 0)
            {
                foreach (var yBSDriverCourseDelivery in list)
                {
                    dt.Rows.Add(yBSDriverCourseDelivery.EventDate, yBSDriverCourseDelivery.PunishmentType.Punishment, yBSDriverCourseDelivery.TotalRecord, yBSDriverCourseDelivery.TrainedYBSDriverInfo.Driver.DriverName, yBSDriverCourseDelivery.TrainedYBSDriverInfo.Age, yBSDriverCourseDelivery.TrainedYBSDriverInfo.FatherName, yBSDriverCourseDelivery.TrainedYBSDriverInfo.EducationLevel, yBSDriverCourseDelivery.TrainedYBSDriverInfo.Address, yBSDriverCourseDelivery.TrainedYBSDriverInfo.Phone, yBSDriverCourseDelivery.TrainedYBSDriverInfo.Driver.VehicleData.VehicleNumber, yBSDriverCourseDelivery.TrainedYBSDriverInfo.Driver.DriverLicense, yBSDriverCourseDelivery.YBSCompany.YBSCompanyName, yBSDriverCourseDelivery.YBSType.YBSTypeName);
                }
            }
                _logger.LogInformation(">>>>>>>>>> Assign list to dataTable success. <<<<<<<<<<");
                return dt;
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when assigning SearchAll or GetAll YBSDriverCourseDelivery list to dataTable. <<<<<<<<<<" + e);
                throw;
            }
        }
    }
}