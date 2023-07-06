using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Infra.Criterias;
using RouteMaster.Models.Infra.EFRepositories;
using RouteMaster.Models.Infra.Extensions;
using RouteMaster.Models.Interfaces;
using RouteMaster.Models.Services;
using RouteMaster.Models.ViewModels;

namespace RouteMaster.Controllers
{
    public class Comments_AttractionsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Comments_Attractions
        public ActionResult Index(Comments_AttractionCriteria criteria)
        {
            var mannerList3 = new List<FAQSortManner>()
            {
               new FAQSortManner(){Id = 0, Name=""},
                new FAQSortManner(){Id = 1, Name="評分分數由高至低"},
				new FAQSortManner(){Id = 2, Name="停留時間由高至低"},
				new FAQSortManner(){Id = 3, Name="花費由多至少"},
				 new FAQSortManner(){Id = 4, Name="最新建立"}
			};

			ViewBag.SortId = new SelectList(mannerList3, "Id", "Name", criteria.SortId);
			ViewBag.Criteria = criteria;

			IComments_AttractionsRepository repo = new Comments_AttractionsEFRepository();
            Comments_AttractionsService service = new Comments_AttractionsService(repo);

            var info = service.Search(criteria).ToList().Select(c => c.ToIndexVM());

            return View(info);

		}

        // GET: Comments_Attractions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var commAttrDb = db.Comments_Attractions.Include(c => c.Attraction)
                 .FirstOrDefault(c => c.Id == id).ToDetailVM();
            var img = db.Comments_AttractionImages.Where(i => i.Comments_AttractionId == id).ToList();
            if (commAttrDb == null)
            {
                return HttpNotFound();
            }

            commAttrDb.Images = img.Select(i=>i.Image);


            return View(commAttrDb);
        }

        // GET: Comments_Attractions/Create
        public ActionResult Create()
        {
            ViewBag.AttractionId = new SelectList(db.Attractions, "Id", "Name");
            ViewBag.MemberId = new SelectList(db.Members, "Id", "Account");
            return View();
        }

        // POST: Comments_Attractions/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Comments_AttractionsCreateVM vm, HttpPostedFileBase[] file1)
        {

			ViewBag.AttractionId = new SelectList(db.Attractions, "Id", "Name", vm.AttractionId);
			ViewBag.MemberId = new SelectList(db.Members, "Id", "Account", vm.MemberId);

			if (ModelState.IsValid)
            {
                var comment= vm.ToCreateDto().ToCreateEnity();
                db.Comments_Attractions.Add(comment);

                Comments_AttractionImages img = new Comments_AttractionImages();
				string path = Server.MapPath("~/Uploads");
				foreach (var i in file1)
				{
					if (i != null)
					{
						string fileName = SaveUploadedFile(path, i);
						img.Image = fileName;
						db.Comments_AttractionImages.Add(img);
						db.SaveChanges();
					
					}
					else
					{
						db.SaveChanges();
						
					}
				}
				return RedirectToAction("Index");

			}
            return View(vm);
        }

        // GET: Comments_Attractions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comments_Attractions comments_Attractions = db.Comments_Attractions.Find(id);
            if (comments_Attractions == null)
            {
                return HttpNotFound();
            }
            ViewBag.AttractionId = new SelectList(db.Attractions, "Id", "Name", comments_Attractions.AttractionId);
            ViewBag.MemberId = new SelectList(db.Members, "Id", "FirstName", comments_Attractions.MemberId);
            return View(comments_Attractions);
        }

        // POST: Comments_Attractions/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MemberId,AttractionId,Score,Content,StayHours,Price,CreateDate")] Comments_Attractions comments_Attractions)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comments_Attractions).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AttractionId = new SelectList(db.Attractions, "Id", "Name", comments_Attractions.AttractionId);
            ViewBag.MemberId = new SelectList(db.Members, "Id", "FirstName", comments_Attractions.MemberId);
            return View(comments_Attractions);
        }

        // GET: Comments_Attractions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comments_Attractions comments_Attractions = db.Comments_Attractions.Find(id);
            if (comments_Attractions == null)
            {
                return HttpNotFound();
            }
            return View(comments_Attractions);
        }

        // POST: Comments_Attractions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comments_Attractions comments_Attractions = db.Comments_Attractions.Find(id);
            db.Comments_Attractions.Remove(comments_Attractions);
            db.SaveChanges();
            return RedirectToAction("Index");
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
