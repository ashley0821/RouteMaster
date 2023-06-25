using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace RouteMaster.Models.ViewModels
{
	public class MemberCreateVM
	{
		
		public int Id { get; set; }

		[Display(Name = "名")]
		[Required]
		[StringLength(50)]
		public string FirstName { get; set; }

		[Display(Name = "姓")]
		[Required]
		[StringLength(50)]
		public string LastName { get; set; }

		[Display(Name = "帳號")]
		[Required]
		[StringLength(30)]
		public string Account { get; set; }

		//[Required]
		//[StringLength(255)]
		//public string EncryptedPassword { get; set; }

		[Display(Name = "電子信箱")]
		[Required]
		[StringLength(255)]
		public string Email { get; set; }

		[Display(Name = "手機號碼")]
		[Required]
		[StringLength(10)]
		public string CellPhoneNumber { get; set; }

		[Display(Name = "聯絡地址")]
		[StringLength(255)]
		public string Address { get; set; }

		[Display(Name = "性別")]
		public bool Gender { get; set; }

		[Display(Name = "生日")]
		public DateTime Birthday { get; set; }

		public DateTime CreateDate { get; set; }

		//[Required]
		//public string Image { get; set; }

		public bool IsConfirmed { get; set; }

		//public string ConfirmCode { get; set; }

		public bool? IsSuspended { get; set; }
	}
}