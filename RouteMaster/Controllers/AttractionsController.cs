using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Infra;
using RouteMaster.Models.Infra.EFRepositories;
using RouteMaster.Models.Interfaces;
using RouteMaster.Models.Services;
using RouteMaster.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RouteMaster.Controllers
{
	public class AttractionsController : Controller
	{
		// GET: Attractions
		public ActionResult Index()
		{
			IEnumerable<AttractionIndexVM> products = GetAttractions();

			return View(products);
		}

		public ActionResult Create()
		{
			AppDbContext _db = new AppDbContext();

			// 取得 Towns 資料並建立 SelectList 物件
			var towns = _db.Towns.ToList();
			SelectList townSelectList = new SelectList(towns, "Id", "Name");
			ViewBag.TownSelectList = townSelectList;

			// 取得 Regions 資料並建立 SelectList 物件
			var regions = _db.Regions.ToList();
			SelectList regionSelectList = new SelectList(regions, "Id", "Name");
			ViewBag.RegionSelectList = regionSelectList;

			var categories = _db.AttractionCategories.ToList();
			SelectList categorySelectList = new SelectList(categories, "Id", "Name");
			ViewBag.CategorySelectList = categorySelectList;


			return View();
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(AttractionCreateVM vm)
		{
			if (ModelState.IsValid == false) return View(vm);

			// 建立新會員
			Result result = CreateAttraction(vm);

			if (result.IsSuccess)
			{
				// 若成功，轉到ConfirmRegister頁
				return RedirectToAction("Index");
			}
			else
			{
				// 在register頁最上方新增一個放錯誤的區塊
				ModelState.AddModelError(string.Empty, result.ErrorMessage);
				return View(vm);
			}
		}

		public ActionResult Details(int? id)
		{
			
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			else
			{
				AttractionDetailVM vm = GetAttractipnDetail(id.Value);
				
				if (vm == null)
				{
					return HttpNotFound();
				}
				return View(vm);
			}
			
		}

		private AttractionDetailVM GetAttractipnDetail(int id)
		{
			IAttractionRepository repo = new AttractionEFRepository();
			AttractionService service = new AttractionService(repo);

			AttractionDetailDto dto = service.Get(id);

			return new AttractionDetailVM
			{
				Id = dto.Id,
				Category = dto.Category,
				Region = dto.Region,
				Town = dto.Town,
				Name = dto.Name,
				Address = dto.Address,
				PositionX = dto.PositionX,
				PositionY = dto.PositionY,
				Description = dto.Description,
				Website = dto.Website,
				AverageScoreText = dto.AverageScoreText,
				AverageStayHoursText = dto.AverageStayHoursText,
				AveragePriceText = dto.AveragePriceText,
			};
			
		}

		[HttpGet]
		public ActionResult GetTownsByRegion(int regionId)
		{
			AppDbContext _db = new AppDbContext();

			var towns = _db.Towns.Where(t => t.RegionId == regionId).ToList();
			return Json(towns, JsonRequestBehavior.AllowGet);
		}

		private Result CreateAttraction(AttractionCreateVM vm)
		{
			IAttractionRepository repo = new AttractionEFRepository();
			AttractionService service = new AttractionService(repo);

			AttractionCreateDto dto = new AttractionCreateDto
			{
				AttractionCategoryId = vm.AttractionCategoryId,
				RegionId = vm.RegionId,
				TownId = vm.TownId,
				Name = vm.Name,
				Address = vm.Address,
				PositionX = vm.PositionX,
				PositionY = vm.PositionY,
				Description = vm.Description,
				Website = vm.Website,
			};
			return service.Create(dto);
		}

		private IEnumerable<AttractionIndexVM> GetAttractions()
		{
			IAttractionRepository repo = new AttractionEFRepository();
			AttractionService service = new AttractionService(repo);

			return service.Search()
				.Select(dto => new AttractionIndexVM
				{
					Id = dto.Id,
					Category = dto.Category,
					Region = dto.Region,
					Town = dto.Town,
					Name = dto.Name,
					DescriptionText = dto.DescriptionText,
					AverageScoreText = dto.AverageScoreText,
					AveragePriceText = dto.AveragePriceText,
					AverageStayHoursText = dto.AverageStayHoursText,
				});
		}
	}
}