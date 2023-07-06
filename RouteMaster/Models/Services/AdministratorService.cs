using RouteMaster.Models.Infra;
using RouteMaster.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Services
{
	public class AdministratorService
	{
		private IAdministratorRepository _repo;

		public AdministratorService(IAdministratorRepository repo)
		{
			_repo = repo;
		}

		public Result Register(AdministratorRegisterDto dto)
		{
			// 判斷註冊信箱是否已被使用
			if (_repo.ExistEmail(dto.Email))
			{
				// 丟出異常,或者傳回 Result
				return Result.Fail($"帳號 {dto.Email} 已存在, 請更換後再試一次");
			}

			// 將密碼進行雜湊
			var salt = HashUtility.GetSalt();
			var hashPassword = HashUtility.ToSHA256(dto.Password, salt);
			dto.EncryptedPassword = hashPassword;
			dto.ConfirmCode = Guid.NewGuid().ToString("N");

			//新增一筆紀錄
			_repo.Register(dto);

			//todo 寄發 email
			return Result.Success();


		}
	}
}