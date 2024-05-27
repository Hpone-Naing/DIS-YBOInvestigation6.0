using YBOInvestigation.Data;
using Microsoft.EntityFrameworkCore;
using System.Data;
using YBOInvestigation.Models;

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
                return GetAll()
                    .OrderByDescending(yboInvestigationDept => yboInvestigationDept.CreatedDate)
                    .Where(yBOInvestigationDept => yBOInvestigationDept.IsDeleted == false).ToList();
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
                            .Include(yBOInvestigationDept => yBOInvestigationDept.PunishmentType)
                            .Include(yBOInvestigationDept => yBOInvestigationDept.Driver)
                            .Include(yBOInvestigationDept => yBOInvestigationDept.Driver.VehicleData)
                            .OrderByDescending(yboInvestigationDept => yboInvestigationDept.CreatedDate)
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
                            .Include(yBOInvestigationDept => yBOInvestigationDept.PunishmentType)
                            .Include(yBOInvestigationDept => yBOInvestigationDept.Driver)
                            .Include(yBOInvestigationDept => yBOInvestigationDept.Driver.VehicleData)
                            .AsEnumerable()
                            .Where(yBOInvestigationDept => IsSearchDataContained(yBOInvestigationDept, searchString)
                            || IsSearchDataContained(yBOInvestigationDept.YBSCompany, searchString)
                            || IsSearchDataContained(yBOInvestigationDept.YBSType, searchString)
                            || IsSearchDataContained(yBOInvestigationDept.Driver, searchString)
                            || IsSearchDataContained(yBOInvestigationDept.PunishmentType, searchString)
                            )
                            .OrderByDescending(yboInvestigationDept => yboInvestigationDept.CreatedDate)
                            .ToList();
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
                            .Include(yBOInvestigationDept => yBOInvestigationDept.PunishmentType)
                            .Include(yBOInvestigationDept => yBOInvestigationDept.Driver.VehicleData)
                            .OrderByDescending(yboInvestigationDept => yboInvestigationDept.CreatedDate)
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
                        else
                        {
                            yBOInvestigationDept.DriverName = "စီစစ်ဆဲ";
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
            VehicleData vehicleData = _vehicleDataService.FindVehicleByVehicleNumber(yBOInvestigationDept.VehicleNumber);
            Driver existingDriver = null;
            _logger.LogInformation(">>>>>>>>>> Get driver by driverLicense. <<<<<<<<<<");
            try
            {
                _logger.LogInformation(">>>>>>>>>> Success Get driver by driverLicense. <<<<<<<<<<");
               
                    existingDriver = _driverService.FindDriverByIdNumberAndLicenseAndVehicle(yBOInvestigationDept.IDNumber, yBOInvestigationDept.DriverLicense, vehicleData.VehicleDataPkid);
                
                
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
                    Driver driver = new Driver
                    {
                        DriverName = yBOInvestigationDept.DriverName,
                    };
                    if (yBOInvestigationDept.IsDefaultIdNumber())
                    {
                        driver.IDNumber = "စီစစ်ဆဲ";
                    }
                    else
                    {
                        driver.IDNumber = yBOInvestigationDept.IDNumber;
                    }
                    if (yBOInvestigationDept.IsDefaultLinenseNumber())
                    {
                        driver.DriverLicense = "စီစစ်ဆဲ";
                    }
                    else
                    {
                        driver.DriverLicense = yBOInvestigationDept.DriverLicense;
                    }
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

            if (int.TryParse(yBOInvestigationDept.DriverName, out int oldDriverPkId))
            {
                Driver oldDriver = _driverService.FindDriverById(oldDriverPkId);
                _logger.LogInformation(">>>>>>>>>> Success find driver by driverPkId and assign old drivername. <<<<<<<<<<");
                yBOInvestigationDept.Driver = oldDriver;
                Update(yBOInvestigationDept);
                return true;
            }
            VehicleData vehicleData = _vehicleDataService.FindVehicleByVehicleNumber(yBOInvestigationDept.VehicleNumber);
            Driver existingDriver = _driverService.FindDriverByIdNumberAndLicenseAndVehicle(yBOInvestigationDept.IDNumber, yBOInvestigationDept.DriverLicense, vehicleData.VehicleDataPkid);
            if (existingDriver != null)
            {
                Console.WriteLine("existing driver not null.................." + existingDriver.DriverPkid);
                existingDriver.DriverName = yBOInvestigationDept.DriverName;
                _driverService.EditDriver(existingDriver);
                Console.WriteLine("before insert driver.............." + yBOInvestigationDept.Driver.DriverPkid);

                yBOInvestigationDept.Driver = existingDriver;
                Console.WriteLine("After insert driver.............." + yBOInvestigationDept.Driver.DriverPkid);
                return Update(yBOInvestigationDept);
            }
            Driver currentDriver = _driverService.FindDriverById(yBOInvestigationDept.Driver.DriverPkid);
            currentDriver.IDNumber = yBOInvestigationDept.IDNumber;
            currentDriver.DriverLicense = yBOInvestigationDept.DriverLicense;
            currentDriver.DriverName = yBOInvestigationDept.DriverName;
            _driverService.EditDriver(currentDriver);
            yBOInvestigationDept.Driver = currentDriver;
            _logger.LogInformation(">>>>>>>>>> Edit success specialEventInvestigationDept with existing driver.<<<<<<<<<<");
            return Update(yBOInvestigationDept);

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
                           .Include(yBOInvestigationDept => yBOInvestigationDept.PunishmentType)
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
            DataTable dt = new DataTable("YBO စာရင်း");
            dt.Columns.AddRange(new DataColumn[13] {
                                        new DataColumn("ဖမ်းဆီးရက်စွဲ"),
                                        new DataColumn("ဖမ်းဆီးသည့်အချိန်"),
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
                        dt.Rows.Add(yBOInvestigationDept.RecordDate, yBOInvestigationDept.RecordTime, yBOInvestigationDept.Phone, yBOInvestigationDept.YbsRecordSender, yBOInvestigationDept.PunishmentType.Punishment, yBOInvestigationDept.CompletionStatus, yBOInvestigationDept.CompletedDate, yBOInvestigationDept.Driver.DriverName, yBOInvestigationDept.Driver.VehicleData.VehicleNumber, yBOInvestigationDept.Driver.DriverLicense, yBOInvestigationDept.Driver.IDNumber, yBOInvestigationDept.YBSCompany.YBSCompanyName, yBOInvestigationDept.YBSType.YBSTypeName);
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

        public int GetTotalRecordByDriver(int driverPkId)
        {
            _logger.LogInformation(">>>>>>>>>> [YBOInvestigationDepts][FindCallCenterInvestigationDeptByIdEgerLoad] Find CallCenterInvestigationDept by pkId with eger load <<<<<<<<<<");
            try
            {
                _logger.LogInformation(">>>>>>>>>> Success find YBOInvestigationDepts by pkId with eger load.<<<<<<<<<<");
                int TotalRecord = _context.YBOInvestigationDepts.Count(record => record.DriverPkid == driverPkId);
                if (TotalRecord == 0)
                    return 1;
                return TotalRecord;
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when finding YBOInvestigationDepts by pkId with eger load. <<<<<<<<<<" + e);
                throw;
            }
        }
    }
}