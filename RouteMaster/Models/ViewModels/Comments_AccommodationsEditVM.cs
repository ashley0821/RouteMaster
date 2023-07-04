using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace RouteMaster.Models.ViewModels
{
	public class Comments_AccommodationsEditVM
	{
        public int Id { get; set; }	
       
		[Display(Name = "評論標題")]
		public string Title { get; set; }

		[Display(Name = "優點")]
		public string Pros { get; set; }

		[Display(Name = "缺點")]
		public string Cons { get; set; }

		[Display(Name = "評分")]
		public float Score { get; set; }
	}
}