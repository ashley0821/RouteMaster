using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Antlr.Runtime;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json.Linq;
using RouteMaster.Filter;
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
using static RouteMaster.Filter.PartnerAuthenticationFilter;

namespace RouteMaster.Controllers
{
    //[PartnerAuthenticationFilter]
    //[PartnerAuthorizeAttribute]

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
        public ActionResult MyAccommodationIndex()
        {
            var profile = GetPartnerEmailAndId();
			// 取得 HTTP 請求中的 Cookie 集合
			if( !string.IsNullOrEmpty(profile.Item1) && profile.Item2 != 0)
            {
			    IEnumerable<AccommodationIndexVM> accommodations = GetAccommodations(profile.Item2);
            
                //var accommodations = db.Accommodations.Include(a => a.Partner).Include(a => a.Region).Include(a => a.Town);

                return View(accommodations);//.ToList());
            }

            return RedirectToAction("PartnerLogin", "Partners");
        }

		private (string, int) GetPartnerEmailAndId()
		{
			HttpCookieCollection cookies = Request.Cookies;

			// 檢查是否存在特定名稱的 Cookie
			if (cookies[FormsAuthentication.FormsCookieName] != null)
			{
				// 從 Cookie 中取得票據的值
				string encryptedTicket = cookies[FormsAuthentication.FormsCookieName].Value;

				// 解密票據
				var ticket = FormsAuthentication.Decrypt(encryptedTicket);

				// 檢查票據是否成功解密
				if (ticket != null && ticket.Expired == false)
				{
					// 取得票據中的使用者資料
					string email = ticket.Name;
					int id = int.Parse(ticket.UserData);

                    return (email, id);
				}
                return (null, 0);
			}
            return(null, 0);
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


            return View(accommodation.ToVM());
        }

        // 新增住宿
        public ActionResult Create()
        {
            //ViewBag.PartnerId = new SelectList(db.Partners, "Id", "FirstName");
            
            PrepareRegionAndTownSelectList();
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

            ViewBag.RegionId = new SelectList(db.Regions.OrderBy(r=>r.Id), "Id", "Name", model.RegionId);
            ViewBag.TownId = new SelectList(db.Towns.Where(t=>t.RegionId == model.RegionId), "Id", "Name", model.TownId);

            return View(model);
        }

		[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AccommodationEditVM vm, ImagesVM iVM)
        {
			ViewBag.RegionId = new SelectList(db.Regions.OrderBy(r => r.Id), "Id", "Name", vm.RegionId);
			ViewBag.TownId = new SelectList(db.Towns.Where(t => t.RegionId == vm.RegionId), "Id", "Name", vm.TownId);
			if (!ModelState.IsValid) return View(vm);

			Result result = EditAccommodationProfile(vm, iVM);

			if (result.IsSuccess)
			{

				return RedirectToAction("Details", new { id = vm.Id });
			}
			else
			{
				ModelState.AddModelError(string.Empty, result.ErrorMessage);
				return View(vm);
			}
			
        }

        public ActionResult RoomsIndex(int id)
        {
            var rooms = db.Rooms.Where(r => r.AccommodationId == id).Include(a => a.RoomImages);

            ViewBag.Id = id;
            return View(rooms.ToList().Select(r => r.ToVM()));
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
		public ActionResult CreateRoom(RoomCreateVM vm, ImagesVM iVM)
		{

			if (!ModelState.IsValid) return View(vm);
            //建立新會員

            Result result = CreateRoomAndImage(vm, iVM);

            IEnumerable<RoomIndexVM> indexVM = db.Rooms.Where(r => r.AccommodationId == vm.AccommodationId).ToList().Select(r=>r.ToVM());
			if (result.IsSuccess)
			{
                ViewBag.Id = vm.AccommodationId;
                return RedirectToAction("RoomsIndex", new {id = vm.AccommodationId});
			}
			else
			{
                PrepareRoomTypeViewBag();
				ModelState.AddModelError(string.Empty, result.ErrorMessage);
				return View(vm);
			}
			//ViewBag.PartnerId = new SelectList(db.Partners, "Id", "FirstName", accommodation.PartnerId);
			//ViewBag.RegionId = new SelectList(db.Regions, "Id", "Name", vm.RegionId);
			//ViewBag.TownId = new SelectList(db.Towns, "Id", "Name", vm.TownId);
			//return View(vm);
		}

		// 客房列表
		public ActionResult EditRoom(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			//var model = GetMemberProfile(currentUserAccount);
			RoomEditVM vm = GetRoomProfile(id);

			//Accommodation accommodation = db.Accommodations.Find(id);
			if (vm == null)
			{
				return HttpNotFound();
			}

			PrepareRoomTypeViewBag();

			return View(vm);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditRoom(RoomEditVM vm, ImagesVM iVM)
		{
			if (!ModelState.IsValid) return View(vm);

			Result result = EditRoomProfile(vm, iVM);

			if (result.IsSuccess)
			{

				return RedirectToAction("RoomsIndex", new { id = vm.AccommodationId });
			}
			else
			{
                PrepareRoomTypeViewBag();
				ModelState.AddModelError(string.Empty, result.ErrorMessage);
				return View(vm);
			}

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
                return RedirectToAction("Details", new { id = vm.AccommodationId });
            }
            else
            {
                ModelState.AddModelError(string.Empty, result.ErrorMessage);
                return View(vm);
            }

        }

		//public ActionResult Delete(int? id)
  //      {
  //          if (id == null)
  //          {
  //              return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
  //          }
  //          Accommodation accommodation = db.Accommodations.Find(id);
  //          if (accommodation == null)
  //          {
  //              return HttpNotFound();
  //          }
  //          return View(accommodation);
  //      }

        [HttpPost, ActionName("Delete")]
     
        public void DeleteConfirmed(int id)
        {
            Accommodation accommodation = db.Accommodations.Find(id);
            var ais = accommodation.AccommodationImages;
            var rs = accommodation.Rooms;
            var ss = accommodation.ServiceInfos;

			if (ais != null || ais.Count() != 0)
            {
                foreach(var ai in ais.ToList())
                {
                   DeleteUploadFile(ai.Image);
                   db.AccommodationImages.Remove(ai);
                }
            }


			if (rs != null || rs.Count() != 0)
            {
			    foreach (var r in rs.ToList())
			    {
                    var ris = r.RoomImages;
					if (ris != null || ris.Count() != 0)
                    {
				        foreach (var ri in r.RoomImages.ToList())
				        {
                            DeleteUploadFile(ri.Image);
					        db.RoomImages.Remove(ri);
				        }
                    }
                   db.Rooms.Remove(r);
			    }
            }
            if(ss!= null || ss.Count() != 0)
            {
                foreach(var s in ss.ToList())
                {
					accommodation.ServiceInfos.Remove(s);
                }
            }
            
            db.Accommodations.Remove(accommodation);
            db.SaveChanges();
        }

		private void DeleteUploadFile(string file1)
		{
			string path = Server.MapPath("~/Uploads");
			string fullName = Path.Combine(path, file1);
			System.IO.File.Delete(fullName);
		}

		[HttpPost]
     
        public void DeleteRoom(int id)
        {
            Room room = db.Rooms.Find(id);

            foreach(var ri in room.RoomImages.ToList())
            {
               DeleteUploadFile(ri.Image);
               db.RoomImages.Remove(ri);
            }

            db.Rooms.Remove(room);
            db.SaveChanges();

        }





		#region:方法
		private Result EditService(ServiceInfoVM vm)
		{
			IAccommodationRepository repo = new AccommodationEFRepository();
			AccommodationService service = new AccommodationService(repo);

			return service.EditService(vm);
		}
		private Result CreateRoomAndImage(RoomCreateVM vm, ImagesVM iVM)
		{
			string path = Server.MapPath("~/Uploads");
			IAccommodationRepository repo = new AccommodationEFRepository();
            AccommodationService service = new AccommodationService(repo) ;

			return service.CreateRoomAndImages(vm.ToDto(), iVM.ToDto(), path);
        }
		private void PrepareRegionAndTownSelectList()
		{
			ViewBag.RegionId = new SelectList(db.Regions.OrderBy(r => r.Id), "Id", "Name")
				.Prepend(new SelectListItem
				{
					Disabled = true,
					Selected = true,
					Text = "請選擇",
					Value = null
				});
			ViewBag.TownId = new SelectList(db.Towns, "Id", "Name");
		}
		private void PrepareRoomTypeViewBag()
		{
            var roomTypes = new List<SelectListItem>
            {
                new SelectListItem { Value = null, Text = "請選擇", Disabled = true, Selected = true },
                new SelectListItem { Value = "單人房", Text = "單人房" },
                new SelectListItem { Value = "雙人房", Text = "雙人房" },
                new SelectListItem { Value = "雙床房", Text = "雙床房" },
                new SelectListItem { Value = "三人房", Text = "三人房" },
                new SelectListItem { Value = "四人房", Text = "四人房" },
                new SelectListItem { Value = "家庭房", Text = "家庭房" },
                new SelectListItem { Value = "套房", Text = "套房" },
                new SelectListItem { Value = "雅房", Text = "雅房" },
                new SelectListItem { Value = "膠囊床位", Text = "膠囊床位" }
            };

            ViewBag.RoomType = roomTypes;

            //         var roomTypes = new List<RoomType>{
            //             new RoomType("單人房","單人房"),
            //             new RoomType("雙人房","雙人房"),
            //             new RoomType("雙床房","雙床房"),
            //             new RoomType("三人房","三人房"),
            //             new RoomType("四人房","四人房"),
            //             new RoomType("家庭房","家庭房"),
            //             new RoomType("套房","套房"),
            //             new RoomType("雅房","雅房"),
            //             new RoomType("膠囊床位","膠囊床位")
            //         };



            //ViewBag.RoomType = new SelectList(roomTypes.Select(rt => new SelectListItem { Value = rt.Value, Text = rt.Text })
            //.Prepend(new SelectListItem
            //{
            //	Disabled = true,
            //	Selected = true,
            //	Text = "請選擇",
            //	Value = null
            //}));

        }

		private Result EditAccommodationProfile(AccommodationEditVM vm, ImagesVM iVM)
		{
			string path = Server.MapPath("~/Uploads");
			IAccommodationRepository repo = new AccommodationEFRepository();
			AccommodationService service = new AccommodationService(repo);

			return service.EditAccommodationProfile(vm.ToDto(), iVM.ToDto(), path);
		}

        private Result EditRoomProfile(RoomEditVM vm, ImagesVM iVM)
		{
			string path = Server.MapPath("~/Uploads");
			IAccommodationRepository repo = new AccommodationEFRepository();
			AccommodationService service = new AccommodationService(repo);

			return service.EditRoomProfile(vm.ToDto(), iVM.ToDto(), path);
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
        
        private RoomEditVM GetRoomProfile(int? id)
		{
			IAccommodationRepository repo = new AccommodationEFRepository();
			//IProductRepository repo = new ProductDapperRepository();
			AccommodationService service = new AccommodationService(repo);
            return service.GetRoomInfo(id)?.ToVM();
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

		[HttpPost]
        public void EditImgName(string imgPath, string imgName)
        {
            AccommodationImage ai = db.AccommodationImages.FirstOrDefault(a=>a.Image ==imgPath);
            ai.Name = imgName;

            db.SaveChanges();
        }
        
        [HttpPost]
        public void DeleteImage(string imgPath)
        {
            AccommodationImage ai = db.AccommodationImages.FirstOrDefault(a => a.Image == imgPath);
            db.AccommodationImages.Remove(ai);

			db.SaveChanges();

        }
        
        [HttpPost]
        public void DeleteRoomImage(string imgPath)
        {
            RoomImage ri = db.RoomImages.FirstOrDefault(r=>r.Image == imgPath);
            db.RoomImages.Remove(ri);

			db.SaveChanges();

        }
		#endregion

	}
}
