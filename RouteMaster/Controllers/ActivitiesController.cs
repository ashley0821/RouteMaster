using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;

using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RouteMaster.Models.EFModels;
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
        public ActionResult Index()
        {
            IEnumerable<ActivityIndexVM> activities = GetActivities();
            return View(activities);
        }
		private IEnumerable<ActivityIndexVM> GetActivities()
		{
            IActivityRepository repo = new ActivityEFRepositoy();
            ActivityService service=new ActivityService(repo);

            return service.Search()
                   .ToList()
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

			IActivityRepository repo = new ActivityEFRepositoy();
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

            //ViewBag.ActivityCategoryId = new SelectList(db.ActivityCategories, "Id", "Name", vm.ActivityCategoryId);
            //ViewBag.AttractionId = new SelectList(db.Attractions, "Id", "Name", vm.AttractionId);
            //ViewBag.RegionId = new SelectList(db.Regions, "Id", "Name", vm.RegionId);


            PrepareActivityCategoryDataSource(vm.ActivityCategoryId);
            PrepareAttractionDataSource(vm.AttractionId);
            PrepareRegionDataSource(vm.AttractionId);
            return View(vm);
        }

        // GET: Activities/Edit/5
        public ActionResult Edit(int? id)
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

            PrepareActivityCategoryDataSource(activity.ActivityCategoryId);
            PrepareAttractionDataSource(activity.AttractionId);
            PrepareRegionDataSource(activity.RegionId);




            //ViewBag.ActivityCategoryId = new SelectList(db.ActivityCategories, "Id", "Name", activity.ActivityCategoryId);
            //ViewBag.AttractionId = new SelectList(db.Attractions, "Id", "Name", activity.AttractionId);
            //ViewBag.RegionId = new SelectList(db.Regions, "Id", "Name", activity.RegionId);



            return View(activity.ToEditDto().ToEditVM());
        }

        // POST: Activities/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ActivityEditVM vm)
        {
            IActivityRepository repo=new ActivityEFRepositoy();
            ActivityService service=new ActivityService(repo);

            if (ModelState.IsValid)
            {
                service.Edit(vm);             
                return RedirectToAction("Index");
            }

			PrepareActivityCategoryDataSource(vm.ActivityCategoryId);
			PrepareAttractionDataSource(vm.AttractionId);
			PrepareRegionDataSource(vm.RegionId);


			//ViewBag.ActivityCategoryId = new SelectList(db.ActivityCategories, "Id", "Name", activity.ActivityCategoryId);
			//ViewBag.AttractionId = new SelectList(db.Attractions, "Id", "Name", activity.AttractionId);
			//ViewBag.RegionId = new SelectList(db.Regions, "Id", "Name", activity.RegionId);
			return View(vm);
        }




        // GET: Activities/Delete/5
        public ActionResult Delete(int? id)
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



        // POST: Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IActivityRepository repo=new ActivityEFRepositoy();
            ActivityService service=new ActivityService(repo);
            service.Delete(id);


            //Activity activity = db.Activities.Find(id);
            //db.Activities.Remove(activity);
            //db.SaveChanges();

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
			var categories = db.ActivityCategories.ToList().Prepend(new ActivityCategory());
			ViewBag.ActivityCategoryId = new SelectList(categories, "Id", "Name", categoryId);
		}
        private void PrepareAttractionDataSource(int? attractionId)
		{
			var attractions = db.Attractions.ToList().Prepend(new Attraction());
			ViewBag.AttractionId = new SelectList(attractions, "Id", "Name", attractionId);
		}
		private void PrepareRegionDataSource(int? regionId)
		{
			var regions = db.Regions.ToList().Prepend(new Region());
			ViewBag.RegionId = new SelectList(regions, "Id", "Name", regionId);
		}

	}
}
