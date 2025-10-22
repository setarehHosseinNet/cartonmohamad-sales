using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace cartonmohamad_sales.Models
{
    [MetadataType(typeof(CrmCustomerMarketingActivitiesMeta))]
    public partial class crm_CustomerMarketingActivities { }

    public sealed class CrmCustomerMarketingActivitiesMeta
    {
        [Key] public long ID { get; set; }

        [Display(Name = "مشتری")]
        [Required(ErrorMessage = "انتخاب مشتری الزامی است")]
        //[ForeignKey("Customer")]
        public int J_ID_Customer { get; set; }

        [Display(Name = "تاریخ فعالیت")]
        [DataType(DataType.Date)]
        [Required] public DateTime Date { get; set; }

        [Display(Name = "نام کارشناس")]
        [Required, StringLength(50)]
        public string expert_name { get; set; }

        [Display(Name = "تماس بعدی")]
        [DataType(DataType.DateTime)]
        public DateTime? next_contact_at { get; set; }

        [Display(Name = "گرید مشتری")]
        [Required, StringLength(10)]
        public string customer_grade { get; set; }

        [Display(Name = "توضیحات")]
        [StringLength(4000)]
        public string Descript { get; set; }

        [Display(Name = "نتیجه جلسه (سفارش گرفت)")]
        public bool meeting_outcome { get; set; }

        [Display(Name = "نوع فعالیت")]
        [Required, StringLength(100)]
        public string activity_type { get; set; }
    }
}