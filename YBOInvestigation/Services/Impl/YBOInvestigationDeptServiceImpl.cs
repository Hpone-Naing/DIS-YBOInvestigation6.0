using YBOInvestigation.Data;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace YBOInvestigation.Services.Impl
{
    public class YBOInvestigationDeptServiceImpl : AbstractServiceImpl<YBOInvestigationDept>, YBOInvestigationDeptService
    {
        private readonly ILogger<YBOInvestigationDeptServiceImpl> _logger;

        private readonly DriverService _driverService;
        private readonly VehicleDataService _vehicleDataService;
        public YBOInvestigationDeptServiceImpl(YBOInvestigationDBContext context, ILogger<YBOInvestigationDeptServiceImpl> logger, DriverService driverService, VehicleDataService vehicleDataService) : base(context, logger)
        {
            _logger = logger;
            _driverService = driverService;
            _vehicleDataService = vehicleDataService;
        }

        public List<YBOInvestigationDept> GetAllYBOInvestigationDepts()
        {
            _logger.LogInformation(">>>>>>>>>> [YBOInvestigationDeptServiceImpl][GetAllYBOInvestigationDepts] Retrieve YBOInvestigationDept List <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Retrieve YBOInvestigationDept List success. <<<<<<<<<<");
                return GetAll().Where(yBOInvestigationDept => yBOInvestigationDept.IsDeleted == false).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when retrieveing YBOInvestigationDept List. <<<<<<<<<<" + e);
                throw;
            }
        }

        public List<YBOInvestigationDept> GetAllYBOInvestigationDeptsEgerLoad()
        {
            _logger.LogInformation(">>>>>>>>>> [YBOInvestigationDeptServiceImpl][GetAllYBOInvestigationDeptsEgerLoad] Retrieve YBOInvestigationDept eger load list <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Retrieve YBOInvestigationDept eger laod list success. <<<<<<<<<<");
                return _context.YBOInvestigationDepts
                    .Where(yBOInvestigationDept => yBOInvestigationDept.IsDeleted == false)
                            .Include(yBOInvestigationDept => yBOInvestigationDept.YBSCompany)
                            .Include(yBOInvestigationDept => yBOInvestigationDept.YBSType)
                            .Include(yBOInvestigationDept => yBOInvestigationDept.Driver)
                            .Include(yBOInvestigationDept => yBOInvestigationDept.Driver.VehicleData)
                            .ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when retrieveing YBOInvestigationDept eger load list. <<<<<<<<<<" + e);
                throw;
            }
        }

        public PagingList<YBOInvestigationDept> GetAllYBOInvestigationDeptsWithPagin(string searchString, int? pageNo, int PageSize)
        {
            _logger.LogInformation(">>>>>>>>>> [YBOInvestigationDeptServiceImpl][GetAllYBOInvestigationDeptsWithPagin] SearchAll or GetAll YBOInvestigationDept list and make pagination <<<<<<<<<<");
            try
            {
                List<YBOInvestigationDept> resultList = new List<YBOInvestigationDept>();
                if (searchString != null && !String.IsNullOrEmpty(searchString))
                {
                    _logger.LogInformation($">>>>>>>>>> Get searchAll result YBOInvestigationDept paginate eger load list. <<<<<<<<<<");
                    try
                    {
                        _logger.LogInformation($">>>>>>>>>> Success. Get searchAll result YBOInvestigationDept paginate eger load list. <<<<<<<<<<");
                        resultList = _context.YBOInvestigationDepts
                    .Where(yBOInvestigationDept => yBOInvestigationDept.IsDeleted == false)
                            .Include(yBOInvestigationDept => yBOInvestigationDept.YBSCompany)
                            .Include(yBOInvestigationDept => yBOInvestigationDept.YBSType)
                            .Include(yBOInvestigationDept => yBOInvestigationDept.Driver)
                            .Include(yBOInvestigationDept => yBOInvestigationDept.Driver.VehicleData)
                            .ToList()
                            .Where(yBOInvestigationDept => IsSearchDataContained(yBOInvestigationDept, searchString)
                            || IsSearchDataContained(yBOInvestigationDept.YBSCompany, searchString)
                            || IsSearchDataContained(yBOInvestigationDept.YBSType, searchString)
                            || IsSearchDataContained(yBOInvestigationDept.Driver, searchString)
                            ).AsQueryable().ToList();
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(">>>>>>>>>> Error occur. Get searchAll result YBOInvestigationDept paginate eger load list. <<<<<<<<<<" + e);
                        throw;
                    }
                }
                else
                {
                    _logger.LogInformation($">>>>>>>>>> GetAll YBOInvestigationDept paginate eger load list. <<<<<<<<<<");
                    try
                    {
                        _logger.LogInformation($">>>>>>>>>> Success. GetAll YBOInvestigationDept paginate eger load list. <<<<<<<<<<");
                        resultList = _context.YBOInvestigationDepts
                    .Where(yBOInvestigationDept => yBOInvestigationDept.IsDeleted == false)
                            .Include(yBOInvestigationDept => yBOInvestigationDept.YBSCompany)
                            .Include(yBOInvestigationDept => yBOInvestigationDept.YBSType)
                            .Include(yBOInvestigationDept => yBOInvestigationDept.Driver)
                            .Include(yBOInvestigationDept => yBOInvestigationDept.Driver.VehicleData)
                            .ToList();
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(">>>>>>>>>> Error occur. GetAll YBOInvestigationDept paginate eger load list. <<<<<<<<<<" + e);
                        throw;
                    }
                }
                _logger.LogInformation($">>>>>>>>>> SearchAll or GetAll YBOInvestigationDept list and make pagination. <<<<<<<<<<");
                return GetAllWithPagin(resultList, pageNo, PageSize);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur. SearchAll or GetAll YBOInvestigationDept list and make pagination. <<<<<<<<<<" + e);
                throw;
            }
        }

        public bool CreateYBOInvestigationDept(YBOInvestigationDept yBOInvestigationDept)
        {
            _logger.LogInformation(">>>>>>>>>> [YBOInvestigationDeptServiceImpl][CreateYBOInvestigationDept] Create YBOInvestigationDept <<<<<<<<<<");
            yBOInvestigationDept.IsDeleted = false;
            yBOInvestigationDept.CreatedDate = DateTime.Now;
            yBOInvestigationDept.CreatedBy = "admin";
            _logger.LogInformation(">>>>>>>>>> Parse integer driverPkId. <<<<<<<<<<");
            try
            {
                if (!string.IsNullOrEmpty(yBOInvestigationDept.DriverName) && int.TryParse(yBOInvestigationDept.DriverName, out int oldDriverPkId))
                {
                    _logger.LogInformation(">>>>>>>>>> Success parse integer driverPkId. <<<<<<<<<<");
                    try
                    {
                        _logger.LogInformation(">>>>>>>>>> Find driver by driverPkId and assign old drivername. <<<<<<<<<<");
                        Driver oldDriver = _driverService.FindDriverById(oldDriverPkId);
                        if (oldDriver != null)
                        {
                            yBOInvestigationDept.DriverName = oldDriver.DriverName;
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
            Driver existingDriver = null;
            _logger.LogInformation(">>>>>>>>>> Get driver by driverLicense. <<<<<<<<<<");
            try
            {
                _logger.LogInformation(">>>>>>>>>> Success Get driver by driverLicense. <<<<<<<<<<");
                existingDriver = _driverService.FindDriverByLicense(yBOInvestigationDept.DriverLicense);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when getting driver by driverLicense <<<<<<<<<<" + e);
                throw;
            }
            try
            {
                _logger.LogInformation(">>>>>>>>>> Create new driver or edit old driver and create YBOInvestigationDept. <<<<<<<<<<");
                if (existingDriver == null)
                {
                    VehicleData vehicleData = _vehicleDataService.FindVehicleByVehicleNumber(yBOInvestigationDept.VehicleNumber);
                    Driver driver = new Driver
                    {
                        DriverName = yBOInvestigationDept.DriverName,
                        DriverLicense = yBOInvestigationDept.DriverLicense,
                    };
                    driver.VehicleData = vehicleData;
                    //_driverService.CreateDriver(driver);
                    yBOInvestigationDept.Driver = driver;
                    _logger.LogInformation(">>>>>>>>>> Create success YBOInvestigationDept with new driver. <<<<<<<<<<");
                    return Create(yBOInvestigationDept);
                }
                else
                {
                    yBOInvestigationDept.Driver = existingDriver;
                    _logger.LogInformation(">>>>>>>>>> Create success YBOInvestigationDept with existing driver.<<<<<<<<<<");
                    return Create(yBOInvestigationDept);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when creating new driver or edit old driver and create YBOInvestigationDept. <<<<<<<<<<" + e);
                throw;
            }
        }

        public bool EditYBOInvestigationDept(YBOInvestigationDept yBOInvestigationDept)
        {
            _logger.LogInformation(">>>>>>>>>> [YBOInvestigationDeptServiceImpl][EditYBOInvestigationDept] Edit YBOInvestigationDept <<<<<<<<<<");
            yBOInvestigationDept.IsDeleted = false;
            yBOInvestigationDept.CreatedDate = DateTime.Now;
            yBOInvestigationDept.CreatedBy = "admin";
            _logger.LogInformation(">>>>>>>>>> Parse integer driverPkId. <<<<<<<<<<");
            try
            {
                if (int.TryParse(yBOInvestigationDept.DriverName, out int oldDriverPkId))
                {
                    _logger.LogInformation(">>>>>>>>>> Success parse integer driverPkId. <<<<<<<<<<");
                    try
                    {
                        _logger.LogInformation(">>>>>>>>>> Find driver by driverPkId and assign old drivername. <<<<<<<<<<");
                        Driver oldDriver = _driverService.FindDriverById(oldDriverPkId);
                        if (oldDriver != null)
                        {
                            yBOInvestigationDept.DriverName = oldDriver.DriverName;
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
            Driver existingDriver = null;

            _logger.LogInformation(">>>>>>>>>> Get driver by driverLicense. <<<<<<<<<<");
            try
            {
                _logger.LogInformation(">>>>>>>>>> Success Get driver by driverLicense. <<<<<<<<<<");
                existingDriver = _driverService.FindDriverByLicense(yBOInvestigationDept.DriverLicense);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when getting driver by driverLicense <<<<<<<<<<" + e);
                throw;
            }
            try
            {
                _logger.LogInformation(">>>>>>>>>> Create new driver or edit old driver and edit YBOInvestigationDept. <<<<<<<<<<");
                if (existingDriver == null)
                {
                    VehicleData vehicleData = _vehicleDataService.FindVehicleByVehicleNumber(yBOInvestigationDept.VehicleNumber);
                    Driver driver = new Driver
                    {
                        DriverName = yBOInvestigationDept.DriverName,
                        DriverLicense = yBOInvestigationDept.DriverLicense,
                    };
                    driver.VehicleData = vehicleData;
                    _driverService.CreateDriver(driver);
                    yBOInvestigationDept.Driver = driver;
                    _logger.LogInformation(">>>>>>>>>> Edit success YBOInvestigationDept with new driver. <<<<<<<<<<");
                    return Update(yBOInvestigationDept);

                }
                else
                {
                    existingDriver.DriverName = yBOInvestigationDept.DriverName;
                    existingDriver.DriverLicense = yBOInvestigationDept.DriverLicense;
                    //existingDriver.VehicleNumber = yBOInvestigationDept.VehicleNumber;
                    _driverService.EditDriver(existingDriver);
                    yBOInvestigationDept.Driver = existingDriver;
                    _logger.LogInformation(">>>>>>>>>> Edit success YBOInvestigationDept with existing driver.<<<<<<<<<<");
                    return Update(yBOInvestigationDept);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when creating new driver or edit old driver and create YBOInvestigationDept. <<<<<<<<<<" + e);
                throw;
            }
        }

        public bool DeleteYBOInvestigationDept(YBOInvestigationDept yBOInvestigationDept)
        {
            _logger.LogInformation(">>>>>>>>>> [YBOInvestigationDeptServiceImpl][DeleteYBOInvestigationDept] Soft delete YBOInvestigationDept <<<<<<<<<<");
            try
            {
                yBOInvestigationDept.IsDeleted = true;
                _logger.LogInformation(">>>>>>>>>> Success soft delete YBOInvestigationDept. <<<<<<<<<<");
                return Update(yBOInvestigationDept);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when soft deleting YBOInvestigationDept. <<<<<<<<<<" + e);
                throw;
            }
        }

        public YBOInvestigationDept FindYBOInvestigationDeptById(int id)
        {
            _logger.LogInformation(">>>>>>>>>> [YBOInvestigationDeptServiceImpl][FindYBOInvestigationDeptById] Find YBOInvestigationDept by pkId <<<<<<<<<<");
            try
            {
                _logger.LogInformation(">>>>>>>>>> Success find YBOInvestigationDept by pkId.<<<<<<<<<<");
                return FindById(id);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when finding YBOInvestigationDept by pkId. <<<<<<<<<<" + e);
                throw;
            }
        }

        public YBOInvestigationDept FindYBOInvestigationDeptByIdEgerLoad(int id)
        {
            _logger.LogInformation(">>>>>>>>>> [YBOInvestigationDeptServiceImpl][FindYBOInvestigationDeptByIdEgerLoad] Find YBOInvestigationDept by pkId with eger load <<<<<<<<<<");
            try
            {
                _logger.LogInformation(">>>>>>>>>> Success find YBOInvestigationDept by pkId with eger load.<<<<<<<<<<");
                return _context.YBOInvestigationDepts.Where(record => record.IsDeleted == false)
                           .Include(yBOInvestigationDept => yBOInvestigationDept.YBSCompany)
                           .Include(yBOInvestigationDept => yBOInvestigationDept.YBSType)
                           .Include(yBOInvestigationDept => yBOInvestigationDept.Driver)
                           .Include(yBOInvestigationDept => yBOInvestigationDept.Driver.VehicleData)
                           .FirstOrDefault(yBOInvestigationDept => yBOInvestigationDept.YBOInvestigationDeptPkid == id);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when finding YBOInvestigationDept by pkId with eger load. <<<<<<<<<<" + e);
                throw;
            }
        }

        public DataTable MakeYBOInvestigationDeptExcelData(PagingList<YBOInvestigationDept> yBOInvestigationDepts, bool exportAll)
        {
            _logger.LogInformation(">>>>>>>>>> [YBOInvestigationDeptServiceImpl][MakeYBOInvestigationDeptExcelData] Assign SearchAll or GetAll YBOInvestigationDept list to dataTable. <<<<<<<<<<");
            DataTable dt = new DataTable("စည်းကမ်းထိန်းသိမ်းရေးနှင့်စစ်ဆေးရေးငှာန(YBO)စာရင်း");
            dt.Columns.AddRange(new DataColumn[13] {
                                        new DataColumn("ဖမ်းဆီးရက်စွဲ"),
                                        new DataColumn("ဖမ်းဆီးသည့်အချိန်"),
                                        new DataColumn("အကြိမ်အရေအတွက်"),
                                        new DataColumn("ဖုန်းနံပါတ်"),
                                        new DataColumn("သတင်းပေးပို့သူ"),
                                        new DataColumn("တိုင်ကြားသည့်အကြောင်းအရာ"),
                                        new DataColumn("ဆောင်ရွက်ပြီးစီးမှု"),
                                        new DataColumn("ဆောင်ရွက်ပြီးစီးသည့်ရက်စွဲ"),
                                        new DataColumn("ယာဥ်မောင်းအမည်"),
                                        new DataColumn("ယာဥ်အမှတ်"),
                                        new DataColumn("လိုင်စင်အမှတ်"),
                                        new DataColumn("YBS Company Name"),
                                        new DataColumn("ယာဥ်လိုင်း"),
                                        });
            var list = new List<YBOInvestigationDept>();
            if (exportAll)
            {
                _logger.LogInformation(">>>>>>>>>> For export all datas. <<<<<<<<<<");
                _logger.LogInformation(">>>>>>>>>> Get all YBOInvestigationDept eger load list. <<<<<<<<<<");
                try
                {
                    _logger.LogInformation(">>>>>>>>>> ှSuccess. Get all YBOInvestigationDept eger load list. <<<<<<<<<<");
                    list = GetAllYBOInvestigationDeptsEgerLoad();
                }
                catch (Exception e)
                {
                    _logger.LogError(">>>>>>>>>> Error occur when getting all YBOInvestigationDept eger load list. <<<<<<<<<<" + e);
                    throw;
                }
            }
            else
            {
                _logger.LogInformation(">>>>>>>>>> For export paginate or searchResult YBOInvestigationDept list. <<<<<<<<<<");
                _logger.LogInformation(">>>>>>>>>> Get all paginate or searchResult YBOInvestigationDept eger load list. <<<<<<<<<<");
                try
                {
                    _logger.LogInformation(">>>>>>>>>> Success. Get all paginate or searchResult YBOInvestigationDept eger load list. <<<<<<<<<<");
                    list = yBOInvestigationDepts;
                }
                catch (Exception e)
                {
                    _logger.LogError(">>>>>>>>>> Error occur when getting all paginate or searchResult YBOInvestigationDept eger load list. <<<<<<<<<<" + e);
                    throw;
                }
            }
            try
            {
                _logger.LogInformation(">>>>>>>>>> Assign list to dataTable. <<<<<<<<<<");
                if (list.Count() > 0)
                {
                    foreach (var yBOInvestigationDept in list)
                    {
                        dt.Rows.Add(yBOInvestigationDept.RecordDate, yBOInvestigationDept.RecordTime, yBOInvestigationDept.TotalRecord, yBOInvestigationDept.Phone, yBOInvestigationDept.YbsRecordSender, yBOInvestigationDept.RecordDescription, yBOInvestigationDept.CompletionStatus, yBOInvestigationDept.CompletedDate, yBOInvestigationDept.Driver.DriverName, yBOInvestigationDept.Driver.VehicleData.VehicleNumber, yBOInvestigationDept.Driver.DriverLicense, yBOInvestigationDept.YBSCompany.YBSCompanyName, yBOInvestigationDept.YBSType.YBSTypeName);
                    }
                }
                _logger.LogInformation(">>>>>>>>>> Assign list to dataTable success. <<<<<<<<<<");
                return dt;
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when assigning SearchAll or GetAll YBOInvestigationDept list to dataTable. <<<<<<<<<<" + e);
                throw;
            }
        }
    }
}