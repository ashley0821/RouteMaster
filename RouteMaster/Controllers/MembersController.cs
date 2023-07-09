using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Infra.EFRepositories;
using RouteMaster.Models.Infra;
using RouteMaster.Models.Interfaces;
using RouteMaster.Models.Services;
using RouteMaster.Models.ViewModels;
using RouteMaster.Models.Dto;
using RouteMaster.Models.Infra.Criterias;
using System.Data.Entity.Migrations;
using Microsoft.Ajax.Utilities;
using Member = RouteMaster.Models.EFModels.Member;
using Microsoft.SqlServer.Server;
using System.IO;
using static System.Net.WebRequestMethods;
using System.Web.Helpers;

namespace RouteMaster.Controllers
{
    public class MembersController : Controller
    {
        
        
        private readonly AppDbContext db = new AppDbContext();





        // GET: Members/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: Members/Create
        public ActionResult Create()
        {
            return View();
        }

        #region entityCreate
        // POST: Members/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Account,EncryptedPassword,Email,CellPhoneNumber,Address,Gender,Birthday,CreateDate,Image,IsConfirmed,ConfirmCode,IsSuspended")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Members.Add(member);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(member);
        }
        #endregion 

        // GET: Members/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Account,EncryptedPassword,Email,CellPhoneNumber,Address,Gender,Birthday,CreateDate,Image,IsConfirmed,ConfirmCode,IsSuspended")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(member);
        }

        // GET: Members/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Member member = db.Members.Find(id);
            db.Members.Remove(member);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Index(MemberCriteria criteria)
        {
            ViewBag.Criteria = criteria;

            IEnumerable<MemberIndexVM> members = GetMembers(criteria);
            return View(members);
        }

        public IEnumerable<MemberIndexVM> GetMembers(MemberCriteria criteria)
        {
            IMemberRepository repo = new MemberEFRepository();
            MemberService service = new MemberService(repo);
            return service.Search(criteria)
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
        public ActionResult Register(MemberRegisterVM vm, HttpPostedFileBase facePhoto1)
        {
            if (ModelState.IsValid)
            {
                // 將 file1存檔, 並取得最後存檔的檔案名稱
                string path = Server.MapPath("/Uploads"); // 檔案要存放的資料夾位置
                string fileName = SaveUploadedFile(path, facePhoto1);

                // 將檔名存入 vm裡
                vm.Image = fileName; // ****
            }
            else
            {
                return View(vm);
            }


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

   
        private string SaveUploadedFile(string path, HttpPostedFileBase facePhoto1)
        {
            // 如果沒有上傳檔案或檔案是空的,就不處理, 傳回 string.empty
            if (facePhoto1 == null || facePhoto1.ContentLength == 0)return string.Empty;
           
            // 取得上傳檔案的副檔名
            string ext = System.IO.Path.GetExtension(facePhoto1.FileName); // ".jpg" 而不是 "jpg"

            // 如果副檔名不在允許的範圍裡,表示上傳不合理的檔案類型, 就不處理, 傳回 string.empty
            string[] allowedExts = new string[] { ".jpg", ".jpeg", ".png", ".tif" };
            if (allowedExts.Contains(ext.ToLower()) == false) return string.Empty;

            // 生成一個不會重複的檔名
            string newFileName = Guid.NewGuid().ToString("N") + ext; // 生成 er534263454r45636t34534sfggtwer6563462343.jpg
            string fullName = System.IO.Path.Combine(path, newFileName);

            // 將上傳檔案存放到指定位置
            facePhoto1.SaveAs(fullName);

            // 傳回存放的檔名
            return newFileName;
        }


        public Result RegisterMember(MemberRegisterVM
            
            vm)
        {
            IMemberRepository repo = new MemberEFRepository();

            MemberService service = new MemberService(repo);


            return service.Register(vm.ToDto());
        }


        public ActionResult ConfirmRegister()
        {

            return View();
        }


        [HttpGet]
        public ActionResult ActiveRegister(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }



			EmailHelper emailHelper = new EmailHelper();
			var url = "https://localhost:44371/Members/ActiveMember";
			var name = member.Account;
			var email = member.Email;
			var Id = member.Id;
			var ConfirmCode = member.ConfirmCode;
			emailHelper.SendConfirmRegisterEmail(url, name, email, Id, ConfirmCode);





			return View(member);
        }

		//     [HttpPost]
		//     public ActionResult ActiveRegister(int id,string confirmcode)
		//     {
		//         var MemberInDb = db.Members.Find(id);
		//         if (MemberInDb.ConfirmCode == confirmcode)
		//         {
		//             MemberInDb.IsConfirmed = true;
		//             MemberInDb.ConfirmCode = confirmcode;
		//	db.SaveChanges();
		//}
		//         else
		//         {
		//             return RedirectToAction("Index");
		//         }            

		//         return RedirectToAction("Index");
		//     }


		[HttpGet]
		public ActionResult ActiveMember(int? memberId, string confirmcode="")
        {
			var MemberInDb = db.Members.Find(memberId);
			if (MemberInDb.ConfirmCode == confirmcode)
			{
				MemberInDb.IsConfirmed = true;
				MemberInDb.ConfirmCode = null;
				db.SaveChanges();
			}

			else
			{
				return RedirectToAction("Index");
			}

			return RedirectToAction("Index");

			//var db = new AppDbContext();
			//// 根據memberId找出一筆記錄, 若找不到就return, 若 isConfirmed不是0, 或confirmCode 不符, 就return
			//// 查詢方式 SELECT * FROM Members WHERE memberid=99 and isConfirmed=0 and confirmCode='xxx'
			//var memberInDb = db.Members.FirstOrDefault(m => m.Id == memberId && m.IsConfirmed == false && m.ConfirmCode == confirmCode);
			//if (memberInDb == null) return Result.Success(); // 就算找不到, 也傳回成功, 不要讓惡意使用者得知測試的結果

			//// 啟用會員
			//// 啟用此會員的方式, UPDATE Members SET isConfirmed=1 , confirmCode=null WHERE id=99
			//memberInDb.IsConfirmed = true;
			//memberInDb.ConfirmCode = null;

			//db.SaveChanges();

			//return Result.Success();
		}




        public ActionResult MemberEdit()
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
            if (result.IsSuccess != true)
            {
                ModelState.AddModelError("", result.ErrorMessage);
                return View(vm);
            }

            const bool rememberMe = false;

            (string returnUrl, HttpCookie cookie) = ProcessLogin(vm.Account, rememberMe);

            Response.Cookies.Add(cookie);

            return Redirect(returnUrl);
        }


        public ActionResult Logout()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return Redirect("/Members/Login");
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

            //if (member.IsConfirmed == false || member.IsConfirmed == false) return Result.Fail("會員資格尚未確認");

            if (member.IsSuspended == true) return Result.Fail("帳號已被停權，如有問題請聯絡客服");

            var salt = HashUtility.GetSalt();
            var hashPassword = HashUtility.ToSHA256(vm.Password, salt);

            return string.Compare(member.EncryptedPassword, hashPassword) == 0
                ? Result.Success()
                : Result.Fail("帳密有誤");
        }


        public ActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgetPassword(MemberForgetPasswordVM vm)
        {
            if (ModelState.IsValid == false) return View(vm);

            // 生成email裡的連結
            var urlTemplate = Request.Url.Scheme + "://" +  // 生成 http:.// 或 https://
                             Request.Url.Authority + "/" + // 生成網域名稱或 ip
                             "Members/ResetPassword?memberid={0}&confirmCode={1}"; // 生成網頁 url

            Result result = ProcessResetPassword(vm.Account, vm.Email, urlTemplate);

            if (result.IsFalse)
            {
                ModelState.AddModelError(string.Empty, result.ErrorMessage);
                return View(vm);
            }

            return View("ConfirmForgetPassword");
        }


        private Result ProcessResetPassword(string account, string email, string urlTemplate)
        {
            var db = new AppDbContext();
            // 檢查account,email正確性
            var memberInDb = db.Members.FirstOrDefault(m => m.Account == account);

            if (memberInDb == null) return Result.Fail("帳號或 Email 錯誤"); // 故意不告知確切錯誤原因

            if (string.Compare(email, memberInDb.Email, StringComparison.CurrentCultureIgnoreCase) != 0) return Result.Fail("帳號或 Email 錯誤");

            // 檢查 IsConfirmed必需是true, 因為只有已啟用的帳號才能重設密碼
            if (memberInDb.IsConfirmed == false) return Result.Fail("您還沒有啟用本帳號, 請先完成才能重設密碼");

            // 更新記錄, 填入 confirmCode
            var confirmCode = Guid.NewGuid().ToString("N");
            memberInDb.ConfirmCode = confirmCode;
            db.SaveChanges();

            // 發email
            var url = string.Format(urlTemplate, memberInDb.Id, confirmCode);
            new EmailHelper().SendForgetPasswordEmail(url, memberInDb.FirstName, email);

            return Result.Success();
        }


        public ActionResult EditPassword()
        {
            return View();
        }


        [HttpPost]
        public ActionResult EditPassword(MemberEditPasswordVM vm)
        {
            if (ModelState.IsValid == false) return View(vm);

            var currentUserAccount = User.Identity.Name;

            Result result = ChangePassword(currentUserAccount, vm);

            if (result.IsSuccess == false)
            {
                ModelState.AddModelError(string.Empty, result.ErrorMessage);
                return View(vm);
            }
            return RedirectToAction("Index");
        }


        private Result ChangePassword(string account, MemberEditPasswordVM vm)
        {
            var salt = HashUtility.GetSalt();
            var hashOrigPassword = HashUtility.ToSHA256(vm.OldPassword, salt);

            var db = new AppDbContext();

            var memberInDb = db.Members.FirstOrDefault(m => m.Account == account && m.EncryptedPassword == hashOrigPassword);
            if (memberInDb == null) return Result.Fail("找不到要修改的會員記錄");

            var hashPassword = HashUtility.ToSHA256(vm.NewPassword, salt);

            // 更新密碼
            memberInDb.EncryptedPassword = hashPassword;
            db.SaveChanges();

            return Result.Success();
        }


        public ActionResult EditMemberImgae(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);

            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member.ToMemberImageVM());
        }


        [HttpPost]
        public ActionResult EditMemberImage(MemberImageEditVM vm, HttpPostedFileBase newFacePhoto)
        {
            string path = Server.MapPath("/Uploads");
            var savedFileName = SaveUploadedFile(path, newFacePhoto);
            vm.Image = savedFileName;

            if (savedFileName == null) ModelState.AddModelError("Image", "請選擇檔案");

            if (ModelState.IsValid)
            {
                var MemberInDb = db.Members.Find(vm.Id);
                MemberInDb.Image = vm.Image;

                db.SaveChanges();

                var MemberImageIndb = db.MemberImages.FirstOrDefault(m => m.MemberId == vm.Id);
                MemberImageIndb.Image = vm.Image;

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(vm);

        }


        public ActionResult SuspendMember(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);

            if (member == null)
            {
                return HttpNotFound();
            }

            return View();
        }

        
        [HttpPost]
        public ActionResult SuspendMember(MemberSuspendVM vm)
        {
            var MemberInDb = db.Members.Find(vm.Id);
            MemberInDb.IsSuspended = vm.IsSuspended;


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
