using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.ViewModels
{
	public class FAQDetailVM
	{
		public int Id { get; set; }

		[Display(Name = "問題類別")]
		public string CategoryName { get; set; }

		[Display(Name = "問題")]
		public string Question { get; set; }

		[Display(Name = "解決方法")]
		public string Answer { get; set; }

		[Display(Name = "有效幫助分數")]
		public int Helpful { get; set; }

		[Display(Name = "相簿")]
		public IEnumerable<string> Images { get; set; }	
    }
}