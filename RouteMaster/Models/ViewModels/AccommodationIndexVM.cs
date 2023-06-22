using RouteMaster.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.ViewModels
{
	public class AccommodationIndexVM
	{
		public int PartnerId { get; set; }

		[Required]
		[StringLength(850)]
		public string Name { get; set; }
		public string Address { get; set; }
		public string AccommodationImage { get; set; }
	}
}