using System.ComponentModel.DataAnnotations;

namespace cartonmohamad_sales.Models
{
    [MetadataType(typeof(TbCatgoriMeta))]
    public partial class Tb_Catgori { }

    public sealed class TbCatgoriMeta
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "نام دسته‌بندی")]
        [Required(ErrorMessage = "نام دسته‌بندی الزامی است.")]
        [StringLength(100, ErrorMessage = "حداکثر ۱۰۰ کاراکتر.")]
        // حروف، اعداد، فاصله و چند علامت ساده (در صورت نیاز الگو را آزادتر کن)
        [RegularExpression(@"^[\u0600-\u06FF\w\s\-\+\&\.\(\)\/]{2,100}$",
            ErrorMessage = "نام دسته‌بندی نامعتبر است.")]
        public string Categori { get; set; }
    }
}
