using RouteMaster.Models.EFModels;
using RouteMaster.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RouteMaster.Models
{
	public class AdministratorRegisterDto
	{
		public int Id { get; set; }

		public int? PermissionId { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Password { get; set; }

		public string EncryptedPassword { get; set; }

		public string Email { get; set; }

		public DateTime CreateDate { get; set; }

		public string ConfirmCode { get; set; }

		public bool? IsSuspended { get; set; }

		public virtual Permission Permission { get; set; }
	}

	public static class AdministratorExts
	{
		public static AdministratorRegisterDto ToDto(this AdministratorRegisterVM vm)
		{
			return new AdministratorRegisterDto()
			{
				Id = vm.Id,
				PermissionId = vm.PermissionId,
				FirstName = vm.FirstName,
				LastName = vm.LastName,
				Password = vm.Password,
				Email = vm.Email,
			};
		}
	}


}