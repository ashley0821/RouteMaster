using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.ViewModels
{
	public class AttractionIndexVM
	{
		public int Id { get; set; }

		public string Category { get; set; }

		public string Region { get; set; }

		public string Town { get; set; }

		public string Name { get; set; }

		public string Image { get; set; }

		public string DescriptionText { get; set; }

		public string AverageScoreText { get; set; }

		public string AverageStayHoursText { get; set; }

		public string AveragePriceText { get; set; }
	}
}