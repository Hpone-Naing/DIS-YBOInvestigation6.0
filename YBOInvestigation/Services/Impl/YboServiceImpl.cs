using YBOInvestigation.Classes;
using YBOInvestigation.Data;
using YBOInvestigation.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;
using YBOInvestigation.Paging;
using YBOInvestigation.Factories;

namespace YBOInvestigation.Services.Impl
{
    public class YboServiceImpl : AbstractServiceImpl<YboRecord>, YboRecordService
    {
        private readonly ILogger<YboServiceImpl> _logger;

        private readonly DriverService _driverService;
        private readonly VehicleDataService _vehicleDataService;
        public YboServiceImpl(YBOInvestigationDBContext context, ILogger<YboServiceImpl> logger, DriverService driverService, VehicleDataService vehicleDataService) : base(context, logger)
        {
            _logger = logger;

            _driverService = driverService;
            _vehicleDataService = vehicleDataService;
        }

        public List<YboRecord> GetAllYboRecords()
        {
            _logger.LogInformation(">>>>>>>>>> [YboServiceImpl][GetAllYboRecords] Retrieve YboRecord List <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Retrieve YboRecord List success. <<<<<<<<<<");
                return GetAll().Where(yboRecord => yboRecord.IsDeleted == false).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when retrieveing YboRecord List. <<<<<<<<<<" + e);
                throw;
            }
        }

        public List<YboRecord> GetAllYboRecordsEgerLoad()
        {
            _logger.LogInformation(">>>>>>>>>> [YboServiceImpl][GetAllYboRecordsEgerLoad] Retrieve YboRecord eger load list <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Retrieve YboRecord eger laod list success. <<<<<<<<<<");
                return _context.YboRecords
                    .Where(yboRecord => yboRecord.IsDeleted == false)
                            .Include(yboRecord => yboRecord.YBSCompany)
                            .Include(yboRecord => yboRecord.YBSType)
                            .Include(yboRecord => yboRecord.Driver)
                            .Include(yboRecord => yboRecord.Driver.VehicleData)
                            .ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when retrieveing YboRecord eger load list. <<<<<<<<<<" + e);
                throw;
            }
        }

        public PagingList<YboRecord> GetAllYboRecordsWithPagin(string searchString, int? pageNo, int PageSize)
        {
            _logger.LogInformation(">>>>>>>>>> [YboServiceImpl][GetAllYboRecordsWithPagin] SearchAll or GetAll YboRecord list and make pagination <<<<<<<<<<");
            try
            {
                List<YboRecord> resultList = new List<YboRecord>();
                if (searchString != null && !String.IsNullOrEmpty(searchString))
                {
                    _logger.LogInformation($">>>>>>>>>> Get searchAll result YboRecord paginate eger load list. <<<<<<<<<<");
                    try
                    {
                        _logger.LogInformation($">>>>>>>>>> Success. Get searchAll result YboRecord paginate eger load list. <<<<<<<<<<");
                        resultList = _context.YboRecords
                    .Where(yboRecord => yboRecord.IsDeleted == false)
                            .Include(yboRecord => yboRecord.YBSCompany)
                            .Include(yboRecord => yboRecord.YBSType)
                            .Include(yboRecord => yboRecord.Driver)
                            .Include(yboRecord => yboRecord.Driver.VehicleData)
                            .ToList()
                            .Where(yboRecord => IsSearchDataContained(yboRecord, searchString)
                            || IsSearchDataContained(yboRecord.YBSCompany, searchString)
                            || IsSearchDataContained(yboRecord.YBSType, searchString)
                            || IsSearchDataContained(yboRecord.Driver, searchString)
                            ).AsQueryable().ToList();
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(">>>>>>>>>> Error occur. Get searchAll result YboRecord paginate eger load list. <<<<<<<<<<" + e);
                        throw;
                    }
                }
                else
                {
                    _logger.LogInformation($">>>>>>>>>> GetAll YboRecord paginate eger load list. <<<<<<<<<<");
                    try
                    {
                        _logger.LogInformation($">>>>>>>>>> Success. GetAll YboRecord paginate eger load list. <<<<<<<<<<");
                        resultList = _context.YboRecords
                    .Where(yboRecord => yboRecord.IsDeleted == false)
                            .Include(yboRecord => yboRecord.YBSCompany)
                            .Include(yboRecord => yboRecord.YBSType)
                            .Include(yboRecord => yboRecord.Driver)
                            .Include(yboRecord => yboRecord.Driver.VehicleData)
                            .ToList();
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(">>>>>>>>>> Error occur. GetAll YboRecord paginate eger load list. <<<<<<<<<<" + e);
                        throw;
                    }
                }
                _logger.LogInformation($">>>>>>>>>> SearchAll or GetAll YboRecord list and make pagination. <<<<<<<<<<");
                return GetAllWithPagin(resultList, pageNo, PageSize);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur. SearchAll or GetAll YboRecord list and make pagination. <<<<<<<<<<" + e);
                throw;
            }
        }

        public bool CreateYboRecord(YboRecord yboRecord)
        {
            _logger.LogInformation(">>>>>>>>>> [YboServiceImpl][CreateYboRecord] Create YboRecord <<<<<<<<<<");
            yboRecord.IsDeleted = false;
            yboRecord.CreatedDate = DateTime.Now;
            yboRecord.CreatedBy = "admin";
            _logger.LogInformation(">>>>>>>>>> Parse integer driverPkId. <<<<<<<<<<");
            try
            {
                _logger.LogInformation(">>>>>>>>>> Success parse integer driverPkId. <<<<<<<<<<");
                try
                {
                    _logger.LogInformation(">>>>>>>>>> Find driver by driverPkId and assign old drivername. <<<<<<<<<<");
                    if (!string.IsNullOrEmpty(yboRecord.DriverName) && int.TryParse(yboRecord.DriverName, out int oldDriverPkId))
                    {
                        Driver oldDriver = _driverService.FindDriverById(oldDriverPkId);
                        if (oldDriver != null)
                        {
                            yboRecord.DriverName = oldDriver.DriverName;
                        }
                        _logger.LogInformation(">>>>>>>>>> Success find driver by driverPkId and assign old drivername. <<<<<<<<<<");
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
                existingDriver = _driverService.FindDriverByLicense(yboRecord.DriverLicense);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when getting driver by driverLicense <<<<<<<<<<" + e);
                throw;
            }
            try
            {
                _logger.LogInformation(">>>>>>>>>> Create new driver or edit old driver and create YboRecord. <<<<<<<<<<");
                if (existingDriver == null)
                {
                    VehicleData vehicleData = _vehicleDataService.FindVehicleByVehicleNumber(yboRecord.VehicleNumber);
                    Driver driver = new Driver
                    {
                        DriverName = yboRecord.DriverName,
                        DriverLicense = yboRecord.DriverLicense,
                    };
                    driver.VehicleData = vehicleData;
                    //_driverService.CreateDriver(driver);
                    yboRecord.Driver = driver;
                    _logger.LogInformation(">>>>>>>>>> Create success YboRecord with new driver. <<<<<<<<<<");
                    return Create(yboRecord);
                }
                else
                {
                    yboRecord.Driver = existingDriver;
                    _logger.LogInformation(">>>>>>>>>> Create success YboRecord with existing driver.<<<<<<<<<<");
                    return Create(yboRecord);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when creating new driver or edit old driver and create YBOInvestigationDept. <<<<<<<<<<" + e);
                throw;
            }

        }

        public bool EditYboRecord(YboRecord yboRecord)
        {
            _logger.LogInformation(">>>>>>>>>> [YboServiceImpl][EditYboRecord] Edit YboRecord <<<<<<<<<<");
            yboRecord.IsDeleted = false;
            yboRecord.CreatedDate = DateTime.Now;
            yboRecord.CreatedBy = "admin";
            _logger.LogInformation(">>>>>>>>>> Parse integer driverPkId. <<<<<<<<<<");
            try
            {
                if (int.TryParse(yboRecord.DriverName, out int oldDriverPkId))
                {
                    _logger.LogInformation(">>>>>>>>>> Success parse integer driverPkId. <<<<<<<<<<");
                    try
                    {
                        _logger.LogInformation(">>>>>>>>>> Find driver by driverPkId and assign old drivername. <<<<<<<<<<");
                        Driver oldDriver = _driverService.FindDriverById(oldDriverPkId);
                        if (oldDriver != null)
                        {
                            yboRecord.DriverName = oldDriver.DriverName;
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
                existingDriver = _driverService.FindDriverByLicense(yboRecord.DriverLicense);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when getting driver by driverLicense <<<<<<<<<<" + e);
                throw;
            }
            try
            {
                _logger.LogInformation(">>>>>>>>>> Create new driver or edit old driver and edit YboRecord. <<<<<<<<<<");
                if (existingDriver == null)
                {

                    VehicleData vehicleData = _vehicleDataService.FindVehicleByVehicleNumber(yboRecord.VehicleNumber);

                    Driver driver = new Driver
                    {
                        DriverName = yboRecord.DriverName,
                        DriverLicense = yboRecord.DriverLicense,
                    };

                    driver.VehicleData = vehicleData;
                    _driverService.CreateDriver(driver);
                    yboRecord.Driver = driver;
                    _logger.LogInformation(">>>>>>>>>> Edit success YboRecord with new driver. <<<<<<<<<<");
                    return Update(yboRecord);

                }
                else
                {
                    Console.WriteLine("here exit driver not null...");
                    existingDriver.DriverName = yboRecord.DriverName;
                    existingDriver.DriverLicense = yboRecord.DriverLicense;
                    //existingDriver.VehicleNumber = yboRecord.VehicleNumber;
                    _driverService.EditDriver(existingDriver);
                    yboRecord.Driver = existingDriver;
                    _logger.LogInformation(">>>>>>>>>> Edit success YboRecord with existing driver.<<<<<<<<<<");
                    return Update(yboRecord);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when creating new driver or edit old driver and create YboRecord. <<<<<<<<<<" + e);
                throw;
            }
        }

        public bool DeleteYboRecord(YboRecord yboRecord)
        {
            _logger.LogInformation(">>>>>>>>>> [YboServiceImpl][DeleteYboRecord] Soft delete YboRecord <<<<<<<<<<");
            try
            {
                yboRecord.IsDeleted = true;
                _logger.LogInformation(">>>>>>>>>> Success soft delete YboRecord. <<<<<<<<<<");
                return Update(yboRecord);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when soft deleting YboRecord. <<<<<<<<<<" + e);
                throw;
            }
        }

        public YboRecord FindYboRecordById(int id)
        {
            _logger.LogInformation(">>>>>>>>>> [YboServiceImpl][FindYboRecordById] Find YboRecord by pkId <<<<<<<<<<");
            try
            {
                _logger.LogInformation(">>>>>>>>>> Success find YboRecord by pkId.<<<<<<<<<<");
                return FindById(id);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when finding YboRecord by pkId. <<<<<<<<<<" + e);
                throw;
            }
        }

        public YboRecord FindYboRecordByIdEgerLoad(int id)
        {
            _logger.LogInformation(">>>>>>>>>> [YboServiceImpl][FindYboRecordByIdEgerLoad] Find YboRecord by pkId with eger load <<<<<<<<<<");
            try
            {
                _logger.LogInformation(">>>>>>>>>> Success find YboRecord by pkId with eger load.<<<<<<<<<<");
                return _context.YboRecords.Where(record => record.IsDeleted == false)
                           .Include(yboRecord => yboRecord.YBSCompany)
                           .Include(yboRecord => yboRecord.YBSType)
                           .Include(yboRecord => yboRecord.Driver)
                           .Include(yboRecord => yboRecord.Driver.VehicleData)
                           .FirstOrDefault(yboRecord => yboRecord.YboRecordPkid == id);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when finding YboRecord by pkId with eger load. <<<<<<<<<<" + e);
                throw;
            }
        }

        public DataTable MakeYboRecordExcelData(PagingList<YboRecord> yboRecords, bool exportAll)
        {
            _logger.LogInformation(">>>>>>>>>> [FindYboRecordByIdEgerLoad][MakeYboRecordExcelData] Assign SearchAll or GetAll YboRecord list to dataTable. <<<<<<<<<<");
            DataTable dt = new DataTable("စည်းကမ်းထိန်းသိမ်းရေးနှင့်စစ်ဆေးရေးငှာနစာရင်း");
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
            var list = new List<YboRecord>();
            if (exportAll)
            {
                _logger.LogInformation(">>>>>>>>>> For export all datas. <<<<<<<<<<");
                _logger.LogInformation(">>>>>>>>>> Get all YboRecord eger load list. <<<<<<<<<<");
                try
                {
                    _logger.LogInformation(">>>>>>>>>> ှSuccess. Get all YboRecord eger load list. <<<<<<<<<<");
                    list = GetAllYboRecordsEgerLoad();
                }
                catch (Exception e)
                {
                    _logger.LogError(">>>>>>>>>> Error occur when getting all YboRecord eger load list. <<<<<<<<<<" + e);
                    throw;
                }
            }
            else
            {
                _logger.LogInformation(">>>>>>>>>> For export paginate or searchResult YboRecord list. <<<<<<<<<<");
                _logger.LogInformation(">>>>>>>>>> Get all paginate or searchResult YboRecord eger load list. <<<<<<<<<<");
                try
                {
                    _logger.LogInformation(">>>>>>>>>> Success. Get all paginate or searchResult YboRecord eger load list. <<<<<<<<<<");
                    list = yboRecords;
                }
                catch (Exception e)
                {
                    _logger.LogError(">>>>>>>>>> Error occur when getting all paginate or searchResult YboRecord eger load list. <<<<<<<<<<" + e);
                    throw;
                }
            }
            try
            {
                _logger.LogInformation(">>>>>>>>>> Assign list to dataTable. <<<<<<<<<<");
                if (list.Count() > 0)
                {
                    foreach (var yboRecord in list)
                    {
                        dt.Rows.Add(yboRecord.RecordDate, yboRecord.TotalRecord, yboRecord.Phone, yboRecord.YbsRecordSender, yboRecord.RecordDescription, yboRecord.CompletionStatus, yboRecord.ChallanNumber, yboRecord.CompletedDate, yboRecord.Driver.DriverName, yboRecord.Driver.VehicleData.VehicleNumber, yboRecord.Driver.DriverLicense, yboRecord.YBSCompany.YBSCompanyName, yboRecord.YBSType.YBSTypeName);
                    }
                }
                _logger.LogInformation(">>>>>>>>>> Assign list to dataTable success. <<<<<<<<<<");
                return dt;
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when assigning SearchAll or GetAll YboRecord list to dataTable. <<<<<<<<<<" + e);
                throw;
            }
        }
    }
}
