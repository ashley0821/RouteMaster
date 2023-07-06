using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime;
using Newtonsoft.Json.Linq;
using RouteMaster.Models.Dto;
using RouteMaster.Models.Dto.Accommodation.Service;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Infra;
using RouteMaster.Models.Infra.EFRepositories;
using RouteMaster.Models.Infra.Extensions;
using RouteMaster.Models.Interfaces;
using RouteMaster.Models.Services;
using RouteMaster.Models.ViewModels;
using RouteMaster.Models.ViewModels.Accommodations;
using RouteMaster.Models.ViewModels.Accommodations.Room;
using static System.Net.Mime.MediaTypeNames;

namespace RouteMaster.Controllers
{
    
    public class AccommodationsController : Controller
    {
        private readonly AppDbContext db = new AppDbContext();

        // 還沒做
        public ActionResult Index()
        {
			var accommodations = db.Accommodations.Include(a => a.Partner).Include(a => a.Region).Include(a => a.Town);
            return View(accommodations.ToList());
        }
        
        // 合夥人的住宿列表
        public ActionResult MyAccommodationIndex(int? id)
        {
			IEnumerable<AccommodationIndexVM> accommodations = GetAccommodations(id);

            //var accommodations = db.Accommodations.Include(a => a.Partner).Include(a => a.Region).Include(a => a.Town);

            return View(accommodations);//.ToList());
        }

        // 還沒做
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

        // 新增住宿
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AccommodationCreateVM vm)
        {
            ViewBag.RegionId = new SelectList(db.Regions, "Id", "Name", vm.RegionId);
                
            ViewBag.TownId = new SelectList(db.Towns, "Id", "Name", vm.TownId);
            if (!ModelState.IsValid) return View(vm);

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

		// 編輯住宿資訊
		public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

			//var model = GetMemberProfile(currentUserAccount);
			AccommodationEditVM model = GetAccommodationProfile(id);

			//Accommodation accommodation = db.Accommodations.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }

            ViewBag.RegionId = new SelectList(db.Regions, "Id", "Name", model.RegionId);
            ViewBag.TownId = new SelectList(db.Towns.Where(t=>t.RegionId == model.RegionId), "Id", "Name", model.TownId);
            return View(model);
        }

		[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AccommodationEditVM vm, HttpPostedFileBase[] files)
        {
			ViewBag.RegionId = new SelectList(db.Regions, "Id", "Name", vm.RegionId);
			ViewBag.TownId = new SelectList(db.Towns.Where(t => t.RegionId == vm.RegionId), "Id", "Name", vm.TownId);
			if (!ModelState.IsValid) return View(vm);

			Result result = EditAccommodationProfile(vm, files);

			if (result.IsSuccess)
			{
				return RedirectToAction("MyAccommodationIndex");
			}
			else
			{
				ModelState.AddModelError(string.Empty, result.ErrorMessage);
				return View(vm);
			}
			
        }

		// 新增客房
		public ActionResult CreateRoom(int? id)
        {
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			AccommodationEditVM model = GetAccommodationProfile(id);

			if (model == null)
			{
				return HttpNotFound();
			}

            RoomCreateVM vm = new RoomCreateVM
            {
                AccommodationId = model.Id
            };
            ViewBag.AccommodationId = model.Id;
            PrepareRoomTypeViewBag();
            
			return View(vm);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult CreateRoom(RoomCreateVM vm, HttpPostedFileBase[] files)
		{

			if (!ModelState.IsValid) return View(vm);
            //建立新會員

            Result result = CreateRoomAndImage(vm, files);

			if (result.IsSuccess)
			{
				return RedirectToAction("MyAccommodationIndex");
			}
			else
			{
				ModelState.AddModelError(string.Empty, result.ErrorMessage);
                PrepareRoomTypeViewBag();
				return View(vm);
			}
			//ViewBag.PartnerId = new SelectList(db.Partners, "Id", "FirstName", accommodation.PartnerId);
			//ViewBag.RegionId = new SelectList(db.Regions, "Id", "Name", vm.RegionId);
			//ViewBag.TownId = new SelectList(db.Towns, "Id", "Name", vm.TownId);
			//return View(vm);
		}

		// 客房列表
        public ActionResult RoomsIndex(int id)
        {
            var rooms = db.Rooms.Where(r => r.AccommodationId == id).Include(a => a.RoomImages);

            ViewBag.Id = id;
            return View(rooms.ToList().Select(r => r.ToVM()));
        }


		// 編輯公共設施
		public ActionResult EditServiceInfo(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			ServiceInfoVM vm = new ServiceInfoVM
			{
				AccommodationId = (int)id,
                ServiceInfoList = db.ServiceInfos.Select(s => new ServiceInfoDto
                {
                    Id = s.Id,
                    Name = s.Name

                })
				.OrderBy(s => s.Name.Length).ToList()
            };

            ViewBag.ServiceInfoIds = db.Accommodations.FirstOrDefault(a => a.Id == id)?.ServiceInfos.Select(s => s.Id);
			return View(vm);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditServiceInfo(ServiceInfoVM vm)
		{

			ViewBag.ServiceInfoIds = db.Accommodations.FirstOrDefault(a => a.Id == vm.AccommodationId)?.ServiceInfos.Select(s => s.Id);
            if (!ModelState.IsValid) return View(vm);

            Result result = EditService(vm);

            if (result.IsSuccess)
            {
                return RedirectToAction("MyAccommodationIndex");
            }
            else
            {
                ModelState.AddModelError(string.Empty, result.ErrorMessage);
                return View(vm);
            }

        }








		#region:方法
		private Result EditService(ServiceInfoVM vm)
		{
			IAccommodationRepository repo = new AccommodationEFRepository();
			AccommodationService service = new AccommodationService(repo);

			return service.EditService(vm);
		}
		private Result CreateRoomAndImage(RoomCreateVM vm, HttpPostedFileBase[] files)
		{
			string path = Server.MapPath("~/Uploads");
			IAccommodationRepository repo = new AccommodationEFRepository();
            AccommodationService service = new AccommodationService(repo) ;

			return service.CreateRoomAndImages(vm.ToDto(), files, path);
        }

		private void PrepareRoomTypeViewBag()
		{
            var roomTypes = new List<RoomType>{
                new RoomType(1,"單人房"),
                new RoomType(2,"雙人房"),
                new RoomType(3,"雙床房"),
                new RoomType(4,"三人房"),
                new RoomType(5,"四人房"),
                new RoomType(6,"家庭房"),
                new RoomType(7,"套房"),
                new RoomType(8,"雅房"),
                new RoomType(9,"膠囊床位")
            };

            ViewBag.RoomType = new SelectList(roomTypes, "Type", "Type", "請選擇")
				.Prepend(new SelectListItem
				{
					Disabled = true,
					Selected = true,
					Text = "請選擇",
					Value = ""
				});
		}

		private Result EditAccommodationProfile(AccommodationEditVM vm, HttpPostedFileBase[] files)
		{
			string path = Server.MapPath("~/Uploads");
			IAccommodationRepository repo = new AccommodationEFRepository();
			AccommodationService service = new AccommodationService(repo);

			return service.EditAccommodationProfile(vm.ToDto(), files, path);
		}

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

		private AccommodationEditVM GetAccommodationProfile(int? id)
		{
			IAccommodationRepository repo = new AccommodationEFRepository();
			//IProductRepository repo = new ProductDapperRepository();
			AccommodationService service = new AccommodationService(repo);
            return service.GetEditInfo(id)?.ToVM();
		}

        private Result CreateAccommodation(AccommodationCreateVM vm)
        {

            IAccommodationRepository repo = new AccommodationEFRepository();
            AccommodationService service = new AccommodationService(repo);

            return service.Create(vm.ToDto());
        }

		private IEnumerable<AccommodationIndexVM> GetAccommodations(int? id)
		{
			IAccommodationRepository repo = new AccommodationEFRepository();
			//IProductRepository repo = new ProductDapperRepository();
			AccommodationService service = new AccommodationService(repo);
			return service.Search(id).
				Select(dto => dto.ToVM()).OrderByDescending(dto=>dto.Id);
		}

		[HttpPost]
        //[ValidateAntiForgeryToken]
		public ActionResult ShowTownList(int regionId)
		{
			IEnumerable<Town> townList = db.Towns.Where(t => t.RegionId == regionId);

			var townData = townList.Select(t => new
			{
				townId = t.Id,
				name = t.Name
			}).ToList();

			//return townList;
			return Json(townData, JsonRequestBehavior.AllowGet);
        }
		#endregion

	}
}
