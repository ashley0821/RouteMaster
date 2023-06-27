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

		private IEnumerable<OrderIndexVM> GetOrders()
		{
			IOrderRepository repo = new OrderEFRepository();
			OrderService service = new OrderService(repo);

			return service.Search()
				   .ToList()
				   .Select(o => o.ToIndexVM());
		}

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
	}
}