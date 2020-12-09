using OsCoreApplication.Logger;
using OsCoreApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OsCoreApplication.Controllers
{
    public class AccountController : Controller
    {

        private IAccount _accountServices;
        public AccountController(IAccount account)
        {
            _accountServices = account;
        }

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Login(string user, string pass)
        {
            try
            {
                var account = _accountServices.GetUserAccountDetail(user, pass);
                if (account != null)
                {
                    Session["UserID"] = account.Username;
                    return Json(new { rel = 1, user = account.Username, msg = "Đăng nhập thành công" });
                }
                else
                {
                    return Json(new { rel = 2, user = "", msg = "Tên truy cập hoặc mật khẩu không đúng" });
                }    
            }
            catch (Exception ex)
            {
                OsLog.Error("AccountController --> Login ", ex);
            }
            return Json(new { rel = 0, user = "", msg = "Sai thông tin đăng nhập"});
        }
    }
}