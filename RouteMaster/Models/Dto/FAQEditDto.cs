using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace RouteMaster.Models.Dto
{
	public class FAQEditDto
	{
		public int Id { get; set; }

		public int CategoryId { get; set; }

		public string Question { get; set; }

		public string Answer { get; set; }

		public int? Helpful { get; set; }
	}
}