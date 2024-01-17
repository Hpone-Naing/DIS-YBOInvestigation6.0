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
        public string IncidencePlace { get; set; }

        [StringLength(50)]
        [DisplayName("ယာဥ်အမှတ်")]
        public string VehicleNumber { get; set; }

        [DisplayName("သတင်းပေးပို့သည့်အချိန်")]
        public DateTime? ReportTime { get; set; }

        [StringLength(50)]
        [DisplayName("သတင်းပေးပို့သူအမည်")]
        public string YbsRecordSender { get; set; }

        [StringLength(50)]
        [DisplayName("သတင်းပေးပို့သူရာထူး")]
        public string YbsRecordSenderPosition { get; set; }

        [StringLength(500)]
        [DisplayName("ဖြစ်စဥ်အကျဥ်း")]
        public string RecordDescription { get; set; }

        public bool? IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        [ForeignKey("YBSCompany")]
        [DisplayName("YBS Company")]
        public int YBSCompanyPkid { get; set; }
        public virtual YBSCompany YBSCompany { get; set; }

        [ForeignKey("YBSType")]
        [DisplayName("ယာဥ်လိုင်း")]
        public int YBSTypePkid { get; set; }
        public virtual YBSType YBSType { get; set; }
    }
}