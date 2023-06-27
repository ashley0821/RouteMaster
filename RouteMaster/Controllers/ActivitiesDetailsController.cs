using RouteMaster.Models.EFModels;
using RouteMaster.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RouteMaster.Controllers
{
	public class ActivitiesDetailsController : Controller
	{
		// GET: ActivitiesDetails

		private readonly AppDbContext db = new AppDbContext();
		public ActionResult IndexDapper()
		{


			//var ActivitiesDetailsItems = new ActivitiesDetailsDapperRepository().GetActivitiesDetails();
			var test = db.ActivitiesDetails;

			var viewModelItems = test
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
			return PartialView(viewModelItems);
		}
	}
}