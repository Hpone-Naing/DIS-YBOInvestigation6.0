using System.ComponentModel;

namespace YBOInvestigation.Models
{
    [Table("TB_Driver")]
    public class Driver
    {
        [Key]
        public int DriverPkid { get; set; }

        [StringLength(50)]
        public string DriverName { get; set; }

        [StringLength(50)]
        public string DriverLicense { get; set; }

        [ForeignKey("VehicleData")]
        public int VehicleDataPkid { get; set; }
        public virtual VehicleData VehicleData { get; set; }
    }
}
