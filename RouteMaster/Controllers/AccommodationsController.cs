using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime;
using Newtonsoft.Json.Linq;
using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Infra;
using RouteMaster.Models.Infra.EFRepositories;
using RouteMaster.Models.Infra.Extensions;
using RouteMaster.Models.Interfaces;
using RouteMaster.Models.Services;
using RouteMaster.Models.ViewModels;
using static System.Net.Mime.MediaTypeNames;

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
            //ViewBag.PartnerId = new SelectList(db.Partners, "Id", "FirstName");
            ViewBag.RegionId = new SelectList(db.Regions, "Id", "Name")
				.Prepend(new SelectListItem
				{
                    Disabled = true,
					Selected = true,
					Text = "請選擇",
					Value = ""
				}); 
            ViewBag.TownId = new SelectList(db.Towns, "Id", "Name");
            return View();
        }

        // POST: Accommodations/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AccommodationCreateVM vm)
        {
            ViewBag.RegionId = new SelectList(db.Regions, "Id", "Name", vm.RegionId);
                
            ViewBag.TownId = new SelectList(db.Towns, "Id", "Name", vm.TownId);
            if (!ModelState.IsValid) return View(vm);
            //建立新會員

            Result result = CreateAccommodation(vm);

            if (result.IsSuccess)
            {
                return RedirectToAction("MyAccommodationIndex");
            }
            else
            {
                ModelState.AddModelError(string.Empty, result.ErrorMessage);
                return View(vm);
            }
            //ViewBag.PartnerId = new SelectList(db.Partners, "Id", "FirstName", accommodation.PartnerId);
            //ViewBag.RegionId = new SelectList(db.Regions, "Id", "Name", vm.RegionId);
            //ViewBag.TownId = new SelectList(db.Towns, "Id", "Name", vm.TownId);
            //return View(vm);
        }

        private Result CreateAccommodation(AccommodationCreateVM vm)
        {
            if (vm.RegionId == 0 || vm.TownId == 0) return Result.Fail("請再確認欄位資料是否正確");

            IAccommodationRepository repo = new AccommodationEFRepository();
            AccommodationService service = new AccommodationService(repo);

            return service.Create(vm.ToDto());
        }

        // GET: Accommodations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

			//var model = GetMemberProfile(currentUserAccount);
			AccommodationEditVM model = GetMemberProfile(id);

			//Accommodation accommodation = db.Accommodations.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }

            ViewBag.RegionId = new SelectList(db.Regions, "Id", "Name", model.RegionId);
            ViewBag.TownId = new SelectList(db.Towns, "Id", "Name", model.TownId);
            return View(model);
        }

		private AccommodationEditVM GetMemberProfile(int? id)
		{
			var accommodationdb = db.Accommodations.FirstOrDefault(x => x.Id == id);

            //var length = db.Regions.Select(r => r.Id == accommodationdb.RegionId);
            int length = accommodationdb.Region.Name.Length + accommodationdb.Town.Name.Length;
            

			return accommodationdb == null ? null : new AccommodationEditVM
			{
				Id = accommodationdb.Id,
                PartnerId = accommodationdb.PartnerId,
				Name = accommodationdb.Name,
                Description = accommodationdb.Description,
                RegionId = accommodationdb.RegionId,
                TownId = accommodationdb.TownId,
                Address = accommodationdb.Address.Substring(length),
                PhoneNumber = accommodationdb.PhoneNumber,
                Website = accommodationdb.Website,
                IndustryEmail = accommodationdb.IndustryEmail,
                ParkingSpace = accommodationdb.ParkingSpace
			};
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
				Select(dto => dto.ToVM()).OrderByDescending(dto=>dto.Id);
		}

		[HttpPost]
		public ActionResult ShowTownList(int regionId)
		{
			IEnumerable<Town> townList = db.Towns.Where(t => t.RegionId == regionId);

			var townData = townList.Select(t => new
			{
				regionId = t.RegionId,
				name = t.Name
			}).ToList();

			//return townList;
			return Json(townData, JsonRequestBehavior.AllowGet);
        }
	}
}
