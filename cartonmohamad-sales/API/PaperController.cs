using cartonmohamad_sales.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
// using System.Data.Entity; // اگر Async EF نیاز داری
// using YourMvcProjectNamespace.Models; // جایی که DbContext و مدل‌ها هستند

namespace cartonmohamad_sales.Controllers.Api
{
    [EnableCors(origins: "*", headers: "*", methods: "*")] // اختیاری
    [RoutePrefix("api/paper")]
    public class PaperController : ApiController
    {
        private readonly CartonMohamad_PriceEntities _db = new CartonMohamad_PriceEntities();

        // DTO برای اینکه فقط فیلدهای لازم بره بیرون
        public class PaperDto
        {
            public int ID { get; set; }
            public string Paper_Name { get; set; }
            public decimal? price_per_sheet { get; set; }
            public decimal? shipping_cost { get; set; }
        }

        // GET: api/paper
        [HttpGet, Route("")]
        public IHttpActionResult GetAll(string q = null)
        {
            var query = _db.Tb_Paper.AsQueryable();

            if (!string.IsNullOrWhiteSpace(q))
                query = query.Where(x => x.Paper_Name.Contains(q));

            var data = query
                .OrderBy(x => x.Paper_Name)
                .Select(x => new PaperDto
                {
                    ID = x.ID,
                    Paper_Name = x.Paper_Name,
                    price_per_sheet = x.price_per_sheet,
                    shipping_cost = x.shipping_cost
                })
                .ToList();

            return Ok(data);
        }

        // GET: api/paper/5
        [HttpGet, Route("{id:int}")]
        public IHttpActionResult GetOne(int id)
        {
            var item = _db.Tb_Paper
                .Where(x => x.ID == id)
                .Select(x => new PaperDto
                {
                    ID = x.ID,
                    Paper_Name = x.Paper_Name,
                    price_per_sheet = x.price_per_sheet,
                    shipping_cost = x.shipping_cost
                })
                .FirstOrDefault();

            if (item == null) return NotFound();
            return Ok(item);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) _db.Dispose();
            base.Dispose(disposing);
        }
    }
}
