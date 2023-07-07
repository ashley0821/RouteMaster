using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RouteMaster.Models.EFModels;

namespace RouteMaster.Controllers
{
    public class AttractionTagsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: AttractionTags
        public ActionResult Index()
        {
            return View(db.AttractionTags.ToList());
        }

        

        // GET: AttractionTags/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AttractionTags/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] AttractionTag attractionTag)
        {
            if (ModelState.IsValid)
            {
                db.AttractionTags.Add(attractionTag);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(attractionTag);
        }

        // GET: AttractionTags/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AttractionTag attractionTag = db.AttractionTags.Find(id);
            if (attractionTag == null)
            {
                return HttpNotFound();
            }
            return View(attractionTag);
        }

        // POST: AttractionTags/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] AttractionTag attractionTag)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attractionTag).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(attractionTag);
        }

        // GET: AttractionTags/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AttractionTag attractionTag = db.AttractionTags.Find(id);
            if (attractionTag == null)
            {
                return HttpNotFound();
            }
            return View(attractionTag);
        }

        // POST: AttractionTags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
				AttractionTag attractionTag = db.AttractionTags.Find(id);
				db.AttractionTags.Remove(attractionTag);
				db.SaveChanges();
				return RedirectToAction("Index");
			}catch (Exception ex)
            {
				AttractionTag attractionTag = db.AttractionTags.Find(id);
                ModelState.AddModelError(string.Empty, "無法刪除");
				return View(attractionTag);
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
