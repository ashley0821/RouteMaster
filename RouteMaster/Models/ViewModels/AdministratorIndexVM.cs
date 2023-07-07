using RouteMaster.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace RouteMaster.Models.ViewModels
{
	public class AdministratorIndexVM
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


		[Display(Name = "電子信箱")]
		[Required]
		[StringLength(255)]
		public string Email { get; set; }


		[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
		[Display(Name = "註冊日期")]
		public DateTime? CreateDate { get; set; }

		public string ConfirmCode { get; set; }

		public bool IsSuspended { get; set; }

		public virtual Permission Permission { get; set; }
	}
}