using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Infra.EFRepositories
{
	public class AdministratorEFRepository : IAdministratorRepository
	{
		private AppDbContext db;

		public AdministratorEFRepository()
		{
			db = new AppDbContext();
		}

		public bool ExistEmail (string email)
		{
			return db.Administrators.Any(m => m.Email == email);
		}

		public void Register(AdministratorRegisterDto dto)
		{
			Administrator administrator = new Administrator
			{
				Permission= dto.Permission,
				FirstName= dto.FirstName,
				LastName= dto.LastName,
				EncryptedPassword= dto.EncryptedPassword,
				Email = dto.Email,
				CreateDate= DateTime.Now,
				ConfirmCode= dto.ConfirmCode,
				IsSuspended= false, //預設為註冊時都未停權
			};

			db.Administrators.Add(administrator);
			db.SaveChanges();
		}
	}
}