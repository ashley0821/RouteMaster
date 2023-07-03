using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace RouteMaster.Models.Dto
{
	public class FAQCreateDto
	{
		[Display(Name = "問題類別")]
		[Required]
		public int CategoryId { get; set; }

		[Display(Name = "問題")]
		[Required]
		public string Question { get; set; }

		[Display(Name = "解決方法")]
		[Required]
		public string Answer { get; set; }


		[Display(Name = "有效幫助分數")]
		public int? Helpful { get; set; }
	}
}