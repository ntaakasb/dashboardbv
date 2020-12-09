using System.Web;
using System.Web.Mvc;
using OsCoreApplication.Common;
using System.Text.RegularExpressions;
using System.Data.Entity.Migrations.Model;

namespace OsCoreApplication.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {     
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //if (HttpContext.Session[SessionAccount.UserId] == null)
            //{
            //    var returnUrl = this.Request.RawUrl ?? "";
            //    if (returnUrl.Length > 5)
            //    {
            //        Regex rgx = new Regex(@"/[a-z]{2}-[a-z]{2}");
            //        if (rgx.IsMatch(returnUrl.Substring(0, 6).ToLower()))
            //            returnUrl = returnUrl.Substring(6);
            //    }
            //    var url = Url.Action("Login", "Account", new { returnUrl });
            //    filterContext.Result = new RedirectResult(HttpUtility.UrlDecode(url));
            //    return;
            //}
           
            base.OnActionExecuting(filterContext);

            //ViewBag.LoggedUser = "1234";
        }


        public BaseController()
        {
           
        }


        //protected override void OnAuthorization(AuthorizationContext filterContext)
        //{
           
        //     ViewBag.LoggedUser = "XXXXX";
        //     base.OnAuthorization(filterContext);
             
        //}

    }
}