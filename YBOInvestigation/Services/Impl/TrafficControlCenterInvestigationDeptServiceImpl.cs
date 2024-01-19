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
        private readonly DriverService _driverService;
        private readonly VehicleDataService _vehicleDataService;
        public TrafficControlCenterInvestigationDeptServiceImpl(YBOInvestigationDBContext context, DriverService driverService, VehicleDataService vehicleDataService) : base(context)
        {
            _driverService = driverService;
            _vehicleDataService = vehicleDataService;
        }

        public List<TrafficControlCenterInvestigationDept> GetAllTrafficControlCenterInvestigationDepts()
        {
            return GetAll().Where(trafficControlCenterInvestigationDept => trafficControlCenterInvestigationDept.IsDeleted == false).ToList();
        }

        public List<TrafficControlCenterInvestigationDept> GetAllTrafficControlCenterInvestigationDeptsEgerLoad()
        {
            return _context.TrafficControlCenterInvestigationDepts
                    .Where(trafficControlCenterInvestigationDept => trafficControlCenterInvestigationDept.IsDeleted == false)
                            .Include(trafficControlCenterInvestigationDept => trafficControlCenterInvestigationDept.YBSCompany)
                            .Include(trafficControlCenterInvestigationDept => trafficControlCenterInvestigationDept.YBSType)
                            .Include(trafficControlCenterInvestigationDept => trafficControlCenterInvestigationDept.Driver)
                            .Include(trafficControlCenterInvestigationDept => trafficControlCenterInvestigationDept.Driver.VehicleData)
                            .ToList();
        }

        public PagingList<TrafficControlCenterInvestigationDept> GetAllTrafficControlCenterInvestigationDeptsWithPagin(string searchString, int? pageNo, int PageSize)
        {
            List<TrafficControlCenterInvestigationDept> trafficControlCenterInvestigationDepts = GetAllTrafficControlCenterInvestigationDepts();
            List<TrafficControlCenterInvestigationDept> resultList = new List<TrafficControlCenterInvestigationDept>();
            if (searchString != null && !String.IsNullOrEmpty(searchString))
            {
                resultList = _context.TrafficControlCenterInvestigationDepts
                    .Where(trafficControlCenterInvestigationDept => trafficControlCenterInvestigationDept.IsDeleted == false)
                            .Include(trafficControlCenterInvestigationDept => trafficControlCenterInvestigationDept.YBSCompany)
                            .Include(trafficControlCenterInvestigationDept => trafficControlCenterInvestigationDept.YBSType)
                            .Include(trafficControlCenterInvestigationDept => trafficControlCenterInvestigationDept.Driver)
                            .Include(trafficControlCenterInvestigationDept => trafficControlCenterInvestigationDept.Driver.VehicleData)
                            .ToList()
                            .Where(trafficControlCenterInvestigationDept => IsSearchDataContained(trafficControlCenterInvestigationDept, searchString) 
                            || IsSearchDataContained(trafficControlCenterInvestigationDept.YBSCompany, searchString)
                            || IsSearchDataContained(trafficControlCenterInvestigationDept.YBSType, searchString)
                            || IsSearchDataContained(trafficControlCenterInvestigationDept.Driver, searchString)
                            ).AsQueryable().ToList();
            }
            else
            {
                resultList = _context.TrafficControlCenterInvestigationDepts
                    .Where(trafficControlCenterInvestigationDept => trafficControlCenterInvestigationDept.IsDeleted == false)
                            .Include(trafficControlCenterInvestigationDept => trafficControlCenterInvestigationDept.YBSCompany)
                            .Include(trafficControlCenterInvestigationDept => trafficControlCenterInvestigationDept.YBSType)
                            .Include(trafficControlCenterInvestigationDept => trafficControlCenterInvestigationDept.Driver)
                            .Include(trafficControlCenterInvestigationDept => trafficControlCenterInvestigationDept.Driver.VehicleData)
                            .ToList();
            }
            return GetAllWithPagin(resultList, pageNo, PageSize);
        }

        
        public bool CreateTrafficControlCenterInvestigationDept(TrafficControlCenterInvestigationDept trafficControlCenterInvestigationDept)
        {
            trafficControlCenterInvestigationDept.IsDeleted = false;
            trafficControlCenterInvestigationDept.CreatedDate = DateTime.Now;
            trafficControlCenterInvestigationDept.CreatedBy = "admin";
            if (!string.IsNullOrEmpty(trafficControlCenterInvestigationDept.DriverName) && int.TryParse(trafficControlCenterInvestigationDept.DriverName, out int oldDriverPkId))
            {
                Driver oldDriver = _driverService.FindDriverById(oldDriverPkId);
                if(oldDriver != null)
                {
                    trafficControlCenterInvestigationDept.DriverName = oldDriver.DriverName;
                }
            }
            Driver existingDriver = _driverService.FindDriverByLicense(trafficControlCenterInvestigationDept.DriverLicense);
            if (existingDriver == null)
            {
                VehicleData vehicleData = _vehicleDataService.FindVehicleByVehicleNumber(trafficControlCenterInvestigationDept.VehicleNumber);
                Driver driver = new Driver
                {
                    DriverName = trafficControlCenterInvestigationDept.DriverName,
                    DriverLicense = trafficControlCenterInvestigationDept.DriverLicense,
                };
                driver.VehicleData = vehicleData;
                //_driverService.CreateDriver(driver);
                trafficControlCenterInvestigationDept.Driver = driver;
                return Create(trafficControlCenterInvestigationDept);
            }
            else
            {
                trafficControlCenterInvestigationDept.Driver = existingDriver;
                return Create(trafficControlCenterInvestigationDept);
            }

        }

        public bool EditTrafficControlCenterInvestigationDept(TrafficControlCenterInvestigationDept trafficControlCenterInvestigationDept)
        {
            trafficControlCenterInvestigationDept.IsDeleted = false;
            trafficControlCenterInvestigationDept.CreatedDate = DateTime.Now;
            trafficControlCenterInvestigationDept.CreatedBy = "admin";
            if (int.TryParse(trafficControlCenterInvestigationDept.DriverName, out int oldDriverPkId))
            {
                Driver oldDriver = _driverService.FindDriverById(oldDriverPkId);
                if(oldDriver != null)
                {
                    trafficControlCenterInvestigationDept.DriverName = oldDriver.DriverName;
                }
            }
            Driver existingDriver = _driverService.FindDriverByLicense(trafficControlCenterInvestigationDept.DriverLicense);
            if (existingDriver == null)
            {
                VehicleData vehicleData = _vehicleDataService.FindVehicleByVehicleNumber(trafficControlCenterInvestigationDept.VehicleNumber);
                Driver driver = new Driver
                {
                    DriverName = trafficControlCenterInvestigationDept.DriverName,
                    DriverLicense = trafficControlCenterInvestigationDept.DriverLicense,
                };
                driver.VehicleData = vehicleData;
                _driverService.CreateDriver(driver);
                trafficControlCenterInvestigationDept.Driver = driver;
                return Update(trafficControlCenterInvestigationDept);

            }
            else
            {
                existingDriver.DriverName = trafficControlCenterInvestigationDept.DriverName;
                existingDriver.DriverLicense = trafficControlCenterInvestigationDept.DriverLicense;
                //existingDriver.VehicleNumber = trafficControlCenterInvestigationDept.VehicleNumber;
                _driverService.EditDriver(existingDriver);
                trafficControlCenterInvestigationDept.Driver = existingDriver;
                return Update(trafficControlCenterInvestigationDept);
            }

        }

        public bool DeleteTrafficControlCenterInvestigationDept(TrafficControlCenterInvestigationDept trafficControlCenterInvestigationDept)
        {
            trafficControlCenterInvestigationDept.IsDeleted = true;
            return Update(trafficControlCenterInvestigationDept);
        }

        public TrafficControlCenterInvestigationDept FindTrafficControlCenterInvestigationDeptById(int id)
        {
            return FindById(id);
        }

        public TrafficControlCenterInvestigationDept FindTrafficControlCenterInvestigationDeptByIdEgerLoad(int id)
        {
            return _context.TrafficControlCenterInvestigationDepts.Where(record => record.IsDeleted == false)
                           .Include(trafficControlCenterInvestigationDept => trafficControlCenterInvestigationDept.YBSCompany)
                           .Include(trafficControlCenterInvestigationDept => trafficControlCenterInvestigationDept.YBSType)
                           .Include(trafficControlCenterInvestigationDept => trafficControlCenterInvestigationDept.Driver)
                           .Include(trafficControlCenterInvestigationDept => trafficControlCenterInvestigationDept.Driver.VehicleData)
                           .FirstOrDefault(trafficControlCenterInvestigationDept => trafficControlCenterInvestigationDept.TrafficControlCenterInvestigationDeptPkid == id);
        }

        public DataTable MakeTrafficControlCenterInvestigationDeptExcelData(PagingList<TrafficControlCenterInvestigationDept> trafficControlCenterInvestigationDepts, bool exportAll)
        {
            DataTable dt = new DataTable("YBO ဖမ်းစီးမှုစာရင်း");
            dt.Columns.AddRange(new DataColumn[14] {
                                        new DataColumn("ဖမ်းဆီးရက်စွဲ"),
                                        new DataColumn("ဖမ်းဆီးသည့်အချိန်"),
                                        new DataColumn("မီးနီဖြတ်သည့်နေရာ"),
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
            var list = new List<TrafficControlCenterInvestigationDept>();
            if (exportAll)
                list = GetAllTrafficControlCenterInvestigationDeptsEgerLoad();
            else
                list = trafficControlCenterInvestigationDepts;
            if (list.Count() > 0)
            {
                foreach (var trafficControlCenterInvestigationDept in list)
                {
                    dt.Rows.Add(trafficControlCenterInvestigationDept.RecordDate, trafficControlCenterInvestigationDept.RecordTime, trafficControlCenterInvestigationDept.RedLightCrossingPlace, trafficControlCenterInvestigationDept.TotalRecord, trafficControlCenterInvestigationDept.Phone, trafficControlCenterInvestigationDept.YbsRecordSender, trafficControlCenterInvestigationDept.RecordDescription, trafficControlCenterInvestigationDept.CompletionStatus, trafficControlCenterInvestigationDept.CompletedDate, trafficControlCenterInvestigationDept.Driver.DriverName, trafficControlCenterInvestigationDept.Driver.VehicleData.VehicleNumber, trafficControlCenterInvestigationDept.Driver.DriverLicense, trafficControlCenterInvestigationDept.YBSCompany.YBSCompanyName, trafficControlCenterInvestigationDept.YBSType.YBSTypeName);
                }
            }
            return dt;

        }
    }
}
