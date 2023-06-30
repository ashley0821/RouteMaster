using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace RouteMaster.Models.ViewModels
{
	public class FAQIndexVM
	{
		public int Id { get; set; }

		[Display(Name = "分類名稱")]
		public string CategoryName { get; set; }

		[Display(Name = "問題")]
		public string Question { get; set; }

		public string Answer { get; set; }

		[Display(Name = "解決方法")]
		public string AnswerText
		{
			get
			{
				return Answer.Length > 15 ? Answer.Substring(0, 15) + "..." : Answer;
			}
		}

		[Display(Name = "有效幫助分數")]
		public int? Helpful { get; set; }

		[Display(Name = "建立時間")]
		[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}")]
		public DateTime? CreateDate { get; set; }

		[Display(Name = "修改時間")]
		[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}")]
		public DateTime? ModifiedDate { get; set; }
	}
}