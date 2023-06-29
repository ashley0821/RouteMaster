using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Dto
{
	public class ActivitiesDetailsEditDto
	{

		public int OrderId { get; set; }

		public int ActivityId { get; set; }

		[Required]
		[StringLength(100)]
		public string ActivityName { get; set; }

		public DateTime StartTime { get; set; }

		public DateTime EndTime { get; set; }

		public int Price { get; set; }

		public int Quantity { get; set; }

	}
}