using YBOInvestigation.Data;
using YBOInvestigation.Services;
using YBOInvestigation.Services.Impl;

namespace YBOInvestigation.Factories.Impl
{
    public class ServiceFactoryImpl : ServiceFactory
    {
        private readonly YBOInvestigationDBContext _context;
        private readonly ILoggerFactory _loggerFactory;
        private readonly DriverService _driverService;
        private readonly TrainedYBSDriverInfoService _trainedYBSDriverInfoService;
        private readonly VehicleDataService _vehicleDataService;

        public ServiceFactoryImpl(YBOInvestigationDBContext context, ILoggerFactory loggerFactory, DriverService driverService, TrainedYBSDriverInfoService trainedYBSDriverInfoService, VehicleDataService vehicleDataService)
        {
            _context = context;
            _loggerFactory = loggerFactory;
            _driverService = driverService;
            _trainedYBSDriverInfoService = trainedYBSDriverInfoService;
            _vehicleDataService = vehicleDataService;
        }

        public UserService CreateUserService()
        {
            ILogger<UserServiceImpl> userLogger = new Logger<UserServiceImpl>(_loggerFactory);
            return new UserServiceImpl(_context, userLogger);
        }
        
        public VehicleDataService CreateVehicleDataService()
        {
            ILogger<VehicleDataServiceImpl> vehicleDataLogger = new Logger<VehicleDataServiceImpl>(_loggerFactory);
            return new VehicleDataServiceImpl(_context, vehicleDataLogger);
        }
        public FuelTypeService CreateFuelTypeService()
        {
            ILogger<FuelTypeServiceImpl> fuelTypeLogger = new Logger<FuelTypeServiceImpl>(_loggerFactory);
            return new FuelTypeServiceImpl(_context, fuelTypeLogger);
        }
        public ManufacturerService CreateManufacturerService()
        {
            ILogger<ManufacturerServiceImpl> manufacturerLogger = new Logger<ManufacturerServiceImpl>(_loggerFactory);
            return new ManufacturerServiceImpl(_context, manufacturerLogger);
        }
        public YBSCompanyService CreateYBSCompanyService()
        {
            ILogger<YBSCompanyServiceImpl> ybsCompanyLogger = new Logger<YBSCompanyServiceImpl>(_loggerFactory);
            return new YBSCompanyServiceImpl(_context, ybsCompanyLogger);
        }
        public YBSTypeService CreateYBSTypeService()
        {
            ILogger<YBSTypeServiceImpl> ybsTypeLogger = new Logger<YBSTypeServiceImpl>(_loggerFactory);
            return new YBSTypeServiceImpl(_context, ybsTypeLogger);
        }
        public DriverService CreateDriverService()
        {
            ILogger<DriverServiceImpl> driverLogger = new Logger<DriverServiceImpl>(_loggerFactory);
            return new DriverServiceImpl(_context, driverLogger);
        }

        public YboRecordService CreateYBORecordService()
        {
            ILogger<YboServiceImpl> yboLogger = new Logger<YboServiceImpl>(_loggerFactory);
            return new YboServiceImpl(_context, yboLogger, _driverService, _vehicleDataService);
        }

        public YBOInvestigationDeptService CreateYBOInvestigationDeptService()
        {
            ILogger<YBOInvestigationDeptServiceImpl> yboInvestigationDeptLogger = new Logger<YBOInvestigationDeptServiceImpl>(_loggerFactory);
            return new YBOInvestigationDeptServiceImpl(_context, yboInvestigationDeptLogger, _driverService, _vehicleDataService);
        }

        public CallCenterInvestigationDeptService CreateCallCenterInvestigationDeptService()
        {
            ILogger<CallCenterInvestigationDeptServiceImpl> callCenterInvestigationDeptLogger = new Logger<CallCenterInvestigationDeptServiceImpl>(_loggerFactory);
            return new CallCenterInvestigationDeptServiceImpl(_context, callCenterInvestigationDeptLogger, _driverService, _vehicleDataService);
        }

        public TrafficControlCenterInvestigationDeptService CreateTrafficControlCenterInvestigationDeptService()
        {
            ILogger<TrafficControlCenterInvestigationDeptServiceImpl> trafficControlCenterDeptLogger = new Logger<TrafficControlCenterInvestigationDeptServiceImpl>(_loggerFactory);
            return new TrafficControlCenterInvestigationDeptServiceImpl(_context, trafficControlCenterDeptLogger, _driverService, _vehicleDataService);
        }

        public SpecialEventInvestigationDeptService CreateSpecialEventInvestigationDeptService()
        {
            ILogger<SpecialEventInvestigationDeptServiceImpl> specialEventInveDeptLogger = new Logger<SpecialEventInvestigationDeptServiceImpl>(_loggerFactory);
            return new SpecialEventInvestigationDeptServiceImpl(_context, specialEventInveDeptLogger, _vehicleDataService, _driverService);
        }

        public PunishmentTypeService CreatePunishmentTypeService()
        {
            ILogger<PunishmentTypeServiceImpl> punishmentTypeLogger = new Logger<PunishmentTypeServiceImpl>(_loggerFactory);
            return new PunishmentTypeServiceImpl(_context, punishmentTypeLogger);
        }

        public YBSDriverCourseDeliveryService CreateYBSDriverCourseDeliveryService()
        {
            ILogger<YBSDriverCourseDeliveryServiceImpl> ybsDriverCourseDeliveryLogger = new Logger<YBSDriverCourseDeliveryServiceImpl>(_loggerFactory);
            return new YBSDriverCourseDeliveryServiceImpl(_context, ybsDriverCourseDeliveryLogger, _driverService, _trainedYBSDriverInfoService, _vehicleDataService);
        }

        public TrainedYBSDriverInfoService CreateTrainedYBSDriverInfoService()
        {
            ILogger<TrainedYBSDriverInfoServiceImpl> trainedYBSDriverInfoLogger = new Logger<TrainedYBSDriverInfoServiceImpl>(_loggerFactory);
            return new TrainedYBSDriverInfoServiceImpl(_context, trainedYBSDriverInfoLogger);
        }

    }
}
