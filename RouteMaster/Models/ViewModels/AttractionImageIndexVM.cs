using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.ViewModels
{
	public class AttractionImageIndexVM
	{
        public int Id { get; set; }
		public string Image { get; set; }

		public int AttractionId { get; set; }
	}
}