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
    public class PackageToursController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: PackageTours
        public ActionResult Index()
        {
            var packageTours = db.PackageTours.Include(p => p.Coupon);
            return View(packageTours.ToList());
        }

        // GET: PackageTours/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PackageTour packageTour = db.PackageTours.Find(id);
            if (packageTour == null)
            {
                return HttpNotFound();
            }
            return View(packageTour);
        }

        // GET: PackageTours/Create
        public ActionResult Create()
        {
            ViewBag.CouponId = new SelectList(db.Coupons, "Id", "Id");
            return View();
        }

        // POST: PackageTours/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Description,Status,CouponId")] PackageTour packageTour)
        {
            if (ModelState.IsValid)
            {
                db.PackageTours.Add(packageTour);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CouponId = new SelectList(db.Coupons, "Id", "Id", packageTour.CouponId);
            return View(packageTour);
        }

        // GET: PackageTours/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PackageTour packageTour = db.PackageTours.Find(id);
            if (packageTour == null)
            {
                return HttpNotFound();
            }
            ViewBag.CouponId = new SelectList(db.Coupons, "Id", "Id", packageTour.CouponId);
            return View(packageTour);
        }

        // POST: PackageTours/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,Status,CouponId")] PackageTour packageTour)
        {
            if (ModelState.IsValid)
            {
                db.Entry(packageTour).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CouponId = new SelectList(db.Coupons, "Id", "Id", packageTour.CouponId);
            return View(packageTour);
        }

        // GET: PackageTours/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PackageTour packageTour = db.PackageTours.Find(id);
            if (packageTour == null)
            {
                return HttpNotFound();
            }
            return View(packageTour);
        }

        // POST: PackageTours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PackageTour packageTour = db.PackageTours.Find(id);
            db.PackageTours.Remove(packageTour);
            db.SaveChanges();
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
