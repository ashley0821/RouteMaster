using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.ViewModels
{
	public class AccommodationEditVM
	{
		public int Id { get; set; }

		public int PartnerId { get; set; }

		[Required]
		[StringLength(850)]
        [Display(Name = "貴住宿的名稱")]
		public string Name { get; set; }

        [Display(Name = "住宿描述")]
		public string Description { get; set; }

		[Required]
        [Display(Name = "縣市")]
		public int RegionId { get; set; }

		[Required]
        [Display(Name = "鄉鎮市區")]
		public int TownId { get; set; }

		[Required]
        [Display(Name = "地址")]
		public string Address { get; set; }

		[StringLength(20)]
        [Display(Name = "聯絡手機或市話")]
		public string PhoneNumber { get; set; }

		public string Website { get; set; }

		[StringLength(255)]
        [Display(Name = "聯絡Email")]
		public string IndustryEmail { get; set; }

		public int? ParkingSpace { get; set; }

	}
}