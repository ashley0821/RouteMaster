using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace RouteMaster.Models.Dto
{
	public class Comments_AccommodationsCreateDto
	{
		public string MemberAccount { get; set; }

		public int AccomodationId { get; set; }

		public string Title { get; set; }

		public string Pros { get; set; }

		public string Cons { get; set; }

		public float Score { get; set; }

	}
}