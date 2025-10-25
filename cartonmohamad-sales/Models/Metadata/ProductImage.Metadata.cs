using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cartonmohamad_sales.Models
{
    [MetadataType(typeof(ProductImageMeta))]
    public partial class ProductImage { }

    public sealed class ProductImageMeta
    {
        [Key]
        public int image_id { get; set; }

        [Display(Name = "محصول")]
        [Required(ErrorMessage = "انتخاب محصول الزامی است.")]
        [ForeignKey("Product")]
        public int product_id { get; set; }

        [Display(Name = "آدرس فایل تصویر")]
        [Required(ErrorMessage = "آدرس فایل الزامی است.")]
        [StringLength(500, ErrorMessage = "حداکثر ۵۰۰ کاراکتر.")]
        // اگر URL کامل می‌خواهی، می‌توانی [Url] هم اضافه کنی؛ ولی برای مسیر نسبی حذف شده است.
        public string file_url { get; set; }

        [Display(Name = "محل ذخیره‌سازی")]
        [StringLength(20, ErrorMessage = "حداکثر ۲۰ کاراکتر.")]
        // مقادیر پیشنهادی: local, azure, s3, gcs, other
        public string storage_provider { get; set; }

        [Display(Name = "متن جایگزین (alt)")]
        [StringLength(200, ErrorMessage = "حداکثر ۲۰۰ کاراکتر.")]
        public string alt_text { get; set; }

        [Display(Name = "کپشن/عنوان")]
        [StringLength(300, ErrorMessage = "حداکثر ۳۰۰ کاراکتر.")]
        public string caption { get; set; }

        [Display(Name = "اصلی؟")]
        public bool is_primary { get; set; }

        [Display(Name = "ترتیب نمایش")]
        [Range(0, int.MaxValue, ErrorMessage = "ترتیب نمایش نمی‌تواند منفی باشد.")]
        public int sort_order { get; set; }

        [Display(Name = "MIME type")]
        [StringLength(100, ErrorMessage = "حداکثر ۱۰۰ کاراکتر.")]
        public string mime_type { get; set; }

        [Display(Name = "عرض (px)")]
        [Range(0, int.MaxValue, ErrorMessage = "عرض نمی‌تواند منفی باشد.")]
        public int? width_px { get; set; }

        [Display(Name = "ارتفاع (px)")]
        [Range(0, int.MaxValue, ErrorMessage = "ارتفاع نمی‌تواند منفی باشد.")]
        public int? height_px { get; set; }

        [Display(Name = "حجم (بایت)")]
        [Range(0, long.MaxValue, ErrorMessage = "حجم نمی‌تواند منفی باشد.")]
        public long? size_bytes { get; set; }

        [Display(Name = "SHA1 چک‌سام")]
        [StringLength(40, ErrorMessage = "طول SHA1 باید ۴۰ کاراکتر باشد.")]
        public string checksum_sha1 { get; set; }

        [Display(Name = "تاریخ ایجاد")]
        [DataType(DataType.DateTime)]
        public System.DateTime created_at { get; set; }
    }
}
