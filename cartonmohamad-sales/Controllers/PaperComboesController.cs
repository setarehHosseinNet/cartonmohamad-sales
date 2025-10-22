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
    public class PaperComboesController : Controller
    {
        private CartonMohamad_PriceEntities db = new CartonMohamad_PriceEntities();

        // GET: PaperComboes
        public async Task<ActionResult> Index()
        {
            var paperCombos = db.PaperCombos.Include(p => p.Tb_Paper).Include(p => p.Tb_Paper1).Include(p => p.Tb_Paper2).Include(p => p.Tb_Paper3).Include(p => p.Product).Include(p => p.Tb_Paper4);
            return View(await paperCombos.ToListAsync());
        }

        // GET: PaperComboes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaperCombo paperCombo = await db.PaperCombos.FindAsync(id);
            if (paperCombo == null)
            {
                return HttpNotFound();
            }
            return View(paperCombo);
        }

        // GET: PaperComboes/Create
        public ActionResult Create()
        {
            ViewBag.bottom_liner_paper_id = new SelectList(db.Tb_Paper, "ID", "Paper_Name");
            ViewBag.flute_be_paper_id = new SelectList(db.Tb_Paper, "ID", "Paper_Name");
            ViewBag.flute_c_paper_id = new SelectList(db.Tb_Paper, "ID", "Paper_Name");
            ViewBag.middle_paper_id = new SelectList(db.Tb_Paper, "ID", "Paper_Name");
            ViewBag.product_id = new SelectList(db.Products, "product_id", "product_code");
            ViewBag.top_liner_paper_id = new SelectList(db.Tb_Paper, "ID", "Paper_Name");
            return View();
        }

        // POST: PaperComboes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "combo_id,product_id,combo_name,combo_kind,top_liner_paper_id,flute_be_paper_id,middle_paper_id,flute_c_paper_id,bottom_liner_paper_id,is_active,notes,created_at")] PaperCombo paperCombo)
        {
            if (ModelState.IsValid)
            {
                db.PaperCombos.Add(paperCombo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.bottom_liner_paper_id = new SelectList(db.Tb_Paper, "ID", "Paper_Name", paperCombo.bottom_liner_paper_id);
            ViewBag.flute_be_paper_id = new SelectList(db.Tb_Paper, "ID", "Paper_Name", paperCombo.flute_be_paper_id);
            ViewBag.flute_c_paper_id = new SelectList(db.Tb_Paper, "ID", "Paper_Name", paperCombo.flute_c_paper_id);
            ViewBag.middle_paper_id = new SelectList(db.Tb_Paper, "ID", "Paper_Name", paperCombo.middle_paper_id);
            ViewBag.product_id = new SelectList(db.Products, "product_id", "product_code", paperCombo.product_id);
            ViewBag.top_liner_paper_id = new SelectList(db.Tb_Paper, "ID", "Paper_Name", paperCombo.top_liner_paper_id);
            return View(paperCombo);
        }

        // GET: PaperComboes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaperCombo paperCombo = await db.PaperCombos.FindAsync(id);
            if (paperCombo == null)
            {
                return HttpNotFound();
            }
            ViewBag.bottom_liner_paper_id = new SelectList(db.Tb_Paper, "ID", "Paper_Name", paperCombo.bottom_liner_paper_id);
            ViewBag.flute_be_paper_id = new SelectList(db.Tb_Paper, "ID", "Paper_Name", paperCombo.flute_be_paper_id);
            ViewBag.flute_c_paper_id = new SelectList(db.Tb_Paper, "ID", "Paper_Name", paperCombo.flute_c_paper_id);
            ViewBag.middle_paper_id = new SelectList(db.Tb_Paper, "ID", "Paper_Name", paperCombo.middle_paper_id);
            ViewBag.product_id = new SelectList(db.Products, "product_id", "product_code", paperCombo.product_id);
            ViewBag.top_liner_paper_id = new SelectList(db.Tb_Paper, "ID", "Paper_Name", paperCombo.top_liner_paper_id);
            return View(paperCombo);
        }

        // POST: PaperComboes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "combo_id,product_id,combo_name,combo_kind,top_liner_paper_id,flute_be_paper_id,middle_paper_id,flute_c_paper_id,bottom_liner_paper_id,is_active,notes,created_at")] PaperCombo paperCombo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paperCombo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.bottom_liner_paper_id = new SelectList(db.Tb_Paper, "ID", "Paper_Name", paperCombo.bottom_liner_paper_id);
            ViewBag.flute_be_paper_id = new SelectList(db.Tb_Paper, "ID", "Paper_Name", paperCombo.flute_be_paper_id);
            ViewBag.flute_c_paper_id = new SelectList(db.Tb_Paper, "ID", "Paper_Name", paperCombo.flute_c_paper_id);
            ViewBag.middle_paper_id = new SelectList(db.Tb_Paper, "ID", "Paper_Name", paperCombo.middle_paper_id);
            ViewBag.product_id = new SelectList(db.Products, "product_id", "product_code", paperCombo.product_id);
            ViewBag.top_liner_paper_id = new SelectList(db.Tb_Paper, "ID", "Paper_Name", paperCombo.top_liner_paper_id);
            return View(paperCombo);
        }

        // GET: PaperComboes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaperCombo paperCombo = await db.PaperCombos.FindAsync(id);
            if (paperCombo == null)
            {
                return HttpNotFound();
            }
            return View(paperCombo);
        }

        // POST: PaperComboes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PaperCombo paperCombo = await db.PaperCombos.FindAsync(id);
            db.PaperCombos.Remove(paperCombo);
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
