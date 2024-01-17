using YBOInvestigation.Data;
using YBOInvestigation.Services;
using YBOInvestigation.Services.Impl;

namespace YBOInvestigation.Factories.Impl
{
    public class ServiceFactoryImpl : ServiceFactory
    {
        private readonly YBOInvestigationDBContext _context;
        private readonly DriverService _driverService;
        private readonly TrainedYBSDriverInfoService _trainedYBSDriverInfoService;
        private readonly VehicleDataService _vehicleDataService;

        public ServiceFactoryImpl(YBOInvestigationDBContext context, DriverService driverService, TrainedYBSDriverInfoService trainedYBSDriverInfoService, VehicleDataService vehicleDataService)
        {
            _context = context;
            _driverService = driverService;
            _trainedYBSDriverInfoService = trainedYBSDriverInfoService;
            _vehicleDataService = vehicleDataService;
        }

        public UserService CreateUserService()
        {
            return new UserServiceImpl(_context);
        }
        
        public VehicleDataService CreateVehicleDataService()
        {
            return new VehicleDataServiceImpl(_context);
        }
        public FuelTypeService CreateFuelTypeService()
        {
            return new FuelTypeServiceImpl(_context);
        }
        public ManufacturerService CreateManufacturerService()
        {
            return new ManufacturerServiceImpl(_context);
        }
        public YBSCompanyService CreateYBSCompanyService()
        {
            return new YBSCompanyServiceImpl(_context);
        }
        public YBSTypeService CreateYBSTypeService()
        {
            return new YBSTypeServiceImpl(_context);
        }
        public DriverService CreateDriverService()
        {
            return new DriverServiceImpl(_context);
        }

        public YboRecordService CreateYBORecordService()
        {
            return new YboServiceImpl(_context, _driverService, _vehicleDataService);
        }

        public YBOInvestigationDeptService CreateYBOInvestigationDeptService()
        {
            return new YBOInvestigationDeptServiceImpl(_context, _driverService, _vehicleDataService);
        }

        public CallCenterInvestigationDeptService CreateCallCenterInvestigationDeptService()
        {
            return new CallCenterInvestigationDeptServiceImpl(_context, _driverService, _vehicleDataService);
        }

        public TrafficControlCenterInvestigationDeptService CreateTrafficControlCenterInvestigationDeptService()
        {
            return new TrafficControlCenterInvestigationDeptServiceImpl(_context, _driverService, _vehicleDataService);
        }

        public SpecialEventInvestigationDeptService CreateSpecialEventInvestigationDeptService()
        {
            return new SpecialEventInvestigationDeptServiceImpl(_context, _vehicleDataService);
        }

        

        public PunishmentTypeService CreatePunishmentTypeService()
        {
            return new PunishmentTypeServiceImpl(_context);
        }

        public YBSDriverCourseDeliveryService CreateYBSDriverCourseDeliveryService()
        {
            return new YBSDriverCourseDeliveryServiceImpl(_context, _driverService, _trainedYBSDriverInfoService, _vehicleDataService);
        }

        public TrainedYBSDriverInfoService CreateTrainedYBSDriverInfoService()
        {
            return new TrainedYBSDriverInfoServiceImpl(_context);
        }

    }
}
