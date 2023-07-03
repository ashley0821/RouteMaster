using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.ViewModels
{
	public class ExtraServiceEditVM
	{
        public int Id { get; set; }

        [Required]
		public string Name { get; set; }

		public int AttractionId { get; set; }

		public int Price { get; set; }

		[Required]
		public string Description { get; set; }

		public bool Status { get; set; }
	}
}