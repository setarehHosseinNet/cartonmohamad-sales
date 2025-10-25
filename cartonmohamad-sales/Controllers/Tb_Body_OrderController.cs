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
    public class Tb_Body_OrderController : Controller
    {
        private CartonMohamad_PriceEntities db = new CartonMohamad_PriceEntities();

        // GET: Tb_Body_Order
        public async Task<ActionResult> Index()
        {
            var tb_Body_Order = db.Tb_Body_Order.Include(t => t.FinalCharge).Include(t => t.OverheadCost).Include(t => t.Product).Include(t => t.Tb_Order);
            return View(await tb_Body_Order.ToListAsync());
        }

        // GET: Tb_Body_Order/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Body_Order tb_Body_Order = await db.Tb_Body_Order.FindAsync(id);
            if (tb_Body_Order == null)
            {
                return HttpNotFound();
            }
            return View(tb_Body_Order);
        }

        // GET: Tb_Body_Order/Create
        public ActionResult Create()
        {
            ViewBag.final_charge_id = new SelectList(db.FinalCharges, "final_charge_id", "name");
            ViewBag.J_id_OverheadCosts = new SelectList(db.OverheadCosts, "overhead_id", "name");
            ViewBag.J_ID_Production = new SelectList(db.Products, "product_id", "product_code");
            ViewBag.J_ID_order = new SelectList(db.Tb_Order, "ID", "Number_ORder_rahkaran");
            return View();
        }

        // POST: Tb_Body_Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,J_ID_order,J_ID_Production,final_charge_id,J_id_OverheadCosts")] Tb_Body_Order tb_Body_Order)
        {
            if (ModelState.IsValid)
            {
                db.Tb_Body_Order.Add(tb_Body_Order);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.final_charge_id = new SelectList(db.FinalCharges, "final_charge_id", "name", tb_Body_Order.final_charge_id);
            ViewBag.J_id_OverheadCosts = new SelectList(db.OverheadCosts, "overhead_id", "name", tb_Body_Order.J_id_OverheadCosts);
            ViewBag.J_ID_Production = new SelectList(db.Products, "product_id", "product_code", tb_Body_Order.J_ID_Production);
            ViewBag.J_ID_order = new SelectList(db.Tb_Order, "ID", "Number_ORder_rahkaran", tb_Body_Order.J_ID_order);
            return View(tb_Body_Order);
        }

        // GET: Tb_Body_Order/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Body_Order tb_Body_Order = await db.Tb_Body_Order.FindAsync(id);
            if (tb_Body_Order == null)
            {
                return HttpNotFound();
            }
            ViewBag.final_charge_id = new SelectList(db.FinalCharges, "final_charge_id", "name", tb_Body_Order.final_charge_id);
            ViewBag.J_id_OverheadCosts = new SelectList(db.OverheadCosts, "overhead_id", "name", tb_Body_Order.J_id_OverheadCosts);
            ViewBag.J_ID_Production = new SelectList(db.Products, "product_id", "product_code", tb_Body_Order.J_ID_Production);
            ViewBag.J_ID_order = new SelectList(db.Tb_Order, "ID", "Number_ORder_rahkaran", tb_Body_Order.J_ID_order);
            return View(tb_Body_Order);
        }

        // POST: Tb_Body_Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,J_ID_order,J_ID_Production,final_charge_id,J_id_OverheadCosts")] Tb_Body_Order tb_Body_Order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_Body_Order).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.final_charge_id = new SelectList(db.FinalCharges, "final_charge_id", "name", tb_Body_Order.final_charge_id);
            ViewBag.J_id_OverheadCosts = new SelectList(db.OverheadCosts, "overhead_id", "name", tb_Body_Order.J_id_OverheadCosts);
            ViewBag.J_ID_Production = new SelectList(db.Products, "product_id", "product_code", tb_Body_Order.J_ID_Production);
            ViewBag.J_ID_order = new SelectList(db.Tb_Order, "ID", "Number_ORder_rahkaran", tb_Body_Order.J_ID_order);
            return View(tb_Body_Order);
        }

        // GET: Tb_Body_Order/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Body_Order tb_Body_Order = await db.Tb_Body_Order.FindAsync(id);
            if (tb_Body_Order == null)
            {
                return HttpNotFound();
            }
            return View(tb_Body_Order);
        }

        // POST: Tb_Body_Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Tb_Body_Order tb_Body_Order = await db.Tb_Body_Order.FindAsync(id);
            db.Tb_Body_Order.Remove(tb_Body_Order);
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
