﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Dto
{
	public class AttractionCreateDto
	{
		public int Id { get; set; }

		public int AttractionCategoryId { get; set; }

		public int RegionId { get; set; }

		public int TownId { get; set; }

		public List<int> TagId { get; set; } = new List<int>();

		public string Name { get; set; }

		public string Address { get; set; }

		public double? PositionX { get; set; }

		public double? PositionY { get; set; }

		public string Description { get; set; }

		public string Website { get; set; }
	}
}