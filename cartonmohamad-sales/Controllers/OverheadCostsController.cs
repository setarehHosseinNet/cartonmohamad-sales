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
    public class OverheadCostsController : Controller
    {
        private CartonMohamad_PriceEntities db = new CartonMohamad_PriceEntities();

        // GET: OverheadCosts
        public async Task<ActionResult> Index()
        {
            return View(await db.OverheadCosts.ToListAsync());
        }

        // GET: OverheadCosts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OverheadCost overheadCost = await db.OverheadCosts.FindAsync(id);
            if (overheadCost == null)
            {
                return HttpNotFound();
            }
            return View(overheadCost);
        }

        // GET: OverheadCosts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OverheadCosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "overhead_id,name,is_required,calc_method,quantity,qty_per_meter,unit_price_irr,show_on_invoice,price")] OverheadCost overheadCost)
        {
            if (ModelState.IsValid)
            {
                db.OverheadCosts.Add(overheadCost);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(overheadCost);
        }

        // GET: OverheadCosts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OverheadCost overheadCost = await db.OverheadCosts.FindAsync(id);
            if (overheadCost == null)
            {
                return HttpNotFound();
            }
            return View(overheadCost);
        }

        // POST: OverheadCosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "overhead_id,name,is_required,calc_method,quantity,qty_per_meter,unit_price_irr,show_on_invoice,price")] OverheadCost overheadCost)
        {
            if (ModelState.IsValid)
            {
                db.Entry(overheadCost).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(overheadCost);
        }

        // GET: OverheadCosts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OverheadCost overheadCost = await db.OverheadCosts.FindAsync(id);
            if (overheadCost == null)
            {
                return HttpNotFound();
            }
            return View(overheadCost);
        }

        // POST: OverheadCosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            OverheadCost overheadCost = await db.OverheadCosts.FindAsync(id);
            db.OverheadCosts.Remove(overheadCost);
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
