using cartonmohamad_sales.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using cartonmohamad_sales.ViewModels;

using System.Web.Mvc;

namespace cartonmohamad_sales.Controllers
{
    public class Tb_OrderController : Controller
    {
        private CartonMohamad_PriceEntities db = new CartonMohamad_PriceEntities();
        private const int PAGE_SIZE = 20;
        [HttpGet]
        public async Task<ActionResult> Index(int? customerId)
        {
            var q = db.Tb_Order
                      .Include(o => o.Customer)
                      .AsQueryable();

            if (customerId.HasValue)
            {
                q = q.Where(o => o.J_ID_Customer == customerId.Value);
                var cust = await db.Customers.FindAsync(customerId.Value);
                ViewBag.CustomerId = customerId.Value;
                ViewBag.CustomerName = cust?.Customer1 ?? "نامشخص";
            }

            var list = await q.OrderByDescending(o => o.Date).ToListAsync();
            return View(list);
        }
        // GET: Tb_Order/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Order tb_Order = await db.Tb_Order.FindAsync(id);
            if (tb_Order == null)
            {
                return HttpNotFound();
            }
            return View(tb_Order);
        }


        // GET: Tb_Order/Create
        // GET: Tb_Order/Create
        public ActionResult Create(int? customerId, int? J_ID_Customer)
        {
            // هر کدام بود، همان را استفاده کن
            var id = J_ID_Customer ?? customerId;

            // لیست مشتری‌ها برای دراپ‌دان؛ اگر id داشت، همون رو انتخاب کن
            var customers = db.Customers
                              .AsNoTracking()
                              .OrderBy(c => c.Customer1)
                              .ToList();

            ViewBag.J_ID_Customer = new SelectList(customers, "ID", "Customer1", id);

            // مدل سفارش
            var model = new Tb_Order
            {
                J_ID_Customer = id ?? 0,               // اگر نداشت 0 می‌ماند (یا خالی نگه دار اگر nullable است)
                Date = DateTime.Now                    // اگر فیلد Date nullable است می‌تونی این خط رو برداری
            };

            // اگر id داریم، اطلاعات مشتری را هم برای ویو بفرست
            if (id.HasValue)
            {
                var customer = db.Customers.Find(id.Value);
                if (customer == null) return HttpNotFound();

                // برای نمایش در ویو
                ViewBag.Customer = customer;
            }







            return View(model);
        }

        // POST: Tb_Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,J_ID_Customer,Number_order,J_ID_Facktor,Number_ORder_rahkaran,Status,Date")] Tb_Order tb_Order)
        {
            if (ModelState.IsValid)
            {
                db.Tb_Order.Add(tb_Order);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.J_ID_Customer = new SelectList(db.Customers, "ID", "Customer1", tb_Order.J_ID_Customer);
            return View(tb_Order);
        }

        // GET: Tb_Order/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Order tb_Order = await db.Tb_Order.FindAsync(id);
            if (tb_Order == null)
            {
                return HttpNotFound();
            }
            ViewBag.J_ID_Customer = new SelectList(db.Customers, "ID", "Customer1", tb_Order.J_ID_Customer);
            return View(tb_Order);
        }

        // POST: Tb_Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,J_ID_Customer,Number_order,J_ID_Facktor,Number_ORder_rahkaran,Status,Date")] Tb_Order tb_Order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_Order).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.J_ID_Customer = new SelectList(db.Customers, "ID", "Customer1", tb_Order.J_ID_Customer);
            return View(tb_Order);
        }

        // GET: Tb_Order/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Order tb_Order = await db.Tb_Order.FindAsync(id);
            if (tb_Order == null)
            {
                return HttpNotFound();
            }
            return View(tb_Order);
        }

        // POST: Tb_Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Tb_Order tb_Order = await db.Tb_Order.FindAsync(id);
            db.Tb_Order.Remove(tb_Order);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        // GET: /Tb_Order/Inquiries
        [HttpGet]
        public ActionResult Inquiries(string q = null, int page = 1, int pageSize = 20)
        {
            var orders = db.Tb_Order
                           .Include(o => o.Customer)
                           .Where(o => o.Status == "created"); // «ارسال برای استعلام»

            if (!string.IsNullOrWhiteSpace(q))
            {
                var qq = q.Trim();
                orders = orders.Where(o =>
                    o.Customer.Customer1.Contains(qq) ||
                    (o.Number_order != null && o.Number_order.ToString().Contains(qq)));
            }

            var total = orders.Count();
            var rows = orders.OrderByDescending(o => o.Date)
                             .Skip((page - 1) * pageSize)
                             .Take(pageSize)
                             .Select(o => new InqRowVM
                             {
                                 OrderId =(int) o.ID,
                                 CustomerName = o.Customer.Customer1,
                                 NumberOrder = (o.Number_order == null ? "-" : o.Number_order.ToString()),
                                 Date = o.Date ?? DateTime.Now,
                                 Status = o.Status
                             })
                             .ToList();

            var vm = new InquiriesVM
            {
                Page = page,
                PageSize = pageSize,
                Total = total,
                Q = q,
                Items = rows
            };

            return View(vm); // به ویوی همنام برمی‌گردیم
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    public class InquiriesVM
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
        public string Q { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public System.Collections.Generic.List<InqRowVM> Items { get; set; }
    }

    public class InqRowVM
    {
        public int OrderId { get; set; }
        public object NumberOrder { get; set; } // ممکن است int یا string باشد
        public DateTime Date { get; set; }
        public string CustomerName { get; set; }
        public string Status { get; set; }
    }



}
