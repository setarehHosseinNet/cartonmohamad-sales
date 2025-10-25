using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cartonmohamad_sales.Models
{
    [MetadataType(typeof(TbJoinCategoriPaperMeta))]
    public partial class Tb_join_Categori_Paper { }

    public sealed class TbJoinCategoriPaperMeta
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "دسته‌بندی")]
        [Required(ErrorMessage = "انتخاب دسته‌بندی الزامی است.")]
        [ForeignKey("Tb_Catgori")]
        public int J_ID_Catgori { get; set; }

        [Display(Name = "نوع کاغذ")]
        [Required(ErrorMessage = "انتخاب کاغذ الزامی است.")]
        [ForeignKey("Tb_Paper")]
        public int J_ID_PAper { get; set; }   // نام ستون همین است، تغییرش نده

        [Display(Name = "مقدار/ویژگی")]
        [StringLength(120, ErrorMessage = "حداکثر ۱۲۰ کاراکتر.")]
        public string Value { get; set; }

        [Display(Name = "نوع")]
        // توضیح: این فیلد بولین است (مثلاً تگ/نوع/فلگ). اگر معنا مشخص است، در UI با Label مناسب نمایش بده.
        public bool type { get; set; }
    }
}
