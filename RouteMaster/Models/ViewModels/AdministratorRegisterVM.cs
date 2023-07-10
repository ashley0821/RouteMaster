using RouteMaster.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.ViewModels
{
	public class AdministratorRegisterVM
	{
		public int Id { get; set; }

		public int PermissionId { get; set; }


		[Display(Name = "名")]
		[Required]
		[StringLength(50)]
		public string FirstName { get; set; }


		[Display(Name = "姓")]
		[Required]
		[StringLength(50)]
		public string LastName { get; set; }


		[Display(Name = "密碼")]
		[Required]
		[StringLength(20)]
		[DataType(DataType.Password)]
		public string Password { get; set; }


		[Display(Name = "密碼確認")]
		[Compare("Password", ErrorMessage = "必需與您輸入的'密碼'相同")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }


		[Display(Name = "電子信箱")]
		[Required]
		[StringLength(255)]
		[EmailAddress(ErrorMessage = "Invalid Email Address")]
		public string Email { get; set; }


		[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
		[Display(Name = "註冊日期")]
		public DateTime? CreateDate { get; set; }

		public virtual Permission Permission { get; set; }

	}
}