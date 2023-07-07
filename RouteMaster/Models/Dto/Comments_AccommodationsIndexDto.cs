using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace RouteMaster.Models.Dto
{
	public class Comments_AccommodationsIndexDto
	{
		public int Id { get; set; }

		public string Account { get; set; }

		public string Name { get; set; }

		public double Score { get; set; }

		public string Title { get; set; }

		public DateTime? CreateDate { get; set; }
	}
}