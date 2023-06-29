using RouteMaster.Models.EFModels;
using RouteMaster.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RouteMaster.Controllers
{
    public class ExtraServicesDetailsController : Controller
    {
        // GET: ExtraServicesDetails
        private readonly AppDbContext db=new AppDbContext();
        public ActionResult ExtraServicesDetailsPartialView()
        {
          var ExtraServicesDetails = db.ExtraServicesDetails;

            var viewModelItems = ExtraServicesDetails
                                .ToList()
                                .Select(dto => new ExtraServicesDetailsVM
                                {
                                    Id= dto.Id,
                                    OrderId= dto.OrderId,
                                    ExtraServiceId= dto.ExtraServiceId,
                                    ExtraServiceName= dto.ExtraServiceName,
                                    Price= dto.Price,
                                    Quantity= dto.Quantity,
                                });
            return PartialView(viewModelItems);


		}
    }
}