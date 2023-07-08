using RouteMaster.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.ViewModels
{
	public class AttractionCreateVM
	{
		public int Id { get; set; }

		public int AttractionCategoryId { get; set; }

		public int RegionId { get; set; }

		public int TownId { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; }

		public int TagId { get; set; }

		[Required]
		[StringLength(255)]
		public string Address { get; set; }

		public double? PositionX { get; set; }

		public double? PositionY { get; set; }

		[Required]
		public string Description { get; set; }

		public string Website { get; set; }
		
	}
}