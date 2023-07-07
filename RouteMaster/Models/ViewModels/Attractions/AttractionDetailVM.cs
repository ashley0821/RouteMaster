using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.ViewModels
{
	public class AttractionDetailVM
	{
		public int Id { get; set; }

		public string Category { get; set; }

		public string Region { get; set; }

		public string Town { get; set; }

		public string Name { get; set; }

		public string Address { get; set; }

		public string Tag { get; set; }

		public double? PositionX { get; set; }

		public double? PositionY { get; set; }

		public string Description { get; set; }

		public string Website { get; set; }

		public List<string> Images { get; set; }

		public string AverageScoreText { get; set; }

		public string AverageStayHoursText { get; set; }

		public string AveragePriceText { get; set; }

		
	}
}