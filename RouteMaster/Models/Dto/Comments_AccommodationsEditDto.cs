using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace RouteMaster.Models.Dto
{
	public class Comments_AccommodationsEditDto
	{
		public int Id { get; set; }

		[Required]
		public string Title { get; set; }

		public string Pros { get; set; }

		public string Cons { get; set; }

		[Required]
		public float Score { get; set; }
	}
}