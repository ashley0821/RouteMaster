using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Services.Description;
using System.Web.UI.WebControls;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Infra;
using RouteMaster.Models.ViewModels;

namespace RouteMaster.Controllers
{
    public class PartnersController : Controller
    {
        private AppDbContext db = new AppDbContext();

		// GET: Partners

		public ActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Register(PartnerRegisterVM vm)
		{
			if (ModelState.IsValid == false) return View(vm);

			// 建立新會員
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

		private Result RegisterMember(PartnerRegisterVM vm)
		{
			if (db.Partners.Any(m => m.Email == vm.Email))
			{
				// 丟出異常,或者傳回 Result
				return Result.Fail($"帳號 {vm.Email} 已存在, 請更換後再試一次");
			}

			// 將密碼進行雜湊
			var salt = HashUtility.GetSalt();
			var hashPassword = HashUtility.ToSHA256(vm.Password, salt);
			vm.EncryptedPassword = hashPassword;

			// 填入 isConfirmed, ConfirmCode
			vm.IsConfirmed = false;
			vm.ConfirmCode = Guid.NewGuid().ToString("N");

            Partner partner = new Partner
            {
                Email = vm.Email,
                EncryptedPassword = vm.EncryptedPassword,
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                IsConfirmed = vm.IsConfirmed,
                ConfirmCode = vm.ConfirmCode,
                IsSuspended = false
			};

			// 將它存到db
			db.Partners.Add(partner);
			db.SaveChanges();
			return Result.Success();
		}


        // 記得要寫
		public ActionResult ActiveRegister(int memberId, string confirmCode)
		{
			// 根據 memberId, confirmCode,去Members table查詢是否有這一筆,若有, 就啟用此會員資格
			//Result result = ActiveMember(memberId, confirmCode);

			return View();
		}

		public ActionResult PartnerLogin()
		{
			return View();
		}

		[HttpPost]
		public ActionResult PartnerLogin(PartnerLoginVM vm)
		{
			if (ModelState.IsValid == false) return View(vm);

			// 驗證帳密的正確性
			Result result = ValidLogin(vm);

			if (result.IsSuccess != true) // 若驗證失敗...
			{
				ModelState.AddModelError("", result.ErrorMessage);
				return View(vm);
			}

			const bool rememberMe = false; // 是否記住登入成功的會員

			// 若登入帳密正確,就開始處理後續登入作業,將登入帳號編碼之後,加到 cookie裡
			(string returnUrl, HttpCookie cookie) processResult = ProcessLogin(vm.Email, rememberMe);

			Response.Cookies.Add(processResult.cookie);

			return Redirect(processResult.returnUrl);
		}

		private (string returnUrl, HttpCookie cookie) ProcessLogin(string email, bool rememberMe)
		{

			var roles = db.Partners.FirstOrDefault(p=>p.Email == email).Id.ToString(); // 在本範例, 沒有用到角色權限,所以存入空白

			// 建立一張認證票
			var ticket =
				new FormsAuthenticationTicket(
					1,          // 版本別, 沒特別用處
					email,
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
			cookie.Expires = DateTime.Now.AddYears(1); // 設定 Cookie 的過期日期為一年後
            // 取得return url
			var url = FormsAuthentication.GetRedirectUrl(email, true); //第二個引數沒有用處

			return (url, cookie);
		}

		private Result ValidLogin(PartnerLoginVM vm)
		{
			var db = new AppDbContext();
			var parnter = db.Partners.FirstOrDefault(m => m.Email == vm.Email);

			if (parnter == null) return Result.Fail("帳密有誤");

			if (parnter.IsConfirmed == false) return Result.Fail("會員資格尚未確認");

			var salt = HashUtility.GetSalt();
			var hashPassword = HashUtility.ToSHA256(vm.Password, salt);

			return string.Compare(parnter.EncryptedPassword, hashPassword) == 0
				? Result.Success()
				: Result.Fail("帳密有誤");
		}

		// 記得要寫
		public ActionResult Logout()
		{
			Session.Abandon();
			FormsAuthentication.SignOut();
			return Redirect("/Members/Login");
		}


		public ActionResult Index()
        {
            return View(db.Partners.ToList());
        }

        // GET: Partners/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Partner partner = db.Partners.Find(id);
            if (partner == null)
            {
                return HttpNotFound();
            }
            return View(partner);
        }

        // GET: Partners/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Partners/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Email,EncryptedPassword,CreateDate,IsConfirmed,ConfirmCode,IsSuspended")] Partner partner)
        {
            if (ModelState.IsValid)
            {
                db.Partners.Add(partner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(partner);
        }

        // GET: Partners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Partner partner = db.Partners.Find(id);
            if (partner == null)
            {
                return HttpNotFound();
            }
            return View(partner);
        }

        // POST: Partners/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Email,EncryptedPassword,CreateDate,IsConfirmed,ConfirmCode,IsSuspended")] Partner partner)
        {
            if (ModelState.IsValid)
            {
                db.Entry(partner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(partner);
        }

        // GET: Partners/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Partner partner = db.Partners.Find(id);
            if (partner == null)
            {
                return HttpNotFound();
            }
            return View(partner);
        }

        // POST: Partners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Partner partner = db.Partners.Find(id);
            db.Partners.Remove(partner);
            db.SaveChanges();
            return RedirectToAction("Index");
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
