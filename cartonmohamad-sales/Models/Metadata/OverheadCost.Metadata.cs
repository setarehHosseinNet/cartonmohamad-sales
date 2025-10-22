using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cartonmohamad_sales.Models
{
    [MetadataType(typeof(OverheadCostMeta))]
    public partial class OverheadCost { }

    public sealed class OverheadCostMeta
    {
        [Key]
        public int overhead_id { get; set; }

        [Display(Name = "نام هزینه")]
        [Required(ErrorMessage = "نام هزینه الزامی است.")]
        [StringLength(120, ErrorMessage = "حداکثر 120 کاراکتر.")]
        public string name { get; set; }

        [Display(Name = "اجباری؟")]
        public bool is_required { get; set; }

        [Display(Name = "روش محاسبه")]
        [Required(ErrorMessage = "روش محاسبه را انتخاب کنید.")]
        [StringLength(20)]
        // سه حالت پیشنهادی: per_length (به ازای طول)، per_piece (به ازای تعداد/قطعه)، fixed (ثابت کل سفارش)
        [RegularExpression(@"^(per_length|per_piece|fixed)$",
            ErrorMessage = "روش محاسبه فقط یکی از per_length / per_piece / fixed است.")]
        public string calc_method { get; set; }

        [Display(Name = "تعداد/Qty")]
        public decimal? quantity { get; set; }

        [Display(Name = "تعداد در هر متر")]
        public decimal? qty_per_meter { get; set; }

        [Display(Name = "فی (ریال)")]
        [Range(0, long.MaxValue, ErrorMessage = "فی نمی‌تواند منفی باشد.")]
        public long unit_price_irr { get; set; }

        [Display(Name = "نمایش در فاکتور؟")]
        public bool show_on_invoice { get; set; }

        [Display(Name = "قیمت محاسبه‌شده (ریال)")]
        [Range(0, int.MaxValue, ErrorMessage = "قیمت نمی‌تواند منفی باشد.")]
        public int price { get; set; }
    }
}
