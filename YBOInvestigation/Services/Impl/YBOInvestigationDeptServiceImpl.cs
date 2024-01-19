using YBOInvestigation.Data;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace YBOInvestigation.Services.Impl
{
    public class YBOInvestigationDeptServiceImpl : AbstractServiceImpl<YBOInvestigationDept>, YBOInvestigationDeptService
    {
        private readonly DriverService _driverService;
        private readonly VehicleDataService _vehicleDataService;
        public YBOInvestigationDeptServiceImpl(YBOInvestigationDBContext context, DriverService driverService, VehicleDataService vehicleDataService) : base(context)
        {
            _driverService = driverService;
            _vehicleDataService = vehicleDataService;
        }

        public List<YBOInvestigationDept> GetAllYBOInvestigationDepts()
        {
            return GetAll().Where(yBOInvestigationDept => yBOInvestigationDept.IsDeleted == false).ToList();
        }

        public List<YBOInvestigationDept> GetAllYBOInvestigationDeptsEgerLoad()
        {
            return _context.YBOInvestigationDepts
                    .Where(yBOInvestigationDept => yBOInvestigationDept.IsDeleted == false)
                            .Include(yBOInvestigationDept => yBOInvestigationDept.YBSCompany)
                            .Include(yBOInvestigationDept => yBOInvestigationDept.YBSType)
                            .Include(yBOInvestigationDept => yBOInvestigationDept.Driver)
                            .Include(yBOInvestigationDept => yBOInvestigationDept.Driver.VehicleData)
                            .ToList();
        }

        public PagingList<YBOInvestigationDept> GetAllYBOInvestigationDeptsWithPagin(string searchString, int? pageNo, int PageSize)
        {
            List<YBOInvestigationDept> yBOInvestigationDepts = GetAllYBOInvestigationDepts();
            List<YBOInvestigationDept> resultList = new List<YBOInvestigationDept>();
            if (searchString != null && !String.IsNullOrEmpty(searchString))
            {
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
            else
            {
                resultList = _context.YBOInvestigationDepts
                    .Where(yBOInvestigationDept => yBOInvestigationDept.IsDeleted == false)
                            .Include(yBOInvestigationDept => yBOInvestigationDept.YBSCompany)
                            .Include(yBOInvestigationDept => yBOInvestigationDept.YBSType)
                            .Include(yBOInvestigationDept => yBOInvestigationDept.Driver)
                            .Include(yBOInvestigationDept => yBOInvestigationDept.Driver.VehicleData)
                            .ToList();
            }
            return GetAllWithPagin(resultList, pageNo, PageSize);
        }


        public bool CreateYBOInvestigationDept(YBOInvestigationDept yBOInvestigationDept)
        {
            yBOInvestigationDept.IsDeleted = false;
            yBOInvestigationDept.CreatedDate = DateTime.Now;
            yBOInvestigationDept.CreatedBy = "admin";
            if (!string.IsNullOrEmpty(yBOInvestigationDept.DriverName) && int.TryParse(yBOInvestigationDept.DriverName, out int oldDriverPkId))
            {
                Driver oldDriver = _driverService.FindDriverById(oldDriverPkId);
                if(oldDriver != null)
                {
                    yBOInvestigationDept.DriverName = oldDriver.DriverName;
                }
            }
            Driver existingDriver = _driverService.FindDriverByLicense(yBOInvestigationDept.DriverLicense);
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
                return Create(yBOInvestigationDept);
            }
            else
            {
                yBOInvestigationDept.Driver = existingDriver;
                return Create(yBOInvestigationDept);
            }

        }

        public bool EditYBOInvestigationDept(YBOInvestigationDept yBOInvestigationDept)
        {
            yBOInvestigationDept.IsDeleted = false;
            yBOInvestigationDept.CreatedDate = DateTime.Now;
            yBOInvestigationDept.CreatedBy = "admin";
            if (int.TryParse(yBOInvestigationDept.DriverName, out int oldDriverPkId))
            {
                Driver oldDriver = _driverService.FindDriverById(oldDriverPkId);
                if(oldDriver != null)
                {
                    yBOInvestigationDept.DriverName = oldDriver.DriverName;
                }
            }
            Driver existingDriver = _driverService.FindDriverByLicense(yBOInvestigationDept.DriverLicense);
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
                return Update(yBOInvestigationDept);

            }
            else
            {
                existingDriver.DriverName = yBOInvestigationDept.DriverName;
                existingDriver.DriverLicense = yBOInvestigationDept.DriverLicense;
                //existingDriver.VehicleNumber = yBOInvestigationDept.VehicleNumber;
                _driverService.EditDriver(existingDriver);
                yBOInvestigationDept.Driver = existingDriver;
                return Update(yBOInvestigationDept);
            }
        }

        public bool DeleteYBOInvestigationDept(YBOInvestigationDept yBOInvestigationDept)
        {
            yBOInvestigationDept.IsDeleted = true;
            return Update(yBOInvestigationDept);
        }

        public YBOInvestigationDept FindYBOInvestigationDeptById(int id)
        {
            return FindById(id);
        }

        public YBOInvestigationDept FindYBOInvestigationDeptByIdEgerLoad(int id)
        {
            return _context.YBOInvestigationDepts.Where(record => record.IsDeleted == false)
                           .Include(yBOInvestigationDept => yBOInvestigationDept.YBSCompany)
                           .Include(yBOInvestigationDept => yBOInvestigationDept.YBSType)
                           .Include(yBOInvestigationDept => yBOInvestigationDept.Driver)
                           .Include(yBOInvestigationDept => yBOInvestigationDept.Driver.VehicleData)
                           .FirstOrDefault(yBOInvestigationDept => yBOInvestigationDept.YBOInvestigationDeptPkid == id);
        }

        public DataTable MakeYBOInvestigationDeptExcelData(PagingList<YBOInvestigationDept> yBOInvestigationDepts, bool exportAll)
        {
            DataTable dt = new DataTable("YBO ဖမ်းစီးမှုစာရင်း");
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
                list = GetAllYBOInvestigationDeptsEgerLoad();
            else
                list = yBOInvestigationDepts;
            if (list.Count() > 0)
            {
                foreach (var yBOInvestigationDept in list)
                {
                    dt.Rows.Add(yBOInvestigationDept.RecordDate, yBOInvestigationDept.RecordTime, yBOInvestigationDept.TotalRecord, yBOInvestigationDept.Phone, yBOInvestigationDept.YbsRecordSender, yBOInvestigationDept.RecordDescription, yBOInvestigationDept.CompletionStatus, yBOInvestigationDept.CompletedDate, yBOInvestigationDept.Driver.DriverName, yBOInvestigationDept.Driver.VehicleData.VehicleNumber, yBOInvestigationDept.Driver.DriverLicense, yBOInvestigationDept.YBSCompany.YBSCompanyName, yBOInvestigationDept.YBSType.YBSTypeName);
                }
            }
            return dt;

        }
    }
}