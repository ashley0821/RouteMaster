using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Dto
{
	public class Comments_AttractionsIndexDto
	{
		public int Id { get; set; }

		public string Account { get; set; }

		public string Name { get; set; }

		public int Score { get; set; }

		public int? StayHours { get; set; }

		public int? Price { get; set; }

		public DateTime CreateDate { get; set; }
	}
}