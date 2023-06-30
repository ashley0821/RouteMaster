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
		public string Type { get; set; }

		[Required]
		public string Name { get; set; }

		public int Quantity { get; set; }

		public int Price { get; set; }

	}
}