using RouteMaster.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Dto
{
	public class AccommodationIndexDto
	{
		public int Id { get; set; }
		public int PartnerId { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }
		public string AccommodationImage { get; set; }
	}
}