using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Infra;
using RouteMaster.Models.Infra.EFRepositories;
using RouteMaster.Models.Interfaces;
using RouteMaster.Models.Services;
using RouteMaster.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;



namespace RouteMaster.Controllers
{
	public class MemberController : Controller
	{
		// GET: Member
		public ActionResult Index()
		{
			IEnumerable<MemberIndexVM> members = GetMembers();
			return View(members);
		}

		public IEnumerable<MemberIndexVM> GetMembers()
		{
			IMemberRepository repo = new MemberEFRepository();
			MemberService service = new MemberService(repo);
			return service.Seacrh()
				.Select(dto => new MemberIndexVM
				{
					Id = dto.Id,
					FirstName = dto.FirstName,
					LastName = dto.LastName,
					Account = dto.Account,
					Email = dto.Email,
					CellPhoneNumber = dto.CellPhoneNumber,
					Address = dto.Address,
					Gender = dto.Gender,
					Birthday = dto.Birthday,
					CreateDate = dto.CreateDate,
					IsConfirmed = dto.IsConfirmed,
					IsSuspended = dto.IsSuspended,
				});
		}

		[HttpGet]
		public ActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Register(MemberRegisterVM vm) 
		{
			if (!ModelState.IsValid) return View(vm);

			Result result = RegisterMember(vm);

			if (result.IsSuccess)
			{
				return View("ConfirmRegister");
			}
			else
			{
				ModelState.AddModelError(string.Empty, result.ErrorMessage);
				return View(vm);
			}
		}

		public Result RegisterMember(MemberRegisterVM vm)
		{
			IMemberRepository repo = new MemberEFRepository();

			MemberService service = new MemberService(repo);
			return service.Register(vm.ToDto());
		}


		public ActionResult ConfirmRegister()
		{
			return View();
		}


		public ActionResult ActiveRegister(int Id, string confirmCode)
		{
			Result result = ActiveMember(Id, confirmCode);

			return View();
		}

		private Result ActiveMember(int Id, string confirmCode)
		{
			var db = new AppDbContext();

			var memberInDb = db.Members.FirstOrDefault(m=>m.Id == Id && m.IsConfirmed ==false && m.ConfirmCode == confirmCode);

			memberInDb.IsConfirmed = true;
			memberInDb.ConfirmCode = null;

			db.SaveChanges();

			return Result.Success();
		}

		public ActionResult EditMember()
		{
			return View();
		}

		public ActionResult DeleteMember()
		{
			return View();
		}

		public ActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Login(MemberLoginVM vm)
		{
			if (ModelState.IsValid == false) return View(vm);

			Result result = ValidLogin(vm);
			 if(result.IsSuccess != true)
			{
				ModelState.AddModelError("",result.ErrorMessage);
				return View(vm);
			}

			const bool rememberMe = false;

			(string returnUrl, HttpCookie cookie) processResult = ProcessLogin(vm.Account, rememberMe);

			Response.Cookies.Add(processResult.cookie);

			return Redirect(processResult.returnUrl);
		}

		private (string returnUrl, HttpCookie cookie) ProcessLogin(string account, bool rememberMe)
		{
			var roles = string.Empty; // 在本範例, 沒有用到角色權限,所以存入空白

			// 建立一張認證票
			var ticket =
				new FormsAuthenticationTicket(
					1,          // 版本別, 沒特別用處
					account,
					DateTime.Now,   // 發行日
					DateTime.Now.AddDays(2), // 到期日
					rememberMe,     // 是否續存
					roles,          // userdata
					"/" // cookie位置
				);

			// 將它加密
			var value = FormsAuthentication.Encrypt(ticket);

			// 存入cookie
			var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, value);

			// 取得return url
			var url = FormsAuthentication.GetRedirectUrl(account, true); //第二個引數沒有用處

			return (url, cookie);
		}

		private Result ValidLogin(MemberLoginVM vm)
		{
			var db = new AppDbContext();
			var member = db.Members.FirstOrDefault(m => m.Account == vm.Account);

			if (member == null) return Result.Fail("帳密有誤");

			if (member.IsConfirmed == false || member.IsConfirmed == false) return Result.Fail("會員資格尚未確認");

			var salt = HashUtility.GetSalt();
			var hashPassword = HashUtility.ToSHA256(vm.Password, salt);

			return string.Compare(member.EncryptedPassword, hashPassword) == 0
				? Result.Success()
				: Result.Fail("帳密有誤");
		}

		
	}
}