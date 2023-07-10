using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace RouteMaster.Models.ViewModels
{
	public class PaymentMethodCreateVM
	{
		[Display(Name = "訂單編號")]
		public int id { get; set; }
		[Display(Name = "付款方式")]
		public string Name { get; set; }
		[Display(Name = "描述")]
		public string Description { get; set; }
	}
}