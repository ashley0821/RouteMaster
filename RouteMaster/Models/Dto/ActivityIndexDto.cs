using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Dto
{
	public class ActivityIndexDto
	{
		public int Id { get; set; }
        //public int ActivityCategoryId { get; set; }
        public string ActivityCategoryName { get; set; }
        //public int AttractionId { get; set; }
        public string AttractionName { get; set; }
        public string Name { get; set; }
		//public int RegionId { get; set; }
        public string RegionName { get; set; }
        public int Price { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public string Description { get; set; }
		public bool Status { get; set; }
	}
}