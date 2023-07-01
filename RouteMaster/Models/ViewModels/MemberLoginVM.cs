using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.ViewModels
{
	public class MemberLoginVM
	{
		[Display(Name="帳號")]
		[Required]
		public string Account { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "密碼")]
		public string Password { get; set; }
	}
}