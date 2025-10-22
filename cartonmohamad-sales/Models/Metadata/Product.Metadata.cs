using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cartonmohamad_sales.Models
{
    [MetadataType(typeof(ProductMeta))]
    public partial class Product { }

    public sealed class ProductMeta
    {
        [Key]
        public int product_id { get; set; }

        [Display(Name = "کد محصول")]
        [Required(ErrorMessage = "کد محصول الزامی است.")]
        [StringLength(50)]
        public string product_code { get; set; }

        [Display(Name = "نام محصول")]
        [Required(ErrorMessage = "نام محصول الزامی است.")]
        [StringLength(120)]
        public string product_name { get; set; }

        [Display(Name = "نوع محصول")]
        [StringLength(30)]
        public string product_type { get; set; }

        [Display(Name = "تیراژ")]
        [Range(0, int.MaxValue, ErrorMessage = "تیراژ نمی‌تواند منفی باشد.")]
        public int tirage { get; set; }

        [Display(Name = "تعداد لایه")]
        [Range(typeof(byte), "0", "255")]
        public byte? layers_count { get; set; }

        [Display(Name = "چند تکه")]
        [Range(typeof(byte), "0", "255")]
        public byte? pieces_count { get; set; }

        [Display(Name = "نوع درب")]
        [StringLength(30)]
        public string door_type { get; set; }

        [Display(Name = "تعداد درب")]
        [Range(typeof(byte), "0", "255")]
        public byte? doors_count { get; set; }

        // ابعاد داخلی (cm)
        [Display(Name = "طول (cm)")]
        [Range(typeof(decimal), "0", "9999999", ErrorMessage = "مقدار نامعتبر است.")]
        public decimal? length_cm { get; set; }

        [Display(Name = "عرض (cm)")]
        [Range(typeof(decimal), "0", "9999999")]
        public decimal? width_cm { get; set; }

        [Display(Name = "ارتفاع (cm)")]
        [Range(typeof(decimal), "0", "9999999")]
        public decimal? height_cm { get; set; }

        // ظرفیت‌ها
        [Display(Name = "شیت در سفارش")]
        [Range(0, int.MaxValue)]
        public int? sheets_per_order { get; set; }

        [Display(Name = "تعداد واحد از هر ورق")]
        [Range(0, int.MaxValue)]
        public int? units_per_sheet { get; set; }

        [Display(Name = "تعداد کفی از هر ورق")]
        [Range(0, int.MaxValue)]
        public int? bottoms_per_sheet { get; set; }

        // لب درب‌ها
        [Display(Name = "مجموع لب درب (cm)")]
        [Range(typeof(decimal), "0", "9999999")]
        public decimal? flap_total_cm { get; set; }

        [Display(Name = "لب درب بالا (cm)")]
        [Range(typeof(decimal), "0", "9999999")]
        public decimal? flap_top_cm { get; set; }

        [Display(Name = "لب درب پایین (cm)")]
        [Range(typeof(decimal), "0", "9999999")]
        public decimal? flap_bottom_cm { get; set; }

        // قالب/دایکات
        [Display(Name = "تیغ به تیغ طول قالب (cm)")]
        [Range(typeof(decimal), "0", "9999999")]
        public decimal? die_to_die_length_cm { get; set; }

        [Display(Name = "عرض قالب (cm)")]
        [Range(typeof(decimal), "0", "9999999")]
        public decimal? mold_width_cm { get; set; }

        [Display(Name = "تعداد کفی/کارتن در قالب")]
        [Range(typeof(short), "0", "32767")]
        public short? bottoms_per_mold { get; set; }

        // ابعاد صنعتی
        [Display(Name = "طول صنعتی (cm)")]
        [Range(typeof(decimal), "0", "9999999")]
        public decimal? industrial_length_cm { get; set; }

        [Display(Name = "عرض صنعتی (cm)")]
        [Range(typeof(decimal), "0", "9999999")]
        public decimal? industrial_width_cm { get; set; }

        [Display(Name = "مصرف کارتن (m²)")]
        [Range(typeof(decimal), "0", "9999999")]
        public decimal? carton_consumption_m2 { get; set; }

        [Display(Name = "گرماژ (gsm)")]
        [Range(typeof(short), "0", "4000")]
        public short? grammage_gsm { get; set; }

        [Display(Name = "سطح صنعتی (m²)")]
        [Range(typeof(decimal), "0", "9999999")]
        public decimal? industrial_area_m2 { get; set; }

        [Display(Name = "گام/فلوت")]
        [StringLength(10)]
        // مقادیر مجاز طبق صحبت قبلی: C,B,E,CB,CE,BE  (تهی هم مجاز)
        [RegularExpression(@"^(C|B|E|CB|CE|BE)?$", ErrorMessage = "فلوت فقط یکی از C, B, E, CB, CE, BE است.")]
        public string flute_type { get; set; }
    }
}
