using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Dto
{
	public class FAQIndexDto
	{
		public int Id { get; set; }

		public string CategoryName { get; set; }

		public string Question { get; set; }

		public string Answer { get; set; }

		public int? Helpful { get; set; }

		public DateTime? CreateDate { get; set; }

		public DateTime? ModifiedDate { get; set; }
	}
}