using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Services.Protocols;

namespace RouteMaster.Models.Dto
{
	public class ExtraServiceIndexDto
	{
        public int Id { get; set; }	
        public string Name { get; set; }
     
        public string AttractionName { get; set; }	

        public int Price { get; set; }

		public string Description { get; set; }

		public bool Status { get; set; }
	}
}