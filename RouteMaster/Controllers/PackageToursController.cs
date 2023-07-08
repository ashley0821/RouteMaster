using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Infra;
using RouteMaster.Models.Infra.Criterias;
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
            IPackageTourRepository repo =new PackageTourEFRepository();
            PackageTourService service = new PackageTourService(repo);
            return View(service.Search().ToList().Select(x => x.ToIndexVM()));
        }



        


        // GET: PackageTours/Create


        public ActionResult Create()
        {
            ViewBag.Attractions=db.Attractions.ToList().Select(x=>x.ToAttractionListIndexDto().ToAttractionListIndexVM());
            ViewBag.Activities = db.Activities.ToList().Select(x=>x.ToIndexDto().ToIndexVM());
            ViewBag.ExtraServices=db.ExtraServices.ToList().Select(x=>x.ToIndexDto().ToIndexVM());       

            PrepareCouponDataSource(null);
            return View();
        }






        // POST: PackageTours/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]

        public ActionResult Create(PackageTourCreateVM vm) 
		{
            IPackageTourRepository repo = new PackageTourEFRepository();
            PackageTourService service = new PackageTourService(repo);



            //根據接收到的活動Id獲取完整的活動對象           
            List<Activity> selectedActivities = new List<Activity>();
            foreach (var activityId in vm.Activities.Select(a => a.Id))
            {
                var activity = db.Activities.Find(activityId);
                selectedActivities.Add(activity);
            }
            //將完整的活動物件添加到Activities列表當中
            vm.Activities = selectedActivities.Select(a => a.ToIndexDto().ToIndexVM()).ToList();
         


            List<ExtraService> selectedExtraService=new List<ExtraService>();
            foreach(var ectraServiceId in vm.ExtraServices.Select(e => e.Id))
            {
                var extraService = db.ExtraServices.Find(ectraServiceId);
                selectedExtraService.Add(extraService);
            }
            vm.ExtraServices=selectedExtraService.Select(e=>e.ToIndexDto().ToIndexVM()).ToList();   




            List<Attraction> selectedAttractions=new List<Attraction>();
            foreach(var attractionId in vm.Attractions.Select(a => a.Id))
            {
                var attraction = db.Attractions.Find(attractionId);
                selectedAttractions.Add(attraction);
            }
            vm.Attractions=selectedAttractions.Select(a=>a.ToAttractionListIndexDto().ToAttractionListIndexVM()).ToList();






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


            return View("Index");
        }





        // GET: PackageTours/Edit/5
        public ActionResult Edit(int id)
        {
            IPackageTourRepository repo = new PackageTourEFRepository();
            PackageTourService service = new PackageTourService(repo);


            var packageTour= service.GetPackageTourById(id);
            PrepareCouponDataSource(packageTour.CouponId);
            
            return View(packageTour.ToEditDto().ToEditVM());
        }




        // POST: PackageTours/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PackageTourEditVM vm)
        {
            IPackageTourRepository repo=new PackageTourEFRepository();  
            PackageTourService service=new PackageTourService(repo);

			//根據接收到的活動Id獲取完整的活動對象           
			List<Activity> selectedActivities = new List<Activity>();
			foreach (var activityId in vm.Activities.Select(a => a.Id))
			{
				var activity = db.Activities.Find(activityId);
				selectedActivities.Add(activity);
			}
			//將完整的活動物件添加到Activities列表當中
			vm.Activities = selectedActivities.Select(a=>a.ToEditDto().ToEditVM()).ToList();


			List<ExtraService> selectedExtraService = new List<ExtraService>();
			foreach (var ectraServiceId in vm.ExtraServices.Select(e => e.Id))
			{
				var extraService = db.ExtraServices.Find(ectraServiceId);
				selectedExtraService.Add(extraService);
			}
			vm.ExtraServices = selectedExtraService.Select(e => e.ToEditDto().ToEditVM()).ToList();




			List<Attraction> selectedAttractions = new List<Attraction>();
			foreach (var attractionId in vm.Attractions.Select(a => a.Id))
			{
				var attraction = db.Attractions.Find(attractionId);
				selectedAttractions.Add(attraction);
			}
			vm.Attractions = selectedAttractions.Select(a => a.ToAttractionListEditDto().ToAttractionListEditVM()).ToList();





			service.Edit(vm.ToEditDto());        

            PrepareCouponDataSource(vm.CouponId); 
            
            return RedirectToAction("Index");   
        }

        // GET: PackageTours/Delete/5
        public ActionResult Delete(int id)
        {
            IPackageTourRepository repo = new PackageTourEFRepository();
            PackageTourService service = new PackageTourService(repo);

            return View(service.GetPackageTourById(id).ToIndexDto().ToIndexVM());
            
        }

        // POST: PackageTours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IPackageTourRepository repo = new PackageTourEFRepository();
            PackageTourService service = new PackageTourService(repo);

            service.Delete(id);

            return RedirectToAction("Index");
        }




        // GET: PackageTours/Details/5
        public ActionResult Details(int id)
        {
            IPackageTourRepository repo = new PackageTourEFRepository();
            PackageTourService service = new PackageTourService(repo);

            return View(service.GetPackageTourById(id).ToIndexDto().ToIndexVM());
        }



        public PartialViewResult ActivitiesList(ActivityIndexCriteria actCriteria)
        {       
            ViewBag.ActCriteria = actCriteria;
            PrepareActivityCategoryDataSource(actCriteria.ActivityCategoryId);
            PrepareRegionDataSource(actCriteria.RegionId);
            PrepareAttractionDataSource(actCriteria.AttractionId);



            IEnumerable<ActivityIndexVM> activities = GetActivities(actCriteria);


            return this.PartialView("_ActivitiesListPartial", activities);
        }



        private IEnumerable<ActivityIndexVM> GetActivities(ActivityIndexCriteria actCriteria)
        {
            IActivityRepository repo = new ActivityEFRepository();
            ActivityService service = new ActivityService(repo);

            return service.Search(actCriteria)
                   .Select(a => a.ToIndexVM());
        }




        public ActionResult ExtraServicesList()
        {
            var model = db.ExtraServices.ToList().Select(x => x.ToIndexDto().ToIndexVM());
            //取得模型

            return this.PartialView("_ExtraServicesListPartial", model);
        }



        //測試Fetch
        [HttpPost]
        public ActionResult SearchExtraService(string searchKeyword)
		{

            var newModel = db.ExtraServices
                 .Where(x => searchKeyword==""?true:x.Name.Contains(searchKeyword)).ToList()
                 .Select(x => x.ToIndexDto().ToIndexVM());


            return Json(newModel);

            //return PartialView("_ExtraServicesListPartial", newModel);
        }





        [HttpPost]
        public ActionResult SearchAttraction(string searchKeyword)
        {
            
            var newModel = db.Attractions
                 .Where(x => searchKeyword == "" ? true : x.Name.Contains(searchKeyword)).ToList()
                 .Select(x => x.ToAttractionListIndexDto().ToAttractionListIndexVM());


            

            return Json(newModel);

            //return PartialView("_ExtraServicesListPartial", newModel);
        }





        public ActionResult AttractionsList()
        {
            var model = db.Attractions.ToList();                
             
            //取得模型

            return this.PartialView("_AttractionsListPartial", model);
        }



        //分頁測試
		public ActionResult GetQueryResult(string search, int? draw, int? start, int length)
		{
			var query = db.ExtraServices.AsQueryable();

			if (!string.IsNullOrEmpty(search))
			{
				query = query.Where(x => x.Name.Contains(search));
			}


			int totalRecords = query.Count();
			int pageNumber = (start ?? 0 / length) + 1;
			int skipRecords = (pageNumber - 1) * length;
			var pagedData = query.OrderBy(x=>x.Id).Skip(skipRecords).Take(length)
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.AttractionId,
                    x.Price,
                    x.Description,
                    x.Status
                })
                .ToList();
			int filteredRecords = pagedData.Count();

			var result = new
			{
				draw = draw,
				recordsTotal = totalRecords, // 總資料數
				recordsFiltered = filteredRecords, // 符合搜尋條件的資料數
				data = pagedData // 分頁後的資料
			};

			return Json(result, JsonRequestBehavior.AllowGet);
		}



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
            var coupons = db.Coupons.OrderBy(x=>x.Id).ToList();
            ViewBag.CouponId = new SelectList(coupons, "Id", "Discount", couponId);
        }

        private void PrepareActivityCategoryDataSource(int? categoryId)
        {
            var categories = db.ActivityCategories
                .OrderBy(x => x.Id)
                .ToList()
                .Prepend(new ActivityCategory { Id = 0, Name = "全部活動種類" });
            ViewBag.ActivityCategoryId = new SelectList(categories, "Id", "Name", categoryId);
        }
        private void PrepareAttractionDataSource(int? attractionId)
        {
            var attractions = db.Attractions
                .OrderBy(x => x.Id)
                .ToList()
                .Prepend(new Attraction { Id = 0, Name = "全部景點" });
            ViewBag.AttractionId = new SelectList(attractions, "Id", "Name", attractionId);
        }
        private void PrepareRegionDataSource(int? regionId)
        {
            var regions = db.Regions
                .OrderBy(x => x.Id)
                .ToList()
                .Prepend(new Region { Id = 0, Name = "全部區域" });
            ViewBag.RegionId = new SelectList(regions, "Id", "Name", regionId);
        }
    }
}
