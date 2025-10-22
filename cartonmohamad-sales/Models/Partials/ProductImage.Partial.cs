using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace cartonmohamad_sales.Models
{
    public partial class ProductImage : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext ctx)
        {
            // file_url الزامی است (در Metadata هم Required داریم)
            if (string.IsNullOrWhiteSpace(file_url))
                yield return new ValidationResult("آدرس فایل تصویر الزامی است.", new[] { nameof(file_url) });

            // MIME باید به شکل type/subtype باشد، اگر مقدار داده شده
            if (!string.IsNullOrWhiteSpace(mime_type) && !mime_type.Contains("/"))
                yield return new ValidationResult("MIME type نامعتبر است (مثال: image/jpeg).", new[] { nameof(mime_type) });

            // SHA1 باید ۴۰ کاراکتر هگز باشد، اگر مقدار داده شده
            if (!string.IsNullOrWhiteSpace(checksum_sha1))
            {
                if (!Regex.IsMatch(checksum_sha1, "^[a-fA-F0-9]{40}$"))
                    yield return new ValidationResult("SHA1 باید شامل ۴۰ کاراکتر هگز باشد.", new[] { nameof(checksum_sha1) });
            }

            // ترتیب نمایش منفی نباشد (در Metadata هم Range گذاشته‌ایم)
            if (sort_order < 0)
                yield return new ValidationResult("ترتیب نمایش نمی‌تواند منفی باشد.", new[] { nameof(sort_order) });

            // ابعاد و سایز اگر داده شده‌اند، منفی نباشند (در Metadata هم Range داریم)
            if (width_px.HasValue && width_px.Value < 0)
                yield return new ValidationResult("عرض نمی‌تواند منفی باشد.", new[] { nameof(width_px) });

            if (height_px.HasValue && height_px.Value < 0)
                yield return new ValidationResult("ارتفاع نمی‌تواند منفی باشد.", new[] { nameof(height_px) });

            if (size_bytes.HasValue && size_bytes.Value < 0)
                yield return new ValidationResult("حجم نمی‌تواند منفی باشد.", new[] { nameof(size_bytes) });
        }
    }
}
