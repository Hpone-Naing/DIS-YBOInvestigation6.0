using YBOInvestigation.Data;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace YBOInvestigation.Services.Impl
{
    public class CallCenterInvestigationDeptServiceImpl : AbstractServiceImpl<CallCenterInvestigationDept>, CallCenterInvestigationDeptService
    {
        private readonly ILogger<CallCenterInvestigationDeptServiceImpl> _logger;

        private readonly DriverService _driverService;
        private readonly VehicleDataService _vehicleDataService;
        public CallCenterInvestigationDeptServiceImpl(YBOInvestigationDBContext context, ILogger<CallCenterInvestigationDeptServiceImpl> logger, DriverService driverService, VehicleDataService vehicleDataService) : base(context, logger)
        {
            _logger = logger;
            _driverService = driverService;
            _vehicleDataService = vehicleDataService;
        }

        public List<CallCenterInvestigationDept> GetAllCallCenterInvestigationDepts()
        {
            _logger.LogInformation(">>>>>>>>>> [CallCenterInvestigationDeptServiceImpl][GetAllCallCenterInvestigationDepts] Retrieve CallCenterInvestigationDept List <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Retrieve CallCenterInvestigationDept List success. <<<<<<<<<<");
                return GetAll().Where(callCenterInvestigationDept => callCenterInvestigationDept.IsDeleted == false).ToList();
            }catch(Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when retrieveing CallCenterInvestigationDept List. <<<<<<<<<<" + e);
                throw;
            }
        }

        public List<CallCenterInvestigationDept> GetAllCallCenterInvestigationDeptsEgerLoad()
        {
            _logger.LogInformation(">>>>>>>>>> [CallCenterInvestigationDeptServiceImpl][GetAllCallCenterInvestigationDeptsEgerLoad] Retrieve CallCenterInvestigationDept eger load list <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Retrieve CallCenterInvestigationDept eger laod list success. <<<<<<<<<<");
                return _context.CallCenterInvestigationDepts
                        .Where(callCenterInvestigationDept => callCenterInvestigationDept.IsDeleted == false)
                                .Include(callCenterInvestigationDept => callCenterInvestigationDept.YBSCompany)
                                .Include(callCenterInvestigationDept => callCenterInvestigationDept.YBSType)
                                .Include(callCenterInvestigationDept => callCenterInvestigationDept.Driver)
                                .Include(callCenterInvestigationDept => callCenterInvestigationDept.Driver.VehicleData)
                                .ToList();
            }catch(Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when retrieveing CallCenterInvestigationDept eger load list. <<<<<<<<<<" + e);
                throw;
            }
        }

        public PagingList<CallCenterInvestigationDept> GetAllCallCenterInvestigationDeptsWithPagin(string searchString, int? pageNo, int PageSize)
        {
            _logger.LogInformation(">>>>>>>>>> [CallCenterInvestigationDeptServiceImpl][GetAllCallCenterInvestigationDeptsWithPagin] SearchAll or GetAll CallCenterInvestigationDept list and make pagination <<<<<<<<<<");
            try
            {
                List<CallCenterInvestigationDept> resultList = new List<CallCenterInvestigationDept>();
                if (searchString != null && !String.IsNullOrEmpty(searchString))
                {
                    _logger.LogInformation($">>>>>>>>>> Get searchAll result CallCenterInvestigationDept paginate eger load list. <<<<<<<<<<");
                    try
                    {
                        _logger.LogInformation($">>>>>>>>>> Success. Get searchAll result CallCenterInvestigationDept paginate eger load list. <<<<<<<<<<");
                        resultList = _context.CallCenterInvestigationDepts
                            .Where(callCenterInvestigationDept => callCenterInvestigationDept.IsDeleted == false)
                                    .Include(callCenterInvestigationDept => callCenterInvestigationDept.YBSCompany)
                                    .Include(callCenterInvestigationDept => callCenterInvestigationDept.YBSType)
                                    .Include(callCenterInvestigationDept => callCenterInvestigationDept.Driver)
                                    .Include(callCenterInvestigationDept => callCenterInvestigationDept.Driver.VehicleData)
                                    .ToList()
                                    .Where(callCenterInvestigationDept => IsSearchDataContained(callCenterInvestigationDept, searchString)
                                    || IsSearchDataContained(callCenterInvestigationDept.YBSCompany, searchString)
                                    || IsSearchDataContained(callCenterInvestigationDept.YBSType, searchString)
                                    || IsSearchDataContained(callCenterInvestigationDept.Driver, searchString)
                                    ).AsQueryable().ToList();
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(">>>>>>>>>> Error occur. Get searchAll result CallCenterInvestigationDept paginate eger load list. <<<<<<<<<<" + e);
                        throw;
                    }
                }
                else
                {
                    _logger.LogInformation($">>>>>>>>>> GetAll CallCenterInvestigationDept paginate eger load list. <<<<<<<<<<");
                    try
                    {
                        _logger.LogInformation($">>>>>>>>>> Success. GetAll CallCenterInvestigationDept paginate eger load list. <<<<<<<<<<");
                        resultList = _context.CallCenterInvestigationDepts
                            .Where(callCenterInvestigationDept => callCenterInvestigationDept.IsDeleted == false)
                                    .Include(callCenterInvestigationDept => callCenterInvestigationDept.YBSCompany)
                                    .Include(callCenterInvestigationDept => callCenterInvestigationDept.YBSType)
                                    .Include(callCenterInvestigationDept => callCenterInvestigationDept.Driver)
                                    .Include(callCenterInvestigationDept => callCenterInvestigationDept.Driver.VehicleData)
                                    .ToList();
                    }
                    catch(Exception e)
                    {
                        _logger.LogError(">>>>>>>>>> Error occur. GetAll CallCenterInvestigationDept paginate eger load list. <<<<<<<<<<" + e);
                        throw;
                    }
                }
                _logger.LogInformation($">>>>>>>>>> SearchAll or GetAll CallCenterInvestigationDept list and make pagination. <<<<<<<<<<");
                return GetAllWithPagin(resultList, pageNo, PageSize);
            }catch(Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur. SearchAll or GetAll CallCenterInvestigationDept list and make pagination. <<<<<<<<<<" + e);
                throw;
            }
        }


        public bool CreateCallCenterInvestigationDept(CallCenterInvestigationDept callCenterInvestigationDept)
        {
            _logger.LogInformation(">>>>>>>>>> [CallCenterInvestigationDeptServiceImpl][CreateCallCenterInvestigationDept] Create CallCenterInvestigationDept <<<<<<<<<<");
                callCenterInvestigationDept.IsDeleted = false;
                callCenterInvestigationDept.CreatedDate = DateTime.Now;
                callCenterInvestigationDept.CreatedBy = "admin";

            _logger.LogInformation(">>>>>>>>>> Parse integer driverPkId. <<<<<<<<<<");
            try
            {
                if (!string.IsNullOrEmpty(callCenterInvestigationDept.DriverName) && int.TryParse(callCenterInvestigationDept.DriverName, out int oldDriverPkId))
                {
                    _logger.LogInformation(">>>>>>>>>> Success parse integer driverPkId. <<<<<<<<<<");
                    try
                    {
                        _logger.LogInformation(">>>>>>>>>> Find driver by driverPkId and assign old drivername. <<<<<<<<<<");
                        Driver oldDriver = _driverService.FindDriverById(oldDriverPkId);

                        if (oldDriver != null)
                        {
                            callCenterInvestigationDept.DriverName = oldDriver.DriverName;
                        }
                        _logger.LogInformation(">>>>>>>>>> Success find driver by driverPkId and assign old drivername. <<<<<<<<<<");
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(">>>>>>>>>> Error occur when finding driver by driverPkId and assign old drivername. <<<<<<<<<<" + e);
                        throw;
                    }
                }
            }catch(Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when parseing driver. <<<<<<<<<<" + e);
                throw;
            }
                Driver existingDriver = null;
                _logger.LogInformation(">>>>>>>>>> Get driver by driverLicense. <<<<<<<<<<");
                try
                {
                    _logger.LogInformation(">>>>>>>>>> Success Get driver by driverLicense. <<<<<<<<<<");
                    existingDriver = _driverService.FindDriverByLicense(callCenterInvestigationDept.DriverLicense);
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
                        VehicleData vehicleData = _vehicleDataService.FindVehicleByVehicleNumber(callCenterInvestigationDept.VehicleNumber);
                        Driver driver = new Driver
                        {
                            DriverName = callCenterInvestigationDept.DriverName,
                            DriverLicense = callCenterInvestigationDept.DriverLicense,
                        };
                        driver.VehicleData = vehicleData;
                        //_driverService.CreateDriver(driver);
                        callCenterInvestigationDept.Driver = driver;
                        _logger.LogInformation(">>>>>>>>>> Create success CallCenterInvestigationDept with new driver. <<<<<<<<<<");
                        return Create(callCenterInvestigationDept);
                    }
                    else
                    {
                        callCenterInvestigationDept.Driver = existingDriver;
                        _logger.LogInformation(">>>>>>>>>> Create success CallCenterInvestigationDept with existing driver.<<<<<<<<<<");
                        return Create(callCenterInvestigationDept);
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(">>>>>>>>>> Error occur when creating new driver or edit old driver and create CallCenterInvestigationDept. <<<<<<<<<<" + e);
                    throw;
                }
        }

        public bool EditCallCenterInvestigationDept(CallCenterInvestigationDept callCenterInvestigationDept)
        {
            _logger.LogInformation(">>>>>>>>>> [CallCenterInvestigationDeptServiceImpl][EditCallCenterInvestigationDept] Edit CallCenterInvestigationDept <<<<<<<<<<");
            callCenterInvestigationDept.IsDeleted = false;
            callCenterInvestigationDept.CreatedDate = DateTime.Now;
            callCenterInvestigationDept.CreatedBy = "admin";

            _logger.LogInformation(">>>>>>>>>> Parse integer driverPkId. <<<<<<<<<<");
            try
            {
                if (int.TryParse(callCenterInvestigationDept.DriverName, out int oldDriverPkId))
                {
                    _logger.LogInformation(">>>>>>>>>> Success parse integer driverPkId. <<<<<<<<<<");
                    try
                    {
                        _logger.LogInformation(">>>>>>>>>> Find driver by driverPkId and assign old drivername. <<<<<<<<<<");
                        Driver oldDriver = _driverService.FindDriverById(oldDriverPkId);
                        if (oldDriver != null)
                        {
                            callCenterInvestigationDept.DriverName = oldDriver.DriverName;
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
            catch(Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when parseing driver. <<<<<<<<<<" + e);
                throw;
            }
            Driver existingDriver = null;

            _logger.LogInformation(">>>>>>>>>> Get driver by driverLicense. <<<<<<<<<<");
            try
            {
                _logger.LogInformation(">>>>>>>>>> Success Get driver by driverLicense. <<<<<<<<<<");
                existingDriver = _driverService.FindDriverByLicense(callCenterInvestigationDept.DriverLicense);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when getting driver by driverLicense <<<<<<<<<<" + e);
                throw;
            }
            try
            {
                _logger.LogInformation(">>>>>>>>>> Create new driver or edit old driver and edit CallCenterInvestigationDept. <<<<<<<<<<");
                if (existingDriver == null)
                {
                    VehicleData vehicleData = _vehicleDataService.FindVehicleByVehicleNumber(callCenterInvestigationDept.VehicleNumber);
                    Driver driver = new Driver
                    {
                        DriverName = callCenterInvestigationDept.DriverName,
                        DriverLicense = callCenterInvestigationDept.DriverLicense,
                    };
                    driver.VehicleData = vehicleData;
                    _driverService.CreateDriver(driver);
                    callCenterInvestigationDept.Driver = driver;
                    _logger.LogInformation(">>>>>>>>>> Edit success CallCenterInvestigationDept with new driver. <<<<<<<<<<");
                    return Update(callCenterInvestigationDept);

                }
                else
                {
                    existingDriver.DriverName = callCenterInvestigationDept.DriverName;
                    existingDriver.DriverLicense = callCenterInvestigationDept.DriverLicense;
                    //existingDriver.VehicleNumber = callCenterInvestigationDept.VehicleNumber;
                    _driverService.EditDriver(existingDriver);
                    callCenterInvestigationDept.Driver = existingDriver;
                    _logger.LogInformation(">>>>>>>>>> Edit success CallCenterInvestigationDept with existing driver.<<<<<<<<<<");
                    return Update(callCenterInvestigationDept);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when creating new driver or edit old driver and create CallCenterInvestigationDept. <<<<<<<<<<" + e);
                throw;
            }

        }

        public bool DeleteCallCenterInvestigationDept(CallCenterInvestigationDept callCenterInvestigationDept)
        {
            _logger.LogInformation(">>>>>>>>>> [CallCenterInvestigationDeptServiceImpl][DeleteCallCenterInvestigationDept] Soft delete CallCenterInvestigationDept <<<<<<<<<<");
            try
            {
                callCenterInvestigationDept.IsDeleted = true;
                _logger.LogInformation(">>>>>>>>>> Success. Soft delete CallCenterInvestigationDept.<<<<<<<<<<");
                return Update(callCenterInvestigationDept);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when soft deleting CallCenterInvestigationDept. <<<<<<<<<<" + e);
                throw;
            }
        }

        public CallCenterInvestigationDept FindCallCenterInvestigationDeptById(int id)
        {
            _logger.LogInformation(">>>>>>>>>> [CallCenterInvestigationDeptServiceImpl][FindCallCenterInvestigationDeptById] Find CallCenterInvestigationDept by pkId <<<<<<<<<<");
            try
            {
                _logger.LogInformation(">>>>>>>>>> Success find CallCenterInvestigationDept by pkId.<<<<<<<<<<");
                return FindById(id);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when finding CallCenterInvestigationDept by pkId. <<<<<<<<<<" + e);
                throw;
            }
        }

        public CallCenterInvestigationDept FindCallCenterInvestigationDeptByIdEgerLoad(int id)
        {
            _logger.LogInformation(">>>>>>>>>> [CallCenterInvestigationDeptServiceImpl][FindCallCenterInvestigationDeptByIdEgerLoad] Find CallCenterInvestigationDept by pkId with eger load <<<<<<<<<<");
            try
            {
                _logger.LogInformation(">>>>>>>>>> Success find CallCenterInvestigationDept by pkId with eger load.<<<<<<<<<<");
                return _context.CallCenterInvestigationDepts.Where(record => record.IsDeleted == false)
                               .Include(callCenterInvestigationDept => callCenterInvestigationDept.YBSCompany)
                               .Include(callCenterInvestigationDept => callCenterInvestigationDept.YBSType)
                               .Include(callCenterInvestigationDept => callCenterInvestigationDept.Driver)
                               .Include(callCenterInvestigationDept => callCenterInvestigationDept.Driver.VehicleData)
                               .FirstOrDefault(callCenterInvestigationDept => callCenterInvestigationDept.CallCenterInvestigationDeptPkid == id);
            }
            catch(Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when finding CallCenterInvestigationDept by pkId with eger load. <<<<<<<<<<" + e);
                throw;
            }
        }

        public DataTable MakeCallCenterInvestigationDeptExcelData(PagingList<CallCenterInvestigationDept> callCenterInvestigationDepts, bool exportAll)
        {
            _logger.LogInformation(">>>>>>>>>> [CallCenterInvestigationDeptServiceImpl][MakeCallCenterInvestigationDeptExcelData] Assign SearchAll or GetAll CallCenterInvestigationDept list to dataTable. <<<<<<<<<<");
            DataTable dt = new DataTable("Call Center တိုင်ကြားမှုစာရင်း");
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
            var list = new List<CallCenterInvestigationDept>();
            if (exportAll)
            {
                _logger.LogInformation(">>>>>>>>>> For export all datas. <<<<<<<<<<");
                _logger.LogInformation(">>>>>>>>>> Get all CallCenterInvestigationDept eger load list. <<<<<<<<<<");
                try
                {
                    _logger.LogInformation(">>>>>>>>>> ှSuccess. Get all CallCenterInvestigationDept eger load list. <<<<<<<<<<");
                    list = GetAllCallCenterInvestigationDeptsEgerLoad();
                }catch(Exception e)
                {
                    _logger.LogError(">>>>>>>>>> Error occur when getting all CallCenterInvestigationDept eger load list. <<<<<<<<<<" + e);
                    throw;
                }
            }
            else
            {
                _logger.LogInformation(">>>>>>>>>> For export paginate or searchResult CallCenterInvestigationDept list. <<<<<<<<<<");
                _logger.LogInformation(">>>>>>>>>> Get all paginate or searchResult CallCenterInvestigationDept eger load list. <<<<<<<<<<");
                try
                {
                    _logger.LogInformation(">>>>>>>>>> Success. Get all paginate or searchResult CallCenterInvestigationDept eger load list. <<<<<<<<<<");
                    list = callCenterInvestigationDepts;
                }catch(Exception e)
                {
                    _logger.LogError(">>>>>>>>>> Error occur when getting all paginate or searchResult CallCenterInvestigationDept eger load list. <<<<<<<<<<" + e);
                    throw;
                }
            }
            try
            {
                _logger.LogInformation(">>>>>>>>>> Assign list to dataTable. <<<<<<<<<<");
                if (list.Count() > 0)
                {
                    foreach (var callCenterInvestigationDept in list)
                    {
                        dt.Rows.Add(callCenterInvestigationDept.RecordDate, callCenterInvestigationDept.RecordTime, callCenterInvestigationDept.TotalRecord, callCenterInvestigationDept.Phone, callCenterInvestigationDept.YbsRecordSender, callCenterInvestigationDept.RecordDescription, callCenterInvestigationDept.CompletionStatus, callCenterInvestigationDept.CompletedDate, callCenterInvestigationDept.Driver.DriverName, callCenterInvestigationDept.Driver.VehicleData.VehicleNumber, callCenterInvestigationDept.Driver.DriverLicense, callCenterInvestigationDept.YBSCompany.YBSCompanyName, callCenterInvestigationDept.YBSType.YBSTypeName);
                    }
                }
                _logger.LogInformation(">>>>>>>>>> Assign list to dataTable success. <<<<<<<<<<");
                return dt;
            }
            catch(Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when assigning SearchAll or GetAll CallCenterInvestigationDept list to dataTable. <<<<<<<<<<" + e);
                throw;
            }
        }
    }
}