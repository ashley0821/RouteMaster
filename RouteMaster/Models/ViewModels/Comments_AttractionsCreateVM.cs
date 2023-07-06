using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace RouteMaster.Models.ViewModels
{
	public class Comments_AttractionsCreateVM
	{
		[Display(Name = "帳號名稱")]
		public int MemberId { get; set; }

		[Display(Name = "景點")]
		public int AttractionId { get; set; }

		[Display(Name = "評分")]
		public int Score { get; set; }

		[Display(Name = "評論內文")]
		public string Content { get; set; }

		[Display(Name = "停留時間")]
		public int? StayHours { get; set; }

		[Display(Name = "花費")]
		public int? Price { get; set; }

		[Display(Name = "照片")]
		public string Image { get; set; }

	}
}