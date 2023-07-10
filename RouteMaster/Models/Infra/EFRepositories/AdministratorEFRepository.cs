using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Infra.Criterias;
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
				PermissionId = dto.PermissionId,
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

		public IEnumerable<AdministratorIndexDto> Search(AdministratorCriteria criteria)
		{
			var query = db.Administrators.AsEnumerable();

			if (string.IsNullOrEmpty(criteria.FirstName) == false)
			{
				query = query.Where(m => m.FirstName.Contains(criteria.FirstName));
			}
			if (string.IsNullOrEmpty(criteria.LastName) == false)
			{
				query = query.Where(m => m.LastName.Contains(criteria.LastName));
			}
			if (string.IsNullOrEmpty(criteria.Email) == false)
			{
				query = query.Where(m => m.Email.Contains(criteria.Email));
			}
            if (criteria.CreateDateBegin.HasValue)
            {
                query = query.Where(m => m.CreateDate >= criteria.CreateDateBegin.Value);
            }
            if (criteria.CreateDateEnd.HasValue)
            {
                query = query.Where(m => m.CreateDate <= criteria.CreateDateEnd.Value);
            }


            var Administrators = query.Select(a => new AdministratorIndexDto
			{
				Id = a.Id,
				PermissionId = (int)a.PermissionId,
				FirstName = a.FirstName,
				LastName = a.LastName,
				Email = a.Email,
				CreateDate= a.CreateDate,
				Permission = a.Permission
			});

			return Administrators;
		}
	}
}