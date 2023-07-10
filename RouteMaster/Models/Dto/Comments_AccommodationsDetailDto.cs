using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Dto
{
	public class Comments_AccommodationsDetailDto
	{
        public int Id { get; set; }

        public string MemberAccount { get; set; }   

        public string AccomodationName  { get; set; }

        public double Score { get; set; }

        public string Title { get; set; }

		public string Pros { get; set; }

		public string Cons { get; set; }

        public IEnumerable<string> Images { get; set; } 
    }
}