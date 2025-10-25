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
    public class Tb_PaperController : Controller
    {
        private CartonMohamad_PriceEntities db = new CartonMohamad_PriceEntities();

        // GET: Tb_Paper
        public async Task<ActionResult> Index()
        {
            return View(await db.Tb_Paper.ToListAsync());
        }

        // GET: Tb_Paper/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Paper tb_Paper = await db.Tb_Paper.FindAsync(id);
            if (tb_Paper == null)
            {
                return HttpNotFound();
            }
            return View(tb_Paper);
        }

        // GET: Tb_Paper/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tb_Paper/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Paper_Name,price_per_sheet,shipping_cost")] Tb_Paper tb_Paper)
        {
            if (ModelState.IsValid)
            {
                db.Tb_Paper.Add(tb_Paper);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tb_Paper);
        }

        // GET: Tb_Paper/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Paper tb_Paper = await db.Tb_Paper.FindAsync(id);
            if (tb_Paper == null)
            {
                return HttpNotFound();
            }
            return View(tb_Paper);
        }

        // POST: Tb_Paper/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Paper_Name,price_per_sheet,shipping_cost")] Tb_Paper tb_Paper)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_Paper).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tb_Paper);
        }

        // GET: Tb_Paper/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Paper tb_Paper = await db.Tb_Paper.FindAsync(id);
            if (tb_Paper == null)
            {
                return HttpNotFound();
            }
            return View(tb_Paper);
        }

        // POST: Tb_Paper/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Tb_Paper tb_Paper = await db.Tb_Paper.FindAsync(id);
            db.Tb_Paper.Remove(tb_Paper);
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
