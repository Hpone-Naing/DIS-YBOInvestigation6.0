using YBOInvestigation.Data;
using YBOInvestigation.Models;

namespace YBOInvestigation.Services.Impl
{
    public class DriverServiceImpl : AbstractServiceImpl<Driver>, DriverService
    {
        private readonly ILogger<DriverServiceImpl> _logger;
        public DriverServiceImpl(YBOInvestigationDBContext context, ILogger<DriverServiceImpl> logger) : base(context, logger)
        {
            _logger = logger;
        }

        public Driver FindDriverByLicense(string licenseNumber)
        {
            _logger.LogInformation(">>>>>>>>>> [DriverServiceImpl][FindDriverByLicense] Find driver by license. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Find driver by license. <<<<<<<<<<");
                return FindByString("DriverLicense", licenseNumber);
            }
            catch(Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when finding driver by license. <<<<<<<<<<" + e);
                throw;
            }
        }

        public Driver FindDriverById(int driverPkId)
        {
            _logger.LogInformation(">>>>>>>>>> [DriverServiceImpl][FindDriverById] Find driver by pkId. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Find driver by pkId. <<<<<<<<<<");
                return FindById(driverPkId);
            }
            catch(Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when finding driver by pkId. <<<<<<<<<<" + e);
                throw;
            }
        }

        public string FindDriverLicenseByDriverName(string driverName)
        {
            _logger.LogInformation(">>>>>>>>>> [DriverServiceImpl][FindDriverLicenseByDriverName] Find driver by driverName. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Find driver by driverName. <<<<<<<<<<");
                return FindByString("DriverName", driverName).DriverLicense;
            }
            catch(Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when finding driver by driverName. <<<<<<<<<<" + e);
                throw;
            }
        }

        public List<Driver> GetDriversByVehicleDataId(int vehicleDataPkId)
        {
            _logger.LogInformation(">>>>>>>>>> [DriverServiceImpl][GetDriversByVehicleDataId] Get driver list by vechicleDataPkId. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Get driver list by vechicleDataPkId. <<<<<<<<<<");
                return GetListByIntVal("VehicleDataPkid", vehicleDataPkId);
            }
            catch(Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when getting driver list by vechicleDataPkId. <<<<<<<<<<" + e);
                throw;
            }
        }

        public bool CreateDriver(Driver driver)
        {
            _logger.LogInformation(">>>>>>>>>> [DriverServiceImpl][CreateDriver] Create driver. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Create driver. <<<<<<<<<<");
                return Create(driver);
            }
            catch(Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when creating driver. <<<<<<<<<<" + e);
                throw;
            }
        }

        public bool EditDriver(Driver driver)
        {
            _logger.LogInformation(">>>>>>>>>> [DriverServiceImpl][EditDriver] Edit driver. <<<<<<<<<<");
            try
            {
                _logger.LogInformation($">>>>>>>>>> Success. Edit driver. <<<<<<<<<<");
                return Update(driver);
            }
            catch(Exception e)
            {
                _logger.LogError(">>>>>>>>>> Error occur when updating driver. <<<<<<<<<<" + e);
                throw;
            }
        }
    }
}
