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
    public class testsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: tests
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Member).Include(o => o.PaymentMethod).Include(o => o.TravelPlan);
            return View(orders.ToList());
        }

        // GET: tests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: tests/Create
        public ActionResult Create()
        {
            ViewBag.MemberId = new SelectList(db.Members, "Id", "FirstName");
            ViewBag.PaymentMethodId = new SelectList(db.PaymentMethods, "Id", "Name");
            ViewBag.TravelPlanId = new SelectList(db.TravelPlans, "Id", "Id");
            return View();
        }

        // POST: tests/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MemberId,TravelPlanId,PaymentMethodId,PaymentStatus,CreateDate,Total")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MemberId = new SelectList(db.Members, "Id", "FirstName", order.MemberId);
            ViewBag.PaymentMethodId = new SelectList(db.PaymentMethods, "Id", "Name", order.PaymentMethodId);
            ViewBag.TravelPlanId = new SelectList(db.TravelPlans, "Id", "Id", order.TravelPlanId);
            return View(order);
        }

        // GET: tests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.MemberId = new SelectList(db.Members, "Id", "FirstName", order.MemberId);
            ViewBag.PaymentMethodId = new SelectList(db.PaymentMethods, "Id", "Name", order.PaymentMethodId);
            ViewBag.TravelPlanId = new SelectList(db.TravelPlans, "Id", "Id", order.TravelPlanId);
            return View(order);
        }

        // POST: tests/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MemberId,TravelPlanId,PaymentMethodId,PaymentStatus,CreateDate,Total")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MemberId = new SelectList(db.Members, "Id", "FirstName", order.MemberId);
            ViewBag.PaymentMethodId = new SelectList(db.PaymentMethods, "Id", "Name", order.PaymentMethodId);
            ViewBag.TravelPlanId = new SelectList(db.TravelPlans, "Id", "Id", order.TravelPlanId);
            return View(order);
        }

        // GET: tests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: tests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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
