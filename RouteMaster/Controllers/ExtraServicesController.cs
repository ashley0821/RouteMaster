using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Infra.DapperRepositories;
using RouteMaster.Models.Infra.Extensions;
using RouteMaster.Models.Interfaces;
using RouteMaster.Models.Services;
using RouteMaster.Models.ViewModels;

namespace RouteMaster.Controllers
{
    public class ExtraServicesController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: ExtraServices
        public ActionResult Index()
        {
            IEnumerable<ExtraServiceIndexVM> extraServices = GetExtraServices();
            return View(extraServices);
        }

		private IEnumerable<ExtraServiceIndexVM> GetExtraServices()
		{
            IExtraServiceRepository repo =new ExtraServiceDapperRepository();
            ExtraServiceService service =new ExtraServiceService(repo);

            return service.Search()
                .Select(dto=>dto.ToIndexVM());
			
		}

		// GET: ExtraServices/Details/5
		public ActionResult Details(int id)
        {
            IExtraServiceRepository repo=new ExtraServiceDapperRepository();
            ExtraServiceService service = new ExtraServiceService(repo);

            var extraService= service.GetExtraServiceById(id);     
            return View(extraService.ToIndexDto().ToIndexVM());
        }

        // GET: ExtraServices/Create
        public ActionResult Create()
        {
            PrepareAttractionDataSource(null);
            return View();
        }

        // POST: ExtraServices/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ExtraServiceCreateVM vm)
        {
            IExtraServiceRepository repo = new ExtraServiceDapperRepository();
            ExtraServiceService service=new ExtraServiceService(repo);  

            if (ModelState.IsValid)
            {
                service.Create(vm.ToCreateDto());        
                return RedirectToAction("Index");
            }


            PrepareAttractionDataSource(vm.AttractionId);
            return View(vm);
        }


        // GET: ExtraServices/Edit/5
        public ActionResult Edit(int id)
        {          
            IExtraServiceRepository repo=new ExtraServiceDapperRepository();
            ExtraServiceService service=new ExtraServiceService(repo);
            var extraService = service.GetExtraServiceById(id);

            if (extraService == null)
            {
                return HttpNotFound();
            }             
            
            PrepareAttractionDataSource(extraService.AttractionId);

            return View(extraService.ToEditDto().ToEditVM());
        }

        // POST: ExtraServices/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ExtraServiceEditVM vm)
        {
            IExtraServiceRepository repo=new ExtraServiceDapperRepository();
            ExtraServiceService service = new ExtraServiceService(repo);

            if (ModelState.IsValid)
            {
               service.Edit(vm.ToEditDto());
                return RedirectToAction("Index");
            }


           PrepareAttractionDataSource (vm.AttractionId);
            return View(vm);
        }

        // GET: ExtraServices/Delete/5
        public ActionResult Delete(int id)
        {
			IExtraServiceRepository repo = new ExtraServiceDapperRepository();
			ExtraServiceService service = new ExtraServiceService(repo);

			var extraService = service.GetExtraServiceById(id);
			return View(extraService.ToIndexDto().ToIndexVM());
		}

        // POST: ExtraServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IExtraServiceRepository repo = new ExtraServiceDapperRepository();
            ExtraServiceService service=new ExtraServiceService(repo);
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

        private void PrepareAttractionDataSource(int? attractionId)
        {
			var attractions = db.Attractions
                .OrderBy(x => x.Id)
                .ToList()
                .Prepend(new Attraction { Id = 0, Name = "全部景點" });
			ViewBag.AttractionId = new SelectList(attractions, "Id", "Name", attractionId);
		}
    }
}
