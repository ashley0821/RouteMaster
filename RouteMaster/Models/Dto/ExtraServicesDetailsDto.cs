using RouteMaster.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Dto
{
	public class ExtraServicesDetailsDto
	{
		public int Id { get; set; }
		[Display(Name = "訂單編號")]
		public int OrderId { get; set; }
		[Display(Name = "額外服務編號")]
		public int ExtraServiceId { get; set; }
		[Display(Name = "額外服務項目")]
		[Required]
		public string ExtraServiceName { get; set; }
		[Display(Name = "金額")]
		public int Price { get; set; }
		[Display(Name = "數量")]
		public int Quantity { get; set; }

	
	}
}