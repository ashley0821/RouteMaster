using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Infra.EFRepositories;
using RouteMaster.Models.Infra.Extensions;
using RouteMaster.Models.Interfaces;
using RouteMaster.Models.Services;
using RouteMaster.Models.ViewModels;
using WebGrease.Css.Extensions;

namespace RouteMaster.Controllers
{
    public class PackageToursController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: PackageTours
        public ActionResult Index()
        {
        
            //test使用導覽屬性直接改動中介表
            //int id = 5;
            //Activity activity = db.Activities.Find(id);
            //List<PackageTour> packageTours = db.PackageTours.ToList();
            //packageTours.ForEach(pt => pt.Activities.Add(activity));
            //db.SaveChanges();



           
            IPackageTourRepository repo =new PackageTourEFRepository();
            PackageTourService service = new PackageTourService(repo);


            return View(service.Search().ToList().Select(x => x.ToIndexVM()));




            //var packageTours = db.PackageTours.Include(p => p.Coupon);
            //return View(packageTours.ToList().Select(x=>x.ToIndexDto().ToIndexVM()));
        }



        

        // GET: PackageTours/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PackageTour packageTour = db.PackageTours.Find(id);
            if (packageTour == null)
            {
                return HttpNotFound();
            }
            return View(packageTour);
        }

        // GET: PackageTours/Create


        public ActionResult Create()
        {



            ViewBag.Activities = db.Activities.ToList().Select(x=>x.ToIndexDto().ToIndexVM());
            ViewBag.ExtraServices=db.ExtraServices.ToList().Select(x=>x.ToIndexDto().ToIndexVM());
            

            PrepareCouponDataSource(null);
            return View();



        }






        // POST: PackageTours/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        
        public ActionResult Create(PackageTourCreateVM vm,List<Activity> arrOfActivities,List<ExtraService> arrOfExtraServices)
        {
            IPackageTourRepository repo = new PackageTourEFRepository();
            PackageTourService service = new PackageTourService(repo);
            

            if (ModelState.IsValid == false)
            {
                PrepareCouponDataSource(vm.CouponId);
                return View(vm);
            }

            if (ModelState.IsValid)
            {
                service.Create(vm.ToCreateDto());
                return RedirectToAction("Index");
            }





            PrepareCouponDataSource(vm.CouponId);
            return View(vm);
        }









        // GET: PackageTours/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PackageTour packageTour = db.PackageTours.Find(id);
            if (packageTour == null)
            {
                return HttpNotFound();
            }
            ViewBag.CouponId = new SelectList(db.Coupons, "Id", "Discount", packageTour.CouponId);
            return View(packageTour);
        }

        // POST: PackageTours/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,Status,CouponId")] PackageTour packageTour)
        {
            if (ModelState.IsValid)
            {
                db.Entry(packageTour).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CouponId = new SelectList(db.Coupons, "Id", "Discount", packageTour.CouponId);
            return View(packageTour);
        }

        // GET: PackageTours/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PackageTour packageTour = db.PackageTours.Find(id);
            if (packageTour == null)
            {
                return HttpNotFound();
            }
            return View(packageTour);
        }

        // POST: PackageTours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PackageTour packageTour = db.PackageTours.Find(id);
            db.PackageTours.Remove(packageTour);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult ActivitiesList()
        {
            var model=db.Activities.ToList().Select(x=>x.ToIndexDto().ToIndexVM());
            //取得模型

            return this.PartialView("_ActivitiesListPartial", model);
        }

        public ActionResult ExtraServicesList()
        {
            var model = db.ExtraServices.ToList().Select(x => x.ToIndexDto().ToIndexVM());
            //取得模型

            return this.PartialView("_ExtraServicesListPartial", model);
        }


        //public ActionResult AttractionsList()
        //{
        //    var model = db.Attractions.ToList().Select(x => x.ToIndexDto().ToIndexVM());
        //    //取得模型

        //    return this.PartialView("_AttractionsListListPartial", model);
        //}






        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }



        private void PrepareCouponDataSource(int? couponId)
        {
            var coupons = db.Coupons.ToList().Prepend(new Coupon() {Discount=1 });
            ViewBag.CouponId = new SelectList(coupons, "Id", "Discount", couponId);
        }
    }
}
