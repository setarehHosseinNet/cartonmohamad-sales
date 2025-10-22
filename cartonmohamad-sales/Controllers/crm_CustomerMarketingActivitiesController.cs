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
    public class crm_CustomerMarketingActivitiesController : Controller
    {
        private CartonMohamad_PriceEntities db = new CartonMohamad_PriceEntities();

        // GET: crm_CustomerMarketingActivities
        public async Task<ActionResult> Index()
        {
            var crm_CustomerMarketingActivities = db.crm_CustomerMarketingActivities.Include(c => c.Customer);
            return View(await crm_CustomerMarketingActivities.ToListAsync());
        }

        // GET: crm_CustomerMarketingActivities/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_CustomerMarketingActivities crm_CustomerMarketingActivities = await db.crm_CustomerMarketingActivities.FindAsync(id);
            if (crm_CustomerMarketingActivities == null)
            {
                return HttpNotFound();
            }
            return View(crm_CustomerMarketingActivities);
        }

        // GET: crm_CustomerMarketingActivities/Create
        public ActionResult Create()
        {
            ViewBag.J_ID_Customer = new SelectList(db.Customers, "ID", "Customer1");
            return View();
        }

        // POST: crm_CustomerMarketingActivities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,J_ID_Customer,Date,expert_name,next_contact_at,customer_grade,Descript,meeting_outcome,activity_type")] crm_CustomerMarketingActivities crm_CustomerMarketingActivities)
        {
            if (ModelState.IsValid)
            {
                db.crm_CustomerMarketingActivities.Add(crm_CustomerMarketingActivities);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.J_ID_Customer = new SelectList(db.Customers, "ID", "Customer1", crm_CustomerMarketingActivities.J_ID_Customer);
            return View(crm_CustomerMarketingActivities);
        }

        // GET: crm_CustomerMarketingActivities/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_CustomerMarketingActivities crm_CustomerMarketingActivities = await db.crm_CustomerMarketingActivities.FindAsync(id);
            if (crm_CustomerMarketingActivities == null)
            {
                return HttpNotFound();
            }
            ViewBag.J_ID_Customer = new SelectList(db.Customers, "ID", "Customer1", crm_CustomerMarketingActivities.J_ID_Customer);
            return View(crm_CustomerMarketingActivities);
        }

        // POST: crm_CustomerMarketingActivities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,J_ID_Customer,Date,expert_name,next_contact_at,customer_grade,Descript,meeting_outcome,activity_type")] crm_CustomerMarketingActivities crm_CustomerMarketingActivities)
        {
            if (ModelState.IsValid)
            {
                db.Entry(crm_CustomerMarketingActivities).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.J_ID_Customer = new SelectList(db.Customers, "ID", "Customer1", crm_CustomerMarketingActivities.J_ID_Customer);
            return View(crm_CustomerMarketingActivities);
        }

        // GET: crm_CustomerMarketingActivities/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crm_CustomerMarketingActivities crm_CustomerMarketingActivities = await db.crm_CustomerMarketingActivities.FindAsync(id);
            if (crm_CustomerMarketingActivities == null)
            {
                return HttpNotFound();
            }
            return View(crm_CustomerMarketingActivities);
        }

        // POST: crm_CustomerMarketingActivities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            crm_CustomerMarketingActivities crm_CustomerMarketingActivities = await db.crm_CustomerMarketingActivities.FindAsync(id);
            db.crm_CustomerMarketingActivities.Remove(crm_CustomerMarketingActivities);
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
