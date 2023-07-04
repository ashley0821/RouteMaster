using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Infra.EFRepositories;
using RouteMaster.Models.Infra.Extensions;
using RouteMaster.Models.Interfaces;
using RouteMaster.Models.Services;

namespace RouteMaster.Controllers
{
    public class Comments_AccommodationsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Comments_Accommodations
        public ActionResult Index()
        {
            IComments_AccommodationsRepository repo = new Comments_AccommodationsEFRepository();
            Comments_AccommodationsService service = new Comments_AccommodationsService(repo);

            var info = service.Search().ToList().Select(c => c.ToIndexVM());

			return View(info);
        }

        // GET: Comments_Accommodations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comments_Accommodations comments_Accommodations = db.Comments_Accommodations.Find(id);
            if (comments_Accommodations == null)
            {
                return HttpNotFound();
            }
            return View(comments_Accommodations);
        }

        // GET: Comments_Accommodations/Create
        public ActionResult Create()
        {
            ViewBag.AccommodationId = new SelectList(db.Accommodations, "Id", "Name");
            ViewBag.MemberId = new SelectList(db.Members, "Id", "FirstName");
            return View();
        }

        // POST: Comments_Accommodations/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MemberId,AccommodationId,Score,Title,Pros,Cons,CreateDate")] Comments_Accommodations comments_Accommodations)
        {
            if (ModelState.IsValid)
            {
                db.Comments_Accommodations.Add(comments_Accommodations);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccommodationId = new SelectList(db.Accommodations, "Id", "Name", comments_Accommodations.AccommodationId);
            ViewBag.MemberId = new SelectList(db.Members, "Id", "FirstName", comments_Accommodations.MemberId);
            return View(comments_Accommodations);
        }

        // GET: Comments_Accommodations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comments_Accommodations comments_Accommodations = db.Comments_Accommodations.Find(id);
            if (comments_Accommodations == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccommodationId = new SelectList(db.Accommodations, "Id", "Name", comments_Accommodations.AccommodationId);
            ViewBag.MemberId = new SelectList(db.Members, "Id", "FirstName", comments_Accommodations.MemberId);
            return View(comments_Accommodations);
        }

        // POST: Comments_Accommodations/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MemberId,AccommodationId,Score,Title,Pros,Cons,CreateDate")] Comments_Accommodations comments_Accommodations)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comments_Accommodations).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccommodationId = new SelectList(db.Accommodations, "Id", "Name", comments_Accommodations.AccommodationId);
            ViewBag.MemberId = new SelectList(db.Members, "Id", "FirstName", comments_Accommodations.MemberId);
            return View(comments_Accommodations);
        }

        // GET: Comments_Accommodations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comments_Accommodations comments_Accommodations = db.Comments_Accommodations.Find(id);
            if (comments_Accommodations == null)
            {
                return HttpNotFound();
            }
            return View(comments_Accommodations);
        }

        // POST: Comments_Accommodations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comments_Accommodations comments_Accommodations = db.Comments_Accommodations.Find(id);
            db.Comments_Accommodations.Remove(comments_Accommodations);
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
