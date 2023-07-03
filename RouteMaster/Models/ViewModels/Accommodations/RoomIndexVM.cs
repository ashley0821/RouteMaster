using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.ViewModels.Accommodations
{
	public class RoomIndexVM
	{
		public int Id { get; set; }

		public int AccommodationId { get; set; }

		public string Name { get; set; }

		public int Quantity { get; set; }

	}
}