using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Dto
{
	public class ExtraServiceCreateDto
	{
		public string Name { get; set; }

		public int AttractionId { get; set; }

		public int Price { get; set; }

		public string Description { get; set; }

		public bool Status { get; set; }
	}
}