using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cartonmohamad_sales.Models
{
    // فقط این فایل برای Tb_Body_Order باید MetadataType داشته باشد
    [MetadataType(typeof(TbBodyOrderMeta))]
    public partial class Tb_Body_Order { }

    public sealed class TbBodyOrderMeta
    {
        [Key]
        public long ID { get; set; }

        [Display(Name = "شماره سفارش")]
        [Required(ErrorMessage = "انتخاب سفارش الزامی است.")]
        [ForeignKey("Tb_Order")]
        public long J_ID_order { get; set; }

        [Display(Name = "محصول")]
        [Required(ErrorMessage = "انتخاب محصول الزامی است.")]
        [ForeignKey("Product")]
        public int J_ID_Production { get; set; }

        [Display(Name = "هزینه نهایی (FinalCharge)")]
        [ForeignKey("FinalCharge")]
        public int? final_charge_id { get; set; }  // اختیاری

        [Display(Name = "هزینه سرباره (Overhead)")]
        [Required(ErrorMessage = "انتخاب هزینه سرباره الزامی است.")]
        [ForeignKey("OverheadCost")]
        public int J_id_OverheadCosts { get; set; }
    }
}
