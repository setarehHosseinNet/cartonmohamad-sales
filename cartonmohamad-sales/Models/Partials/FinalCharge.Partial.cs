using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cartonmohamad_sales.Models
{
    public partial class FinalCharge : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext ctx)
        {
            var calc = (calc_type ?? "").ToLowerInvariant();

            if (calc == "percent")
            {
                if (!percent_rate.HasValue)
                    yield return new ValidationResult("نرخ درصدی الزامی است.", new[] { nameof(percent_rate) });
                if (fixed_amount_irr.HasValue)
                    yield return new ValidationResult("در حالت درصدی، مبلغ ثابت نباید مقدار داشته باشد.", new[] { nameof(fixed_amount_irr) });
            }
            else if (calc == "fixed")
            {
                if (!fixed_amount_irr.HasValue)
                    yield return new ValidationResult("مبلغ ثابت (ریال) الزامی است.", new[] { nameof(fixed_amount_irr) });
                if (percent_rate.HasValue)
                    yield return new ValidationResult("در حالت ثابت، درصد نباید مقدار داشته باشد.", new[] { nameof(percent_rate) });
            }

            if (priority < 1)
                yield return new ValidationResult("اولویت باید عددی ≥ ۱ باشد.", new[] { nameof(priority) });
        }
    }
}
