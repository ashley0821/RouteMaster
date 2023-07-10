using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Infra.Criterias
{
	public class OrderCriteria
	{

		public string MemberName { get; set; }
		public int? PaymentStatus { get; set; }
		public DateTime? CreateStartDate { get; set; }
		public DateTime? CreateEndDate { get; set; }
    }
}