using System.ComponentModel;

namespace YBOInvestigation.Models
{
    [Table("TB_InvestigationDept")]
    public class YboRecord
    {
        [Key]
        public int YboRecordPkid { get; set; }

        [DisplayName("ဖမ်းဆီးရက်စွဲ")]
        public DateTime? RecordDate { get; set; }

        [NotMapped]
        [StringLength(50)]
        [DisplayName("ယာဥ်မောင်းအမည်")]
        public string DriverName { get; set; }

        [NotMapped]
        [StringLength(50)]
        [DisplayName("ယာဥ်အမှတ်")]
        public string VehicleNumber { get; set; }

        [DisplayName("အကြိမ်အရေအတွက်")]
        public int? TotalRecord { get; set; }

        [StringLength(50)]
        [DisplayName("ဖုန်းနံပါတ်")]
        public string Phone { get; set; }

        [StringLength(50)]
        [DisplayName("သတင်းပေးပို့သူ/အကြောင်းအရာ")]
        public string YbsRecordSender { get; set; }

        [StringLength(500)]
        [DisplayName("တိုင်ကြားသည့်အကြောင်းအရာ")]
        public string RecordDescription { get; set; }

        [StringLength(50)]
        [DisplayName("ဆောင်ရွက်ပြီးစီးမှု")]
        public string CompletionStatus { get; set; }

        [StringLength(50)]
        [DisplayName("ချလန်နံပါတ်")]
        public string ChallanNumber { get; set; }

        [DisplayName("ပေးသွင်းရက်စွဲ")]
        public DateTime? CompletedDate { get; set; }

        [NotMapped]
        [StringLength(50)]
        [DisplayName("လိုင်စင်အမှတ်")]
        public string DriverLicense { get; set; }

        [StringLength(50)]
        [DisplayName("ID အမှတ်")]
        public string IDNumber { get; set; }

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

        [ForeignKey("Driver")]
        [DisplayName("ယာဥ်မောင်း")]
        public int DriverPkid { get; set; }
        public virtual Driver Driver { get; set; }
    }
}
