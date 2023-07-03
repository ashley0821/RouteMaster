using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace RouteMaster.Models.ViewModels
{
	public class MemberEditVM
	{

		[Display(Name = "名")]
		[Required]
		[StringLength(50)]
		public string FirstName { get; set; }

		[Display(Name = "姓")]
		[Required]
		[StringLength(50)]
		public string LastName { get; set; }

		//[Required]
		//[StringLength(255)]
		//public string EncryptedPassword { get; set; }

		[Display(Name = "信箱")]
		[Required]
		[StringLength(255)]
		public string Email { get; set; }

		[Display(Name = "手機號碼")]
		[Required]
		[StringLength(10)]
		public string CellPhoneNumber { get; set; }

		[Display(Name = "地址")]
		[StringLength(255)]
		public string Address { get; set; }

		[Display(Name = "性別")]
		public bool Gender { get; set; }

		[Display(Name = "生日")]
		public DateTime Birthday { get; set; }


	}
}