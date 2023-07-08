using RouteMaster.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.ViewModels.Accommodations.Room
{
	public class RoomEditVM
	{
		public int Id { get; set; }

		public int AccommodationId { get; set; }

		[Required]
		[StringLength(100)]
		public string Type { get; set; }

		[Required]
		public string Name { get; set; }

		[Range(1,int.MaxValue, ErrorMessage = "數量必填")]
		public int Quantity { get; set; }

		[Range(1,int.MaxValue, ErrorMessage = "價格必填")]
		public int Price { get; set; }

		public virtual ICollection<RoomImage> RoomImages { get; set; }
	}
}