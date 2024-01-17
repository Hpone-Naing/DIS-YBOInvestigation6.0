namespace YBOInvestigation.Models
{
    [Table("TB_PunishmentType")]
    public class PunishmentType
    {
        [Key]
        public int PunishmentTypePkid { get; set; }

        [StringLength(200)]
        public string Punishment { get; set; }

        public bool IsDeleted { get; set; }
    }
}
