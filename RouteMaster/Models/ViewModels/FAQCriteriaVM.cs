using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.ViewModels
{
	public class FAQCriteriaVM
	{
		public int? CategoryId { get; set; }

		public string Answer { get; set; }

		public int? OrderId { get; set; }
	}
}