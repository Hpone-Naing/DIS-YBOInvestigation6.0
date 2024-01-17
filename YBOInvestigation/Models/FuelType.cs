namespace YBOInvestigation.Models
{
    [Table("TB_FuelType")]
    public class FuelType
    {
        [Key]
        public int FuelTypePkid { get; set; }

        [StringLength(50)]
        public string FuelTypeName { get; set; }

        public bool IsDeleted { get; set; }
    }
}
