using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cartonmohamad_sales.Models
{
    public partial class Tb_Order : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext ctx)
        {
            if (J_ID_Customer <= 0)
                yield return new ValidationResult(
                    "انتخاب مشتری معتبر الزامی است.",
                    new[] { nameof(J_ID_Customer) });

            if (Number_order <= 0)
                yield return new ValidationResult(
                    "شماره سفارش باید عددی مثبت باشد.",
                    new[] { nameof(Number_order) });

            if (J_ID_Facktor < 0)
                yield return new ValidationResult(
                    "شناسه فاکتور نمی‌تواند منفی باشد.",
                    new[] { nameof(J_ID_Facktor) });

            // اگر وضعیت را محدود می‌خواهی، این بخش را با لیست مجازها هماهنگ کن
            if (!string.IsNullOrWhiteSpace(Status))
            {
                var s = Status.Trim().ToLowerInvariant();
                var allowed = new HashSet<string>
                {
                    "created","confirmed","in_production","shipped","cancelled"
                };
                if (!allowed.Contains(s))
                {
                    yield return new ValidationResult(
                        "وضعیت نامعتبر است. مقادیر مجاز: created, confirmed, in_production, shipped, cancelled.",
                        new[] { nameof(Status) });
                }
            }

            // تاریخ آینده؟
            if (Date.HasValue && Date.Value.Date > DateTime.Today)
                yield return new ValidationResult(
                    "تاریخ سفارش نمی‌تواند در آینده باشد.",
                    new[] { nameof(Date) });
        }
    }
}
