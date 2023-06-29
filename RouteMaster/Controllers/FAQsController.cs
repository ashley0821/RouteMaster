using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Infra;
using RouteMaster.Models.Infra.Extensions;
using RouteMaster.Models.ViewModels;

namespace RouteMaster.Controllers
{
    public class FAQsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: FAQs
        public ActionResult Index()
        {
            var fAQs = db.FAQs.Include(q => q.FAQCategory).ToList().Select(q => q.ToFAQIndexVM());
            return View(fAQs);
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
			var cate = db.FAQCategories.ToList().Prepend(new FAQCategory());
			ViewBag.CategoryId = new SelectList(cate, "Id", "Name", string.Empty);
            return View();
        }

        // POST: FAQs/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。

        [HttpPost]
        [ValidateAntiForgeryToken]
		public ActionResult Create(FAQCreateVM vm, HttpPostedFileBase file1)
		{
			if (!ModelState.IsValid) return View(vm);

			Result result = ProcessCreate(vm, file1);
			var cate = db.FAQCategories.ToList().Prepend(new FAQCategory());
			ViewBag.CategoryId = new SelectList(cate, "Id", "Name", vm.CategoryId);

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

		private Result ProcessCreate(FAQCreateVM vm, HttpPostedFileBase file1)
		{
			var text = new FAQ
			{
				CategoryId = vm.CategoryId,
				Question = vm.Question,
				Answer = vm.Answer,
				Helpful = vm.Helpful,
				CreateDate = DateTime.Now,
				ModifiedDate = DateTime.Now

			};
			db.FAQs.Add(text);

			//將HttpPostedFileBase 集合化
			//var files = Request.Files;
			FAQImage img = new FAQImage();
			img.FAQId = vm.Id;

			string path = Server.MapPath("~/Uploads");

			foreach (string i in Request.Files)
			{
				var imgItems = Request.Files[i];
				if (imgItems.ContentLength > 0)
				{
					string fileName = SaveUploadedFile(path, imgItems);

					img.Image = fileName;
					db.FAQImages.Add(img);
				}

			}
			db.SaveChanges();

			return Result.Success();
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

		// GET: FAQs/Edit/5
		public ActionResult Edit(int? id)
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
            ViewBag.CategoryId = new SelectList(db.FAQCategories, "Id", "Name", fAQ.CategoryId);
            return View(fAQ);
        }

        // POST: FAQs/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CategoryId,Question,Answer,Helpful,CreateDate,ModifiedDate")] FAQ fAQ)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fAQ).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.FAQCategories, "Id", "Name", fAQ.CategoryId);
            return View(fAQ);
        }

        // GET: FAQs/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: FAQs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FAQ fAQ = db.FAQs.Find(id);
            db.FAQs.Remove(fAQ);
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
