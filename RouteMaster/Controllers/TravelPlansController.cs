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
    public class TravelPlansController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: TravelPlans
        public ActionResult Index()
        {
            var travelPlans = db.TravelPlans.Include(t => t.Member);
            return View(travelPlans.ToList());
        }

        // GET: TravelPlans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TravelPlan travelPlan = db.TravelPlans.Find(id);
            if (travelPlan == null)
            {
                return HttpNotFound();
            }
            return View(travelPlan);
        }

        // GET: TravelPlans/Create
        public ActionResult Create()
        {
            ViewBag.MemberId = new SelectList(db.Members, "Id", "FirstName");
            return View();
        }

        // POST: TravelPlans/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MemberId,TravelDays,CreateDate")] TravelPlan travelPlan)
        {
            if (ModelState.IsValid)
            {
                db.TravelPlans.Add(travelPlan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MemberId = new SelectList(db.Members, "Id", "FirstName", travelPlan.MemberId);
            return View(travelPlan);
        }

        // GET: TravelPlans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TravelPlan travelPlan = db.TravelPlans.Find(id);
            if (travelPlan == null)
            {
                return HttpNotFound();
            }
            ViewBag.MemberId = new SelectList(db.Members, "Id", "FirstName", travelPlan.MemberId);
            return View(travelPlan);
        }

        // POST: TravelPlans/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MemberId,TravelDays,CreateDate")] TravelPlan travelPlan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(travelPlan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MemberId = new SelectList(db.Members, "Id", "FirstName", travelPlan.MemberId);
            return View(travelPlan);
        }

        // GET: TravelPlans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TravelPlan travelPlan = db.TravelPlans.Find(id);
            if (travelPlan == null)
            {
                return HttpNotFound();
            }
            return View(travelPlan);
        }

        // POST: TravelPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TravelPlan travelPlan = db.TravelPlans.Find(id);
            db.TravelPlans.Remove(travelPlan);
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
