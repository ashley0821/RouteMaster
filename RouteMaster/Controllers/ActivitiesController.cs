using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
    public class ActivitiesController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Activities
        public ActionResult Index(ActivityIndexCriteria criteria)
        {
            ViewBag.Criteria=criteria;
            PrepareActivityCategoryDataSource(criteria.ActivityCategoryId);
            PrepareRegionDataSource(criteria.RegionId);
            PrepareAttractionDataSource(criteria.AttractionId); 



            IEnumerable<ActivityIndexVM> activities = GetActivities(criteria);
            return View(activities);
        }


		private IEnumerable<ActivityIndexVM> GetActivities(ActivityIndexCriteria criteria)
		{
            IActivityRepository repo = new ActivityEFRepository();
            ActivityService service=new ActivityService(repo);

            return service.Search(criteria)                   
                   .Select(a => a.ToIndexVM());           
		}


        // GET: Activities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // GET: Activities/Create


        public ActionResult Create()  
        {
            //ViewBag.ActivityCategoryId = new SelectList(db.ActivityCategories, "Id", "Name");          
            //ViewBag.AttractionId = new SelectList(db.Attractions, "Id", "Name");
            //ViewBag.RegionId = new SelectList(db.Regions, "Id", "Name");


			PrepareActivityCategoryDataSource(null);
            PrepareAttractionDataSource(null);
            PrepareRegionDataSource(null);			
            return View();
        }



        // POST: Activities/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ActivityCreateVM vm)
        {

			IActivityRepository repo = new ActivityEFRepository();
			ActivityService service = new ActivityService(repo);

			if (ModelState.IsValid == false)
            {
                return View();
            }

            if (ModelState.IsValid)
            {
                service.Create(vm.ToCreateDto());
                return RedirectToAction("Index");
            }



            PrepareActivityCategoryDataSource(vm.ActivityCategoryId);
            PrepareAttractionDataSource(vm.AttractionId);
            PrepareRegionDataSource(vm.AttractionId);
            return View(vm);
        }







        // GET: Activities/Edit/5
        public ActionResult Edit(int id)
        {
            IActivityRepository repo=new ActivityEFRepository();
            ActivityService service = new ActivityService(repo);
            var activity = service.GetActivityById(id);




            if (activity == null)
            {
                return HttpNotFound();
            }
           

            PrepareActivityCategoryDataSource(activity.ActivityCategoryId);
            PrepareAttractionDataSource(activity.AttractionId);
            PrepareRegionDataSource(activity.RegionId);



            return View(activity.ToEditDto().ToEditVM());
        }

        // POST: Activities/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ActivityEditVM vm)
        {
            IActivityRepository repo=new ActivityEFRepository();
            ActivityService service=new ActivityService(repo);

            if (ModelState.IsValid)
            {
                service.Edit(vm.ToEditDto());             
                return RedirectToAction("Index");
            }

			PrepareActivityCategoryDataSource(vm.ActivityCategoryId);
			PrepareAttractionDataSource(vm.AttractionId);
			PrepareRegionDataSource(vm.RegionId);

			return View(vm);
        }




        // GET: Activities/Delete/5
        public ActionResult Delete(int id)
        {
            IActivityRepository repo=new ActivityEFRepository();
            ActivityService service = new ActivityService(repo);
            var activity = service.GetActivityById(id);                        
           
            
            return View(activity.ToIndexDto().ToIndexVM());
        }  
           
           
           
        // POST: Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IActivityRepository repo=new ActivityEFRepository();
            ActivityService service=new ActivityService(repo);
            service.Delete(id);


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

		private void PrepareActivityCategoryDataSource(int? categoryId)
		{
			var categories = db.ActivityCategories
                .OrderBy(x=>x.Id)
                .ToList()
                .Prepend(new ActivityCategory {Id=0,Name="全部活動種類" });
			ViewBag.ActivityCategoryId = new SelectList(categories, "Id", "Name", categoryId);
		}
        private void PrepareAttractionDataSource(int? attractionId)
		{
			var attractions = db.Attractions
                .OrderBy(x=>x.Id)
                .ToList()
                .Prepend(new Attraction { Id=0,Name="全部景點"});
			ViewBag.AttractionId = new SelectList(attractions, "Id", "Name", attractionId);
		}
		private void PrepareRegionDataSource(int? regionId)
		{
			var regions = db.Regions
                .OrderBy(x=>x.Id)
                .ToList()
                .Prepend(new Region { Id=0,Name="全部區域"});
			ViewBag.RegionId = new SelectList(regions, "Id", "Name", regionId);
		}

        public ActionResult GetAttractionsByRegion(int regionId)
        {

            List<Attraction> attractions;

            if (regionId > 0)
            {
                attractions = db.Attractions.Where(a => a.RegionId == regionId).ToList();
            }
            else
            {
                attractions = db.Attractions.ToList();
            }


            //構建一個包含景點Id和名稱的列表，用於返回給Ajax請求
            var attractionList = attractions.Select(a => new
            {
                Value = a.Id,
                Text = a.Name
            });

            return Json(attractionList, JsonRequestBehavior.AllowGet);
        }

        public int GetRegionIdByAttraction(int attractionId)
        {
            var regionId = db.Attractions.Where(a => a.Id == attractionId).Select(a => a.RegionId).FirstOrDefault();
            return regionId;
        }



    }
}
