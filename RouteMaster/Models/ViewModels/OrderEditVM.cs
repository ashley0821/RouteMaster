using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace RouteMaster.Models.ViewModels
{
	public class OrderEditVM
	{
		[Display(Name = "訂單編號")]
		public int Id { get; set; }

		[Display(Name = "訂購人編號")]
		public int MemberId { get; set; }

		[Display(Name = "訂購人編號")]
		public string MemberName { get; set; }

		[Display(Name = "電子信箱")]
		public string MemberEmail { get; set; }

		[Display(Name = "旅行計畫編號")]
		public int TravelPlanId { get; set; }

		[Display(Name = "付款方式代號")]
		public int PaymentMethodId { get; set; }

		[Display(Name = "付款方式")]
		public string PaymentMethodName { get; set; }

		[Display(Name = "付款狀態代號")]
		public int PaymentStatus { get; set; }

		[Display(Name = "付款狀態")]
		public string PaymentStatusText
		{
			get
			{
				switch (PaymentStatus)
				{
					case 1:
						return "已付款";
					case 2:
						return "未付款";
					case 3:
						return "已取消";
					default:
						return "未知狀態";
				}
			}
		}
		[Display(Name = "訂單成立日期")]
		public DateTime? CreateDate { get; set; }

		[Display(Name = "總金額")]
		public int Total { get; set; }
	}
}