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
    public class Tb_CatgoriController : Controller
    {
        private CartonMohamad_PriceEntities db = new CartonMohamad_PriceEntities();

        // GET: Tb_Catgori
        public async Task<ActionResult> Index()
        {
            return View(await db.Tb_Catgori.ToListAsync());
        }

        // GET: Tb_Catgori/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Catgori tb_Catgori = await db.Tb_Catgori.FindAsync(id);
            if (tb_Catgori == null)
            {
                return HttpNotFound();
            }
            return View(tb_Catgori);
        }

        // GET: Tb_Catgori/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tb_Catgori/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Categori")] Tb_Catgori tb_Catgori)
        {
            if (ModelState.IsValid)
            {
                db.Tb_Catgori.Add(tb_Catgori);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tb_Catgori);
        }

        // GET: Tb_Catgori/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Catgori tb_Catgori = await db.Tb_Catgori.FindAsync(id);
            if (tb_Catgori == null)
            {
                return HttpNotFound();
            }
            return View(tb_Catgori);
        }

        // POST: Tb_Catgori/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Categori")] Tb_Catgori tb_Catgori)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_Catgori).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tb_Catgori);
        }

        // GET: Tb_Catgori/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Catgori tb_Catgori = await db.Tb_Catgori.FindAsync(id);
            if (tb_Catgori == null)
            {
                return HttpNotFound();
            }
            return View(tb_Catgori);
        }

        // POST: Tb_Catgori/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Tb_Catgori tb_Catgori = await db.Tb_Catgori.FindAsync(id);
            db.Tb_Catgori.Remove(tb_Catgori);
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
