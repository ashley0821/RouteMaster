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


		//ActivitiesDetails (EF)
	
		public ActionResult IndexDapper(int id)
		{

			var viewModelItems = db.ActivitiesDetails
				.ToList()
				.Select(dto => new ActivitiesDetailsIndexVM
				{
					Id = dto.Id,
					OrderId = dto.OrderId,
					ActivityId = dto.ActivityId,
					ActivityName = dto.ActivityName,
					StartTime = dto.StartTime,
					EndTime = dto.EndTime,
					Price = dto.Price,
					Quantity = dto.Quantity,

				});

			//viewModelItems.Select(x => x.OrderId = id).ToList();
			return PartialView("_IndexDapper", viewModelItems.Where(x => x.OrderId == id).ToList());
		}

		//ExtraServiceDetails (EF)
		public ActionResult ExtraServicesDetailsPartialView(int id)
		{
			

			var viewModelItems = db.ExtraServicesDetails
								.ToList()
								.Select(dto => new ExtraServicesDetailsVM
								{
									Id = dto.Id,
									OrderId = dto.OrderId,
									ExtraServiceId = dto.ExtraServiceId,
									ExtraServiceName = dto.ExtraServiceName,
									Price = dto.Price,
									Quantity = dto.Quantity,
								});

			//viewModelItems.Select(x=>x.OrderId = id).ToList();
			//return PartialView("ExtraServicesDetailsPartialView",viewModelItems.Where(x=>x.OrderId==id).ToList());
			return PartialView("_ExtraServicesDetailsPartialView", viewModelItems.Where(x => x.OrderId == id).ToList());

			


		}

		//AccomodationDetails (Dapper)
		public ActionResult AccomodationDetailsPartialView()
		{
			IEnumerable<AccomodationDetailsVM> accomodationDetails = (IEnumerable<AccomodationDetailsVM>)GetAccomodationdetails();
			return PartialView("_AccomodationDetailsPartialView", accomodationDetails);
		}

		private IEnumerable<AccomodationDetailsVM> GetAccomodationdetails()
		{
			IAccomodationDetailsRepository repo = new AccomodationDetailsDapperRepository();
			AccomodationDetailsService service = new AccomodationDetailsService(repo);

			return service.Search()
				.Select(dto => dto.ToIndexVM());

		}

	}
}