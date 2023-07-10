using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Infra.Criterias;
using RouteMaster.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Data.Entity;

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
				IsConfirmed = false,
				ConfirmCode = dto.ConfirmCode,
				Birthday = dto.Birthday,
				IsSuspended = false, //預設為註冊時都未停權
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

		public IEnumerable<MemberIndexDto> Search(MemberCriteria criteria)
		{

			
			var query = _db.Members.AsEnumerable();


			if (string.IsNullOrEmpty(criteria.FirstName) == false)
			{
				query = query.Where(m => m.FirstName.Contains(criteria.FirstName));
			}
			if (string.IsNullOrEmpty(criteria.LastName) == false)
			{
				query = query.Where(m => m.LastName.Contains(criteria.LastName));
			}
			#region 姓名一起搜
			//if (string.IsNullOrEmpty(criteria.FirstName) == false && string.IsNullOrEmpty(criteria.LastName) == false)
			//{
			//	query = query.Where(m => m.FirstName.Contains(criteria.FirstName) && m.LastName.Contains(criteria.LastName));
			//}
			#endregion 

			if (string.IsNullOrEmpty(criteria.Account) == false)
			{
				query = query.Where(m => m.Account.Contains(criteria.Account));
			}
			if (string.IsNullOrEmpty(criteria.Email) == false)
			{
				query = query.Where(m => m.Email.Contains(criteria.Email));
			}
			if (string.IsNullOrEmpty(criteria.CellPhoneNumber) == false)
			{
				query = query.Where(m => m.CellPhoneNumber.Contains(criteria.CellPhoneNumber));
			}
			if (criteria.CreateDateBegin.HasValue)
			{
				query = query.Where(m => m.CreateDate >= criteria.CreateDateBegin.Value);
			}
			if (criteria.CreateDateEnd.HasValue)
			{
				query = query.Where(m => m.CreateDate <= criteria.CreateDateEnd.Value);
			}


			var Members = query.Select(m => new MemberIndexDto
				{
					Id = m.Id,
					FirstName = m.FirstName,
					LastName = m.LastName,
					Account = m.Account,
					Email = m.Email,
					CellPhoneNumber = m.CellPhoneNumber,
					Address = m.Address,
					Gender = m.Gender,
					Birthday = m.Birthday,
					CreateDate = m.CreateDate,
					IsConfirmed = m.IsConfirmed,
					IsSuspended = m.IsSuspended,
					ConfirmCode = m.ConfirmCode,
				});
			return Members;
		}

		public IEnumerable<MemberIndexDto> SearchPerson(int id)
		{
			var query = _db.Members.AsEnumerable();

			var Members = query.Select(m => new MemberIndexDto
			{
				Id = m.Id,
				FirstName = m.FirstName,
				LastName = m.LastName,
				Account = m.Account,
				Email = m.Email,
				CellPhoneNumber = m.CellPhoneNumber,
				Address = m.Address,
				Gender = m.Gender,
				Birthday = m.Birthday,
				CreateDate = m.CreateDate,
				IsConfirmed = m.IsConfirmed,
				IsSuspended = m.IsSuspended,
				ConfirmCode = m.ConfirmCode,
			});
			return Members;
		}


		public void UpdateMemberImage(MemberImageCreateDto dto)
		{
		}

		//private string GetFullName(MemberCriteria criteria)
		//{
		//	if (!string.IsNullOrEmpty(criteria.FirstName) && !string.IsNullOrEmpty(criteria.LastName))
		//	{
		//		return criteria.FirstName + " " + criteria.LastName;
		//	}
		//	else if (!string.IsNullOrEmpty(criteria.FirstName))
		//	{
		//		return criteria.FirstName;
		//	}
		//	else if (!string.IsNullOrEmpty(criteria.LastName))
		//	{
		//		return criteria.LastName;
		//	}

		//	return string.Empty;
		//}

	}
}