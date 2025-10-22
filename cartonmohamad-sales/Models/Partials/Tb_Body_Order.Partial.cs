using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cartonmohamad_sales.Models
{
    public partial class Tb_Body_Order : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext ctx)
        {
            if (J_ID_order <= 0)
                yield return new ValidationResult(
                    "انتخاب سفارش معتبر الزامی است.",
                    new[] { nameof(J_ID_order) });

            if (J_ID_Production <= 0)
                yield return new ValidationResult(
                    "انتخاب محصول معتبر الزامی است.",
                    new[] { nameof(J_ID_Production) });

            if (J_id_OverheadCosts <= 0)
                yield return new ValidationResult(
                    "انتخاب هزینهٔ سرباره معتبر الزامی است.",
                    new[] { nameof(J_id_OverheadCosts) });

            // final_charge_id اختیاری است؛ اگر مقدار دارد، باید مثبت باشد
            if (final_charge_id.HasValue && final_charge_id.Value <= 0)
                yield return new ValidationResult(
                    "شناسهٔ هزینهٔ نهایی نامعتبر است.",
                    new[] { nameof(final_charge_id) });
        }
    }
}
