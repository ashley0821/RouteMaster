using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.ViewModels
{
	public class OrderEditVM
	{
		public int Id { get; set; }

		public int MemberId { get; set; }
		public string MemberName { get; set; }
		public string MemberEmail { get; set; }
        public int TravelPlanId { get; set; }

        public int PaymentMethodId { get; set; }
		public string PaymentMethodName { get; set; }

		public int PaymentStatus { get; set; }
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

		public DateTime? CreateDate { get; set; }

		public int Total { get; set; }
	}
}