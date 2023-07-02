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
		public int Id { get; set; }

		public int PartnerId { get; set; }

		[Required]
		[StringLength(850)]
		public string Name { get; set; }
		[Required]
		public string Address { get; set; }
		[Required]
		public string AccommodationImage { get; set; }
	}
}