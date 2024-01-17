namespace YBOInvestigation.Models
{
    [Table("TB_DriverPunishmentType")]
    public class PunishmentType
    {
        [Key]
        public int PunishmentTypePkid { get; set; }

        [StringLength(200)]
        public string Punishment { get; set; }

        public bool IsDeleted { get; set; }
    }
}
