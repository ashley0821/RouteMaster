using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using RouteMaster.Models;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Infra;
using RouteMaster.Models.Infra.Criterias;
using RouteMaster.Models.Infra.EFRepositories;
using RouteMaster.Models.Interfaces;
using RouteMaster.Models.Services;
using RouteMaster.Models.ViewModels;

namespace RouteMaster.Controllers
{
    public class AdministratorsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Administrators
        //public ActionResult Index()
        //{
        //    var administrators = db.Administrators.Include(a => a.Permission);
        //    return View(administrators.ToList());
        //}

        // GET: Administrators/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrator administrator = db.Administrators.Find(id);
            if (administrator == null)
            {
                return HttpNotFound();
            }
            return View(administrator);
        }

        // GET: Administrators/Create
        public ActionResult Create()
        {
            ViewBag.PermissionId = new SelectList(db.Permissions, "Id", "Name");
            return View();
        }

        // POST: Administrators/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PermissionId,FirstName,LastName,EncryptedPassword,Email,CreateDate,ConfirmCode,IsSuspended")] Administrator administrator)
        {
            if (ModelState.IsValid)
            {
                db.Administrators.Add(administrator);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PermissionId = new SelectList(db.Permissions, "Id", "Name", administrator.PermissionId);
            return View(administrator);
        }

        // GET: Administrators/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrator administrator = db.Administrators.Find(id);
            if (administrator == null)
            {
                return HttpNotFound();
            }
            ViewBag.PermissionId = new SelectList(db.Permissions, "Id", "Name", administrator.PermissionId);
            return View(administrator);
        }

        // POST: Administrators/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PermissionId,FirstName,LastName,EncryptedPassword,Email,CreateDate,ConfirmCode,IsSuspended")] Administrator administrator)
        {
            if (ModelState.IsValid)
            {
                db.Entry(administrator).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PermissionId = new SelectList(db.Permissions, "Id", "Name", administrator.PermissionId);
            return View(administrator);
        }

        // GET: Administrators/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrator administrator = db.Administrators.Find(id);
            if (administrator == null)
            {
                return HttpNotFound();
            }
            return View(administrator);
        }

        // POST: Administrators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Administrator administrator = db.Administrators.Find(id);
            db.Administrators.Remove(administrator);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

		public ActionResult Index(AdministratorCriteria criteria)
		{
			ViewBag.Criteria = criteria;

			IEnumerable<AdministratorIndexVM> administrators = GetAdministrators(criteria);
			return View(administrators);
		}

		private IEnumerable<AdministratorIndexVM> GetAdministrators(AdministratorCriteria criteria)
		{
			IAdministratorRepository repo = new AdministratorEFRepository();
            AdministratorService service = new AdministratorService(repo);
            return service.Search(criteria)
                          .Select(dto => new AdministratorIndexVM
                          {
                              Id = dto.Id,
                              PermissionId = dto.PermissionId,
                              FirstName = dto.FirstName,
                              LastName = dto.LastName,
                              Email = dto.Email,
                              CreateDate = dto.CreateDate,
                          });
        }

        public ActionResult Register()
        {
			PreparePermissionDataSource(null);
			return View();
        }

		private void PreparePermissionDataSource(int? permissionId)
		{
			var permission = db.Permissions.ToList().Prepend(new Permission());
			ViewBag.PermissionId = new SelectList(permission, "Id", "Name", permissionId);
		}

		[HttpPost]
        public ActionResult Register(AdministratorRegisterVM vm)
        {
            Result result =  RegisterAdministrator(vm);

            if (result.IsSuccess)
            {
                return View("ConfirmRegister");
            }
            else
            {
                ModelState.AddModelError(string.Empty, result.ErrorMessage);
                return View();
            }
        }
		
		public Result RegisterAdministrator(AdministratorRegisterVM vm)
        {
            IAdministratorRepository repo  = new AdministratorEFRepository();

            AdministratorService service = new AdministratorService(repo);
            return service.Register(vm.ToDto());
        }

		public ActionResult Login()
        {
            return View();
        }

		[HttpPost]
		public ActionResult Login(AdministratorLoginVM vm)
		{
			if (ModelState.IsValid)
			{
				using (var context = new AppDbContext())
				{
					Administrator user = context.Administrators
									   .Where(a => a.Email == vm.Email && a.EncryptedPassword == vm.EncryptedPassword)
									   .FirstOrDefault();

					if (user != null)
					{
						Session["UserName"] = user.LastName;
						Session["UserEmail"] = user.Email;
						return RedirectToAction("Index", "Home");
					}
					else
					{
						ModelState.AddModelError("", "Invalid User Name or Password");
						return View(vm);
					}
				}
			}
			else
			{
				return View(vm);
			}
		}


        [HttpPost]
        //public ActionResult Login(AdministratorLoginVM vm)
        //{
        //    if (ModelState.IsValid == false) return View(vm);
        //    Result result = ValidLogin(vm);
        //    if (result.IsSuccess != true)
        //    {
        //        ModelState.AddModelError("", result.ErrorMessage);
        //        return View(vm);
        //    }
        //    const bool rememberMe = false;

        //    (string returnUrl, HttpCookie cookie) processResult = ProcessLogin(vm.FirstName, rememberMe);

        //    Response.Cookies.Add(processResult.cookie);

        //    return Redirect(processResult.returnUrl);
        //}

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

        private Result ValidLogin(AdministratorLoginVM vm)
        {
            var db = new AppDbContext();
            var administrator = db.Administrators.FirstOrDefault(a => a.FirstName == vm.Email);

            if (administrator == null) return Result.Fail("帳密有誤");

            /*if (member.IsConfirmed == false || member.IsConfirmed == false) return Result.Fail("管理人員資格尚未確認");*/

            var salt = HashUtility.GetSalt();
            var hashPassword = HashUtility.ToSHA256(vm.EncryptedPassword, salt);

            return string.Compare(administrator.EncryptedPassword, hashPassword) == 0
                ? Result.Success()
                : Result.Fail("帳密有誤");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
