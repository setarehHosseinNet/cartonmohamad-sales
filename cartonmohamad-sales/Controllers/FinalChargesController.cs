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
    public class FinalChargesController : Controller
    {
        private CartonMohamad_PriceEntities db = new CartonMohamad_PriceEntities();

        // GET: FinalCharges
        public async Task<ActionResult> Index()
        {
            return View(await db.FinalCharges.ToListAsync());
        }

        // GET: FinalCharges/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FinalCharge finalCharge = await db.FinalCharges.FindAsync(id);
            if (finalCharge == null)
            {
                return HttpNotFound();
            }
            return View(finalCharge);
        }

        // GET: FinalCharges/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FinalCharges/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "final_charge_id,name,is_active,is_required,calc_type,priority,apply_on,percent_rate,fixed_amount_irr")] FinalCharge finalCharge)
        {
            if (ModelState.IsValid)
            {
                db.FinalCharges.Add(finalCharge);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(finalCharge);
        }

        // GET: FinalCharges/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FinalCharge finalCharge = await db.FinalCharges.FindAsync(id);
            if (finalCharge == null)
            {
                return HttpNotFound();
            }
            return View(finalCharge);
        }

        // POST: FinalCharges/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "final_charge_id,name,is_active,is_required,calc_type,priority,apply_on,percent_rate,fixed_amount_irr")] FinalCharge finalCharge)
        {
            if (ModelState.IsValid)
            {
                db.Entry(finalCharge).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(finalCharge);
        }

        // GET: FinalCharges/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FinalCharge finalCharge = await db.FinalCharges.FindAsync(id);
            if (finalCharge == null)
            {
                return HttpNotFound();
            }
            return View(finalCharge);
        }

        // POST: FinalCharges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            FinalCharge finalCharge = await db.FinalCharges.FindAsync(id);
            db.FinalCharges.Remove(finalCharge);
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
