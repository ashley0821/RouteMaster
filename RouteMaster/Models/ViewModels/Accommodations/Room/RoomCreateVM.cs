using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.ViewModels.Accommodations.Room
{
	public class RoomCreateVM
	{
		public int Id { get; set; }

		public int AccommodationId { get; set; }

		[Required]
		[StringLength(100)]
		[Display(Name ="客房類型")]
		public string Type { get; set; }

		[Required]
		[Display(Name ="客房名稱")]
		public string Name { get; set; }

		[Required]
		[Display(Name = "客房數量(此類型)")]
		public int Quantity { get; set; }

		[Required]
		[Display(Name = "每晚最低房價")]
		public int Price { get; set; }

		//[Display(Name = "房間照片")]
		//public HttpPostedFileBase[] Files { get; set; }
	}
}