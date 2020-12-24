using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OsCoreApplication.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public JsonResult LoginSubmit(string email, string password)
        {
            if(email.ToUpper().Trim() == "ADMIN" && password.Trim()  == "admin")
            {
                Session["IsLoged"] = 1;
                return Json("1");
            }
            return Json("0");
        }
    }
}