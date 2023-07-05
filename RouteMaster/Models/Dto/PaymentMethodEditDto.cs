using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Dto
{
	public class PaymentMethodEditDto
	{
		public int id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
	}
}