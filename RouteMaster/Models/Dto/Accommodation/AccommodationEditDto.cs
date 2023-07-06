using RouteMaster.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace RouteMaster.Models.Dto
{
	public class AccommodationEditDto
	{
		public int Id { get; set; }

		public int PartnerId { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public int RegionId { get; set; }

		public int TownId { get; set; }

		public string Address { get; set; }

		public string PhoneNumber { get; set; }

		public string Website { get; set; }

		public string IndustryEmail { get; set; }

		public int? ParkingSpace { get; set; }
		public virtual ICollection<AccommodationImage> AccommodationImages { get; set; }

	}

}