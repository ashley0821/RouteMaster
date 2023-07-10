using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Dto
{
	public class AttractionIndexDto
	{
		public int Id { get; set; }

		public string Category { get; set; }

		public string Region { get; set; }

		public string Town { get; set; }

		public string Name { get; set; }

		public string Image { get; set; }

		public string Description { get; set; }

		

		public string DescriptionText
		{
			get
			{
				return Description.Length>10? Description.Substring(0,10)+"...":Description;
			}
		}

		public double? AverageScore { get; set; }

		public string AverageScoreText
		{
			get
			{
				if (AverageScore == null) { return "尚未有評分"; }
				else { return Math.Round(AverageScore.Value, 1).ToString("0.0"); }
			}
		}

		public double? AverageStayHours { get; set; }

		public string AverageStayHoursText
		{
			get
			{
				if (AverageStayHours == null) { return "尚未有資料"; }
				else { return Math.Round(AverageStayHours.Value, 1).ToString("0.0"); }
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