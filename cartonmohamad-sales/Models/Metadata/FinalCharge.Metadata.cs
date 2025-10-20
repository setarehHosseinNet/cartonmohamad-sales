using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cartonmohamad_sales.Models
{
    // فقط یک بار این اتریبیوت را داشته باشیم
    [MetadataType(typeof(FinalChargeMeta))]
    public partial class FinalCharge { }

    public class FinalChargeMeta
    {
        [Key]
        public int final_charge_id { get; set; }

        [Display(Name = "نام آیتم")]
        [Required(ErrorMessage = "نام آیتم الزامی است.")]
        [StringLength(100, ErrorMessage = "حداکثر ۱۰۰ کاراکتر.")]
        public string name { get; set; }

        [Display(Name = "فعال؟")]
        public bool is_active { get; set; }

        [Display(Name = "اجباری؟")]
        public bool is_required { get; set; }

        [Display(Name = "نوع محاسبه")]
        [Required(ErrorMessage = "نوع محاسبه را انتخاب کنید.")]
        [StringLength(10)]
        [RegularExpression(@"^(percent|fixed)$", ErrorMessage = "فقط «percent» یا «fixed».")]
        public string calc_type { get; set; }

        [Display(Name = "اولویت اعمال")]
        [Range(typeof(short), "1", "32767", ErrorMessage = "اولویت باید ≥ ۱ باشد.")]
        public short priority { get; set; }

        [Display(Name = "پایهٔ اعمال")]
        [Required(ErrorMessage = "پایهٔ اعمال را مشخص کنید.")]
        [StringLength(24)]
        [RegularExpression(@"^(subtotal|subtotal_plus_shipping|running_total)$",
            ErrorMessage = "فقط: subtotal / subtotal_plus_shipping / running_total.")]
        public string apply_on { get; set; }

        [Display(Name = "نرخ درصدی")]
        public decimal? percent_rate { get; set; }

        [Display(Name = "مبلغ ثابت (ریال)")]
        [Range(0, long.MaxValue, ErrorMessage = "منفی مجاز نیست.")]
        public long? fixed_amount_irr { get; set; }
    }
}
