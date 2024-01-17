using YBOInvestigation.Data;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace YBOInvestigation.Services.Impl
{
    public class CallCenterInvestigationDeptServiceImpl : AbstractServiceImpl<CallCenterInvestigationDept>, CallCenterInvestigationDeptService
    {
        private readonly DriverService _driverService;
        private readonly VehicleDataService _vehicleDataService;
        public CallCenterInvestigationDeptServiceImpl(YBOInvestigationDBContext context, DriverService driverService, VehicleDataService vehicleDataService) : base(context)
        {
            _driverService = driverService;
            _vehicleDataService = vehicleDataService;
        }

        public List<CallCenterInvestigationDept> GetAllCallCenterInvestigationDepts()
        {
            return GetAll().Where(callCenterInvestigationDept => callCenterInvestigationDept.IsDeleted == false).ToList();
        }

        public List<CallCenterInvestigationDept> GetAllCallCenterInvestigationDeptsEgerLoad()
        {
            return _context.CallCenterInvestigationDepts
                    .Where(callCenterInvestigationDept => callCenterInvestigationDept.IsDeleted == false)
                            .Include(callCenterInvestigationDept => callCenterInvestigationDept.YBSCompany)
                            .Include(callCenterInvestigationDept => callCenterInvestigationDept.YBSType)
                            .Include(callCenterInvestigationDept => callCenterInvestigationDept.Driver)
                            .Include(callCenterInvestigationDept => callCenterInvestigationDept.Driver.VehicleData)
                            .ToList();
        }

        public PagingList<CallCenterInvestigationDept> GetAllCallCenterInvestigationDeptsWithPagin(string searchString, int? pageNo, int PageSize)
        {
            List<CallCenterInvestigationDept> callCenterInvestigationDepts = GetAllCallCenterInvestigationDepts();
            List<CallCenterInvestigationDept> resultList = new List<CallCenterInvestigationDept>();
            if (searchString != null && !String.IsNullOrEmpty(searchString))
            {
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
            else
            {
                resultList = _context.CallCenterInvestigationDepts
                    .Where(callCenterInvestigationDept => callCenterInvestigationDept.IsDeleted == false)
                            .Include(callCenterInvestigationDept => callCenterInvestigationDept.YBSCompany)
                            .Include(callCenterInvestigationDept => callCenterInvestigationDept.YBSType)
                            .Include(callCenterInvestigationDept => callCenterInvestigationDept.Driver)
                            .Include(callCenterInvestigationDept => callCenterInvestigationDept.Driver.VehicleData)
                            .ToList();
            }
            return GetAllWithPagin(resultList, pageNo, PageSize);
        }


        public bool CreateCallCenterInvestigationDept(CallCenterInvestigationDept callCenterInvestigationDept)
        {
            callCenterInvestigationDept.IsDeleted = false;
            callCenterInvestigationDept.CreatedDate = DateTime.Now;
            callCenterInvestigationDept.CreatedBy = "admin";
            if (!string.IsNullOrEmpty(callCenterInvestigationDept.DriverName) && int.TryParse(callCenterInvestigationDept.DriverName, out int oldDriverPkId))
            {
                string oldDriverName = _driverService.FindDriverById(oldDriverPkId).DriverName;
                callCenterInvestigationDept.DriverName = oldDriverName;
            }
            Driver existingDriver = _driverService.FindDriverByLicense(callCenterInvestigationDept.DriverLicense);
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
                return Create(callCenterInvestigationDept);
            }
            else
            {
                callCenterInvestigationDept.Driver = existingDriver;
                return Create(callCenterInvestigationDept);
            }

        }

        public bool EditCallCenterInvestigationDept(CallCenterInvestigationDept callCenterInvestigationDept)
        {
            callCenterInvestigationDept.IsDeleted = false;
            callCenterInvestigationDept.CreatedDate = DateTime.Now;
            callCenterInvestigationDept.CreatedBy = "admin";
            if (int.TryParse(callCenterInvestigationDept.DriverName, out int oldDriverPkId))
            {
                string oldDriverName = _driverService.FindDriverById(oldDriverPkId).DriverName;
                callCenterInvestigationDept.DriverName = oldDriverName;
            }
            Driver existingDriver = _driverService.FindDriverByLicense(callCenterInvestigationDept.DriverLicense);
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
                return Update(callCenterInvestigationDept);

            }
            else
            {
                existingDriver.DriverName = callCenterInvestigationDept.DriverName;
                existingDriver.DriverLicense = callCenterInvestigationDept.DriverLicense;
                //existingDriver.VehicleNumber = callCenterInvestigationDept.VehicleNumber;
                _driverService.EditDriver(existingDriver);
                callCenterInvestigationDept.Driver = existingDriver;
                return Update(callCenterInvestigationDept);
            }

        }

        public bool DeleteCallCenterInvestigationDept(CallCenterInvestigationDept callCenterInvestigationDept)
        {
            callCenterInvestigationDept.IsDeleted = true;
            return Update(callCenterInvestigationDept);
        }

        public CallCenterInvestigationDept FindCallCenterInvestigationDeptById(int id)
        {
            return FindById(id);
        }

        public CallCenterInvestigationDept FindCallCenterInvestigationDeptByIdEgerLoad(int id)
        {
            return _context.CallCenterInvestigationDepts.Where(record => record.IsDeleted == false)
                           .Include(callCenterInvestigationDept => callCenterInvestigationDept.YBSCompany)
                           .Include(callCenterInvestigationDept => callCenterInvestigationDept.YBSType)
                           .Include(callCenterInvestigationDept => callCenterInvestigationDept.Driver)
                           .Include(callCenterInvestigationDept => callCenterInvestigationDept.Driver.VehicleData)
                           .FirstOrDefault(callCenterInvestigationDept => callCenterInvestigationDept.CallCenterInvestigationDeptPkid == id);
        }

        public DataTable MakeCallCenterInvestigationDeptExcelData(PagingList<CallCenterInvestigationDept> callCenterInvestigationDepts, bool exportAll)
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
            var list = new List<CallCenterInvestigationDept>();
            if (exportAll)
                list = GetAllCallCenterInvestigationDeptsEgerLoad();
            else
                list = callCenterInvestigationDepts;
            if (list.Count() > 0)
            {
                foreach (var callCenterInvestigationDept in list)
                {
                    dt.Rows.Add(callCenterInvestigationDept.RecordDate, callCenterInvestigationDept.RecordTime, callCenterInvestigationDept.TotalRecord, callCenterInvestigationDept.Phone, callCenterInvestigationDept.YbsRecordSender, callCenterInvestigationDept.RecordDescription, callCenterInvestigationDept.CompletionStatus, callCenterInvestigationDept.CompletedDate, callCenterInvestigationDept.Driver.DriverName, callCenterInvestigationDept.Driver.VehicleData.VehicleNumber, callCenterInvestigationDept.Driver.DriverLicense, callCenterInvestigationDept.YBSCompany.YBSCompanyName, callCenterInvestigationDept.YBSType.YBSTypeName);
                }
            }
            return dt;

        }
    }
}