using RouteMaster.Models.Dto;
using RouteMaster.Models.Infra.EFRepositories;
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
	public class AttractionsController : Controller
	{
		// GET: Attractions
		public ActionResult Index()
		{
			IEnumerable<AttractionIndexVM> products = GetProducts();

			return View(products);
		}

		private IEnumerable<AttractionIndexVM> GetProducts()
		{
			IAttractionRepository repo = new AttractionEFRepository();
			AttractionService service = new AttractionService(repo);

			return service.Search()
				.Select(dto => new AttractionIndexVM
				{
					Id = dto.Id,
					Category = dto.Category,
					Region = dto.Region,
					Town = dto.Town,
					Name = dto.Name,
					DescriptionText = dto.DescriptionText,
					AverageScoreText = dto.AverageScoreText,
					AveragePriceText = dto.AveragePriceText,
					AverageStayHoursText = dto.AverageStayHoursText,
				});
		}
	}
}