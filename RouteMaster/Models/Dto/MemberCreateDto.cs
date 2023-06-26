using RouteMaster.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Dto
{
	public class MemberCreateDto
	{
		public int Id { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Account { get; set; }

		public string EncryptedPassword { get; set; }

		public string Email { get; set; }

		public string CellPhoneNumber { get; set; }

		public string Address { get; set; }

		public bool Gender { get; set; }

		public DateTime Birthday { get; set; }

		public DateTime CreateDate { get; set; }

		public string Image { get; set; }

		public bool IsConfirmed { get; set; }

		public string ConfirmCode { get; set; }

		public bool? IsSuspended { get; set; }
	}

	public static class MemberCreateExts
	{
		public static MemberCreateDto ToDto(this MemberCreateVM vm)
		{
			return new MemberCreateDto()
			{
				Id = vm.Id,
				FirstName = vm.FirstName,
				LastName = vm.LastName,
				Account = vm.Account,
				Email = vm.Email,
				CellPhoneNumber = vm.CellPhoneNumber,
				Address = vm.Address,
				Gender = vm.Gender,
				Birthday = vm.Birthday,

			};
		}
	}
}