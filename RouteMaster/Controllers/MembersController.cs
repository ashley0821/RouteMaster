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

		private IEnumerable<MemberIndexVM> GetMembers()
		{
			IMemberRepository repo = new MemberRepository();
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
		private ActionResult Create()
		{
			return View();
		}

		//[HttpPost]
		//private ActionResult Create()
		//{
		//	return View();
		//}
	}
}