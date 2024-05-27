using System.ComponentModel;

namespace YBOInvestigation.Models
{
    [Table("TB_VehicleData")]
    public class VehicleData
    {
        [Key]
        public int VehicleDataPkid { get; set; }

        [StringLength(10)]
        [DisplayName("စဥ်")]
        public string? SerialNo { get; set; }

        [StringLength(50)]
        [DisplayName("အမျိုးအစား")]
        public string? YBSName { get; set; }

        [StringLength(50)]
        [DisplayName("ယာဥ်အမှတ်")]
        public string? VehicleNumber { get; set; }

        [StringLength(50)]
        [DisplayName("ထုတ်လုပ်သည့်ခန့်မှန်းနှစ်")]
        public string? ManufacturedYear { get; set; }

        [StringLength(50)]
        [DisplayName("CNG အိုးသက်တမ်း")]
        public string? CngQty { get; set; }

        [StringLength(50)]
        [DisplayName("CCTVတပ်ဆင်ထားမှု")]
        public string? CctvInstalled { get; set; }

        [StringLength(100)]
        [DisplayName("ခ္ငင့်ပြုခရီးစဥ်လမ်းကြောင်း")]
        public string? AssignedRoute { get; set; }

        [StringLength(50)]
        [DisplayName("Telematic Deviceတပ်ဆင်မှု")]
        public string? TelematicDeviceInstalled { get; set; }

        [StringLength(50)]
        [DisplayName("မှတ်တိုင်အရေအတွက်")]
        public string? TotalBusStop { get; set; }

        [StringLength(50)]
        [DisplayName("POS တပ်ဆင်ထားမှု")]
        public string? POSInstalled { get; set; }

        public DateTime? RegistrationDate { get; set; }

        [StringLength(50)]
        [DisplayName("Operator အမည်")]
        public string? OperatorName { get; set; }

        [StringLength(50)]
        [DisplayName("ကမ-၃တွင်မှတ်ပုံတင်ထားသူအမည်")]
        public string? RegistrantOperatorName { get; set; }

        [StringLength(300)]
        [DisplayName("မှတ်ချက်")]
        public string? Remarks { get; set; }

        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public bool? IsDeleted { get; set; }

        [ForeignKey("YBSCompany")]
        [DisplayName("YBS Company Name")]
        public int YBSCompanyPkid { get; set; }
        public virtual YBSCompany YBSCompany { get; set; }

        [ForeignKey("YBSType")]
        [DisplayName("ယာဥ်အမျိုးအမည်")]
        public int VehicleTypePkid { get; set; }
        public virtual YBSType YBSType { get; set; }

        [ForeignKey("FuelType")]
        [DisplayName("စက်သုံးဆီအမျိုးအစား")]
        public int FuelTypePkid { get; set; }
        public virtual FuelType FuelType { get; set; }

        [ForeignKey("Manufacturer")]
        [DisplayName("ယာဥ်အမျိုးအများ")]
        public int VehicleManufacturer { get; set; }
        public Manufacturer Manufacturer { get; set; }

    }
}
