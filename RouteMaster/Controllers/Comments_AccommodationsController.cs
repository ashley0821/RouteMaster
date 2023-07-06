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
using RouteMaster.Models.Infra.EFRepositories;
using RouteMaster.Models.Infra.Extensions;
using RouteMaster.Models.Interfaces;
using RouteMaster.Models.Services;
using RouteMaster.Models.ViewModels;

namespace RouteMaster.Controllers
{
    [Authorize]
    public class Comments_AccommodationsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Comments_Accommodations
        public ActionResult Index()
        {
            IComments_AccommodationsRepository repo = new Comments_AccommodationsEFRepository();
            Comments_AccommodationsService service = new Comments_AccommodationsService(repo);

            var info = service.Search().ToList().Select(c => c.ToIndexVM());

			return View(info);
        }

        // GET: Comments_Accommodations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comments_Accommodations comments_Accommodations = db.Comments_Accommodations.Find(id);
            if (comments_Accommodations == null)
            {
                return HttpNotFound();
            }
            return View(comments_Accommodations);
        }

        // GET: Comments_Accommodations/Create
        public ActionResult Create(int? id=1)
        {
            //住宿id由特定住宿網址連接至Create，Autolink 其住宿id
            if(id == null)
            {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
            Comments_AccommodationsCreateVM vm = new Comments_AccommodationsCreateVM();

            vm.MemberAccount = "Allen"; //之後串接完成改User.Identity.Name########
            vm.AccomodationId = (int)id;
            
            return View(vm);
        }

        // POST: Comments_Accommodations/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Comments_AccommodationsCreateVM vm, HttpPostedFileBase[] file1)
        {
            if (!ModelState.IsValid) return View(vm);

            Result result= ProcessCreate(vm, file1);

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

		private Result ProcessCreate(Comments_AccommodationsCreateVM vm, HttpPostedFileBase[] file1)
		{
			string path = Server.MapPath("~/Uploads");
			IComments_AccommodationsRepository repo = new Comments_AccommodationsEFRepository();
            Comments_AccommodationsService service = new Comments_AccommodationsService(repo);

             return service.Create(vm.ToCreateDto(), file1, path);

		}

		// GET: Comments_Accommodations/Edit/5
		public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comments_Accommodations commAccDb = db.Comments_Accommodations.Find(id);
            if (commAccDb == null)
            {
                return HttpNotFound();
            }
            Comments_AccommodationsEditVM vm = commAccDb.ToEditDto().ToEditVM();

			return View(vm);
        }

        // POST: Comments_Accommodations/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Comments_AccommodationsEditVM vm)
        {
            if (!ModelState.IsValid) return View(vm);

            Result result = UpdateComment(vm);

            if(result.IsFalse)
            {
				ModelState.AddModelError(string.Empty, errorMessage: result.ErrorMessage);
				return View(vm);
			}
			return RedirectToAction("Index");
		}

		private Result UpdateComment(Comments_AccommodationsEditVM vm)
		{
            IComments_AccommodationsRepository repo = new Comments_AccommodationsEFRepository();
            Comments_AccommodationsService service = new Comments_AccommodationsService(repo);

            return service.Update(vm.ToEditDto());
		}

		//GET:Comments_AccommodationImages
        public ActionResult ImgIndex(int? id)
        {
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
            ViewBag.Id = id;

            var imgShow= db.Comments_AccommodationImages.Where(i => i.Comments_AccommodationId == id);
            if(imgShow == null)
            {
                return HttpNotFound();
			}

            var vm = imgShow.ToList().Select(i => i.ToImgIndexVM());
            return View(vm);
		}

        public ActionResult ChangeImg(int? imgId)
        {
            Comments_AccommodationImages img = db.Comments_AccommodationImages.Find(imgId);
            if (img == null)
            {
                return HttpNotFound();
            }
            Comments_AccommodationsChangeImgVM vm= img.ToChangeImgVM();
            return View(vm);
        }
        [HttpPost]
        public ActionResult ChangeImg(Comments_AccommodationsChangeImgVM vm, HttpPostedFileBase file1)
        {
			string path = Server.MapPath("/Uploads");
			var savedFileName = SaveUploadedFile(path, file1);

			if (string.IsNullOrEmpty(savedFileName) == true)
			{
				ModelState.AddModelError("Image", "請選擇檔案");
				return View(vm);
			}

            var img = db.Comments_AccommodationImages.Find(vm.ImgId);
            img.Image = savedFileName;
            db.SaveChanges();

            return RedirectToAction("ImgIndex", new {id= vm.CommentId});

		}

        public ActionResult UploadImg(int id)
        {
            ViewBag.ParentId = id;
            return View();
        }

        [HttpPost]
        public ActionResult UploadImg(int id, Comments_AccommodationsUploadImgVM vm, HttpPostedFileBase[] file1)
        {
			string path = Server.MapPath("~/Uploads");
			//將HttpPostedFileBase 集合化條列出各檔案一一存取
            foreach(var i in file1)
            {
                if (i != null)
                {
                    Comments_AccommodationImages img= new Comments_AccommodationImages();
                    img.Comments_AccommodationId = id;

					string fileName = SaveUploadedFile(path, i);

                    img.Image= fileName;
                    db.Comments_AccommodationImages.Add(img);
                    db.SaveChanges();
				}
                else
                {
                    ViewBag.ParentId=id;
					ModelState.AddModelError("Image", "請選擇檔案");
					return View(vm);
				}
            }

			return RedirectToAction("ImgIndex", new {id=id});

		}

        public ActionResult DeleteImg(int imgId, int commentId)
        {
            var img =db.Comments_AccommodationImages.FirstOrDefault(i =>i.Id == imgId);
            if(img == null)
            {
                return HttpNotFound();
            }
            db.Comments_AccommodationImages.Remove(img);
            db.SaveChanges();
            return RedirectToAction("ImgIndex",new {id=commentId});

        }

		// GET: Comments_Accommodations/Delete/5
		public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comments_Accommodations commAccDb = db.Comments_Accommodations.Find(id);
            if (commAccDb == null)
            {
                return HttpNotFound();
            }
            return View(commAccDb);
        }

        // POST: Comments_Accommodations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IComments_AccommodationsRepository repo = new Comments_AccommodationsEFRepository();
            Comments_AccommodationsService service= new Comments_AccommodationsService(repo);
            service.DeleteComment_Accommodation(id);

            return RedirectToAction("Index");
        }

        public ActionResult Comments()
        {
            return View();
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
