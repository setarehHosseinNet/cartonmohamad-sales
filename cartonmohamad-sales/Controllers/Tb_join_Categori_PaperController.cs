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
    public class Tb_join_Categori_PaperController : Controller
    {
        private CartonMohamad_PriceEntities db = new CartonMohamad_PriceEntities();

        // GET: Tb_join_Categori_Paper
        public async Task<ActionResult> Index()
        {
            var tb_join_Categori_Paper = db.Tb_join_Categori_Paper.Include(t => t.Tb_Catgori).Include(t => t.Tb_Paper);
            return View(await tb_join_Categori_Paper.ToListAsync());
        }

        // GET: Tb_join_Categori_Paper/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_join_Categori_Paper tb_join_Categori_Paper = await db.Tb_join_Categori_Paper.FindAsync(id);
            if (tb_join_Categori_Paper == null)
            {
                return HttpNotFound();
            }
            return View(tb_join_Categori_Paper);
        }

        // GET: Tb_join_Categori_Paper/Create
        public ActionResult Create()
        {
            ViewBag.J_ID_Catgori = new SelectList(db.Tb_Catgori, "ID", "Categori");
            ViewBag.J_ID_PAper = new SelectList(db.Tb_Paper, "ID", "Paper_Name");
            return View();
        }

        // POST: Tb_join_Categori_Paper/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,J_ID_Catgori,J_ID_PAper,Value,type")] Tb_join_Categori_Paper tb_join_Categori_Paper)
        {
            if (ModelState.IsValid)
            {
                db.Tb_join_Categori_Paper.Add(tb_join_Categori_Paper);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.J_ID_Catgori = new SelectList(db.Tb_Catgori, "ID", "Categori", tb_join_Categori_Paper.J_ID_Catgori);
            ViewBag.J_ID_PAper = new SelectList(db.Tb_Paper, "ID", "Paper_Name", tb_join_Categori_Paper.J_ID_PAper);
            return View(tb_join_Categori_Paper);
        }

        // GET: Tb_join_Categori_Paper/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_join_Categori_Paper tb_join_Categori_Paper = await db.Tb_join_Categori_Paper.FindAsync(id);
            if (tb_join_Categori_Paper == null)
            {
                return HttpNotFound();
            }
            ViewBag.J_ID_Catgori = new SelectList(db.Tb_Catgori, "ID", "Categori", tb_join_Categori_Paper.J_ID_Catgori);
            ViewBag.J_ID_PAper = new SelectList(db.Tb_Paper, "ID", "Paper_Name", tb_join_Categori_Paper.J_ID_PAper);
            return View(tb_join_Categori_Paper);
        }

        // POST: Tb_join_Categori_Paper/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,J_ID_Catgori,J_ID_PAper,Value,type")] Tb_join_Categori_Paper tb_join_Categori_Paper)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_join_Categori_Paper).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.J_ID_Catgori = new SelectList(db.Tb_Catgori, "ID", "Categori", tb_join_Categori_Paper.J_ID_Catgori);
            ViewBag.J_ID_PAper = new SelectList(db.Tb_Paper, "ID", "Paper_Name", tb_join_Categori_Paper.J_ID_PAper);
            return View(tb_join_Categori_Paper);
        }

        // GET: Tb_join_Categori_Paper/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_join_Categori_Paper tb_join_Categori_Paper = await db.Tb_join_Categori_Paper.FindAsync(id);
            if (tb_join_Categori_Paper == null)
            {
                return HttpNotFound();
            }
            return View(tb_join_Categori_Paper);
        }

        // POST: Tb_join_Categori_Paper/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Tb_join_Categori_Paper tb_join_Categori_Paper = await db.Tb_join_Categori_Paper.FindAsync(id);
            db.Tb_join_Categori_Paper.Remove(tb_join_Categori_Paper);
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
