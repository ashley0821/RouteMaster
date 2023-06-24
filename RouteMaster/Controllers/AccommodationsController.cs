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
using RouteMaster.Models.ViewModels;

namespace RouteMaster.Controllers
{
    public class AccommodationsController : Controller
    {
        private readonly AppDbContext db = new AppDbContext();

        // GET: Accommodations
        public ActionResult Index()
        {
			var accommodations = db.Accommodations.Include(a => a.Partner).Include(a => a.Region).Include(a => a.Town);
            return View(accommodations.ToList());
        }
        
        public ActionResult MyAccommodationIndex()
        {
			IEnumerable<AccommodationIndexVM> accommodations = GetAccommodations();

            //var accommodations = db.Accommodations.Include(a => a.Partner).Include(a => a.Region).Include(a => a.Town);

            return View(accommodations);//.ToList());
        }


		// GET: Accommodations/Details/5
		public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accommodation accommodation = db.Accommodations.Find(id);
            if (accommodation == null)
            {
                return HttpNotFound();
            }
            return View(accommodation);
        }

        // GET: Accommodations/Create
        public ActionResult Create()
        {
            ViewBag.PartnerId = new SelectList(db.Partners, "Id", "FirstName");
            ViewBag.RegionId = new SelectList(db.Regions, "Id", "Name");
            ViewBag.TownId = new SelectList(db.Towns, "Id", "Name");
            return View();
        }

        // POST: Accommodations/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PartnerId,Name,Description,Grade,RegionId,TownId,Address,PositionX,PositionY,PhoneNumber,Website,IndustryEmail,ParkingSpace,CreateDate")] Accommodation accommodation)
        {
            if (ModelState.IsValid)
            {
                db.Accommodations.Add(accommodation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PartnerId = new SelectList(db.Partners, "Id", "FirstName", accommodation.PartnerId);
            ViewBag.RegionId = new SelectList(db.Regions, "Id", "Name", accommodation.RegionId);
            ViewBag.TownId = new SelectList(db.Towns, "Id", "Name", accommodation.TownId);
            return View(accommodation);
        }

        // GET: Accommodations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accommodation accommodation = db.Accommodations.Find(id);
            if (accommodation == null)
            {
                return HttpNotFound();
            }
            ViewBag.PartnerId = new SelectList(db.Partners, "Id", "FirstName", accommodation.PartnerId);
            ViewBag.RegionId = new SelectList(db.Regions, "Id", "Name", accommodation.RegionId);
            ViewBag.TownId = new SelectList(db.Towns, "Id", "Name", accommodation.TownId);
            return View(accommodation);
        }

        // POST: Accommodations/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PartnerId,Name,Description,Grade,RegionId,TownId,Address,PositionX,PositionY,PhoneNumber,Website,IndustryEmail,ParkingSpace,CreateDate")] Accommodation accommodation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accommodation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PartnerId = new SelectList(db.Partners, "Id", "FirstName", accommodation.PartnerId);
            ViewBag.RegionId = new SelectList(db.Regions, "Id", "Name", accommodation.RegionId);
            ViewBag.TownId = new SelectList(db.Towns, "Id", "Name", accommodation.TownId);
            return View(accommodation);
        }

        // GET: Accommodations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accommodation accommodation = db.Accommodations.Find(id);
            if (accommodation == null)
            {
                return HttpNotFound();
            }
            return View(accommodation);
        }

        // POST: Accommodations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Accommodation accommodation = db.Accommodations.Find(id);
            db.Accommodations.Remove(accommodation);
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

		private IEnumerable<AccommodationIndexVM> GetAccommodations()
		{
			IAccommodationRepository repo = new AccommodationEFRepository();
			//IProductRepository repo = new ProductDapperRepository();
			AccommodationService service = new AccommodationService(repo);
			return service.Search().
				Select(dto => dto.ToVM());
		}

	}
}
