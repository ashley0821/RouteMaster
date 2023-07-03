using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace RouteMaster.Models.Dto
{
	public class RoomCreateDto
	{
		public int Id { get; set; }

		public int AccommodationId { get; set; }

		public string Type { get; set; }

		public string Name { get; set; }

		public int Quantity { get; set; }

		public int Price { get; set; }
	}
}