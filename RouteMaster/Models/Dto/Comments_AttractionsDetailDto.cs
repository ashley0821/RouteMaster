using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace RouteMaster.Models.Dto
{
	public class Comments_AttractionsDetailDto
	{
		public int Id { get; set; }

		public string MemberAccount { get; set; }

		public string AttractioName { get; set; }

		public string Content { get; set; }
		public int Score { get; set; }
		public IEnumerable<string> Images { get; set; }
	}
}