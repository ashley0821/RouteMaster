using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace RouteMaster.Models.ViewModels
{
	public class FAQUploadImgVM
	{
		public int Id { get; set; }
		public int FAQId { get; set; }

		[Display(Name = "上傳新圖")]
		public string Image { get; set; }
	}
}