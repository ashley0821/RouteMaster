using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.ViewModels
{
	public class ActivityCreateVM
	{
		[Required]
		[Display(Name = "活動分類")]
		public int ActivityCategoryId { get; set; }
		[Required]
		[Display(Name = "舉辦景點")]
		public int AttractionId { get; set; }
		[Required]
		[Display(Name = "活動名稱")]
		public string Name { get; set; }
		[Required]
		[Display(Name = "所在縣市")]
		public int RegionId { get; set; }
		[Required]
		[Display(Name = "價格")]
		[DisplayFormat(DataFormatString = "{0:#,#}")]
		public int Price { get; set; }
		[Required]
		[Display(Name = "開始時間")]
		public DateTime StartTime { get; set; }
		[Required]
		[Display(Name = "結束時間")]
		public DateTime EndTime { get; set; }

		[Required]
		[Display(Name = "活動內容介紹")]
		public string Description { get; set; }
		[Required]

		[Display(Name = "上架狀態")]
		public bool Status { get; set; }
	}
}