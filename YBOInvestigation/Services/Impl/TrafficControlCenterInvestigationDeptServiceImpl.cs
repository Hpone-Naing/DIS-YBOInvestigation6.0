using YBOInvestigation.Classes;
using YBOInvestigation.Data;
using YBOInvestigation.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;
using YBOInvestigation.Paging;

namespace YBOInvestigation.Services.Impl
{
    public class TrafficControlCenterInvestigationDeptServiceImpl : AbstractServiceImpl<TrafficControlCenterInvestigationDept>, TrafficControlCenterInvestigationDeptService
    {
        private readonly ILogger<TrafficControlCenterInvestigationDeptServiceImpl> _logger;

        private readonly DriverService _driverService;
        private readonly VehicleDataService _vehicleDataService;
        public TrafficControlCenterInvestigationDeptServiceImpl(YBOInvestigationDBContext context, ILogger<TrafficControlCenterInvestigationDeptServiceImpl> logger, DriverService driverService, VehicleDataService vehicleDataService) : base(context, logger)
        {
            _logger = logger;
            _driverService = driverService;
            _vehicleDataService = vehicleDataService;
        }

        public List<TrafficControlCenterInvestigationDept> GetAllTrafficControlCenterInvestigationDepts()
        {
            _logger.LogInformation(">>>>>>>>>> [TrafficControlCenterInvestigationDeptServiceImpl][GetAllTrafficControlCenterInvestigationDepts] Retrieve TrafficControlCenterInvestigationDept List <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Retrieve CallCenterInvestigationDept List success. <<<<<<<<<<");
                return GetAll()
                    .OrderByDescending(trafficControlCenterInvestigationDept => trafficControlCenterInvestigationDept.CreatedDate)
                    .Where(trafficControlCenterInvestigationDept => trafficControlCenterInvestigationDept.IsDeleted == false).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when retrieveing CallCenterInvestigationDept List. <<<<<<<<<<" + e);
                throw;
            }
        }

        public List<TrafficControlCenterInvestigationDept> GetAllTrafficControlCenterInvestigationDeptsEgerLoad()
        {
            _logger.LogInformation(">>>>>>>>>> [TrafficControlCenterInvestigationDeptServiceImpl][GetAllTrafficControlCenterInvestigationDeptsEgerLoad] Retrieve TrafficControlCenterInvestigationDept eger load list <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Retrieve TrafficControlCenterInvestigationDept eger laod list success. <<<<<<<<<<");
                return _context.TrafficControlCenterInvestigationDepts
                    .Where(trafficControlCenterInvestigationDept => trafficControlCenterInvestigationDept.IsDeleted == false)
                            .Include(trafficControlCenterInvestigationDept => trafficControlCenterInvestigationDept.YBSCompany)
                            .Include(trafficControlCenterInvestigationDept => trafficControlCenterInvestigationDept.YBSType)
                            .Include(trafficControlCenterInvestigationDept => trafficControlCenterInvestigationDept.PunishmentType)
                            .Include(trafficControlCenterInvestigationDept => trafficControlCenterInvestigationDept.Driver)
                            .Include(trafficControlCenterInvestigationDept => trafficControlCenterInvestigationDept.Driver.VehicleData)
                            .OrderByDescending(trafficControlCenterInvestigationDept => trafficControlCenterInvestigationDept.CreatedDate)
                            .ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when retrieveing TrafficControlCenterInvestigationDept eger load list. <<<<<<<<<<" + e);
                throw;
            }
        }

        public PagingList<TrafficControlCenterInvestigationDept> GetAllTrafficControlCenterInvestigationDeptsWithPagin(string searchString, int? pageNo, int PageSize)
        {
            _logger.LogInformation(">>>>>>>>>> [TrafficControlCenterInvestigationDeptServiceImpl][GetAllTrafficControlCenterInvestigationDeptsWithPagin] SearchAll or GetAll TrafficControlCenterInvestigationDept list and make pagination <<<<<<<<<<");
            try
            {
                List<TrafficControlCenterInvestigationDept> resultList = new List<TrafficControlCenterInvestigationDept>();
                if (searchString != null && !String.IsNullOrEmpty(searchString))
                {
                    _logger.LogInformation($">>>>>>>>>> Get searchAll result TrafficControlCenterInvestigationDept paginate eger load list. <<<<<<<<<<");
                    try
                    {
                        _logger.LogInformation($">>>>>>>>>> Success. Get searchAll result TrafficControlCenterInvestigationDept paginate eger load list. <<<<<<<<<<");
                        resultList = _context.TrafficControlCenterInvestigationDepts
                    .Where(trafficControlCenterInvestigationDept => trafficControlCenterInvestigationDept.IsDeleted == false)
                            .Include(trafficControlCenterInvestigationDept => trafficControlCenterInvestigationDept.YBSCompany)
                            .Include(trafficControlCenterInvestigationDept => trafficControlCenterInvestigationDept.YBSType)
                            .Include(trafficControlCenterInvestigationDept => trafficControlCenterInvestigationDept.PunishmentType)
                            .Include(trafficControlCenterInvestigationDept => trafficControlCenterInvestigationDept.Driver)
                            .Include(trafficControlCenterInvestigationDept => trafficControlCenterInvestigationDept.Driver.VehicleData)
                            .AsEnumerable()
                            .Where(trafficControlCenterInvestigationDept => IsSearchDataContained(trafficControlCenterInvestigationDept, searchString)
                            || IsSearchDataContained(trafficControlCenterInvestigationDept.YBSCompany, searchString)
                            || IsSearchDataContained(trafficControlCenterInvestigationDept.YBSType, searchString)
                            || IsSearchDataContained(trafficControlCenterInvestigationDept.Driver, searchString)
                            || IsSearchDataContained(trafficControlCenterInvestigationDept.PunishmentType, searchString)
                            )
                            .OrderByDescending(trafficControlCenterInvestigationDept => trafficControlCenterInvestigationDept.CreatedDate)
                            .ToList();
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(">>>>>>>>>> Error occur. Get searchAll result TrafficControlCenterInvestigationDept paginate eger load list. <<<<<<<<<<" + e);
                        throw;
                    }
                }
                else
                {
                    _logger.LogInformation($">>>>>>>>>> GetAll TrafficControlCenterInvestigationDept paginate eger load list. <<<<<<<<<<");
                    try
                    {
                        _logger.LogInformation($">>>>>>>>>> Success. GetAll TrafficControlCenterInvestigationDept paginate eger load list. <<<<<<<<<<");
                        resultList = _context.TrafficControlCenterInvestigationDepts
                    .Where(trafficControlCenterInvestigationDept => trafficControlCenterInvestigationDept.IsDeleted == false)
                            .Include(trafficControlCenterInvestigationDept => trafficControlCenterInvestigationDept.YBSCompany)
                            .Include(trafficControlCenterInvestigationDept => trafficControlCenterInvestigationDept.YBSType)
                            .Include(trafficControlCenterInvestigationDept => trafficControlCenterInvestigationDept.PunishmentType)
                            .Include(trafficControlCenterInvestigationDept => trafficControlCenterInvestigationDept.Driver)
                            .Include(trafficControlCenterInvestigationDept => trafficControlCenterInvestigationDept.Driver.VehicleData)
                            .OrderByDescending(trafficControlCenterInvestigationDept => trafficControlCenterInvestigationDept.CreatedDate)
                            .ToList();
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(">>>>>>>>>> Error occur. GetAll TrafficControlCenterInvestigationDept paginate eger load list. <<<<<<<<<<" + e);
                        throw;
                    }
                }
                _logger.LogInformation($">>>>>>>>>> SearchAll or GetAll TrafficControlCenterInvestigationDept list and make pagination. <<<<<<<<<<");
                return GetAllWithPagin(resultList, pageNo, PageSize);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur. SearchAll or GetAll TrafficControlCenterInvestigationDept list and make pagination. <<<<<<<<<<" + e);
                throw;
            }
        }


        public bool CreateTrafficControlCenterInvestigationDept(TrafficControlCenterInvestigationDept trafficControlCenterInvestigationDept)
        {
            _logger.LogInformation(">>>>>>>>>> [TrafficControlCenterInvestigationDeptServiceImpl][CreateTrafficControlCenterInvestigationDept] Create TrafficControlCenterInvestigationDept <<<<<<<<<<");
            trafficControlCenterInvestigationDept.IsDeleted = false;
            trafficControlCenterInvestigationDept.CreatedDate = DateTime.Now;
            trafficControlCenterInvestigationDept.CreatedBy = "admin";

            _logger.LogInformation(">>>>>>>>>> Parse integer driverPkId. <<<<<<<<<<");
            try
            {
                if (!string.IsNullOrEmpty(trafficControlCenterInvestigationDept.DriverName) && int.TryParse(trafficControlCenterInvestigationDept.DriverName, out int oldDriverPkId))
                {
                    _logger.LogInformation(">>>>>>>>>> Success parse integer driverPkId. <<<<<<<<<<");
                    try
                    {
                        _logger.LogInformation(">>>>>>>>>> Find driver by driverPkId and assign old drivername. <<<<<<<<<<");

                        Driver oldDriver = _driverService.FindDriverById(oldDriverPkId);
                        if (oldDriver != null)
                        {
                            trafficControlCenterInvestigationDept.DriverName = oldDriver.DriverName;
                        }
                        else
                        {
                            trafficControlCenterInvestigationDept.DriverName = "စီစစ်ဆဲ";
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
            VehicleData vehicleData = _vehicleDataService.FindVehicleByVehicleNumber(trafficControlCenterInvestigationDept.VehicleNumber);
            Driver existingDriver = null;
            _logger.LogInformation(">>>>>>>>>> Get driver by driverLicense. <<<<<<<<<<");
            try
            {
                _logger.LogInformation(">>>>>>>>>> Success Get driver by driverLicense. <<<<<<<<<<");
                
                    existingDriver = _driverService.FindDriverByIdNumberAndLicenseAndVehicle(trafficControlCenterInvestigationDept.IDNumber, trafficControlCenterInvestigationDept.DriverLicense, vehicleData.VehicleDataPkid);
                
                
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when getting driver by driverLicense <<<<<<<<<<" + e);
                throw;
            }
            try
            {
                _logger.LogInformation(">>>>>>>>>> Create new driver or edit old driver and create CallCenterInvestigationDept. <<<<<<<<<<");

                if (existingDriver == null)
                {
                    Driver driver = new Driver
                    {
                        DriverName = trafficControlCenterInvestigationDept.DriverName,
                    };
                    if (trafficControlCenterInvestigationDept.IsDefaultIdNumber())
                    {
                        driver.IDNumber = "စီစစ်ဆဲ";
                    }
                    else
                    {
                        driver.IDNumber = trafficControlCenterInvestigationDept.IDNumber;
                    }
                    if (trafficControlCenterInvestigationDept.IsDefaultLinenseNumber())
                    {
                        driver.DriverLicense = "စီစစ်ဆဲ";
                    }
                    else
                    {
                        driver.DriverLicense = trafficControlCenterInvestigationDept.DriverLicense;
                    }
                    driver.VehicleData = vehicleData;
                    //_driverService.CreateDriver(driver);
                    trafficControlCenterInvestigationDept.Driver = driver;
                    _logger.LogInformation(">>>>>>>>>> Create success TrafficControlCenterInvestigationDept with new driver. <<<<<<<<<<");
                    return Create(trafficControlCenterInvestigationDept);
                }
                else
                {
                    trafficControlCenterInvestigationDept.Driver = existingDriver;
                    _logger.LogInformation(">>>>>>>>>> Create success TrafficControlCenterInvestigationDept with existing driver.<<<<<<<<<<");
                    return Create(trafficControlCenterInvestigationDept);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when creating new driver or edit old driver and create TrafficControlCenterInvestigationDept. <<<<<<<<<<" + e);
                throw;
            }
        }

        public bool EditTrafficControlCenterInvestigationDept(TrafficControlCenterInvestigationDept trafficControlCenterInvestigationDept)
        {
            _logger.LogInformation(">>>>>>>>>> [TrafficControlCenterInvestigationDeptServiceImpl][EditTrafficControlCenterInvestigationDept] Edit TrafficControlCenterInvestigationDept <<<<<<<<<<");
            trafficControlCenterInvestigationDept.IsDeleted = false;
            trafficControlCenterInvestigationDept.CreatedDate = DateTime.Now;
            trafficControlCenterInvestigationDept.CreatedBy = "admin";
            _logger.LogInformation(">>>>>>>>>> Parse integer driverPkId. <<<<<<<<<<");

                if (int.TryParse(trafficControlCenterInvestigationDept.DriverName, out int oldDriverPkId))
                {
                Driver oldDriver = _driverService.FindDriverById(oldDriverPkId);
                _logger.LogInformation(">>>>>>>>>> Success find driver by driverPkId and assign old drivername. <<<<<<<<<<");
                trafficControlCenterInvestigationDept.Driver = oldDriver;
                Update(trafficControlCenterInvestigationDept);
                return true;
            }
            VehicleData vehicleData = _vehicleDataService.FindVehicleByVehicleNumber(trafficControlCenterInvestigationDept.VehicleNumber);
            Driver existingDriver = _driverService.FindDriverByIdNumberAndLicenseAndVehicle(trafficControlCenterInvestigationDept.IDNumber, trafficControlCenterInvestigationDept.DriverLicense, vehicleData.VehicleDataPkid);
            if (existingDriver != null)
            {
                Console.WriteLine("existing driver not null.................." + existingDriver.DriverPkid);
                existingDriver.DriverName = trafficControlCenterInvestigationDept.DriverName;
                _driverService.EditDriver(existingDriver);
                Console.WriteLine("before insert driver.............." + trafficControlCenterInvestigationDept.Driver.DriverPkid);

                trafficControlCenterInvestigationDept.Driver = existingDriver;
                Console.WriteLine("After insert driver.............." + trafficControlCenterInvestigationDept.Driver.DriverPkid);
                return Update(trafficControlCenterInvestigationDept);
            }
            Driver currentDriver = _driverService.FindDriverById(trafficControlCenterInvestigationDept.Driver.DriverPkid);
            currentDriver.IDNumber = trafficControlCenterInvestigationDept.IDNumber;
            currentDriver.DriverLicense = trafficControlCenterInvestigationDept.DriverLicense;
            currentDriver.DriverName = trafficControlCenterInvestigationDept.DriverName;
            _driverService.EditDriver(currentDriver);
            trafficControlCenterInvestigationDept.Driver = currentDriver;
            _logger.LogInformation(">>>>>>>>>> Edit success specialEventInvestigationDept with existing driver.<<<<<<<<<<");
            return Update(trafficControlCenterInvestigationDept);
        }

        public bool DeleteTrafficControlCenterInvestigationDept(TrafficControlCenterInvestigationDept trafficControlCenterInvestigationDept)
        {
            _logger.LogInformation(">>>>>>>>>> [TrafficControlCenterInvestigationDeptServiceImpl][DeleteTrafficControlCenterInvestigationDept] Soft delete TrafficControlCenterInvestigationDept. <<<<<<<<<<");
            try
            {
                _logger.LogInformation(">>>>>>>>>> Success. Soft delete TrafficControlCenterInvestigationDept.<<<<<<<<<<");
                trafficControlCenterInvestigationDept.IsDeleted = true;
                return Update(trafficControlCenterInvestigationDept);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when soft deleting TrafficControlCenterInvestigationDept. <<<<<<<<<<" + e);
                throw;
            }
        }

        public TrafficControlCenterInvestigationDept FindTrafficControlCenterInvestigationDeptById(int id)
        {
            _logger.LogInformation(">>>>>>>>>> [TrafficControlCenterInvestigationDeptServiceImpl][FindTrafficControlCenterInvestigationDeptById] Find TrafficControlCenterInvestigationDept by pkId <<<<<<<<<<");
            try
            {
                _logger.LogInformation(">>>>>>>>>> Success find TrafficControlCenterInvestigationDept by pkId.<<<<<<<<<<");
                return FindById(id);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when finding TrafficControlCenterInvestigationDept by pkId. <<<<<<<<<<" + e);
                throw;
            }
        }

        public TrafficControlCenterInvestigationDept FindTrafficControlCenterInvestigationDeptByIdEgerLoad(int id)
        {
            _logger.LogInformation(">>>>>>>>>> [TrafficControlCenterInvestigationDeptServiceImpl][FindTrafficControlCenterInvestigationDeptByIdEgerLoad] Find TrafficControlCenterInvestigationDept by pkId with eger load <<<<<<<<<<");
            try
            {
                _logger.LogInformation(">>>>>>>>>> Success find TrafficControlCenterInvestigationDept by pkId with eger load.<<<<<<<<<<");
                return _context.TrafficControlCenterInvestigationDepts.Where(record => record.IsDeleted == false)
                           .Include(trafficControlCenterInvestigationDept => trafficControlCenterInvestigationDept.YBSCompany)
                           .Include(trafficControlCenterInvestigationDept => trafficControlCenterInvestigationDept.YBSType)
                           .Include(trafficControlCenterInvestigationDept => trafficControlCenterInvestigationDept.Driver)
                           .Include(trafficControlCenterInvestigationDept => trafficControlCenterInvestigationDept.PunishmentType)
                           .Include(trafficControlCenterInvestigationDept => trafficControlCenterInvestigationDept.Driver.VehicleData)
                           .FirstOrDefault(trafficControlCenterInvestigationDept => trafficControlCenterInvestigationDept.TrafficControlCenterInvestigationDeptPkid == id);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when finding TrafficControlCenterInvestigationDept by pkId with eger load. <<<<<<<<<<" + e);
                throw;
            }
        }

        public DataTable MakeTrafficControlCenterInvestigationDeptExcelData(PagingList<TrafficControlCenterInvestigationDept> trafficControlCenterInvestigationDepts, bool exportAll)
        {
            _logger.LogInformation(">>>>>>>>>> [TrafficControlCenterInvestigationDeptServiceImpl][MakeTrafficControlCenterInvestigationDeptExcelData] Assign SearchAll or GetAll TrafficControlCenterInvestigationDept list to dataTable. <<<<<<<<<<");
            DataTable dt = new DataTable("Traffic Control Centerစာရင်း");
            dt.Columns.AddRange(new DataColumn[14] {
                                        new DataColumn("ဖမ်းဆီးရက်စွဲ"),
                                        new DataColumn("ဖမ်းဆီးသည့်အချိန်"),
                                        new DataColumn("မီးနီဖြတ်သည့်နေရာ"),
                                        new DataColumn("ဖုန်းနံပါတ်"),
                                        new DataColumn("သတင်းပေးပို့သူ"),
                                        new DataColumn("အမှုအမျိုးအစား"),
                                        new DataColumn("ဆောင်ရွက်ပြီးစီးမှု"),
                                        new DataColumn("ဆောင်ရွက်ပြီးစီးသည့်ရက်စွဲ"),
                                        new DataColumn("ယာဥ်မောင်းအမည်"),
                                        new DataColumn("ယာဥ်အမှတ်"),
                                        new DataColumn("လိုင်စင်အမှတ်"),
                                        new DataColumn("ID Number"),
                                        new DataColumn("YBS Company Name"),
                                        new DataColumn("ယာဥ်လိုင်း"),
                                        });
            var list = new List<TrafficControlCenterInvestigationDept>();
            if (exportAll)
            {
                _logger.LogInformation(">>>>>>>>>> For export all datas. <<<<<<<<<<");
                _logger.LogInformation(">>>>>>>>>> Get all TrafficControlCenterInvestigationDept eger load list. <<<<<<<<<<");
                try
                {
                    _logger.LogInformation(">>>>>>>>>> ှSuccess. Get all TrafficControlCenterInvestigationDept eger load list. <<<<<<<<<<");
                    list = GetAllTrafficControlCenterInvestigationDeptsEgerLoad();
                }
                catch (Exception e)
                {
                    _logger.LogError(">>>>>>>>>> Error occur when getting all TrafficControlCenterInvestigationDept eger load list. <<<<<<<<<<" + e);
                    throw;
                }
            }
            else
            {
                _logger.LogInformation(">>>>>>>>>> For export paginate or searchResult TrafficControlCenterInvestigationDept list. <<<<<<<<<<");
                _logger.LogInformation(">>>>>>>>>> Get all paginate or searchResult TrafficControlCenterInvestigationDept eger load list. <<<<<<<<<<");
                try
                {
                    _logger.LogInformation(">>>>>>>>>> Success. Get all paginate or searchResult TrafficControlCenterInvestigationDept eger load list. <<<<<<<<<<");
                    list = trafficControlCenterInvestigationDepts;
                }
                catch (Exception e)
                {
                    _logger.LogError(">>>>>>>>>> Error occur when getting all paginate or searchResult TrafficControlCenterInvestigationDept eger load list. <<<<<<<<<<" + e);
                    throw;
                }
            }
            try
            {
                _logger.LogInformation(">>>>>>>>>> Assign list to dataTable. <<<<<<<<<<");
                if (list.Count() > 0)
                {
                    foreach (var trafficControlCenterInvestigationDept in list)
                    {
                        dt.Rows.Add(trafficControlCenterInvestigationDept.RecordDate, trafficControlCenterInvestigationDept.RecordTime, trafficControlCenterInvestigationDept.RedLightCrossingPlace, trafficControlCenterInvestigationDept.Phone, trafficControlCenterInvestigationDept.YbsRecordSender, trafficControlCenterInvestigationDept.PunishmentType.Punishment, trafficControlCenterInvestigationDept.CompletionStatus, trafficControlCenterInvestigationDept.CompletedDate, trafficControlCenterInvestigationDept.Driver.DriverName, trafficControlCenterInvestigationDept.Driver.VehicleData.VehicleNumber, trafficControlCenterInvestigationDept.Driver.DriverLicense, trafficControlCenterInvestigationDept.YBSCompany.YBSCompanyName, trafficControlCenterInvestigationDept.Driver.IDNumber,trafficControlCenterInvestigationDept.YBSType.YBSTypeName);
                    }
                }
                _logger.LogInformation(">>>>>>>>>> Assign list to dataTable success. <<<<<<<<<<");
                return dt;
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when assigning SearchAll or GetAll TrafficControlCenterInvestigationDept list to dataTable. <<<<<<<<<<" + e);
                throw;
            }
        }

        public int GetTotalRecordByDriver(int driverPkId)
        {
            _logger.LogInformation(">>>>>>>>>> [TrafficControlCenterInvestigationDepts][FindCallCenterInvestigationDeptByIdEgerLoad] Find CallCenterInvestigationDept by pkId with eger load <<<<<<<<<<");
            try
            {
                _logger.LogInformation(">>>>>>>>>> Success find TrafficControlCenterInvestigationDepts by pkId with eger load.<<<<<<<<<<");
                int TotalRecord = _context.TrafficControlCenterInvestigationDepts.Count(record => record.DriverPkid == driverPkId);
                if (TotalRecord == 0)
                    return 1;
                return TotalRecord;
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when finding TrafficControlCenterInvestigationDepts by pkId with eger load. <<<<<<<<<<" + e);
                throw;
            }
        }
    }
}
