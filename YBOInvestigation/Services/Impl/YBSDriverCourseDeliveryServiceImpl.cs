using YBOInvestigation.Data;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace YBOInvestigation.Services.Impl
{
    public class YBSDriverCourseDeliveryServiceImpl : AbstractServiceImpl<YBSDriverCourseDelivery>, YBSDriverCourseDeliveryService
    {
        private readonly DriverService _driverService;
        private readonly TrainedYBSDriverInfoService _trainedDriverInfoService;
        private readonly VehicleDataService _vehicleDataService;
        public YBSDriverCourseDeliveryServiceImpl(YBOInvestigationDBContext context, DriverService driverService, TrainedYBSDriverInfoService trainedDriverInfoService, VehicleDataService vehicleDataService) : base(context)
        {
            _driverService = driverService;
            _trainedDriverInfoService = trainedDriverInfoService;
            _vehicleDataService = vehicleDataService;
        }

        public List<YBSDriverCourseDelivery> GetAllYBSDriverCourseDeliveries()
        {
            return GetAll().Where(yBSDriverCourseDelivery => yBSDriverCourseDelivery.IsDeleted == false).ToList();
        }

        public List<YBSDriverCourseDelivery> GetAllYBSDriverCourseDeliveriesEgerLoad()
        {
            return _context.YBSDriverCourseDeliveries
                    .Where(yBSDriverCourseDelivery => yBSDriverCourseDelivery.IsDeleted == false)
                            .Include(yBSDriverCourseDelivery => yBSDriverCourseDelivery.YBSCompany)
                            .Include(yBSDriverCourseDelivery => yBSDriverCourseDelivery.YBSType)
                            .Include(yBSDriverCourseDelivery => yBSDriverCourseDelivery.TrainedYBSDriverInfo)
                            .Include(yBSDriverCourseDelivery => yBSDriverCourseDelivery.TrainedYBSDriverInfo.Driver)
                            .Include(yBSDriverCourseDelivery => yBSDriverCourseDelivery.TrainedYBSDriverInfo.Driver.VehicleData)
                            .ToList();
        }

        public PagingList<YBSDriverCourseDelivery> GetAllYBSDriverCourseDeliveriesWithPagin(string searchString, int? pageNo, int PageSize)
        {
            List<YBSDriverCourseDelivery> resultList = new List<YBSDriverCourseDelivery>();
            if (searchString != null && !String.IsNullOrEmpty(searchString))
            {
                resultList = _context.YBSDriverCourseDeliveries
                            .Include(yBSDriverCourseDelivery => yBSDriverCourseDelivery.YBSCompany)
                            .Include(yBSDriverCourseDelivery => yBSDriverCourseDelivery.YBSType)
                            .Include(yBSDriverCourseDelivery => yBSDriverCourseDelivery.PunishmentType)
                            .Include(yBSDriverCourseDelivery => yBSDriverCourseDelivery.TrainedYBSDriverInfo)
                            .Include(yBSDriverCourseDelivery => yBSDriverCourseDelivery.TrainedYBSDriverInfo.Driver)
                            .Include(yBSDriverCourseDelivery => yBSDriverCourseDelivery.TrainedYBSDriverInfo.Driver.VehicleData)
                            .ToList()
                            .Where(yBSDriverCourseDelivery => yBSDriverCourseDelivery.IsDeleted == false)
                            .Where(yBSDriverCourseDelivery => yBSDriverCourseDelivery.YBSCompany.IsDeleted == false)
                            .Where(yBSDriverCourseDelivery => yBSDriverCourseDelivery.YBSType.IsDeleted == false)
                            .Where(yBSDriverCourseDelivery => yBSDriverCourseDelivery.PunishmentType.IsDeleted == false)

                            .Where(yBSDriverCourseDelivery => IsSearchDataContained(yBSDriverCourseDelivery, searchString)
                            || IsSearchDataContained(yBSDriverCourseDelivery.YBSCompany, searchString)
                            || IsSearchDataContained(yBSDriverCourseDelivery.YBSType, searchString)
                            || IsSearchDataContained(yBSDriverCourseDelivery.TrainedYBSDriverInfo, searchString)
                            || IsSearchDataContained(yBSDriverCourseDelivery.TrainedYBSDriverInfo.Driver, searchString)
                            || IsSearchDataContained(yBSDriverCourseDelivery.TrainedYBSDriverInfo.Driver.VehicleData, searchString)
                            || IsSearchDataContained(yBSDriverCourseDelivery.PunishmentType, searchString)
                            ).AsQueryable().ToList();
            }
            else
            {
                resultList = _context.YBSDriverCourseDeliveries
                    .Where(yBSDriverCourseDelivery => yBSDriverCourseDelivery.IsDeleted == false)
                            .Include(yBSDriverCourseDelivery => yBSDriverCourseDelivery.YBSCompany)
                            .Include(yBSDriverCourseDelivery => yBSDriverCourseDelivery.YBSType)
                            .Include(yBSDriverCourseDelivery => yBSDriverCourseDelivery.PunishmentType)
                            .Include(yBSDriverCourseDelivery => yBSDriverCourseDelivery.TrainedYBSDriverInfo)
                            .Include(yBSDriverCourseDelivery => yBSDriverCourseDelivery.TrainedYBSDriverInfo.Driver)
                            .Include(yBSDriverCourseDelivery => yBSDriverCourseDelivery.TrainedYBSDriverInfo.Driver.VehicleData)
                            .ToList();
            }
            return GetAllWithPagin(resultList, pageNo, PageSize);
        }

        
        public bool CreateYBSDriverCourseDeliveries(YBSDriverCourseDelivery yBSDriverCourseDelivery)
        {
            yBSDriverCourseDelivery.IsDeleted = false;
            yBSDriverCourseDelivery.CreatedDate = DateTime.Now;
            yBSDriverCourseDelivery.CreatedBy = "admin";
            if (!string.IsNullOrEmpty(yBSDriverCourseDelivery.DriverName) && int.TryParse(yBSDriverCourseDelivery.DriverName, out int oldDriverPkId))
            {
                Driver oldDriver = _driverService.FindDriverById(oldDriverPkId);
                if(oldDriver != null)
                {
                    yBSDriverCourseDelivery.DriverName = oldDriver.DriverName;
                }
            }
            Driver existingDriver = _driverService.FindDriverByLicense(yBSDriverCourseDelivery.DriverLicense);
            TrainedYBSDriverInfo trainedDriverInfo = new TrainedYBSDriverInfo
            {
                Age = yBSDriverCourseDelivery.Age,
                FatherName = yBSDriverCourseDelivery.FatherName,
                Address = yBSDriverCourseDelivery.Address,
                EducationLevel = yBSDriverCourseDelivery.EducationLevel,
                Phone = yBSDriverCourseDelivery.Phone
            };
            if (existingDriver == null)
            {
                VehicleData vehicleData = _vehicleDataService.FindVehicleByVehicleNumber(yBSDriverCourseDelivery.VehicleNumber);
                
                Driver driver = new Driver
                {
                    DriverName = yBSDriverCourseDelivery.DriverName,
                    DriverLicense = yBSDriverCourseDelivery.DriverLicense,
                };
                driver.VehicleData = vehicleData;
                trainedDriverInfo.Driver = driver;
                //_driverService.CreateDriver(driver);
                yBSDriverCourseDelivery.TrainedYBSDriverInfo = trainedDriverInfo;
                return Create(yBSDriverCourseDelivery);
            }
            else
            {
                TrainedYBSDriverInfo existingTrainedDriverInfo = _trainedDriverInfoService.GetTrainedYBSDriverInfoByDriverId(existingDriver.DriverPkid);
                if(existingTrainedDriverInfo == null)
                {
                    trainedDriverInfo.Driver = existingDriver;
                    yBSDriverCourseDelivery.TrainedYBSDriverInfo = trainedDriverInfo;
                }
                else
                {
                    existingTrainedDriverInfo.Age = yBSDriverCourseDelivery.Age;
                    existingTrainedDriverInfo.Address = yBSDriverCourseDelivery.Address;
                    existingTrainedDriverInfo.Phone = yBSDriverCourseDelivery.Phone;
                    existingTrainedDriverInfo.FatherName = yBSDriverCourseDelivery.FatherName;
                    existingTrainedDriverInfo.EducationLevel = yBSDriverCourseDelivery.EducationLevel;
                    yBSDriverCourseDelivery.TrainedYBSDriverInfo = existingTrainedDriverInfo;
                }
                return Create(yBSDriverCourseDelivery);
            }

        }

        public bool EditYBSDriverCourseDeliveries(YBSDriverCourseDelivery yBSDriverCourseDelivery)
        {
            yBSDriverCourseDelivery.IsDeleted = false;
            yBSDriverCourseDelivery.CreatedDate = DateTime.Now;
            yBSDriverCourseDelivery.CreatedBy = "admin";
            if (int.TryParse(yBSDriverCourseDelivery.DriverName, out int oldDriverPkId))
            {
                Driver oldDriver = _driverService.FindDriverById(oldDriverPkId);
                if(oldDriver != null)
                {
                    yBSDriverCourseDelivery.DriverName = oldDriver.DriverName;
                }
            }
            TrainedYBSDriverInfo trainedDriverInfo = new TrainedYBSDriverInfo
            {
                Age = yBSDriverCourseDelivery.Age,
                FatherName = yBSDriverCourseDelivery.FatherName,
                Address = yBSDriverCourseDelivery.Address,
                EducationLevel = yBSDriverCourseDelivery.EducationLevel,
                Phone = yBSDriverCourseDelivery.Phone
            };

            Driver existingDriver = _driverService.FindDriverByLicense(yBSDriverCourseDelivery.DriverLicense);
            if (existingDriver == null)
            { 
                VehicleData vehicleData = _vehicleDataService.FindVehicleByVehicleNumber(yBSDriverCourseDelivery.VehicleNumber);
                Driver driver = new Driver
                {
                    DriverName = yBSDriverCourseDelivery.DriverName,
                    DriverLicense = yBSDriverCourseDelivery.DriverLicense,
                };
                
                driver.VehicleData = vehicleData;
                _driverService.CreateDriver(driver);
                trainedDriverInfo.Driver = driver;
                _trainedDriverInfoService.CreateTrainedYBSDriverInfo(trainedDriverInfo);
                yBSDriverCourseDelivery.TrainedYBSDriverInfo = trainedDriverInfo;
                return Update(yBSDriverCourseDelivery);

            }
            else
            {
                existingDriver.DriverName = yBSDriverCourseDelivery.DriverName;
                existingDriver.DriverLicense = yBSDriverCourseDelivery.DriverLicense;
                _driverService.EditDriver(existingDriver);

                TrainedYBSDriverInfo existingTrainedDriverInfo = _trainedDriverInfoService.GetTrainedYBSDriverInfoByDriverId(existingDriver.DriverPkid);
                if (existingTrainedDriverInfo == null)
                {
                    trainedDriverInfo.Driver = existingDriver;
                    _trainedDriverInfoService.CreateTrainedYBSDriverInfo(trainedDriverInfo);
                    yBSDriverCourseDelivery.TrainedYBSDriverInfo = trainedDriverInfo;
                    return Update(yBSDriverCourseDelivery);
                }
                else
                {
                    existingTrainedDriverInfo.Age = yBSDriverCourseDelivery.Age;
                    existingTrainedDriverInfo.Address = yBSDriverCourseDelivery.Address;
                    existingTrainedDriverInfo.Phone = yBSDriverCourseDelivery.Phone;
                    existingTrainedDriverInfo.FatherName = yBSDriverCourseDelivery.FatherName;
                    existingTrainedDriverInfo.EducationLevel = yBSDriverCourseDelivery.EducationLevel;
                    yBSDriverCourseDelivery.TrainedYBSDriverInfo = existingTrainedDriverInfo;
                    _trainedDriverInfoService.EditTrainedYBSDriverInfo(existingTrainedDriverInfo);
                    yBSDriverCourseDelivery.TrainedYBSDriverInfo = existingTrainedDriverInfo;
                    return Update(yBSDriverCourseDelivery);
                }
            }

        }

        public bool DeleteYBSDriverCourseDeliveries(YBSDriverCourseDelivery yBSDriverCourseDelivery)
        {
            yBSDriverCourseDelivery.IsDeleted = true;
            return Update(yBSDriverCourseDelivery);
        }

        public YBSDriverCourseDelivery FindYBSDriverCourseDeliveriesById(int id)
        {
            return FindById(id);
        }

        public YBSDriverCourseDelivery FindYBSDriverCourseDeliveriesByIdEgerLoad(int id)
        {
            return _context.YBSDriverCourseDeliveries.Where(record => record.IsDeleted == false)
                           .Include(yBSDriverCourseDelivery => yBSDriverCourseDelivery.YBSCompany)
                           .Include(yBSDriverCourseDelivery => yBSDriverCourseDelivery.YBSType)
                           .Include(yBSDriverCourseDelivery => yBSDriverCourseDelivery.TrainedYBSDriverInfo)
                            .Include(yBSDriverCourseDelivery => yBSDriverCourseDelivery.TrainedYBSDriverInfo.Driver)
                            .Include(yBSDriverCourseDelivery => yBSDriverCourseDelivery.TrainedYBSDriverInfo.Driver.VehicleData)

                           .FirstOrDefault(yBSDriverCourseDelivery => yBSDriverCourseDelivery.YBSDriverCourseDeliveryPkid == id);
        }

        public DataTable MakeYBSDriverCourseDeliveriesExcelData(PagingList<YBSDriverCourseDelivery> yBSDriverCourseDeliverys, bool exportAll)
        {
            DataTable dt = new DataTable("YBO ဖမ်းစီးမှုစာရင်း");
            dt.Columns.AddRange(new DataColumn[13] {
                                        new DataColumn("ပြုလုပ်ရက်စွဲ"),
                                        new DataColumn("အမှုအမျိုးအစား"),
                                        new DataColumn("အကြိမ်အရေ"),
                                        new DataColumn("သင်တန်းသားအမည်"),
                                        new DataColumn("အသက်"),
                                        new DataColumn("အဖအမည်"),
                                        new DataColumn("ပညာအရည်အချင်း"),
                                        new DataColumn("နေရပ်လိပ်စာ"),
                                        new DataColumn("ဖုန်းနံပါတ်"),
                                        new DataColumn("ယာဥ်အမှတ်"),
                                        new DataColumn("လိုင်စင်အမှတ်"),
                                        new DataColumn("YBS Company Name"),
                                        new DataColumn("ယာဥ်လိုင်း"),
                                        });
            var list = new List<YBSDriverCourseDelivery>();
            if (exportAll)
                list = GetAllYBSDriverCourseDeliveriesEgerLoad();
            else
                list = yBSDriverCourseDeliverys;
            if (list.Count() > 0)
            {
                foreach (var yBSDriverCourseDelivery in list)
                {
                    dt.Rows.Add(yBSDriverCourseDelivery.EventDate, yBSDriverCourseDelivery.PunishmentType.Punishment, yBSDriverCourseDelivery.TrainedYBSDriverInfo.Driver.DriverName, yBSDriverCourseDelivery.TrainedYBSDriverInfo.Age, yBSDriverCourseDelivery.TrainedYBSDriverInfo.FatherName, yBSDriverCourseDelivery.TrainedYBSDriverInfo.EducationLevel, yBSDriverCourseDelivery.TrainedYBSDriverInfo.Address, yBSDriverCourseDelivery.TrainedYBSDriverInfo.Phone, yBSDriverCourseDelivery.TrainedYBSDriverInfo.Driver.VehicleData.VehicleNumber, yBSDriverCourseDelivery.TrainedYBSDriverInfo.Driver.DriverLicense, yBSDriverCourseDelivery.YBSCompany.YBSCompanyName, yBSDriverCourseDelivery.YBSType.YBSTypeName);
                }
            }
            return dt;

        }
    }
}