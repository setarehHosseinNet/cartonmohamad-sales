using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cartonmohamad_sales.Models
{
    // نام‌های امن برای enum (Fixed به‌جای fixed)
    public enum FinalCalcType { Percent, Fixed }
    public enum ApplyBase { subtotal, subtotal_plus_shipping, running_total }

    // دقت کن: اینجا [MetadataType] نداریم
    public partial class FinalCharge : IValidatableObject
    {
        [NotMapped]
        [Display(Name = "نوع محاسبه")]
        public FinalCalcType? CalcTypeEnum
        {
            get => string.IsNullOrWhiteSpace(calc_type)
                ? (FinalCalcType?)null
                : (FinalCalcType)Enum.Parse(typeof(FinalCalcType), calc_type, true); // "fixed"→Fixed
            set => calc_type = value?.ToString().ToLowerInvariant(); // ذخیره "percent"/"fixed"
        }

        [NotMapped]
        [Display(Name = "پایهٔ اعمال")]
        public ApplyBase? ApplyOnEnum
        {
            get => string.IsNullOrWhiteSpace(apply_on)
                ? (ApplyBase?)null
                : (ApplyBase)Enum.Parse(typeof(ApplyBase), apply_on, true);
            set => apply_on = value?.ToString();
        }

        // ولیدیشن ترکیبی سمت سرور
        public IEnumerable<ValidationResult> Validate(ValidationContext ctx)
        {
            if (string.Equals(calc_type, "percent", StringComparison.OrdinalIgnoreCase))
            {
                if (percent_rate == null)
                    yield return new ValidationResult("نرخ درصدی الزامی است.", new[] { nameof(percent_rate) });
                if (fixed_amount_irr != null)
                    yield return new ValidationResult("در حالت درصدی، مبلغ ثابت نباید مقدار داشته باشد.", new[] { nameof(fixed_amount_irr) });
            }
            else if (string.Equals(calc_type, "fixed", StringComparison.OrdinalIgnoreCase))
            {
                if (fixed_amount_irr == null)
                    yield return new ValidationResult("مبلغ ثابت (ریال) الزامی است.", new[] { nameof(fixed_amount_irr) });
                if (percent_rate != null)
                    yield return new ValidationResult("در حالت ثابت، درصد نباید مقدار داشته باشد.", new[] { nameof(percent_rate) });
            }

            if (priority < 1)
                yield return new ValidationResult("اولویت باید عددی ≥ ۱ باشد.", new[] { nameof(priority) });
        }
    }
}
