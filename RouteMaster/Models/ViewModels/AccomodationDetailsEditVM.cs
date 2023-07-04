using RouteMaster.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace RouteMaster.Models.ViewModels
{
	public class AccomodationDetailsEditVM
	{
		public int Id { get; set; }

		[Display(Name = "訂單編號")]
		public int OrderId { get; set; }

		[Display(Name = "住宿編號")]
		public int AccommodationId { get; set; }
		[Display(Name = "住宿名稱")]

		public string AccommodationName { get; set; }

		[Display(Name = "房型")]

		public string RoomType { get; set; }

		[Display(Name = "房間名稱")]
		public string RoomName { get; set; }
		[Display(Name = "入住時間")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
		public DateTime CheckIn { get; set; }
		[Display(Name = "退房時間")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
		public DateTime CheckOut { get; set; }
		[Display(Name = "價格")]
		[DisplayFormat(DataFormatString = "${0:#,#}")]
		public int RoomPrice { get; set; }

		
	}
}