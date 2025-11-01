using System;

namespace cartonmohamad_sales.Models
{
    public class InquiryRequest
    {
        // هویت مشتری
        public int? J_ID_Customer { get; set; } // اگر از فرم ID می‌آید
        public string CustomerName { get; set; } // در صورت نیاز برای match بر اساس نام

        // سفارش
        public string Number_Order { get; set; }
        public string Date { get; set; } // رشته (ISO/Jalali)؛ سرور تبدیل می‌کند

        // محصول
        public string Product_Code { get; set; }
        public string Product_Type { get; set; }
        public string Product_Name { get; set; }
        public int? Product_Tirage { get; set; }
        public string Product_Desc { get; set; } // توضیحات

        public string Layers_Count { get; set; } // "3" یا "5" (اگر عددی است می‌توانی int کنی)
        public string Pieces_Count { get; set; } // "1","half","4"
        public string Door_Type { get; set; }

        public decimal? LengthCm { get; set; }
        public decimal? WidthCm { get; set; }
        public decimal? HeightCm { get; set; }
    }
}
