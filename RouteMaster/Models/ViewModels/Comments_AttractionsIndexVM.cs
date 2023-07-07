using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace RouteMaster.Models.ViewModels
{
	public class Comments_AttractionsIndexVM
	{
		public int Id { get; set; }

		[Display(Name = "帳號名稱")]
		public string Account { get; set; }

		[Display(Name = "景點名稱")]
		public string Name { get; set; }

		[Display(Name = "評分")]
		public int Score { get; set; }

		[Display(Name = "停留時間")]
		public int? StayHours { get; set; }

		[Display(Name = "花費")]
		public int? Price { get; set; }

		[Display(Name = "評論建立時間")]
		[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}")]
		public DateTime CreateDate { get; set; }
	}
}