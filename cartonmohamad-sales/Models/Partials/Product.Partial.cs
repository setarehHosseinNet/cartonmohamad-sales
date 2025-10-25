using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cartonmohamad_sales.Models
{
    public partial class Product : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext ctx)
        {
            // اگر تعداد درب وارد شده، نوع درب هم لازم است
            if (doors_count.HasValue && doors_count.Value > 0 && string.IsNullOrWhiteSpace(door_type))
                yield return new ValidationResult(
                    "با وارد کردن تعداد درب، تعیین «نوع درب» الزامی است.",
                    new[] { nameof(door_type), nameof(doors_count) });

            // اگر هر دو بعد صنعتی وارد شد، سطح صنعتی هم می‌تواند چک شود (هشدار/سازگاری)
            if (industrial_length_cm.HasValue && industrial_width_cm.HasValue && industrial_area_m2.HasValue)
            {
                var expected = Math.Round((industrial_length_cm.Value * industrial_width_cm.Value) / 10000m, 4);
                var diff = Math.Abs(expected - industrial_area_m2.Value);
                if (diff > 0.01m) // 0.01 m² تلورانس
                    yield return new ValidationResult(
                        "سطح صنعتی با طول×عرض هم‌خوانی ندارد.",
                        new[] { nameof(industrial_area_m2), nameof(industrial_length_cm), nameof(industrial_width_cm) });
            }

            // اگر فلوت وارد شده، باید یکی از مجازها باشد (در Metadata هم regex گذاشته‌ایم؛ اینجا فقط یادآوری)
            if (!string.IsNullOrWhiteSpace(flute_type))
            {
                var v = flute_type.ToUpperInvariant();
                var ok = v == "C" || v == "B" || v == "E" || v == "CB" || v == "CE" || v == "BE";
                if (!ok)
                    yield return new ValidationResult(
                        "فلوت فقط یکی از C, B, E, CB, CE, BE است.", new[] { nameof(flute_type) });
            }
        }
    }
}
