using System;
using System.Web.Mvc;
using cartonmohamad_sales.Utilities;

namespace cartonmohamad_sales.Infrastructure
{
    public class PersianDateModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (value == null) return bindingContext.ModelType == typeof(DateTime?) ? (DateTime?)null : default(DateTime);

            var raw = value.AttemptedValue;

            // اگر ورودی واقعاً DateTime بود (از AJAX/JSON)، همون رو بده
            if (DateTime.TryParse(raw, out var dt))
                return Convert.ChangeType(dt, bindingContext.ModelType);

            var p = PersianDateExtensions.ParsePersianDate(raw);
            if (p.HasValue)
            {
                if (bindingContext.ModelType == typeof(DateTime?)) return p.Value;
                return p.Value;
            }

            bindingContext.ModelState.AddModelError(bindingContext.ModelName, "تاریخ نامعتبر است.");
            return bindingContext.ModelType == typeof(DateTime?) ? (DateTime?)null : default(DateTime);
        }
    }
}
