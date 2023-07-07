using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Infra;

namespace RouteMaster.Controllers
{
    public class AttractionCategoriesController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: AttractionCategories
        public ActionResult Index()
        {
            return View(db.AttractionCategories.ToList());
        }

        // GET: AttractionCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AttractionCategories/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] AttractionCategory attractionCategory)
        {
            if (ModelState.IsValid)
            {
                db.AttractionCategories.Add(attractionCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(attractionCategory);
        }

        // GET: AttractionCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AttractionCategory attractionCategory = db.AttractionCategories.Find(id);
            if (attractionCategory == null)
            {
                return HttpNotFound();
            }
            return View(attractionCategory);
        }

        // POST: AttractionCategories/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] AttractionCategory attractionCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attractionCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(attractionCategory);
        }

        // GET: AttractionCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AttractionCategory attractionCategory = db.AttractionCategories.Find(id);
            if (attractionCategory == null)
            {
                return HttpNotFound();
            }
            return View(attractionCategory);
        }

        // POST: AttractionCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
				AttractionCategory attractionCategory = db.AttractionCategories.Find(id);
				db.AttractionCategories.Remove(attractionCategory);
				db.SaveChanges();

				return RedirectToAction("Index");
			}catch (Exception ex)
            {
				AttractionCategory attractionCategory = db.AttractionCategories.Find(id);
				ModelState.AddModelError(string.Empty, "無法刪除");
                return View(attractionCategory);
            }
            
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
