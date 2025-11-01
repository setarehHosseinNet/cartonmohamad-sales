using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Data.Entity;
using System.Reflection;
using cartonmohamad_sales.Models;

namespace cartonmohamad_sales.Controllers
{
    [RoutePrefix("api/inquiry")]
    public class InquiryController : ApiController
    {
        private readonly CartonMohamad_PriceEntities db = new CartonMohamad_PriceEntities();

        // ========= DTO =========
        public class InquiryRequest
        {
            public int? J_ID_Customer { get; set; }
            public string CustomerName { get; set; }
            public string Number_Order { get; set; }
            public string Date { get; set; }

            public string Product_Code { get; set; }
            public string Product_Type { get; set; }
            public string Product_Name { get; set; }
            public int? Product_Tirage { get; set; }
            public string Product_Desc { get; set; }

            public string Layers_Count { get; set; }
            public string Pieces_Count { get; set; }
            public string Door_Type { get; set; }

            public decimal? LengthCm { get; set; }
            public decimal? WidthCm { get; set; }
            public decimal? HeightCm { get; set; }

            public string Flute_Type { get; set; }   // C,B,E,CB,CE,BE
            public string Order_Status { get; set; }   // created, confirmed, ...
        }

        // ========= Helpers =========
        private static bool IsNullableType(Type t) => Nullable.GetUnderlyingType(t) != null;

        private static object ConvertTo(object value, Type targetType)
        {
            if (value == null) return null;
            var nn = Nullable.GetUnderlyingType(targetType) ?? targetType;
            try { return Convert.ChangeType(value, nn, CultureInfo.InvariantCulture); }
            catch
            {
                if (value is int i)
                { if (nn == typeof(byte)) return (byte)i; if (nn == typeof(long)) return (long)i; if (nn == typeof(short)) return (short)i; if (nn == typeof(decimal)) return (decimal)i; }
                if (value is decimal d)
                { if (nn == typeof(long)) return (long)d; if (nn == typeof(int)) return (int)d; if (nn == typeof(byte)) return (byte)d; }
                if (value is string s)
                {
                    if (nn == typeof(int) && int.TryParse(s, out var ii)) return ii;
                    if (nn == typeof(long) && long.TryParse(s, out var ll)) return ll;
                    if (nn == typeof(byte) && byte.TryParse(s, out var bb)) return bb;
                    if (nn == typeof(decimal) && decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out var dd)) return dd;
                    if (nn == typeof(DateTime) && DateTime.TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out var dt)) return dt;
                }
                return value;
            }
        }

        private static void SetProp(object entity, string propName, object value)
        {
            if (entity == null) return;
            var pi = entity.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (pi == null) return;
            if (value == null && !IsNullableType(pi.PropertyType)) return;
            var converted = ConvertTo(value, pi.PropertyType);
            if (converted == null && !IsNullableType(pi.PropertyType)) return;
            try { pi.SetValue(entity, converted, null); } catch { }
        }

        private static bool TrySetAnyProp(object entity, object value, params string[] names)
        {
            foreach (var n in names)
            {
                var pi = entity.GetType().GetProperty(n, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (pi != null)
                {
                    var conv = ConvertTo(value, pi.PropertyType);
                    try { pi.SetValue(entity, conv, null); return true; } catch { }
                }
            }
            return false;
        }

        private static int? ParseNullableInt(string s)
            => string.IsNullOrWhiteSpace(s) ? (int?)null : (int.TryParse(s.Trim(), out var n) ? n : (int?)null);
        private static byte? ToNullableByte(string s)
            => string.IsNullOrWhiteSpace(s) ? (byte?)null : (byte.TryParse(s.Trim(), out var b) ? (byte?)b : null);
        private static int? NormalizePieces(string pieces)
        {
            if (string.IsNullOrWhiteSpace(pieces)) return null;
            if (pieces.Equals("half", StringComparison.OrdinalIgnoreCase)) return 0;
            return int.TryParse(pieces.Trim(), out var n) ? n : (int?)null;
        }
        private static string NormalizeFlute(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return "C";
            s = s.Trim().ToUpperInvariant();
            switch (s) { case "C": case "B": case "E": case "CB": case "CE": case "BE": return s; default: return "C"; }
        }
        private static string NormalizeOrderStatus(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return "created";
            s = s.Trim().ToLowerInvariant();
            if (s.Contains("ایستعلام") || s.Contains("جدید")) return "created";
            if (s.Contains("تایید")) return "confirmed";
            if (s.Contains("تولید")) return "in_production";
            if (s.Contains("ارسال")) return "shipped";
            if (s.Contains("لغو")) return "cancelled";
            switch (s)
            {
                case "created": case "confirmed": case "in_production": case "shipped": case "cancelled": return s;
                case "inquiry": return "created";
                default: return "created";
            }
        }

        // روش محاسبه معتبر
        private static string NormalizeCalcMethod(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return "fixed";
            s = s.Trim().ToLowerInvariant();
            return (s == "per_length" || s == "per_piece" || s == "fixed") ? s : "fixed";
        }

        // null کردن ایمن یک پراپرتی (فقط اگر nullable باشد)
        private static void ClearNullable(object entity, params string[] names)
        {
            foreach (var n in names)
            {
                var pi = entity.GetType().GetProperty(n, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (pi == null) continue;
                if (IsNullableType(pi.PropertyType))
                {
                    try { pi.SetValue(entity, null, null); } catch { }
                }
            }
        }

        // مقداردهی پیش‌فرض امن (به‌جز فیلدهای خاص)
        private static void EnsureNonNullDefaults(object entity)
        {
            if (entity == null) return;
            var props = entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var p in props)
            {
                if (!p.CanRead || !p.CanWrite) continue;
                var t = Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType;

                // استثناء: فیلدهای حساس که ممکن است باید null باشند
                var name = p.Name.ToLowerInvariant();
                if (name == "flute_type" || name == "qty" || name == "quantity" || name == "qty_per_meter")
                    continue;

                if (t == typeof(string))
                {
                    if (p.GetValue(entity) == null) { try { p.SetValue(entity, "-"); } catch { } }
                    continue;
                }
                if (!IsNullableType(p.PropertyType) && t.IsValueType && p.GetValue(entity) == null)
                {
                    try
                    {
                        object def =
                            t == typeof(DateTime) ? (object)DateTime.Now :
                            t == typeof(decimal) ? (object)0m :
                            t == typeof(double) ? (object)0d :
                            t == typeof(float) ? (object)0f :
                            t == typeof(long) ? (object)0L :
                            t == typeof(int) ? (object)0 :
                            t == typeof(short) ? (object)(short)0 :
                            t == typeof(byte) ? (object)(byte)0 :
                            Activator.CreateInstance(t);
                        p.SetValue(entity, def);
                    }
                    catch { }
                }
            }
        }

        // ========= Endpoint =========
        [HttpPost, Route("")]
        public IHttpActionResult CreateInquiry([FromBody] InquiryRequest req)
        {
            try
            {
                if (req == null) return Content(HttpStatusCode.BadRequest, new { ok = false, message = "بدنهٔ درخواست خالی است." });
                if (string.IsNullOrWhiteSpace(req.Product_Type))
                    return Content(HttpStatusCode.BadRequest, new { ok = false, message = "نوع کارتن الزامی است." });
                if (string.IsNullOrWhiteSpace(req.Product_Code) && string.IsNullOrWhiteSpace(req.Product_Name))
                    return Content(HttpStatusCode.BadRequest, new { ok = false, message = "کد یا نام محصول را ارسال کنید." });

                db.Database.Log = s => System.Diagnostics.Debug.WriteLine("EF: " + s);

                // مشتری
                int? customerId = req.J_ID_Customer;
                if (!customerId.HasValue && !string.IsNullOrWhiteSpace(req.CustomerName))
                {
                    var c = db.Set<Customer>().FirstOrDefault(x => x.Customer1 == req.CustomerName);
                    if (c != null) customerId = c.ID;
                }
                if (!customerId.HasValue)
                    return Content(HttpStatusCode.BadRequest, new { ok = false, message = "مشتری نامشخص است." });

                // تاریخ
                DateTime orderDate = DateTime.Now;
                if (!string.IsNullOrWhiteSpace(req.Date))
                    DateTime.TryParse(req.Date, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out orderDate);

                using (var tx = db.Database.BeginTransaction())
                {
                    // ---------- Product ----------
                    var product = !string.IsNullOrWhiteSpace(req.Product_Code)
                        ? db.Set<Product>().FirstOrDefault(p => p.product_code == req.Product_Code)
                        : null;
                    if (product == null) { product = new Product(); db.Set<Product>().Add(product); }

                    SetProp(product, nameof(Product.product_code), req.Product_Code);
                    SetProp(product, nameof(Product.product_name), req.Product_Name);
                    SetProp(product, nameof(Product.product_type), req.Product_Type);
                    if (req.Product_Tirage.HasValue) SetProp(product, nameof(Product.tirage), req.Product_Tirage.Value);

                    var lv = ToNullableByte(req.Layers_Count); if (lv.HasValue) SetProp(product, nameof(Product.layers_count), lv.Value);
                    var pv = NormalizePieces(req.Pieces_Count); if (pv.HasValue) SetProp(product, nameof(Product.pieces_count), pv.Value);
                    SetProp(product, nameof(Product.door_type), req.Door_Type);

                    if (req.LengthCm.HasValue) SetProp(product, nameof(Product.length_cm), req.LengthCm.Value);
                    if (req.WidthCm.HasValue) SetProp(product, nameof(Product.width_cm), req.WidthCm.Value);
                    if (req.HeightCm.HasValue) SetProp(product, nameof(Product.height_cm), req.HeightCm.Value);

                    if (product.GetType().GetProperty("flute_type") != null)
                        SetProp(product, "flute_type", NormalizeFlute(req.Flute_Type));

                    if (product.GetType().GetProperty("notes") != null && !string.IsNullOrWhiteSpace(req.Product_Desc))
                        SetProp(product, "notes", req.Product_Desc);

                    EnsureNonNullDefaults(product);

                    var v1 = db.Entry(product).GetValidationResult();
                    if (!v1.IsValid)
                        return Content(HttpStatusCode.BadRequest, new { ok = false, entity = "Product", message = "EntityValidation", errors = v1.ValidationErrors.Select(e => e.PropertyName + ": " + e.ErrorMessage).ToArray() });

                    db.SaveChanges();

                    // ---------- Tb_Order ----------
                    var order = new Tb_Order();
                    SetProp(order, nameof(Tb_Order.J_ID_Customer), customerId.Value);
                    SetProp(order, nameof(Tb_Order.Status), NormalizeOrderStatus(req.Order_Status));
                    SetProp(order, nameof(Tb_Order.Date), orderDate);

                    var numOrder = ParseNullableInt(req.Number_Order);
                    var piNo = typeof(Tb_Order).GetProperty(nameof(Tb_Order.Number_order));
                    if (piNo != null)
                    {
                        var tno = Nullable.GetUnderlyingType(piNo.PropertyType) ?? piNo.PropertyType;
                        if (tno == typeof(int) || tno == typeof(long) || tno == typeof(short))
                            SetProp(order, nameof(Tb_Order.Number_order), numOrder ?? 0);
                        else
                            SetProp(order, nameof(Tb_Order.Number_order), string.IsNullOrWhiteSpace(req.Number_Order) ? "-" : req.Number_Order);
                    }

                    SetProp(order, nameof(Tb_Order.J_ID_Facktor), null);
                    SetProp(order, nameof(Tb_Order.Number_ORder_rahkaran), null);

                    EnsureNonNullDefaults(order);

                    var v2 = db.Entry(order).GetValidationResult();
                    if (!v2.IsValid)
                        return Content(HttpStatusCode.BadRequest, new { ok = false, entity = "Tb_Order", message = "EntityValidation", errors = v2.ValidationErrors.Select(e => e.PropertyName + ": " + e.ErrorMessage).ToArray() });

                    db.Set<Tb_Order>().Add(order);
                    db.SaveChanges(); // order.ID

                    // ---------- OverheadCost ----------
                    var ov = db.Set<OverheadCost>().FirstOrDefault();
                    if (ov == null)
                    {
                        ov = new OverheadCost();

                        // عمومی
                        TrySetAnyProp(ov, "DEFAULT", "name", "title");
                        TrySetAnyProp(ov, false, "is_required", "required");

                        // calc_method معتبر
                        var calc = NormalizeCalcMethod(null);
                        TrySetAnyProp(ov, calc, "calc_method", "calc_method_name");

                        // مقداردهی مطابق calc_method
                        ApplyOverheadQuantitiesByCalc(ov, calc);

                        // عددی‌های دیگر
                        TrySetAnyProp(ov, 0m, "unit_price_irr", "unit_price", "unit_price_ir");
                        TrySetAnyProp(ov, 0m, "price", "total_price", "amount");
                        TrySetAnyProp(ov, false, "show_on_invoice", "show_invoice");

                        EnsureNonNullDefaults(ov);
                        db.Set<OverheadCost>().Add(ov);
                        db.SaveChanges();
                    }
                    else
                    {
                        // اصلاح روش محاسبه و مقادیر مرتبط
                        string calc = "fixed";
                        var piCalc = ov.GetType().GetProperty("calc_method", BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                        if (piCalc != null)
                        {
                            calc = NormalizeCalcMethod(piCalc.GetValue(ov) as string);
                            piCalc.SetValue(ov, calc);
                        }
                        ApplyOverheadQuantitiesByCalc(ov, calc);
                        db.SaveChanges();
                    }

                    // ---------- Tb_Body_Order ----------
                    var body = new Tb_Body_Order { Tb_Order = order, Product = product };

                    TrySetAnyProp(body, order.ID, "J_ID_Order", "J_ID_order", "order_id", "OrderId");
                    TrySetAnyProp(body, product.product_id, "J_ID_Production", "product_id", "J_ID_Product", "ProductId");
                    TrySetAnyProp(body, ov.overhead_id, "J_id_OverheadCosts", "overhead_id", "OverheadCostId");

                    SetProp(body, "OverheadCost", ov);

                    var pQty = body.GetType().GetProperty("qty", BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                    if (pQty != null) { try { pQty.SetValue(body, (long)(req.Product_Tirage ?? 0)); } catch { } }

                    EnsureNonNullDefaults(body);

                    var v3 = db.Entry(body).GetValidationResult();
                    if (!v3.IsValid)
                        return Content(HttpStatusCode.BadRequest, new { ok = false, entity = "Tb_Body_Order", message = "EntityValidation", errors = v3.ValidationErrors.Select(e => e.PropertyName + ": " + e.ErrorMessage).ToArray() });

                    db.Set<Tb_Body_Order>().Add(body);
                    db.SaveChanges();

                    tx.Commit();

                    return Ok(new
                    {
                        ok = true,
                        productId = product.product_id,
                        orderId = order.ID,
                        bodyOverheadId = ov.overhead_id,
                        message = "ثبت شد."
                    });
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException vex)
            {
                var errs = vex.EntityValidationErrors
                              .SelectMany(e => e.ValidationErrors)
                              .Select(e => e.PropertyName + ": " + e.ErrorMessage)
                              .ToArray();
                return Content(HttpStatusCode.BadRequest, new { ok = false, message = "EntityValidation", errors = errs });
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new
                {
                    ok = false,
                    message = ex.Message,
                    inner = ex.InnerException?.Message,
                    stack = ex.StackTrace
                });
            }
        }

        // اعمال مقادیر qty/qty_per_meter بر اساس روش محاسبه
        private static void ApplyOverheadQuantitiesByCalc(object ov, string calc)
        {
            // اول هر دو را پاک می‌کنیم تا اگر nullable هستند، واقعاً null شوند.
            ClearNullable(ov, "qty", "quantity");
            ClearNullable(ov, "qty_per_meter");

            if (string.Equals(calc, "per_piece", StringComparison.OrdinalIgnoreCase))
            {
                // qty = 1 (اگر فیلد وجود دارد)
                TrySetAnyProp(ov, 1, "qty", "quantity");
                // qty_per_meter = null (همان پاک کردن بالا)
            }
            else if (string.Equals(calc, "per_length", StringComparison.OrdinalIgnoreCase))
            {
                // qty_per_meter = 1
                TrySetAnyProp(ov, 1, "qty_per_meter");
                // qty = null (همان پاک کردن بالا)
            }
            // fixed ⇒ هر دو null باقی می‌مانند
        }
    }
}
