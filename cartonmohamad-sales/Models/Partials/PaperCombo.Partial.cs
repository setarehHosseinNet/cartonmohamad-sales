using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cartonmohamad_sales.Models
{
    public partial class PaperCombo : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext ctx)
        {
            if (product_id <= 0)
                yield return new ValidationResult("انتخاب محصول الزامی است.", new[] { nameof(product_id) });

            // حداقل یک کاغذ باید انتخاب شود
            var hasAnyPaper =
                top_liner_paper_id.HasValue ||
                flute_be_paper_id.HasValue ||
                middle_paper_id.HasValue ||
                flute_c_paper_id.HasValue ||
                bottom_liner_paper_id.HasValue;

            if (!hasAnyPaper)
                yield return new ValidationResult(
                    "حداقل یکی از لایه‌های کاغذ را انتخاب کنید.",
                    new[]
                    {
                        nameof(top_liner_paper_id), nameof(flute_be_paper_id),
                        nameof(middle_paper_id), nameof(flute_c_paper_id),
                        nameof(bottom_liner_paper_id)
                    });

            if (is_active && string.IsNullOrWhiteSpace(combo_name))
                yield return new ValidationResult("برای ترکیب فعال، نام ترکیب الزامی است.", new[] { nameof(combo_name) });
        }
    }
}
