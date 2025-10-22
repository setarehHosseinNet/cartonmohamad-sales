using System.ComponentModel.DataAnnotations;

namespace cartonmohamad_sales.Models
{
    [MetadataType(typeof(TbPaperMeta))]
    public partial class Tb_Paper { }

    public sealed class TbPaperMeta
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "نام کاغذ/کارتن")]
        [Required(ErrorMessage = "نام کاغذ الزامی است.")]
        [StringLength(120, ErrorMessage = "حداکثر ۱۲۰ کاراکتر.")]
        public string Paper_Name { get; set; }

        [Display(Name = "فی هر شیت (ریال)")]
        [DataType(DataType.Currency)]
        [Range(0, int.MaxValue, ErrorMessage = "فی نمی‌تواند منفی باشد.")]
        public int? price_per_sheet { get; set; }

        [Display(Name = "هزینه حمل (ریال)")]
        [DataType(DataType.Currency)]
        [Range(0, int.MaxValue, ErrorMessage = "هزینه حمل نمی‌تواند منفی باشد.")]
        public int? shipping_cost { get; set; }

        // ناوبری‌ها نیاز به Annotation ندارند:
        // public virtual ICollection<PaperCombo> PaperCombos{...} و ...
    }
}
