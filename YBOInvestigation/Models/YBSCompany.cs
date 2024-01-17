namespace YBOInvestigation.Models
{
    [Table("TB_YBSCompany")]
    public class YBSCompany
    {
        [Key]
        public int YBSCompanyPkid { get; set; }

        [StringLength(100)]
        public string YBSCompanyName { get; set; }

        public bool IsDeleted { get; set; }

    }
}
