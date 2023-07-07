using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Dto
{
	public class FAQDetailDto
	{
		public int Id { get; set; }

		public string CategoryName { get; set; }

		public string Question { get; set; }

		public string Answer { get; set; }

		public int Helpful { get; set; }

		public IEnumerable<string> Images { get; set; }
	}
}