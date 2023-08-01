using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.ViewModels
{
	public class AttractionCriteria
	{
        public string Category { get; set; }

		public string Region { get; set; }

		public string Town { get; set; }

		public List<string> Tag { get; set; }

		public string Name { get; set; }

		public int? MinScore { get; set; }

		public int? MaxScore { get; set; }

		public int? MinHours { get; set; }

		public int? MaxHours { get; set; }

		public int? MinPrice { get; set; }

		public int? MaxPrice { get; set; }
	}
}