using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.ViewModels
{
	public class MemberIndexVM
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
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime Birthday { get; set; }

		[Display(Name = "創建日期")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime CreateDate { get; set; }

		//[Required]
		//public string Image { get; set; }

		[Display(Name = "已驗證")]
		public bool IsConfirmed { get; set; }

		//public string ConfirmCode { get; set; }

		[Display(Name = "已被停帳")]
		public bool? IsSuspended { get; set; }

     
    }
}