using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Infra.DapperRepositories;
using RouteMaster.Models.Infra.Extensions;
using RouteMaster.Models.Interfaces;
using RouteMaster.Models.Services;
using RouteMaster.Models.ViewModels;

namespace RouteMaster.Controllers
{
    public class ExtraServicesController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: ExtraServices
        public ActionResult Index()
        {
            IEnumerable<ExtraServiceIndexVM> extraServices = GetExtraServices();
            return View(extraServices);
        }

		private IEnumerable<ExtraServiceIndexVM> GetExtraServices()
		{
            IExtraServiceRepository repo =new ExtraServiceDapperRepository();
            ExtraServiceService service =new ExtraServiceService(repo);

            return service.Search()
                .Select(dto=>dto.ToIndexVM());
			
		}













		// GET: ExtraServices/Details/5
		public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExtraService extraService = db.ExtraServices.Find(id);
            if (extraService == null)
            {
                return HttpNotFound();
            }
            return View(extraService);
        }

        // GET: ExtraServices/Create
        public ActionResult Create()
        {
            ViewBag.AttractionId = new SelectList(db.Attractions, "Id", "Name");
            return View();
        }

        // POST: ExtraServices/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,AttractionId,Price,Description,Status")] ExtraService extraService)
        {
            if (ModelState.IsValid)
            {
                db.ExtraServices.Add(extraService);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AttractionId = new SelectList(db.Attractions, "Id", "Name", extraService.AttractionId);
            return View(extraService);
        }

        // GET: ExtraServices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExtraService extraService = db.ExtraServices.Find(id);
            if (extraService == null)
            {
                return HttpNotFound();
            }
            ViewBag.AttractionId = new SelectList(db.Attractions, "Id", "Name", extraService.AttractionId);
            return View(extraService);
        }

        // POST: ExtraServices/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,AttractionId,Price,Description,Status")] ExtraService extraService)
        {
            if (ModelState.IsValid)
            {
                db.Entry(extraService).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AttractionId = new SelectList(db.Attractions, "Id", "Name", extraService.AttractionId);
            return View(extraService);
        }

        // GET: ExtraServices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExtraService extraService = db.ExtraServices.Find(id);
            if (extraService == null)
            {
                return HttpNotFound();
            }
            return View(extraService);
        }

        // POST: ExtraServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExtraService extraService = db.ExtraServices.Find(id);
            db.ExtraServices.Remove(extraService);
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
