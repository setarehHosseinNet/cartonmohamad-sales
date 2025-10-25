using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cartonmohamad_sales.Models
{
    public partial class Tb_join_Categori_Paper : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext ctx)
        {
            if (J_ID_Catgori <= 0)
                yield return new ValidationResult(
                    "انتخاب دسته‌بندی معتبر الزامی است.",
                    new[] { nameof(J_ID_Catgori) });

            if (J_ID_PAper <= 0)
                yield return new ValidationResult(
                    "انتخاب کاغذ معتبر الزامی است.",
                    new[] { nameof(J_ID_PAper) });

            // اگر Value داده شد، خیلی کوتاه نباشد
            if (!string.IsNullOrWhiteSpace(Value) && Value.Trim().Length < 2)
                yield return new ValidationResult(
                    "مقدار/ویژگی باید حداقل ۲ کاراکتر باشد.",
                    new[] { nameof(Value) });
        }
    }
}
