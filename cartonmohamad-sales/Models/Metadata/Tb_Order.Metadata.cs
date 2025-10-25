using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cartonmohamad_sales.Models
{
    [MetadataType(typeof(TbOrderMeta))]
    public partial class Tb_Order { }

    public sealed class TbOrderMeta
    {
        [Key]
        public long ID { get; set; }

        [Display(Name = "مشتری")]
        [Required(ErrorMessage = "انتخاب مشتری الزامی است.")]
        [ForeignKey("Customer")]
        public int J_ID_Customer { get; set; }

        [Display(Name = "شماره سفارش داخلی")]
        [Range(1, int.MaxValue, ErrorMessage = "شماره سفارش باید عددی مثبت باشد.")]
        public int Number_order { get; set; }

        [Display(Name = "شناسه فاکتور")]
        [Range(0, long.MaxValue, ErrorMessage = "شناسه فاکتور نمی‌تواند منفی باشد.")]
        public long J_ID_Facktor { get; set; }

        [Display(Name = "شماره سفارش راهکاران")]
        [StringLength(100, ErrorMessage = "حداکثر ۱۰۰ کاراکتر.")]
        public string Number_ORder_rahkaran { get; set; }

        [Display(Name = "وضعیت")]
        [StringLength(30)]
        // اگر وضعیت‌ها را محدود می‌خواهی، عبارت زیر را فعال کن و مقادیر را مطابق سیستم خودت بگذار:
        // [RegularExpression(@"^(created|confirmed|in_production|shipped|cancelled)?$",
        //   ErrorMessage = "وضعیت فقط یکی از created/confirmed/in_production/shipped/cancelled باشد.")]
        public string Status { get; set; }

        [Display(Name = "تاریخ سفارش")]
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }
    }
}
