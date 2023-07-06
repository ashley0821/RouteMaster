using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace RouteMaster.Models.ViewModels
{
	public class Comments_AttractionsDetailVM
	{
        public int Id { get; set; }

		[Display(Name = "景點")]
		public string AttractioName { get; set; }

		[Display(Name = "評論")]
		public string Content { get; set; }

		[Display(Name = "評分")]
		public int Score { get; set; }

		[Display(Name = "相簿")]
		public IEnumerable<string> Images { get; set; }    
    }

}