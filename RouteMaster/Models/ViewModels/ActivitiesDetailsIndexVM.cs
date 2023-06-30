using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace RouteMaster.Models.ViewModels
{
	public class ActivitiesDetailsIndexVM
	{
		public int Id { get; set; }

		[Display(Name = "訂單編號")]
		public int OrderId { get; set; }
		[Display(Name = "活動編號")]
		public int ActivityId { get; set; }

		[Display(Name = "活動名稱")]
		[Required]
		[StringLength(100)]

		public string ActivityName { get; set; }
		[Display(Name = "開始時間")]
		public DateTime StartTime { get; set; }
		[Display(Name = "結束時間")]
		public DateTime EndTime { get; set; }
		[Display(Name = "價格")]
		public int Price { get; set; }
		[Display(Name = "數量")]
		public int Quantity { get; set; }
	}
}