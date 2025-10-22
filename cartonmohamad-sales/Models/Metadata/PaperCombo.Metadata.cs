using System.ComponentModel.DataAnnotations;

namespace cartonmohamad_sales.Models
{
    // فقط همین فایل برای PaperCombo باید MetadataType داشته باشد
    [MetadataType(typeof(PaperComboMeta))]
    public partial class PaperCombo { }

    public sealed class PaperComboMeta
    {
        [Key]
        public int combo_id { get; set; }

        [Display(Name = "محصول")]
        [Required(ErrorMessage = "انتخاب محصول الزامی است.")]
        public int product_id { get; set; }

        [Display(Name = "نام ترکیب")]
        [Required(ErrorMessage = "نام ترکیب را وارد کنید.")]
        [StringLength(100, ErrorMessage = "حداکثر ۱۰۰ کاراکتر.")]
        public string combo_name { get; set; }

        [Display(Name = "نوع ترکیب")]
        [StringLength(12)]
        // مقادیر نمونه: original / substitute  (در صورت داشتن مقادیر دیگر این الگو را اصلاح کن)
        [RegularExpression(@"^(original|substitute)?$", ErrorMessage = "نوع ترکیب فقط original یا substitute.")]
        public string combo_kind { get; set; }

        // لایه‌ها (همه اختیاری‌اند)
        [Display(Name = "رویه (Top Liner)")]
        public int? top_liner_paper_id { get; set; }

        [Display(Name = "فلوت B/E")]
        public int? flute_be_paper_id { get; set; }

        [Display(Name = "میانی (Middle)")]
        public int? middle_paper_id { get; set; }

        [Display(Name = "فلوت C")]
        public int? flute_c_paper_id { get; set; }

        [Display(Name = "زیره (Bottom Liner)")]
        public int? bottom_liner_paper_id { get; set; }

        [Display(Name = "فعال؟")]
        public bool is_active { get; set; }

        [Display(Name = "توضیحات")]
        [StringLength(300, ErrorMessage = "حداکثر ۳۰۰ کاراکتر.")]
        public string notes { get; set; }

        [Display(Name = "تاریخ ایجاد")]
        [DataType(DataType.DateTime)]
        public System.DateTime created_at { get; set; }
    }
}
