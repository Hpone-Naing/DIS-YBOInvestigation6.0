namespace YBOInvestigation.Classes
{
    public class DriverPunishmentInfo
    {
        public VehicleData? VehicleData { get; set; }

        public Driver? Driver { get; set; }

        public CallCenterPunishmentDriverInfo? CallCenterPunishmentDriverInfo;
        public YBOInvestigationPunishmentDriverInfo? YBOInvestigationPunishmentDriverInfo;
        public YBOPunishmentDriverInfo? YBOPunishmentDriverInfo;
        public TrafficControlCenterPunishmentDriverInfo? TrafficControlCenterPunishmentDriverInfo;
        public SpecialEventInvestigationPunishmentDriverInfo? SpecialEventInvestigationPunishmentDriverInfo;

    }
}
