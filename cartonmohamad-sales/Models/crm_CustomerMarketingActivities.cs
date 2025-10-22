// Models/Generated/crm_CustomerMarketingActivities.cs
using System;

namespace cartonmohamad_sales.Models
{
    // این فایل باید فقط تعریف موجودیت را داشته باشد؛
    // متادیتا را در فایل جدا با [MetadataType] اعمال کن.
    public partial class crm_CustomerMarketingActivities
    {
        // کلید اصلی
        public long ID { get; set; }

        // کلید خارجی به Customer(ID)
        public int J_ID_Customer { get; set; }

        // تاریخ فعالیت (date)
        public DateTime Date { get; set; }

        // نام کارشناس
        public string expert_name { get; set; }

        // تماس بعدی (nullable)
        public DateTime? next_contact_at { get; set; }

        // گرید مشتری
        public string customer_grade { get; set; }

        // توضیحات
        public string Descript { get; set; }

        // نتیجه جلسه (سفارش گرفت؟)
        public bool meeting_outcome { get; set; }

        // نوع فعالیت
        public string activity_type { get; set; }

        // ناوبری‌ها
        public virtual Customer Customer { get; set; }
    }
}
