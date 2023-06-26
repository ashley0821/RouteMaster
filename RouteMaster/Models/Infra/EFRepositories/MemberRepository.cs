using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Infra.EFRepositories
{
	public class MemberRepository : IMemberRepository
	{
		private AppDbContext _db;

		public MemberRepository()
		{
			_db = new AppDbContext();
		}
		public IEnumerable<MemberIndexDto> Seacrh()
		{
			var db = new AppDbContext();
			return db.Members
				.AsNoTracking()
				.Select(p => new MemberIndexDto
				{
					Id = p.Id,
					FirstName = p.FirstName,
					LastName = p.LastName,
					Account = p.Account,
					Email = p.Email,
					CellPhoneNumber = p.CellPhoneNumber,
					Address = p.Address,
					Gender = p.Gender,
					Birthday = p.Birthday,
					CreateDate = p.CreateDate,
					IsConfirmed = p.IsConfirmed,
					IsSuspended = p.IsSuspended,

				});
		}
	}
}