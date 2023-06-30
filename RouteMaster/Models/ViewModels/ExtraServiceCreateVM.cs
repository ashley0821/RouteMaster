using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.ViewModels
{
	public class ExtraServiceCreateVM
	{	

		[Required]
        public string Name { get; set; }

        [Required]
        public int AttractionId { get; set; }

        [Required]
        public int Price { get; set; }

		[Required]
        public string Description { get; set; }

        [Required]
        public bool Status { get; set; }
	}
}