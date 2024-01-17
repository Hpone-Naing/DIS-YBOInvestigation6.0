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
        private readonly DriverService _driverService;
        private readonly VehicleDataService _vehicleDataService;
        public YboServiceImpl(YBOInvestigationDBContext context, DriverService driverService, VehicleDataService vehicleDataService) : base(context)
        {
            _driverService = driverService;
            _vehicleDataService = vehicleDataService;
        }

        public List<YboRecord> GetAllYboRecords()
        {
            return GetAll().Where(yboRecord => yboRecord.IsDeleted == false).ToList();
        }

        public List<YboRecord> GetAllYboRecordsEgerLoad()
        {
            return _context.YboRecords
                    .Where(yboRecord => yboRecord.IsDeleted == false)
                            .Include(yboRecord => yboRecord.YBSCompany)
                            .Include(yboRecord => yboRecord.YBSType)
                            .Include(yboRecord => yboRecord.Driver)
                            .Include(yboRecord => yboRecord.Driver.VehicleData)
                            .ToList();
        }

        public PagingList<YboRecord> GetAllYboRecordsWithPagin(string searchString, int? pageNo, int PageSize)
        {
            List<YboRecord> yboRecords = GetAllYboRecords();
            List<YboRecord> resultList = new List<YboRecord>();
            if (searchString != null && !String.IsNullOrEmpty(searchString))
            {
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
            else
            {
                resultList = _context.YboRecords
                    .Where(yboRecord => yboRecord.IsDeleted == false)
                            .Include(yboRecord => yboRecord.YBSCompany)
                            .Include(yboRecord => yboRecord.YBSType)
                            .Include(yboRecord => yboRecord.Driver)
                            .Include(yboRecord => yboRecord.Driver.VehicleData)
                            .ToList();
            }
            return GetAllWithPagin(resultList, pageNo, PageSize);
        }

        
        public bool CreateYboRecord(YboRecord yboRecord)
        {
            yboRecord.IsDeleted = false;
            yboRecord.CreatedDate = DateTime.Now;
            yboRecord.CreatedBy = "admin";
            if (!string.IsNullOrEmpty(yboRecord.DriverName) && int.TryParse(yboRecord.DriverName, out int oldDriverPkId))
            {
                string oldDriverName = _driverService.FindDriverById(oldDriverPkId).DriverName;
                yboRecord.DriverName = oldDriverName;
            }
            Driver existingDriver = _driverService.FindDriverByLicense(yboRecord.DriverLicense);
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
                return Create(yboRecord);
            }
            else
            {
                yboRecord.Driver = existingDriver;
                return Create(yboRecord);
            }

        }

        public bool EditYboRecord(YboRecord yboRecord)
        {
            yboRecord.IsDeleted = false;
            yboRecord.CreatedDate = DateTime.Now;
            yboRecord.CreatedBy = "admin";
            if (int.TryParse(yboRecord.DriverName, out int oldDriverPkId))
            {
                Console.WriteLine("here if");
                string oldDriverName = _driverService.FindDriverById(oldDriverPkId).DriverName;
                yboRecord.DriverName = oldDriverName;
            }
            Driver existingDriver = _driverService.FindDriverByLicense(yboRecord.DriverLicense);
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
                return Update(yboRecord);
            }

        }

        public bool DeleteYboRecord(YboRecord yboRecord)
        {
            yboRecord.IsDeleted = true;
            return Update(yboRecord);
        }

        public YboRecord FindYboRecordById(int id)
        {
            return FindById(id);
        }

        public YboRecord FindYboRecordByIdEgerLoad(int id)
        {
            return _context.YboRecords.Where(record => record.IsDeleted == false)
                           .Include(yboRecord => yboRecord.YBSCompany)
                           .Include(yboRecord => yboRecord.YBSType)
                           .Include(yboRecord => yboRecord.Driver)
                           .Include(yboRecord => yboRecord.Driver.VehicleData)
                           .FirstOrDefault(yboRecord => yboRecord.YboRecordPkid == id);
        }

        public DataTable MakeYboRecordExcelData(PagingList<YboRecord> yboRecords, bool exportAll)
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
            var list = new List<YboRecord>();
            if (exportAll)
                list = GetAllYboRecordsEgerLoad();
            else
                list = yboRecords;
            if (list.Count() > 0)
            {
                foreach (var yboRecord in list)
                {
                    dt.Rows.Add(yboRecord.RecordDate, yboRecord.TotalRecord, yboRecord.Phone, yboRecord.YbsRecordSender, yboRecord.RecordDescription, yboRecord.CompletionStatus, yboRecord.ChallanNumber, yboRecord.CompletedDate, yboRecord.Driver.DriverName, yboRecord.Driver.VehicleData.VehicleNumber, yboRecord.Driver.DriverLicense, yboRecord.YBSCompany.YBSCompanyName, yboRecord.YBSType.YBSTypeName);
                }
            }
            return dt;

        }
    }
}
