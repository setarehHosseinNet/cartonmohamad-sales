using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cartonmohamad_sales.Models
{
    // فقط همین فایل برای Info_Compuni باید MetadataType داشته باشد
    [MetadataType(typeof(InfoCompuniMeta))]
    public partial class Info_Compuni { }

    public sealed class InfoCompuniMeta
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "مشتری")]
        [Required(ErrorMessage = "انتخاب مشتری الزامی است")]
        [ForeignKey("Customer")] // ناوبری در کلاس اصلی: public virtual Customer Customer { get; set; }
        public int J_ID_Customer { get; set; }

        // آدرس‌ها
        [Display(Name = "آدرس کارخانه")]
        [StringLength(300, ErrorMessage = "حداکثر 300 کاراکتر")]
        public string address_factory { get; set; }

        [Display(Name = "آدرس دفتر فروش")]
        [StringLength(300, ErrorMessage = "حداکثر 300 کاراکتر")]
        public string address_sales_office { get; set; }

        [Display(Name = "آدرس انبار")]
        [StringLength(300, ErrorMessage = "حداکثر 300 کاراکتر")]
        public string address_warehouse { get; set; }

        [Display(Name = "آدرس ارسال/دیسپچ")]
        [StringLength(300, ErrorMessage = "حداکثر 300 کاراکتر")]
        public string address_dispatch { get; set; }

        // تلفن‌ها
        [Display(Name = "تلفن کارخانه")]
        [StringLength(30, ErrorMessage = "حداکثر 30 کاراکتر")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\+?\d[\d\s\-\(\)]{5,}$", ErrorMessage = "شماره تلفن نامعتبر است")]
        public string phone_factory { get; set; }

        [Display(Name = "تلفن دفتر فروش")]
        [StringLength(30)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\+?\d[\d\s\-\(\)]{5,}$", ErrorMessage = "شماره تلفن نامعتبر است")]
        public string phone_sales_office { get; set; }

        [Display(Name = "تلفن مسئول خرید")]
        [StringLength(30)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\+?\d[\d\s\-\(\)]{5,}$", ErrorMessage = "شماره تلفن نامعتبر است")]
        public string phone_procurement_manager { get; set; }
    }
}
