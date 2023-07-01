using RouteMaster.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Dto
{
	public class MemberRegisterDto
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Account { get; set; }
		public string Password { get; set; }
		public string EncryptedPassword { get; set; }
		
		public string Email { get; set; }
		public string CellPhoneNumber { get; set; }
		public string Address { get; set; }
		public bool Gender { get; set; }
		public DateTime Birthday { get; set; }

		//public DateTime CreateDate { get; set; } //於repository做.datetime.now
		public string Image { get; set; }
		public bool IsConfirmed { get; set; }

		public string ConfirmCode { get; set; } //不用傳到dto了，屬於前端驗證功能

		//11個屬性
	}

	public static class MemberRegisterExts
	{
		public static MemberRegisterDto ToDto(this MemberRegisterVM vm)
		{
			return new MemberRegisterDto()
			{
				Id = vm.Id,
				FirstName = vm.FirstName,
				LastName = vm.LastName,
				Account = vm.Account,
				Password = vm.Password,
				Email = vm.Email,
				CellPhoneNumber = vm.CellPhoneNumber,
				Address = vm.Address,
				Gender = vm.Gender,
				Birthday = vm.Birthday,
				Image = vm.Image,
			};

		}
	}
}