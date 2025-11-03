using System;
using System.Collections.Generic;
using System.Globalization;

namespace cartonmohamad_sales.ViewModels
{
    public class InquiriesVM
    {
        // جستجو/فیلتر
        public string Q { get; set; }          // متن جستجو
        public string From { get; set; }       // yyyy-MM-dd (نمایشی)
        public string To { get; set; }         // yyyy-MM-dd (نمایشی)

        // صفحه‌بندی
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }

        public List<InqRowVM> Items { get; set; } = new List<InqRowVM>();
    }

    public class InqRowVM
    {
        public long OrderId { get; set; }
        public string CustomerName { get; set; }
        public int? NumberOrder { get; set; }
        public DateTime? Date { get; set; }
        public string Status { get; set; }

        public string NumberOrderText => NumberOrder?.ToString() ?? "-";

        // ⬇️ ایمن در برابر PersianCalendar
        public string DateText
        {
            get
            {
                if (!Date.HasValue) return "-";
                var d = Date.Value;

                if (d.Year < 1900) return "-"; // تاریخ‌های خراب

                var pc = new PersianCalendar();
                // اگر نیاز داری مطمئن شوی در بازه مجاز است:
                if (d < new DateTime(622, 3, 22, 0, 0, 0, DateTimeKind.Unspecified)) return "-";

                int y = pc.GetYear(d);
                int m = pc.GetMonth(d);
                int day = pc.GetDayOfMonth(d);
                return $"{y:0000}-{m:00}-{day:00}";
            }
        }
    }

    public class OrderRowVM
    {
        public long Id { get; set; }
        public int? CustomerId { get; set; }
        public string CustomerName { get; set; }

        public int? NumberOrder { get; set; }
        public long? InvoiceCode { get; set; } // ← عددی
        public string RahkaranNo { get; set; }   // Number_ORder_rahkaran
        public string Status { get; set; }
        public DateTime? Date { get; set; }
    }
}
