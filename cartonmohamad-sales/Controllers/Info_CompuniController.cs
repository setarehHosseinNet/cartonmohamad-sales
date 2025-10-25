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
    public class Info_CompuniController : Controller
    {
        private CartonMohamad_PriceEntities db = new CartonMohamad_PriceEntities();

        // GET: Info_Compuni
        public async Task<ActionResult> Index()
        {
            var info_Compuni = db.Info_Compuni.Include(i => i.Customer);
            return View(await info_Compuni.ToListAsync());
        }

        // GET: Info_Compuni/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Info_Compuni info_Compuni = await db.Info_Compuni.FindAsync(id);
            if (info_Compuni == null)
            {
                return HttpNotFound();
            }
            return View(info_Compuni);
        }

        // GET: Info_Compuni/Create
        public ActionResult Create()
        {
            ViewBag.J_ID_Customer = new SelectList(db.Customers, "ID", "Customer1");
            return View();
        }

        // POST: Info_Compuni/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,J_ID_Customer,address_factory,address_sales_office,address_warehouse,address_dispatch,phone_factory,phone_sales_office,phone_procurement_manager")] Info_Compuni info_Compuni)
        {
            if (ModelState.IsValid)
            {
                db.Info_Compuni.Add(info_Compuni);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.J_ID_Customer = new SelectList(db.Customers, "ID", "Customer1", info_Compuni.J_ID_Customer);
            return View(info_Compuni);
        }

        // GET: Info_Compuni/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Info_Compuni info_Compuni = await db.Info_Compuni.FindAsync(id);
            if (info_Compuni == null)
            {
                return HttpNotFound();
            }
            ViewBag.J_ID_Customer = new SelectList(db.Customers, "ID", "Customer1", info_Compuni.J_ID_Customer);
            return View(info_Compuni);
        }

        // POST: Info_Compuni/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,J_ID_Customer,address_factory,address_sales_office,address_warehouse,address_dispatch,phone_factory,phone_sales_office,phone_procurement_manager")] Info_Compuni info_Compuni)
        {
            if (ModelState.IsValid)
            {
                db.Entry(info_Compuni).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.J_ID_Customer = new SelectList(db.Customers, "ID", "Customer1", info_Compuni.J_ID_Customer);
            return View(info_Compuni);
        }

        // GET: Info_Compuni/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Info_Compuni info_Compuni = await db.Info_Compuni.FindAsync(id);
            if (info_Compuni == null)
            {
                return HttpNotFound();
            }
            return View(info_Compuni);
        }

        // POST: Info_Compuni/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Info_Compuni info_Compuni = await db.Info_Compuni.FindAsync(id);
            db.Info_Compuni.Remove(info_Compuni);
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
