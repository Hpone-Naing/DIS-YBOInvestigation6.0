using YBOInvestigation.Models;

namespace YBOInvestigation.Services
{
    public interface DriverService
    {
        public Driver FindDriverByLicense(string licenseNumber);
        public Driver FindDriverByIdNumberAndLicenseAndVehicle(string idNumber, string licenseNumber, int vehicleId);
        public string FindDriverLicenseByDriverName(string driverName);
        public Driver FindDriverById(int driverPkId);
        public List<Driver> GetDriversByVehicleDataId(int vehicleDataPkId);
        public bool CreateDriver(Driver driver);
        public bool CreateDefaultDriver(VehicleData vehicleData);
        public bool EditDriver(Driver driver);
        public Driver IsExistingDriver(string idNumber, string licenseNumber);
    }
}
