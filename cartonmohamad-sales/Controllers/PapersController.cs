using cartonmohamad_sales.Dtos;
using cartonmohamad_sales.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace cartonmohamad_sales.Controllers
{
    // مسیرهای نمونه:
    // GET /Papers/GetAll
    // GET /Papers/GetFiltered?minIndustrialWidth=110
    // GET /Papers/GetCategories
    public class PapersController : Controller
    {
        private readonly CartonMohamad_PriceEntities db = new CartonMohamad_PriceEntities(); // <-- نام DbContext خودت

        // کمکی برای Parse امن
        private static decimal? TryDec(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return null;
            decimal d;
            // هم نقطه، هم کاما
            if (decimal.TryParse(s.Replace('/', '.').Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out d))
                return d;
            return null;
        }

        // تمام کاغذها + کتگوری‌ها + عرض‌ها + گرماژها
        [HttpGet]
        public JsonResult GetAll()
        {
            // Joins را هم‌زمان می‌گیریم، بعد ToList تا در حافظه پردازش کنیم
            var raw = db.Tb_Paper
                        .Select(p => new
                        {
                            p.ID,
                            p.Paper_Name,
                            p.price_per_sheet,
                            p.shipping_cost,
                            Joins = p.Tb_join_Categori_Paper
                                     .Select(j => new { j.J_ID_Catgori, j.Value })
                        })
                        .ToList();

            var list = raw.Select(p =>
            {
                var cats = p.Joins
                            .GroupBy(g => g.J_ID_Catgori)
                            .ToDictionary(
                                g => g.Key.ToString(),       // کلید = ID دسته
                                g => g.Select(x => x.Value ?? string.Empty).ToList()
                            );

                var widths = p.Joins.Where(j => j.J_ID_Catgori == 1)
                                    .Select(j => TryDec(j.Value))
                                    .Where(v => v.HasValue).Select(v => v.Value)
                                    .Distinct().OrderBy(v => v).ToList();

                var grams = p.Joins.Where(j => j.J_ID_Catgori == 2)
                                   .Select(j => TryDec(j.Value))
                                   .Where(v => v.HasValue).Select(v => v.Value)
                                   .Distinct().OrderBy(v => v).ToList();

                return new PaperDto
                {
                    id = p.ID,
                    name = p.Paper_Name,
                    price = p.price_per_sheet,
                    shipping_cost = p.shipping_cost,
                    widths = widths,
                    grammages = grams,
                    categories = cats
                };
            }).ToList();

            return Json(new ApiResult<List<PaperDto>> { ok = true, data = list }, JsonRequestBehavior.AllowGet);
        }

        // برگرداندن کاغذها که حداقل یک عرض (Cat=1) >= عرض صنعتی انتخابی دارند
        [HttpGet]
        public JsonResult GetFiltered(decimal? minIndustrialWidth)
        {
            if (!minIndustrialWidth.HasValue)
                return GetAll(); // بدون فیلتر

            // ابتدا خام
            var raw = db.Tb_Paper
                        .Select(p => new
                        {
                            p.ID,
                            p.Paper_Name,
                            p.price_per_sheet,
                            p.shipping_cost,
                            Joins = p.Tb_join_Categori_Paper
                                     .Select(j => new { j.J_ID_Catgori, j.Value })
                        })
                        .ToList();

            // سپس فیلتر روی عرض‌ها
            var list = raw.Select(p =>
            {
                var widths = p.Joins.Where(j => j.J_ID_Catgori == 1)
                                    .Select(j => TryDec(j.Value))
                                    .Where(v => v.HasValue).Select(v => v.Value)
                                    .Distinct().OrderBy(v => v).ToList();

                var grams = p.Joins.Where(j => j.J_ID_Catgori == 2)
                                    .Select(j => TryDec(j.Value))
                                    .Where(v => v.HasValue).Select(v => v.Value)
                                    .Distinct().OrderBy(v => v).ToList();

                var cats = p.Joins
                            .GroupBy(g => g.J_ID_Catgori)
                            .ToDictionary(
                                g => g.Key.ToString(),
                                g => g.Select(x => x.Value ?? string.Empty).ToList()
                            );

                return new PaperDto
                {
                    id = p.ID,
                    name = p.Paper_Name,
                    price = p.price_per_sheet,
                    shipping_cost = p.shipping_cost,
                    widths = widths,
                    grammages = grams,
                    categories = cats
                };
            })
            .Where(pd => pd.widths.Any(w => w >= minIndustrialWidth.Value))
            .ToList();

            return Json(new ApiResult<List<PaperDto>> { ok = true, data = list }, JsonRequestBehavior.AllowGet);
        }

        // همه‌ی دسته‌ها (برای نمایش نام‌ها)
        [HttpGet]
        public JsonResult GetCategories()
        {
            var cats = db.Tb_Catgori
                         .Select(c => new CategoryDto { id = c.ID, name = c.Categori })
                         .OrderBy(c => c.id)
                         .ToList();

            return Json(new ApiResult<List<CategoryDto>> { ok = true, data = cats }, JsonRequestBehavior.AllowGet);
        }


        /// dfgdgfdgdfgsdg
        /// 






       


    }
}
