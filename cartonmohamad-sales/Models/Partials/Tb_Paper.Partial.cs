using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cartonmohamad_sales.Models
{
    public partial class Tb_Paper : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext ctx)
        {
            // Trim و حداقل طول نام
            var name = (Paper_Name ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(name) || name.Length < 2)
                yield return new ValidationResult(
                    "نام کاغذ باید حداقل ۲ کاراکتر باشد.",
                    new[] { nameof(Paper_Name) });

            // عدم منفی بودن (در متادیتا هم Range گذاشته‌ایم؛ اینجا تکرارِ مطمئن)
            if (price_per_sheet.HasValue && price_per_sheet.Value < 0)
                yield return new ValidationResult(
                    "فی نمی‌تواند منفی باشد.",
                    new[] { nameof(price_per_sheet) });

            if (shipping_cost.HasValue && shipping_cost.Value < 0)
                yield return new ValidationResult(
                    "هزینه حمل نمی‌تواند منفی باشد.",
                    new[] { nameof(shipping_cost) });
        }
    }
}
