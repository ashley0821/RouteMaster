using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace RouteMaster.Models.ViewModels
{
	public class ActivityIndexVM
	{
		public int Id { get; set; }

		//public int ActivityCategoryId { get; set; }  

		[Display(Name = "活動分類")]
		public string ActivityCategoryName { get; set; }

		//public int AttractionId { get; set; }

		[Display(Name = "舉辦景點")]
		public string AttractionName { get; set; }

		[Display(Name = "活動名稱")]
		public string Name { get; set; }

		[Display(Name = "所在縣市")]
		//public int RegionId { get; set; }
		public string RegionName { get; set; }


		[Display(Name = "價格")]
		[DisplayFormat(DataFormatString = "{0:#,#}")]
		public int Price { get; set; }


		[Display(Name = "開始時間")]
		public DateTime StartTime { get; set; }

		[Display(Name = "結束時間")]
		public DateTime EndTime { get; set; }

		[Display(Name = "活動內容介紹")]
		public string Description { get; set; }
		public string DescriptionText
		{
			get
			{
				return this.Description.Length > 30
					? this.Description.Substring(0, 30) + "...."
					: this.Description;
			}
		}




		[Display(Name = "上架狀態")]
		public bool Status { get; set; }
		public string StatusText
		{
			get
			{
				return Status == true ? "上架中" : "已下架";
			}
		}

	}
}
