using RouteMaster.Models.Dto;
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
			if (ModelState.IsValid == false) return View(vm);

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

		public ActionResult ActiveRegister()
		{
			return View();
		}

		public Result RegisterMember(MemberRegisterVM vm)
		{
			IMemberRepository repo = new MemberEFRepository();

			MemberService service = new MemberService(repo);
			return service.Register(vm.ToDto());
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
		public ActionResult Login() 
		{
			return View();
		}


	}
}