using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Dto
{
	public class ActivityCreateDto
	{	
		public int ActivityCategoryId { get; set; }

		public int AttractionId { get; set; }

		public string Name { get; set; }

		public int RegionId { get; set; }

		public int Price { get; set; }

		public DateTime StartTime { get; set; }

		public DateTime EndTime { get; set; }

		public string Description { get; set; }

		public bool Status { get; set; }
	}
}