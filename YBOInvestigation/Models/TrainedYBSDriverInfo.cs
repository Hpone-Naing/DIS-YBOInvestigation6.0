namespace YBOInvestigation.Models
{
    [Table("TB_TrainedYBSDriverInfo")]
    public class TrainedYBSDriverInfo
    {
        [Key]
        public int TrainedYBSDriverInfoPkid { get; set; }

        public int Age { get; set; }

        [StringLength(50)]
        public string? FatherName { get; set; }

        [StringLength(50)]
        public string? EducationLevel { get; set; }

        [StringLength(100)]
        public string? Address { get; set; }

        [StringLength(50)]
        public string? Phone { get; set; }

        [ForeignKey("Driver")]
        public int DriverPkid { get; set; }
        public virtual Driver Driver { get; set; }
    }
}
