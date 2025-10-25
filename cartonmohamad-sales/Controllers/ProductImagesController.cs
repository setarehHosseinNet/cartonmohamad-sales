﻿using System;
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
    public class ProductImagesController : Controller
    {
        private CartonMohamad_PriceEntities db = new CartonMohamad_PriceEntities();

        // GET: ProductImages
        public async Task<ActionResult> Index()
        {
            var productImages = db.ProductImages.Include(p => p.Product);
            return View(await productImages.ToListAsync());
        }

        // GET: ProductImages/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductImage productImage = await db.ProductImages.FindAsync(id);
            if (productImage == null)
            {
                return HttpNotFound();
            }
            return View(productImage);
        }

        // GET: ProductImages/Create
        public ActionResult Create()
        {
            ViewBag.product_id = new SelectList(db.Products, "product_id", "product_code");
            return View();
        }

        // POST: ProductImages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "image_id,product_id,file_url,storage_provider,alt_text,caption,is_primary,sort_order,mime_type,width_px,height_px,size_bytes,checksum_sha1,created_at")] ProductImage productImage)
        {
            if (ModelState.IsValid)
            {
                db.ProductImages.Add(productImage);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.product_id = new SelectList(db.Products, "product_id", "product_code", productImage.product_id);
            return View(productImage);
        }

        // GET: ProductImages/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductImage productImage = await db.ProductImages.FindAsync(id);
            if (productImage == null)
            {
                return HttpNotFound();
            }
            ViewBag.product_id = new SelectList(db.Products, "product_id", "product_code", productImage.product_id);
            return View(productImage);
        }

        // POST: ProductImages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "image_id,product_id,file_url,storage_provider,alt_text,caption,is_primary,sort_order,mime_type,width_px,height_px,size_bytes,checksum_sha1,created_at")] ProductImage productImage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productImage).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.product_id = new SelectList(db.Products, "product_id", "product_code", productImage.product_id);
            return View(productImage);
        }

        // GET: ProductImages/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductImage productImage = await db.ProductImages.FindAsync(id);
            if (productImage == null)
            {
                return HttpNotFound();
            }
            return View(productImage);
        }

        // POST: ProductImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ProductImage productImage = await db.ProductImages.FindAsync(id);
            db.ProductImages.Remove(productImage);
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
