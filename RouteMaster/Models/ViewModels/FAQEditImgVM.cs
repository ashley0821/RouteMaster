using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace RouteMaster.Models.ViewModels
{
	public class FAQEditImgVM
	{
		public int Id { get; set; }

		[Display(Name = "圖檔上傳")]
		//[Required]
		public string Image { get; set; }
	}
}