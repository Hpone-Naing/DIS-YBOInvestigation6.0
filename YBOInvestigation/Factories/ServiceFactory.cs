using YBOInvestigation.Services;

namespace YBOInvestigation.Factories
{
    public interface ServiceFactory
    {
        UserService CreateUserService();
        VehicleDataService CreateVehicleDataService();
        FuelTypeService CreateFuelTypeService();
        ManufacturerService CreateManufacturerService();
        YBSCompanyService CreateYBSCompanyService();
        YBSTypeService CreateYBSTypeService();

        YboRecordService CreateYBORecordService();
        YBOInvestigationDeptService CreateYBOInvestigationDeptService();
        CallCenterInvestigationDeptService CreateCallCenterInvestigationDeptService();

        TrafficControlCenterInvestigationDeptService CreateTrafficControlCenterInvestigationDeptService();
        SpecialEventInvestigationDeptService CreateSpecialEventInvestigationDeptService();
        DriverService CreateDriverService();
        PunishmentTypeService CreatePunishmentTypeService();
        YBSDriverCourseDeliveryService CreateYBSDriverCourseDeliveryService();
        TrainedYBSDriverInfoService CreateTrainedYBSDriverInfoService();
    }
}
