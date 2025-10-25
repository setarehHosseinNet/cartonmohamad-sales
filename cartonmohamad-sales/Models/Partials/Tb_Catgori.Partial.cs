using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cartonmohamad_sales.Models
{
    public partial class Tb_Catgori : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext ctx)
        {
            var name = (Categori ?? string.Empty).Trim();

            if (string.IsNullOrWhiteSpace(name) || name.Length < 2)
                yield return new ValidationResult(
                    "نام دسته‌بندی باید حداقل ۲ کاراکتر باشد.",
                    new[] { nameof(Categori) });

            // می‌تونی اگر لازم داری، هم‌نامی را در سرویس/ریپازیتوری چک کنی (ایندکس یکتا بهتر است).
            // مثال سمت دیتابیس: UNIQUE INDEX روی Categori
        }
    }
}
