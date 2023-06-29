using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
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

		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Order order = db.Orders.Find(id);
			if (order == null)
			{
				return HttpNotFound();
			}
			return View(order);
		}


		//ActivitiesDetails
		public ActionResult IndexDapper()
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
			return PartialView("_IndexDapper", viewModelItems);
		}

		public ActionResult ExtraServicesDetailsPartialView()
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
			return PartialView(viewModelItems);


		}


	}
}