using RouteMaster.Models.Dto;
using RouteMaster.Models.Infra;
using RouteMaster.Models.Infra.Criterias;
using RouteMaster.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

namespace RouteMaster.Models.Services
{
	public class MemberService
	{
		private IMemberRepository _repo;
		public MemberService(IMemberRepository repo)
		{
			_repo = repo;
		}
		public IEnumerable<MemberIndexDto> Search(MemberCriteria criteria)
		{
			return _repo.Search(criteria);
		}
		public Result Register(MemberRegisterDto dto)
		{
			// 判斷帳號是否已被使用
			if (_repo.ExistAccount(dto.Account))
			{
				// 丟出異常,或者傳回 Result
				return Result.Fail($"帳號 {dto.Account} 已存在, 請更換後再試一次");
			}
			
			//判斷性別
			//if(dto.Gender == )

			// 將密碼進行雜湊
			var salt = HashUtility.GetSalt();
			var hashPassword = HashUtility.ToSHA256(dto.Password, salt);
			dto.EncryptedPassword = hashPassword;

			// 填入 isConfirmed, ConfirmCode
			dto.IsConfirmed = false;
			dto.ConfirmCode = Guid.NewGuid().ToString("N");

			// 新增一筆記錄
			_repo.Register(dto);

   //         // todo 寄發 email
   //         EmailHelper emailHelper = new EmailHelper();

			//var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
			//var url = urlHelper.Action("ActiveRegister", "Members", new { Id = dto.Id, confirmCode = dto.ConfirmCode }, HttpContext.Current.Request.Url.Scheme);

			////var url = "https://localhost:44371/Members/ActiveRegister";
			//var name = dto.Account;
			//var email = dto.Email;


			//emailHelper.SendConfirmRegisterEmail(url, name, email);


            return Result.Success();
        }
	}
}