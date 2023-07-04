using RouteMaster.Models.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.ViewModels
{
	public class ExtraServicesDetailsEditVM
	{

		public int Id { get; set; }

		public int OrderId { get; set; }

		public int ExtraServiceId { get; set; }

		
		public string ExtraServiceName { get; set; }

		public int Price { get; set; }

		public int Quantity { get; set; }
	}

	
}