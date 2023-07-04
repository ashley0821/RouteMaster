using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Infra;
using RouteMaster.Models.Infra.Criterias;
using RouteMaster.Models.Infra.EFRepositories;
using RouteMaster.Models.Infra.Extensions;
using RouteMaster.Models.Interfaces;
using RouteMaster.Models.Services;
using RouteMaster.Models.ViewModels;

namespace RouteMaster.Controllers
{
    public class FAQsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: FAQs
        public ActionResult Index(FAQCriteria criteria)
        {
            PrepareFAQCategoryDataSource(criteria.CategoryId);

            var mannerList = new List<FAQSortManner>()
            {
                new FAQSortManner(){Id = 0, Name=""},
                new FAQSortManner(){Id = 1, Name="幫助分數由高到低"},
                new FAQSortManner(){Id = 2, Name="最新建立"},
                new FAQSortManner(){Id = 3, Name="最新修改"}
            };

            ViewBag.SortId= new SelectList(mannerList,"Id", "Name", criteria.SortId);

            ViewBag.Criteria= criteria;

            IFAQRepository repo = new FAQEFRepository();
            FAQService service = new FAQService(repo);

            var info = service.Search(criteria)
                .ToList()
                .Select(q => q.ToIndexVM());
           
            return View(info);
        }

        // GET: FAQs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FAQ fAQ = db.FAQs.Find(id);
            if (fAQ == null)
            {
                return HttpNotFound();
            }
            return View(fAQ);
        }

        // GET: FAQs/Create
        public ActionResult Create()
        {
			PrepareFAQCategoryDataSource(null);
			return View();
        }

        // POST: FAQs/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。

        [HttpPost]
        [ValidateAntiForgeryToken]
		public ActionResult Create(FAQCreateVM vm, HttpPostedFileBase[] file1)
		{
			if (!ModelState.IsValid) return View(vm);

			Result result = ProcessCreate(vm, file1);

			PrepareFAQCategoryDataSource(vm.CategoryId);


			if (result.IsSuccess)
			{
				return RedirectToAction("Index");
			}
			else
			{
				ModelState.AddModelError(string.Empty, result.ErrorMessage);
				return View(vm);
			}

		}

		private Result ProcessCreate(FAQCreateVM vm, HttpPostedFileBase[] file1)
		{
			string path = Server.MapPath("~/Uploads");
			FAQCreateDto dto = vm.ToCreateDto();
			IFAQRepository repo = new FAQEFRepository();

			FAQService service = new FAQService(repo);
			return service.Create(dto, file1, path);

		}

		// GET: FAQs/Edit/5
		public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FAQ detail  = db.FAQs.Find(id);
            if (detail == null)
            {
                return HttpNotFound();
            }

            FAQEditVM vm= detail.ToEditDto().ToEditVM();

			PrepareFAQCategoryDataSource(vm.CategoryId);

			return View(vm);
        }

        // POST: FAQs/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FAQEditVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            Result result = UpdateFAQ(vm);
			if (result.IsFalse)
			{
				ModelState.AddModelError(string.Empty, errorMessage: result.ErrorMessage);
				return View(vm);
			}
			PrepareFAQCategoryDataSource(vm.CategoryId);

            return RedirectToAction("Index");
        }

		private Result UpdateFAQ(FAQEditVM vm)
		{
			IFAQRepository repo = new FAQEFRepository();
			FAQService service = new FAQService(repo);

			return service.Update(vm.ToEditDto());
		}

		// GET: FAQImages
		public ActionResult EditImgIndex(int? id)
        {
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			ViewBag.Id = id;

			var imgShow = db.FAQImages.Where(i => i.FAQId == id);

			if (imgShow == null)
			{
				return HttpNotFound();
			}
			var vm = imgShow.ToList().Select(i => i.ToEditImgIndexVM());
			return View(vm);

		}

		public ActionResult ChangeImg(int? imgId)
		{
			FAQImage img = db.FAQImages.Find(imgId);
			if (img == null)
			{
				return HttpNotFound();
			}
            //回傳ImgId,FAQId都綁定的entity紀錄，給予修改
            FAQChangeImgVM vm = img.ToChangeImgVM();
            
            return View(vm);

		}

		[HttpPost]
        public ActionResult ChangeImg(FAQChangeImgVM vm, HttpPostedFileBase file1)
        {
			if (vm.ImgId == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			string path = Server.MapPath("/Uploads");
			var savedFileName = SaveUploadedFile(path, file1);
         

			if (string.IsNullOrEmpty(savedFileName) == true)
			{
				ModelState.AddModelError("Image", "請選擇檔案");
				return View(vm);
			}
			vm.Image = savedFileName;

			var img = db.FAQImages.Find(vm.ImgId);
			img.Image = vm.Image;
			db.SaveChanges();

			return RedirectToAction("Index");

		}

		public ActionResult UploadImg(int id)
		{
			ViewBag.Id = id;
			return View();
		}
		[HttpPost]
		public ActionResult UploadImg(int id, FAQUploadImgVM vm, HttpPostedFileBase[] file1)
		{
			

				string path = Server.MapPath("~/Uploads");
				//將HttpPostedFileBase 集合化條列出各檔案一一存取
				foreach (var i in file1)
				{
					if (i != null)
					{
						FAQImage img = new FAQImage();
						img.FAQId = id;

						string fileName = SaveUploadedFile(path, i);
						img.Image = fileName;
						db.FAQImages.Add(img);
						db.SaveChanges();


					}
					else
					{
						ViewBag.Id = id;
						ModelState.AddModelError("Image", "請選擇檔案");
						return View(vm);
					}
				}

				return RedirectToAction("Index");

			
			//ModelState.AddModelError("Image", "請選擇檔案");
			//return View(vm);

		}

		public ActionResult DeleteFAQImg(int id)
		{
			var img = db.FAQImages.FirstOrDefault(i => i.Id == id);
			if (img == null)
			{
				return HttpNotFound();
			}
			db.FAQImages.Remove(img);
			db.SaveChanges();
			return RedirectToAction("Index"); //todo
		}

		// GET: FAQs/Delete/5
		public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FAQ detail = db.FAQs.Find(id);
            if (detail == null)
            {
                return HttpNotFound();
            }
            return View(detail);
        }

        // POST: FAQs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
			IFAQRepository repo = new FAQEFRepository();
			FAQService service = new FAQService(repo);

			service.DeleteFAQ(id);
			return RedirectToAction("Index");
		}

		private void PrepareFAQCategoryDataSource(int? categoryId)
		{
			var cate = db.FAQCategories.ToList().Prepend(new FAQCategory());
			ViewBag.CategoryId = new SelectList(cate, "Id", "Name", categoryId);
		}

		private string SaveUploadedFile(string path, HttpPostedFileBase file1)
		{
			// 如果沒有上傳檔案或檔案是空的,就不處理, 傳回 string.empty
			if (file1 == null || file1.ContentLength == 0) return string.Empty;

			// 取得上傳檔案的副檔名
			string ext = System.IO.Path.GetExtension(file1.FileName); // ".jpg" 而不是 "jpg"

			// 如果副檔名不在允許的範圍裡,表示上傳不合理的檔案類型, 就不處理, 傳回 string.empty
			string[] allowedExts = new string[] { ".jpg", ".jpeg", ".png", ".tif" };
			if (allowedExts.Contains(ext.ToLower()) == false) return string.Empty;

			// 生成一個不會重複的檔名
			string newFileName = Guid.NewGuid().ToString("N") + ext; // 生成 亂碼.jpg
			string fullName = System.IO.Path.Combine(path, newFileName);

			// 將上傳檔案存放到指定位置
			file1.SaveAs(fullName);

			// 傳回存放的檔名
			return newFileName;
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
