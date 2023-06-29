using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.ViewModels
{
	public class MemberRegisterVM
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

		//[Display(Name = "雜湊後密碼")]
		//[Required]
		//[StringLength(255)]
		//public string EncryptedPassword { get; set; }

		[Display(Name = "電子信箱")]
		[Required]
		[StringLength(255)]
		public string Email { get; set; }

		[Display(Name = "電話號碼")]
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

		//[Display(Name = "註冊日期")]
		//public DateTime CreateDate { get; set; }

		[Display(Name = "大頭貼")]
		[Required]
		public string Image { get; set; }

	}
}