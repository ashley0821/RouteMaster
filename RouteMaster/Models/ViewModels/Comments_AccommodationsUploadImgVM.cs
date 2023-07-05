﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace RouteMaster.Models.ViewModels
{
	public class Comments_AccommodationsUploadImgVM
	{
		public int ImgId { get; set; }

		public int CommentId { get; set; }

		[Display(Name = "上傳圖片")]
		public string Image { get; set; }
	}
}