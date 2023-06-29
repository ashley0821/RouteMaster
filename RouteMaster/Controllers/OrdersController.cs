using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Infra.DapperRepositories;
using RouteMaster.Models.Infra.EFRepositories;
using RouteMaster.Models.Infra.Extensions;
using RouteMaster.Models.Interfaces;
using RouteMaster.Models.Services;
using RouteMaster.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RouteMaster.Controllers
{
	public class OrdersController : Controller

	{
		private readonly AppDbContext db = new AppDbContext();
		// GET: Orders
		public ActionResult Index()
		{
			IEnumerable<OrderIndexVM> orders = (IEnumerable<OrderIndexVM>)GetOrders();

			return View(orders);


		}
		//Order
		private IEnumerable<OrderIndexVM> GetOrders()
		{
			IOrderRepository repo = new OrderEFRepository();
			OrderService service = new OrderService(repo);

			return service.Search()
				   .ToList()
				   .Select(o => o.ToIndexVM());
		}

		public ActionResult Details(int id)
		{
			
			Order order = db.Orders.Find(id);
			if (order == null)
			{
				return HttpNotFound();
			}
			return View(order);
		}


		//activitiesdetails(ef)

		//public actionresult indexdapper(int id)
		//{

		//	var viewmodelitems = db.activitiesdetails
		//		.tolist()
		//		.select(dto => new activitiesdetailsindexvm
		//		{
		//			id = dto.id,
		//			orderid = dto.orderid,
		//			activityid = dto.activityid,
		//			activityname = dto.activityname,
		//			starttime = dto.starttime,
		//			endtime = dto.endtime,
		//			price = dto.price,
		//			quantity = dto.quantity,

		//		});

		//	//viewmodelitems.select(x => x.orderid = id).tolist();
		//	return partialview("_indexdapper", viewmodelitems.where(x => x.orderid == id).tolist());
		//}


		//
		public ActionResult IndexDapper(int orderId)

		{
			ActivitiesDetailsDapperRepository repo= new ActivitiesDetailsDapperRepository();
			List<ActivitiesDetailsIndexVM> activitiesdetails = repo.GetActivitiesDetails(orderId);
			return PartialView("_IndexDapper", activitiesdetails);

		}


        //ExtraServiceDetails (EF)
  //      public ActionResult ExtraServicesDetailsPartialView(int id)
		//{
			

		//	var viewModelItems = db.ExtraServicesDetails
		//						.ToList()
		//						.Select(dto => new ExtraServicesDetailsVM
		//						{
		//							Id = dto.Id,
		//							OrderId = dto.OrderId,
		//							ExtraServiceId = dto.ExtraServiceId,
		//							ExtraServiceName = dto.ExtraServiceName,
		//							Price = dto.Price,
		//							Quantity = dto.Quantity,
		//						});

		//	//viewModelItems.Select(x=>x.OrderId = id).ToList();
		//	//return PartialView("ExtraServicesDetailsPartialView",viewModelItems.Where(x=>x.OrderId==id).ToList());
		//	return PartialView("_ExtraServicesDetailsPartialView", viewModelItems.Where(x => x.OrderId == id).ToList());

		//}

		//AccomodationDetails (Dapper)
		
		
		
		public ActionResult ExtraServicesDetailsPartialView(int orderId)
		{
			ExtraServicesDetailsDapperRepository repo= new ExtraServicesDetailsDapperRepository();
			List<ExtraServicesDetailsVM> extraServicesDetails = repo.GetExtraServicesDetails(orderId);

            return PartialView("_ExtraServicesDetailsPartialView", extraServicesDetails);
        } 









        public ActionResult AccomodationDetailsPartialView(int orderId)
			{
				AccomodationDetailsDapperRepository repo = new AccomodationDetailsDapperRepository();
				List<AccomodationDetailsVM> accomodationDetails = repo.GetAccomodationDetails(orderId);

				return PartialView("_AccomodationDetailsPartialView", accomodationDetails);
			}

	}
}