using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Dto
{
	public class AttractionDetailDto
	{
		public int Id { get; set; }

		public string Category { get; set; }

		public string Region { get; set; }

		public string Town { get; set; }

		public string Name { get; set; }

		public string Address { get; set; }

		public double? PositionX { get; set; }

		public double? PositionY { get; set; }

		public string Description { get; set; }

		public string Website { get; set; }

		

		public double? AverageScore { get; set; }

		public string AverageScoreText
		{
			get
			{
				if (AverageScore == null) { return "尚未有評分"; }
				else { return AverageScore.Value.ToString(); }
			}
		}

		public double? AverageStayHours { get; set; }

		public string AverageStayHoursText
		{
			get
			{
				if (AverageStayHours == null) { return "尚未有資料"; }
				else { return AverageStayHours.Value.ToString(); }
			}
		}

		public int? AveragePrice { get; set; }

		public string AveragePriceText
		{
			get
			{
				if (AveragePrice == null) { return "尚未有資料"; }
				else { return AveragePrice.Value.ToString(); }
			}
		}
	}
}