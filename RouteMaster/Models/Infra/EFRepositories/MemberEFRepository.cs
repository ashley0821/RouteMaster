﻿using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace RouteMaster.Models.Infra.EFRepositories
{
	public class MemberEFRepository : IMemberRepository
	{
		private AppDbContext _db;

		public MemberEFRepository()
		{
			_db = new AppDbContext();
		}

		public bool ExistAccount(string account)
		{
			return _db.Members.Any(m => m.Account == account);
		}

		public void Register(MemberRegisterDto dto)
		{
			//將RegisterDTO轉為member
			Member member = new Member
			{
				FirstName = dto.FirstName,
				LastName = dto.LastName,
				Account = dto.Account,
				Email = dto.Email,
				EncryptedPassword = dto.EncryptedPassword,
				CellPhoneNumber = dto.CellPhoneNumber,
				Address = dto.Address,
				Gender = dto.Gender,
				Image = dto.Image,
				CreateDate = DateTime.Now,
				IsConfirmed = true,
				ConfirmCode = "0",
				Birthday = dto.Birthday,
				IsSuspended = null
			};

			_db.Members.Add(member);
			

			MemberImage memberImage = new MemberImage
			{
				Image = dto.Image,
				Name = "未命名",
			};
			//存到DB
			

			_db.MemberImages.Add(memberImage);
			_db.SaveChanges();

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