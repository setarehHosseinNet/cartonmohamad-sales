using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cartonmohamad_sales.Models
{
    public partial class OverheadCost : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var method = (calc_method ?? string.Empty).ToLowerInvariant();

            if (method == "fixed")
            {
                if (quantity.HasValue)
                    yield return new ValidationResult(
                        "در روش ثابت، فیلد «تعداد/Qty» نباید مقدار داشته باشد.",
                        new[] { nameof(quantity) });

                if (qty_per_meter.HasValue)
                    yield return new ValidationResult(
                        "در روش ثابت، «تعداد در هر متر» نباید مقدار داشته باشد.",
                        new[] { nameof(qty_per_meter) });
            }
            else if (method == "per_piece")
            {
                if (!quantity.HasValue || quantity <= 0)
                    yield return new ValidationResult(
                        "در روش «به ازای تعداد»، مقدار «تعداد/Qty» الزامی و باید مثبت باشد.",
                        new[] { nameof(quantity) });
            }
            else if (method == "per_length")
            {
                if (!qty_per_meter.HasValue || qty_per_meter <= 0)
                    yield return new ValidationResult(
                        "در روش «به ازای طول»، مقدار «تعداد در هر متر» الزامی و باید مثبت باشد.",
                        new[] { nameof(qty_per_meter) });
            }

            if (unit_price_irr < 0)
                yield return new ValidationResult(
                    "فی (ریال) نمی‌تواند منفی باشد.",
                    new[] { nameof(unit_price_irr) });

            if (price < 0)
                yield return new ValidationResult(
                    "قیمت نمی‌تواند منفی باشد.",
                    new[] { nameof(price) });
        }
    }
}
