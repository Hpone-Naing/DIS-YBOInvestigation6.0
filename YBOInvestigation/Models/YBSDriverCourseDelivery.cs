using System.ComponentModel;

namespace YBOInvestigation.Models
{
    [Table("TB_YBSDriverCourseDelivery")]
    public class YBSDriverCourseDelivery
    {
        [Key]
        public int YBSDriverCourseDeliveryPkid { get; set; }

        [DisplayName("ပြုလုပ်ရက်စွဲ")]
        public DateTime? EventDate { get; set; }

        [DisplayName("အကြိမ်အရေ")]
        public int? TotalRecord { get; set; }

        [NotMapped]
        [StringLength(50)]
        [DisplayName("သင်တန်းသားအမည်")]
        public string DriverName { get; set; }

        [NotMapped]
        [StringLength(50)]
        [DisplayName("ယာဥ်အမှတ်")]
        public string VehicleNumber { get; set; }

        [NotMapped]
        [DisplayName("အသက်")]
        public int Age { get; set; }

        [NotMapped]
        [StringLength(50)]
        [DisplayName("အဖအမည်")]
        public string FatherName { get; set; }

        [NotMapped]
        [StringLength(50)]
        [DisplayName("ပညာအရည်အချင်း")]
        public string EducationLevel { get; set; }

        [NotMapped]
        [StringLength(100)]
        [DisplayName("နေရပ်လိပ်စာ")]
        public string Address { get; set; }

        [NotMapped]
        [StringLength(50)]
        [DisplayName("ဖုန်းနံပါတ်")]
        public string Phone { get; set; }

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

        [ForeignKey("PunishmentType")]
        [DisplayName("အမှုအမျိုးအစား")]
        public int PunishmentTypePkid { get; set; }
        public virtual PunishmentType PunishmentType { get; set; }


        [ForeignKey("TrainedYBSDriverInfo")]
        [DisplayName("သင်တန်းသားအချက်အလက်")]
        public int TrainedYBSDriverInfoPkid { get; set; }
        public virtual TrainedYBSDriverInfo TrainedYBSDriverInfo { get; set; }

    }
}
