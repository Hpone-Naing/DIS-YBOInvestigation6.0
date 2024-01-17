using YBOInvestigation.Data;
using YBOInvestigation.Models;

namespace YBOInvestigation.Services.Impl
{
    public class DriverServiceImpl : AbstractServiceImpl<Driver>, DriverService
    {
        public DriverServiceImpl(YBOInvestigationDBContext context) : base(context)
        {
        }

        public Driver FindDriverByLicense(string licenseNumber)
        {
            return FindByString("DriverLicense", licenseNumber);
        }

        public Driver FindDriverById(int driverPkId)
        {
            return FindById(driverPkId);
        }

        public string FindDriverLicenseByDriverName(string driverName)
        {
            return FindByString("DriverName", driverName).DriverLicense;
        }

        public List<Driver> GetDriversByVehicleDataId(int vehicleDataPkId)
        {
            return GetListByIntVal("VehicleDataPkid", vehicleDataPkId);
        }

        public bool CreateDriver(Driver driver)
        {
            return Create(driver);
        }

        public bool EditDriver(Driver driver)
        {
            return Update(driver);
        }
    }
}
