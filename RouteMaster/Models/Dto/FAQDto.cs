using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Dto
{
	public class FAQDto
	{
		public int Id { get; set; }

		public int CategoryId { get; set; }

		[Required]
		public string Question { get; set; }

		[Required]
		public string Answer { get; set; }

		public int? Helpful { get; set; }

		public DateTime? CreateDate { get; set; }

		public DateTime? ModifiedDate { get; set; }
	}
}