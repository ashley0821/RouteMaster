using Microsoft.Ajax.Utilities;
using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Infra;
using RouteMaster.Models.Infra.DapperRepositories;
using RouteMaster.Models.Infra.EFRepositories;
using RouteMaster.Models.Infra.Extensions;
using RouteMaster.Models.Interfaces;
using RouteMaster.Models.Services;
using RouteMaster.Models.ViewModels;
using RouteMaster.Models.ViewModels.Attractions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RouteMaster.Controllers
{
	public class AttractionsController : Controller
	{


		// GET: Attractions
		public ActionResult Index(AttractionCriteria criteria)
		{
			AppDbContext _db = new AppDbContext();

			ViewBag.Criteria = criteria;

			var categories = _db.AttractionCategories.ToList().Prepend(new AttractionCategory());
			ViewBag.AttractionCategories = new SelectList(categories, "Id", "Name");

			var regions = _db.Regions.ToList().Prepend(new Region());
			ViewBag.Regions = new SelectList(regions, "Id", "Name");

			var towns = _db.Towns.ToList().Prepend(new Town());
			ViewBag.Towns = new SelectList(towns, "Id", "Name");


			// 查詢紀錄，由於第一次進到網頁時，criteria是沒有值的
			var query2 = GetAttractions().ToList();

			AttractionTagsDapperRepository repo = new AttractionTagsDapperRepository();
			var tags = repo.SearchTags().ToList();

			var tagSelector = repo.AllTags().ToList().Prepend(new AttractionTagVM());
			ViewBag.Tags = new SelectList(tagSelector, "Id", "Name");

			for (var i = 0; i < query2.Count(); i++)
			{
				query2[i].Tag = tags[i];
			}

			var query = query2.AsEnumerable();

			#region where
			if (string.IsNullOrEmpty(criteria.Category) == false)
			{
				query = query.Where(p => p.Category == criteria.Category);
			}
			if (string.IsNullOrEmpty(criteria.Region) == false)
			{
				query = query.Where(p => p.Region == criteria.Region);
			}
			if (string.IsNullOrEmpty(criteria.Town) == false)
			{
				query = query.Where(p => p.Town == criteria.Town);
			}
			if (string.IsNullOrEmpty(criteria.Tag) == false)
			{
				query = query.Where(p => p.Tag == criteria.Tag);
			}

			if (string.IsNullOrEmpty(criteria.Name) == false)
			{
				query = query.Where(p => p.Name.Contains(criteria.Name));
			}

			if (criteria.MinScore.HasValue)
			{

				query = query.Where(p => double.TryParse(p.AverageScoreText, out double averageScore) && averageScore >= criteria.MinScore.Value);
			}
			if (criteria.MaxScore.HasValue)
			{
				query = query.Where(p => double.TryParse(p.AverageScoreText, out double averageScore) && averageScore <= criteria.MaxScore.Value);
			}

			if (criteria.MinHours.HasValue)
			{

				query = query.Where(p => double.TryParse(p.AverageStayHoursText, out double averageHours) && averageHours >= criteria.MinHours.Value);
			}
			if (criteria.MaxHours.HasValue)
			{
				query = query.Where(p => double.TryParse(p.AverageStayHoursText, out double averageHours) && averageHours <= criteria.MaxHours.Value);
			}

			if (criteria.MinPrice.HasValue)
			{
				query = query.Where(p => double.TryParse(p.AveragePriceText, out double averagePrice) && averagePrice >= criteria.MinPrice.Value);
			}
			if (criteria.MaxPrice.HasValue)
			{
				query = query.Where(p => double.TryParse(p.AveragePriceText, out double averagePrice) && averagePrice <= criteria.MaxPrice.Value);
			}
			#endregion

			// IEnumerable<AttractionIndexVM> products = GetAttractions();

			

			return View(query);
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

			AttractionTagsDapperRepository repo = new AttractionTagsDapperRepository();
			var tags = repo.AllTags().ToList().Distinct();

			ViewBag.Tags = tags;

			return View();
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		[ValidateInput(false)]
		public ActionResult Create(AttractionCreateVM vm, HttpPostedFileBase[] files)
		{

			if (ModelState.IsValid == false) return View(vm);

			// 建立新會員
			Result result = CreateAttraction(vm, files);

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

		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			else
			{
				IAttractionRepository repo = new AttractionEFRepository();
				AttractionService service = new AttractionService(repo);

				var vm = service.GetEditDto(id.Value).ToEditVM();
				if (vm == null)
				{
					return HttpNotFound();
				}

				AppDbContext _db = new AppDbContext();

				ViewBag.AttractionCategories = _db.AttractionCategories.ToList();
				ViewBag.Regions = _db.Regions.ToList();
				ViewBag.Towns = _db.Towns.Where(t => t.RegionId == vm.RegionId).ToList();

				AttractionTagsDapperRepository tagRepo = new AttractionTagsDapperRepository();
				ViewBag.Tags = tagRepo.AllTags().Prepend(new AttractionTagVM());

				vm.TagId = tagRepo.GetTagId(id.Value);

				return View(vm);
			}
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		[ValidateInput(false)]
		public ActionResult Edit(AttractionEditVM vm)
		{
			if (ModelState.IsValid == false) { return View(vm); }

			IAttractionRepository repo = new AttractionDapperRepository();
			AttractionService service = new AttractionService(repo);

			Result result = service.Edit(vm.ToEditDto());


			if (result.IsSuccess)
			{
				if (vm.TagId.HasValue)
				{
					AttractionTagsDapperRepository tagRepo = new AttractionTagsDapperRepository();
					tagRepo.EditTag(vm.Id, vm.TagId.Value);
				}

				return RedirectToAction("Index");
			}

			return View(vm);
		}

		public ActionResult UploadImg(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			else
			{
				ViewBag.AttractionId = id.Value;
				return View();
			}
		}

		[HttpPost]
		public ActionResult UploadImg(AttractionImageIndexVM vm, HttpPostedFileBase[] files)
		{
			if (ModelState.IsValid == false) return View(vm);

			IAttractionRepository repo = new AttractionEFRepository();
			AttractionService service = new AttractionService(repo);

			string path = Server.MapPath("~/Uploads");
			Result result = service.UploadImage(vm.ToImageIndexDto(), files, path);

			if (result.IsSuccess)
			{
				// 若成功，轉到ConfirmRegister頁
				return RedirectToAction("ImagesIndex", new {id = vm.AttractionId});
			}
			else
			{
				// 在register頁最上方新增一個放錯誤的區塊
				ModelState.AddModelError(string.Empty, result.ErrorMessage);
				return View(vm);
			}
		}

		public ActionResult ImagesIndex(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			else
			{
				var vm = GetImages(id.Value);

				if (vm == null)
				{
					return HttpNotFound();
				}

				ViewBag.AttractionId = id.Value;
				return View(vm);
			}
		}

		public ActionResult EditImage(int? imageId, int attractionId)
		{
			if (imageId == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			else
			{
				AttractionImageIndexVM vm = GetImages(attractionId)
											.Where(i => i.Id == imageId)
											.FirstOrDefault();

				if (vm == null)
				{
					return HttpNotFound();
				}

				vm.AttractionId = attractionId;
				return View(vm);
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditImage(AttractionImageIndexVM vm, HttpPostedFileBase file1)
		{
			if (vm.Id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

			if (file1 == null)
			{
				ModelState.AddModelError(string.Empty, "請選擇圖片檔案");
				return View(vm);
			}
			

			string path = Server.MapPath("/Uploads");

			IAttractionRepository repo = new AttractionEFRepository();
			AttractionService service = new AttractionService(repo);

			Result result = service.EditImage(vm.ToImageIndexDto(), file1, path);

			if (result.IsSuccess)
			{
				int attractionId = vm.AttractionId;
				return RedirectToAction("ImagesIndex", new { id = attractionId });
			}

			return View(vm);
		}

		public ActionResult DeleteImage (int? imageId,int attractionId)
		{
			if (imageId == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			else
			{
				IAttractionRepository repo = new AttractionEFRepository();
				AttractionService service = new AttractionService(repo);

				Result result = service.DeleteImage(imageId.Value);

				return RedirectToAction("ImagesIndex", new { id = attractionId });
			}
		}

		

		private IEnumerable<AttractionImageIndexVM> GetImages(int id)
		{
			IAttractionRepository repo = new AttractionEFRepository();
			AttractionService service = new AttractionService(repo);

			return service.GetImages(id).Select(i => i.ToImageIndexVM());
		}

		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			IAttractionRepository repo = new AttractionEFRepository();
			AttractionService service = new AttractionService(repo);

			var vm = service.Get(id.Value).ToDetailVM();

			if (vm == null)
			{
				return HttpNotFound();
			}

			AttractionTagsDapperRepository tagRepo = new AttractionTagsDapperRepository();

			vm.Tag = tagRepo.GetTag(id.Value);

			return View(vm);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			IAttractionRepository repo = new AttractionEFRepository();
			AttractionService service = new AttractionService(repo);

			var result = service.Delete(id);

			if (result.IsSuccess)
			{
				return RedirectToAction("Index");
			}

			var dto = service.Get(id);
			ModelState.AddModelError(string.Empty, result.ErrorMessage);

			return View(dto.ToDetailVM());
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

			var vm = service.Get(id).ToDetailVM();

			AttractionTagsDapperRepository tagRepo = new AttractionTagsDapperRepository();

			vm.Tag = tagRepo.GetTag(id);

			return vm;
		}



		private Result CreateAttraction(AttractionCreateVM vm, HttpPostedFileBase[] files)
		{
			string path = Server.MapPath("~/Uploads");

			IAttractionRepository repo = new AttractionEFRepository();
			AttractionService service = new AttractionService(repo);

			return service.Create(vm.ToCreateDto(), files, path);
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