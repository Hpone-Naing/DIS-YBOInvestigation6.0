using YBOInvestigation.Data;
using Microsoft.EntityFrameworkCore;
using System.Data;
using YBOInvestigation.Models;

namespace YBOInvestigation.Services.Impl
{
    public class SpecialEventInvestigationDeptServiceImpl : AbstractServiceImpl<SpecialEventInvestigationDept>, SpecialEventInvestigationDeptService
    {
        private readonly ILogger<SpecialEventInvestigationDeptServiceImpl> _logger;
        private readonly VehicleDataService _vehicleDataService;
        private readonly DriverService _driverService;

        public SpecialEventInvestigationDeptServiceImpl(YBOInvestigationDBContext context, ILogger<SpecialEventInvestigationDeptServiceImpl> logger, VehicleDataService vehicleDataService, DriverService driverService) : base(context, logger)
        {
            _logger = logger;
            _vehicleDataService = vehicleDataService;
            _driverService = driverService;
        }

        public List<SpecialEventInvestigationDept> GetAllSpecialEventInvestigationDepts()
        {
            _logger.LogInformation(">>>>>>>>>> [SpecialEventInvestigationDeptServiceImpl][GetAllSpecialEventInvestigationDepts] Get SpecialEventInvestigationDept list. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Get SpecialEventInvestigationDept list. <<<<<<<<<<");
                return GetAll()
                            .OrderByDescending(specialEventInvestigationDept => specialEventInvestigationDept.CreatedDate)
                            .Where(specialEventInvestigationDept => specialEventInvestigationDept.IsDeleted == false).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when retrieving SpecialEventInvestigationDept list. <<<<<<<<<<" + e);
                throw;
            }
        }

        public List<SpecialEventInvestigationDept> GetAllSpecialEventInvestigationDeptsEgerLoad()
        {
            _logger.LogInformation(">>>>>>>>>> [SpecialEventInvestigationDeptServiceImpl][GetAllSpecialEventInvestigationDeptsEgerLoad] Get SpecialEventInvestigationDept eger load list. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Get SpecialEventInvestigationDept eger load list. <<<<<<<<<<");
                return _context.SpecialEventInvestigationDepts
                        .Where(specialEventInvestigationDept => specialEventInvestigationDept.IsDeleted == false)
                                .Include(specialEventInvestigationDept => specialEventInvestigationDept.YBSCompany)
                                .Include(specialEventInvestigationDept => specialEventInvestigationDept.YBSType)
                                .Include(specialEventInvestigationDept => specialEventInvestigationDept.Driver)
                                .Include(specialEventInvestigationDept => specialEventInvestigationDept.Driver.VehicleData)
                                 .OrderByDescending(specialEventInvestigationDept => specialEventInvestigationDept.CreatedDate)
                                .ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when retrieving SpecialEventInvestigationDept eger load list. <<<<<<<<<<" + e);
                throw;
            }
        }

        public PagingList<SpecialEventInvestigationDept> GetAllSpecialEventInvestigationDeptsWithPagin(string searchString, int? pageNo, int PageSize)
        {
            _logger.LogInformation(">>>>>>>>>> [SpecialEventInvestigationDeptServiceImpl][GetAllSpecialEventInvestigationDeptsWithPagin] SearchAll or GetAll SpecialEventInvestigationDept paginate eger load list. <<<<<<<<<<");
            try
            {
                List<SpecialEventInvestigationDept> resultList = new List<SpecialEventInvestigationDept>();
                if (searchString != null && !String.IsNullOrEmpty(searchString))
                {

                    _logger.LogInformation($">>>>>>>>>> Get searchAll result SpecialEventInvestigationDept paginate eger load list. <<<<<<<<<<");
                    try
                    {
                        _logger.LogInformation($">>>>>>>>>> Success. Get searchAll result SpecialEventInvestigationDept paginate eger load list. <<<<<<<<<<");
                        resultList = _context.SpecialEventInvestigationDepts
                            .Where(specialEventInvestigationDept => specialEventInvestigationDept.IsDeleted == false)
                                    .Include(specialEventInvestigationDept => specialEventInvestigationDept.YBSCompany)
                                    .Include(specialEventInvestigationDept => specialEventInvestigationDept.YBSType)
                                    .Include(specialEventInvestigationDept => specialEventInvestigationDept.Driver)
                                    .Include(specialEventInvestigationDept => specialEventInvestigationDept.Driver.VehicleData)
                                    .AsEnumerable()
                                    .Where(specialEventInvestigationDept => IsSearchDataContained(specialEventInvestigationDept, searchString)
                                    || IsSearchDataContained(specialEventInvestigationDept.YBSCompany, searchString)
                                    || IsSearchDataContained(specialEventInvestigationDept.YBSType, searchString)
                                    || IsSearchDataContained(specialEventInvestigationDept.YBSType, searchString)
                                    || IsSearchDataContained(specialEventInvestigationDept.Driver, searchString)
                                    || IsSearchDataContained(specialEventInvestigationDept.Driver.VehicleData, searchString)
                                    )
                                    .OrderByDescending(specialEventInvestigationDept => specialEventInvestigationDept.CreatedDate)
                                    .ToList();
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(">>>>>>>>>> Error occur. Get searchAll result SpecialEventInvestigationDept paginate eger load list. <<<<<<<<<<" + e);
                        throw;
                    }
                }
                else
                {
                    _logger.LogInformation($">>>>>>>>>> GetAll SpecialEventInvestigationDept paginate eger load list. <<<<<<<<<<");
                    try
                    {
                        _logger.LogInformation($">>>>>>>>>> Success. GetAll SpecialEventInvestigationDept paginate eger load list. <<<<<<<<<<");
                        resultList = _context.SpecialEventInvestigationDepts
                            .Where(specialEventInvestigationDept => specialEventInvestigationDept.IsDeleted == false)
                                    .Include(specialEventInvestigationDept => specialEventInvestigationDept.YBSCompany)
                                    .Include(specialEventInvestigationDept => specialEventInvestigationDept.YBSType)
                                    .Include(specialEventInvestigationDept => specialEventInvestigationDept.Driver)
                                    .Include(specialEventInvestigationDept => specialEventInvestigationDept.Driver.VehicleData)
                                    .OrderByDescending(specialEventInvestigationDept => specialEventInvestigationDept.CreatedDate)
                                    .ToList();
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(">>>>>>>>>> Error occur. GetAll SpecialEventInvestigationDept paginate eger load list. <<<<<<<<<<" + e);
                        throw;
                    }
                }
                _logger.LogInformation($">>>>>>>>>> Success. SearchAll or GetAll SpecialEventInvestigationDept paginate eger load list. <<<<<<<<<<");
                return GetAllWithPagin(resultList, pageNo, PageSize);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur. SearchAll or GetAll SpecialEventInvestigationDept paginate eger load list. <<<<<<<<<<" + e);
                throw;
            }
        }


        public bool CreateSpecialEventInvestigationDept(SpecialEventInvestigationDept specialEventInvestigationDept)
        {
            _logger.LogInformation(">>>>>>>>>> [SpecialEventInvestigationDeptServiceImpl][CreateSpecialEventInvestigationDept] Create SpecialEventInvestigationDept <<<<<<<<<<");
            try
            {
                specialEventInvestigationDept.IsDeleted = false;
                specialEventInvestigationDept.CreatedDate = DateTime.Now;
                specialEventInvestigationDept.CreatedBy = "admin";
                _logger.LogInformation(">>>>>>>>>> Create success SpecialEventInvestigationDept. <<<<<<<<<<");
                try
                {
                    if (!string.IsNullOrEmpty(specialEventInvestigationDept.DriverName) && int.TryParse(specialEventInvestigationDept.DriverName, out int oldDriverPkId))
                    {
                        _logger.LogInformation(">>>>>>>>>> Success parse integer driverPkId. <<<<<<<<<<");
                        try
                        {
                            _logger.LogInformation(">>>>>>>>>> Find driver by driverPkId and assign old drivername. <<<<<<<<<<");
                            Driver oldDriver = _driverService.FindDriverById(oldDriverPkId);
                            if (oldDriver != null)
                            {
                                specialEventInvestigationDept.DriverName = oldDriver.DriverName;
                            }
                            else
                            {
                                specialEventInvestigationDept.DriverName = "စီစစ်ဆဲ";
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
                VehicleData vehicleData = _vehicleDataService.FindVehicleByVehicleNumber(specialEventInvestigationDept.VehicleNumber);
                Driver existingDriver = null;
                _logger.LogInformation(">>>>>>>>>> Get driver by driverLicense. <<<<<<<<<<");
                try
                {
                    _logger.LogInformation(">>>>>>>>>> Success Get driver by driverLicense. <<<<<<<<<<");
                    
                        existingDriver = _driverService.FindDriverByIdNumberAndLicenseAndVehicle(specialEventInvestigationDept.IDNumber, specialEventInvestigationDept.DriverLicense, vehicleData.VehicleDataPkid);
                    
                    
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
                            DriverName = specialEventInvestigationDept.DriverName,
                        };
                        if (specialEventInvestigationDept.IsDefaultIdNumber())
                        {
                            driver.IDNumber = "စီစစ်ဆဲ";
                        }
                        else
                        {
                            driver.IDNumber = specialEventInvestigationDept.IDNumber;
                        }
                        if (specialEventInvestigationDept.IsDefaultLinenseNumber())
                        {
                            driver.DriverLicense = "စီစစ်ဆဲ";
                        }
                        else
                        {
                            driver.DriverLicense = specialEventInvestigationDept.DriverLicense;
                        }
                        driver.VehicleData = vehicleData;
                        //_driverService.CreateDriver(driver);
                        specialEventInvestigationDept.Driver = driver;
                        _logger.LogInformation(">>>>>>>>>> Create success YBOInvestigationDept with new driver. <<<<<<<<<<");
                        return Create(specialEventInvestigationDept);
                    }
                    else
                    {
                        specialEventInvestigationDept.Driver = existingDriver;
                        _logger.LogInformation(">>>>>>>>>> Create success YBOInvestigationDept with existing driver.<<<<<<<<<<");
                        return Create(specialEventInvestigationDept);
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(">>>>>>>>>> Error occur when creating new driver or edit old driver and create YBOInvestigationDept. <<<<<<<<<<" + e);
                    throw;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when creating SpecialEventInvestigationDept. <<<<<<<<<<" + e);
                throw;
            }
        }

        public bool EditSpecialEventInvestigationDept(SpecialEventInvestigationDept specialEventInvestigationDept)
        {
            _logger.LogInformation(">>>>>>>>>> [SpecialEventInvestigationDeptServiceImpl][EditSpecialEventInvestigationDept] Edit SpecialEventInvestigationDept <<<<<<<<<<");
            specialEventInvestigationDept.IsDeleted = false;
            specialEventInvestigationDept.CreatedDate = DateTime.Now;
            specialEventInvestigationDept.CreatedBy = "admin";
            _logger.LogInformation(">>>>>>>>>> Parse integer driverPkId. <<<<<<<<<<");

            if (int.TryParse(specialEventInvestigationDept.DriverName, out int oldDriverPkId))
            {
                Driver oldDriver = _driverService.FindDriverById(oldDriverPkId);
                _logger.LogInformation(">>>>>>>>>> Success find driver by driverPkId and assign old drivername. <<<<<<<<<<");
                specialEventInvestigationDept.Driver = oldDriver;
                Update(specialEventInvestigationDept);
                return true;
            }
            VehicleData vehicleData = _vehicleDataService.FindVehicleByVehicleNumber(specialEventInvestigationDept.VehicleNumber);
            Driver existingDriver = _driverService.FindDriverByIdNumberAndLicenseAndVehicle(specialEventInvestigationDept.IDNumber, specialEventInvestigationDept.DriverLicense, vehicleData.VehicleDataPkid);
            if (existingDriver != null)
            {
                Console.WriteLine("existing driver not null.................." + existingDriver.DriverPkid);
                existingDriver.DriverName = specialEventInvestigationDept.DriverName;
                _driverService.EditDriver(existingDriver);
                Console.WriteLine("before insert driver.............." + specialEventInvestigationDept.Driver.DriverPkid);

                specialEventInvestigationDept.Driver = existingDriver;
                Console.WriteLine("After insert driver.............." + specialEventInvestigationDept.Driver.DriverPkid);
                return Update(specialEventInvestigationDept);
            }
            Driver currentDriver = _driverService.FindDriverById(specialEventInvestigationDept.Driver.DriverPkid);
            currentDriver.IDNumber = specialEventInvestigationDept.IDNumber;
            currentDriver.DriverLicense = specialEventInvestigationDept.DriverLicense;
            currentDriver.DriverName = specialEventInvestigationDept.DriverName;
            _driverService.EditDriver(currentDriver);
            specialEventInvestigationDept.Driver = currentDriver;
            _logger.LogInformation(">>>>>>>>>> Edit success specialEventInvestigationDept with existing driver.<<<<<<<<<<");
            return Update(specialEventInvestigationDept);

        }

        public bool DeleteSpecialEventInvestigationDept(SpecialEventInvestigationDept specialEventInvestigationDept)
        {
            _logger.LogInformation(">>>>>>>>>> [SpecialEventInvestigationDeptServiceImpl][DeleteSpecialEventInvestigationDept] Soft delete SpecialEventInvestigationDept <<<<<<<<<<");
            try
            {
                specialEventInvestigationDept.IsDeleted = true;
                _logger.LogInformation(">>>>>>>>>> Success soft delete SpecialEventInvestigationDept. <<<<<<<<<<");
                return Update(specialEventInvestigationDept);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when soft deleting SpecialEventInvestigationDept. <<<<<<<<<<" + e);
                throw;
            }
        }

        public SpecialEventInvestigationDept FindSpecialEventInvestigationDeptById(int id)
        {
            _logger.LogInformation(">>>>>>>>>> [SpecialEventInvestigationDeptServiceImpl][FindSpecialEventInvestigationDeptById] Find SpecialEventInvestigationDept by pkId. <<<<<<<<<<");
            try
            {
                _logger.LogInformation(">>>>>>>>>> Success. Find SpecialEventInvestigationDept by pkId. <<<<<<<<<<");
                return FindById(id);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when finding SpecialEventInvestigationDept by pkId. <<<<<<<<<<" + e);
                throw;
            }
        }

        public SpecialEventInvestigationDept FindSpecialEventInvestigationDeptByIdEgerLoad(int id)
        {
            _logger.LogInformation(">>>>>>>>>> [SpecialEventInvestigationDeptServiceImpl][FindSpecialEventInvestigationDeptById] Find SpecialEventInvestigationDept by pkId with eger load. <<<<<<<<<<");
            try
            {
                _logger.LogInformation(">>>>>>>>>> Success. Find SpecialEventInvestigationDept by pkId with eger load. <<<<<<<<<<");
                return _context.SpecialEventInvestigationDepts.Where(record => record.IsDeleted == false)
                               .Include(specialEventInvestigationDept => specialEventInvestigationDept.YBSCompany)
                               .Include(specialEventInvestigationDept => specialEventInvestigationDept.YBSType)
                               .Include(specialEventInvestigationDept => specialEventInvestigationDept.Driver)
                               .Include(specialEventInvestigationDept => specialEventInvestigationDept.Driver.VehicleData)
                               .FirstOrDefault(specialEventInvestigationDept => specialEventInvestigationDept.SpecialEventInvestigationDeptDeptPkid == id);
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when finding SpecialEventInvestigationDept by pkId with eger load. <<<<<<<<<<" + e);
                throw;
            }
        }

        public DataTable MakeSpecialEventInvestigationDeptExcelData(PagingList<SpecialEventInvestigationDept> specialEventInvestigationDepts, bool exportAll)
        {
            _logger.LogInformation(">>>>>>>>>> [SpecialEventInvestigationDeptServiceImpl][MakeSpecialEventInvestigationDeptExcelData] Expor excel SpecialEventInvestigationDept. <<<<<<<<<<");
            DataTable dt = new DataTable("ထူးခြားဖြစ်စဥ် စာရင်း");
            dt.Columns.AddRange(new DataColumn[10] {
                                        new DataColumn("ဖြစ်ပွားရက်စွဲ"),
                                        new DataColumn("ဖြစ်ပွားသည့်အချိန်"),
                                        new DataColumn("ဖြစ်ပွားသည့်နေရာ"),
                                        new DataColumn("ယာဥ်အမှတ်"),
                                        new DataColumn("သတင်းပေးပို့သည့်အချိန်"),
                                        new DataColumn("သတင်းပေးပို့သူအမည်"),
                                        new DataColumn("သတင်းပေးပို့သူရာထူး"),
                                        new DataColumn("ဖြစ်စဥ်အကျဥ်း"),
                                        new DataColumn("YBS Company Name"),
                                        new DataColumn("ယာဥ်လိုင်း"),
                                        });
            var list = new List<SpecialEventInvestigationDept>();
            if (exportAll)
            {
                _logger.LogInformation(">>>>>>>>>> For export all datas. <<<<<<<<<<");
                _logger.LogInformation(">>>>>>>>>> Get all SpecialEventInvestigationDept eger load list. <<<<<<<<<<");
                try
                {
                    _logger.LogInformation(">>>>>>>>>> ှSuccess. Get all SpecialEventInvestigationDept eger load list. <<<<<<<<<<");
                    list = GetAllSpecialEventInvestigationDeptsEgerLoad();
                }
                catch (Exception e)
                {
                    _logger.LogError(">>>>>>>>>> Error occur when getting all SpecialEventInvestigationDept eger load list. <<<<<<<<<<" + e);
                    throw;
                }
            }
            else
            {
                _logger.LogInformation(">>>>>>>>>> For export paginate or searchResult SpecialEventInvestigationDept list. <<<<<<<<<<");
                _logger.LogInformation(">>>>>>>>>> Get all paginate or searchResult SpecialEventInvestigationDept eger load list. <<<<<<<<<<");
                try
                {
                    _logger.LogInformation(">>>>>>>>>> Success. Get all paginate or searchResult SpecialEventInvestigationDept eger load list. <<<<<<<<<<");
                    list = specialEventInvestigationDepts;
                }
                catch (Exception e)
                {
                    _logger.LogError(">>>>>>>>>> Error occur when getting all paginate or searchResult SpecialEventInvestigationDept eger load list. <<<<<<<<<<" + e);
                    throw;
                }
            }
            try
            {
                _logger.LogInformation(">>>>>>>>>> Assign list to dataTable. <<<<<<<<<<");
                if (list.Count() > 0)
                {
                    foreach (var specialEventInvestigationDept in list)
                    {
                        dt.Rows.Add(specialEventInvestigationDept.IncidenceDate, specialEventInvestigationDept.IncidenceTime, specialEventInvestigationDept.IncidencePlace, specialEventInvestigationDept.VehicleNumber, specialEventInvestigationDept.ReportTime, specialEventInvestigationDept.YbsRecordSender, specialEventInvestigationDept.YbsRecordSenderPosition, specialEventInvestigationDept.RecordDescription, specialEventInvestigationDept.YBSCompany.YBSCompanyName, specialEventInvestigationDept.YBSType.YBSTypeName);
                    }
                }
                _logger.LogInformation(">>>>>>>>>> Assign list to dataTable success. <<<<<<<<<<");
                return dt;
            }
            catch (Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when assigning SearchAll or GetAll CallCenterInvestigationDept list to dataTable. <<<<<<<<<<" + e);
                throw;
            }
        }
    }
}
