using RouteMaster.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Dto.Accommodation.Room
{
	public class RoomEditDto
	{
		public int Id { get; set; }

		public int AccommodationId { get; set; }

		
		public string Type { get; set; }

		public string Name { get; set; }

		public int Quantity { get; set; }

		public int Price { get; set; }

		public virtual ICollection<RoomImage> RoomImages { get; set; }
	}
}