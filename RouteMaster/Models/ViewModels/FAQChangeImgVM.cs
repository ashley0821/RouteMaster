using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.ViewModels
{
	public class FAQChangeImgVM
	{
		public int FAQId { get; set; }

		public int ImgId { get; set; }

		[Display(Name = "上傳新圖")]

		public string Image { get; set; }
	}
}