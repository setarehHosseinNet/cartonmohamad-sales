using System.Collections.Generic;

namespace cartonmohamad_sales.Dtos
{
    public class PaperDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public decimal? price { get; set; }
        public decimal? shipping_cost { get; set; }

        // فقط برای راحتی کلاینت:
        public List<decimal> widths { get; set; }      // Cat = 1
        public List<decimal> grammages { get; set; }   // Cat = 2

        // تمام کتگوری‌ها: کلید=ID یا نام دسته، مقدار=لیست Value ها (به‌صورت رشته)
        public Dictionary<string, List<string>> categories { get; set; }
    }

    public class CategoryDto
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class ApiResult<T>
    {
        public bool ok { get; set; }
        public T data { get; set; }
        public string message { get; set; }
    }
}
