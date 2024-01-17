using Microsoft.EntityFrameworkCore;
using YBOInvestigation.Models;

namespace YBOInvestigation.Data
{
    public class YBOInvestigationDBContext : DbContext
    {
        public YBOInvestigationDBContext(DbContextOptions<YBOInvestigationDBContext> options) : base(options)
        {

        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<FuelType> FuelTypes { get; set; }
        public virtual DbSet<Manufacturer> Manufacturers { get; set; }
        public virtual DbSet<VehicleData> VehicleDatas { get; set; }
        public virtual DbSet<YBSCompany> YBSCompanies { get; set; }
        public virtual DbSet<YBSType> YBSTypes { get; set; }
        public virtual DbSet<YboRecord> YboRecords { get; set; }
        public virtual DbSet<YBOInvestigationDept> YBOInvestigationDepts { get; set; }
        public virtual DbSet<TrafficControlCenterInvestigationDept> TrafficControlCenterInvestigationDepts { get; set; }
        public virtual DbSet<SpecialEventInvestigationDept> SpecialEventInvestigationDepts { get; set; }
        public virtual DbSet<CallCenterInvestigationDept> CallCenterInvestigationDepts { get; set; }
        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<PunishmentType> PunishmentTypes { get; set; }
        public virtual DbSet<TrainedYBSDriverInfo> TrainedYBSDriverInfos { get; set; }
        public virtual DbSet<YBSDriverCourseDelivery> YBSDriverCourseDeliveries { get; set; }

    }
}
