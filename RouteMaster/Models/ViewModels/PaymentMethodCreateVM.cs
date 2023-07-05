using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.ViewModels
{
	public class PaymentMethodCreateVM
	{
		public int id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
	}
}