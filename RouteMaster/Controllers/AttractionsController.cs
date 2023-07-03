using Microsoft.Ajax.Utilities;
using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Infra;
using RouteMaster.Models.Infra.EFRepositories;
using RouteMaster.Models.Infra.Extensions;
using RouteMaster.Models.Interfaces;
using RouteMaster.Models.Services;
using RouteMaster.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

			// 获取AttractionCategories表中的Id和Name数据
			var attractionCategories = _db.AttractionCategories.ToList();
			// 将数据传递给视图
			ViewBag.AttractionCategories = attractionCategories;

			// 获取Regions表格的数据
			var regions = _db.Regions.ToList();
			// 将Regions数据传递给视图
			ViewBag.Regions = regions;

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

		[HttpPost]
		public JsonResult LoadTowns(int regionId)
		{
			AppDbContext _db = new AppDbContext();

			// 从数据库中获取与地区Id相关的城镇数据
			var towns = _db.Towns.Where(t => t.RegionId == regionId).ToList();

			// 构造包含城镇Id和名称的匿名对象列表
			var townData = towns.Select(t => new { Id = t.Id, Name = t.Name }).ToList();

			return Json(townData);
		}

		private AttractionDetailVM GetAttractipnDetail(int id)
		{
			IAttractionRepository repo = new AttractionEFRepository();
			AttractionService service = new AttractionService(repo);

			return service.Get(id).ToDetailVM();
		}

		

		private Result CreateAttraction(AttractionCreateVM vm)
		{
			IAttractionRepository repo = new AttractionEFRepository();
			AttractionService service = new AttractionService(repo);

			AttractionCreateDto dto = vm.ToCreateDto();

			return service.Create(dto);
			
		}

		private IEnumerable<AttractionIndexVM> GetAttractions()
		{
			IAttractionRepository repo = new AttractionEFRepository();
			AttractionService service = new AttractionService(repo);

			return service.Search()
				.Select(dto => dto.ToIndexVM());
		}
	}
}