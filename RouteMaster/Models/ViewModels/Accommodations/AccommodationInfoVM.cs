using RouteMaster.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace RouteMaster.Models.Infra.ViewModels.Accommodations
{
	public class AccommodationInfoVM
	{
		public int Id { get; set; }

		[Display(Name = "貴住宿的名稱")]
		public string Name { get; set; }

		[Display(Name = "住宿描述")]
		public string Description { get; set; }

		[Display(Name = "地址")]
		public string Address { get; set; }

		[Display(Name="旅宿評分")]
		public string Grade { get; set; }

		[Display(Name = "聯絡手機或市話")]
		public string PhoneNumber { get; set; }

		[Display(Name = "聯絡Email")]
		public string IndustryEmail { get; set; }

		[Display(Name = "官方網站")]
		public string Website { get; set; }

		[Display(Name = "停車位數量")]
		public int? ParkingSpace { get; set; }

		public virtual ICollection<AccommodationImage> AccommodationImages { get; set; }

	}
}