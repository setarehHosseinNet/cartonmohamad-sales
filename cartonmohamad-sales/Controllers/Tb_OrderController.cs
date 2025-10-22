using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using cartonmohamad_sales.Models;

namespace cartonmohamad_sales.Controllers
{
    public class Tb_OrderController : Controller
    {
        private CartonMohamad_PriceEntities db = new CartonMohamad_PriceEntities();

        // GET: Tb_Order
        public async Task<ActionResult> Index()
        {
            var tb_Order = db.Tb_Order.Include(t => t.Customer);
            return View(await tb_Order.ToListAsync());
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
        public ActionResult Create()
        {
            ViewBag.J_ID_Customer = new SelectList(db.Customers, "ID", "Customer1");
            return View();
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
