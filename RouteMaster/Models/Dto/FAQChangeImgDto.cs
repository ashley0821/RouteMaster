using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace RouteMaster.Models.Dto
{
	public class FAQChangeImgDto
	{
		public int ImgId { get; set; }

		public int FAQId { get; set; }

		public string Image { get; set; }
	}
}