using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace RouteMaster.Models.ViewModels
{
	public class PartnerRegisterVM
	{

		public int Id { get; set; }

		[Display(Name = "密碼")]
		[Required]
		[StringLength(20)]
		[DataType(DataType.Password)]
		//public string EncryptedPassword { get; set; }
		public string Password { get; set; }

		[Display(Name = "確認密碼")]
		[Required]
		[DataType(DataType.Password)]
		[StringLength(20)]
		[Compare("Password", ErrorMessage = "必需與 '密碼' 欄位值相同")]
		public string ConfirmPassword { get; set; }

		public string EncryptedPassword { get; set; } //雜湊後的密碼

		[Required]
		[StringLength(256)]
		[EmailAddress(ErrorMessage = "Email 格式有誤")]
		public string Email { get; set; }

		[Required]
		[StringLength(50)]
		[Display(Name = "姓")]
		public string FirstName { get; set; }

		[Required]
		[StringLength(50)]
		[Display(Name = "名")]
		public string LastName { get; set; }
		

		[Display(Name = "手機")]
		[StringLength(10)]
		public string Mobile { get; set; }
		public bool IsConfirmed { get; set; }
		public string ConfirmCode { get; set; }
	}
}