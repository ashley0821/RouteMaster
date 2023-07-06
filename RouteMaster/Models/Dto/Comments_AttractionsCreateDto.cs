using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Dto
{
	public class Comments_AttractionsCreateDto
	{
        public int MemberId { get; set; }	

        public int AttractionId	 { get; set; }

		public int Score { get; set; }

		public string Content { get; set; }

		public int? StayHours { get; set; }

		public int? Price { get; set; }

		public string Image { get; set; }	
    }
}