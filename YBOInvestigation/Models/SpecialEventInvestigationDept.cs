using System.ComponentModel;

namespace YBOInvestigation.Models
{
    [Table("TB_SpecialEventInvestigationDept")]
    public class SpecialEventInvestigationDept
    {
        [Key]
        public int SpecialEventInvestigationDeptDeptPkid { get; set; }

        [DisplayName("ဖြစ်ပွားရက်စွဲ")]
        public DateTime? IncidenceDate { get; set; }

        [DisplayName("ဖြစ်ပွားသည့်အချိန်")]
        public DateTime? IncidenceTime { get; set; }

        [StringLength(50)]
        [DisplayName("ဖြစ်ပွားသည့်နေရာ")]
        public string? IncidencePlace { get; set; }

        [StringLength(50)]
        [DisplayName("ယာဥ်အမှတ်")]
        public string? VehicleNumber { get; set; }

        [DisplayName("သတင်းပေးပို့သည့်အချိန်")]
        public DateTime? ReportTime { get; set; }

        [StringLength(50)]
        [DisplayName("သတင်းပေးပို့သူအမည်")]
        public string? YbsRecordSender { get; set; }

        [StringLength(50)]
        [DisplayName("သတင်းပေးပို့သူရာထူး")]
        public string? YbsRecordSenderPosition { get; set; }

        [StringLength(500)]
        [DisplayName("ဖြစ်စဥ်အကျဥ်း")]
        public string? RecordDescription { get; set; }

        [NotMapped]
        [StringLength(50)]
        [DisplayName("ယာဥ်မောင်းအမည်")]
        public string? DriverName { get; set; }

        [NotMapped]
        [StringLength(50)]
        [DisplayName("လိုင်စင်အမှတ်")]
        public string? DriverLicense { get; set; }

        [NotMapped]
        [StringLength(50)]
        [DisplayName("ID အမှတ်")]
        public string? IDNumber { get; set; }

        [StringLength(500)]
        [DisplayName("သုံးသပ်ချက်")]
        public string? Review { get; set; }

        [StringLength(50)]
        [DisplayName("အရေးယူဆောင်ရွက်မှု")]
        public string? ActionResponse { get; set; }

        public bool? IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string? CreatedBy { get; set; }

        [ForeignKey("YBSCompany")]
        [DisplayName("YBS Company")]
        public int YBSCompanyPkid { get; set; }
        public virtual YBSCompany YBSCompany { get; set; }

        [ForeignKey("YBSType")]
        [DisplayName("ယာဥ်လိုင်း")]
        public int YBSTypePkid { get; set; }
        public virtual YBSType YBSType { get; set; }

        [ForeignKey("Driver")]
        [DisplayName("ယာဥ်မောင်း")]
        public int DriverPkid { get; set; }
        public virtual Driver Driver { get; set; }

        public bool IsDefaultIdNumber()
        {
            return IDNumber == null || IDNumber == "စီစစ်ဆဲ";
        }

        public bool IsDefaultLinenseNumber()
        {
            return DriverLicense == null || DriverLicense == "စီစစ်ဆဲ";
        }

        public bool IsNotDefaultDriver()
        {
            return (!IsDefaultLinenseNumber() || !IsDefaultIdNumber());
        }
    }
}