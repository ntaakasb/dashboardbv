using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OsCoreApplication.Services;

namespace OsCoreApplication.Controllers
{
    public class InfoFooterController : Controller
    {
        private IConfigWeb _configWebServices;

        public InfoFooterController(IConfigWeb configWeb)
        {
            _configWebServices = configWeb;
        }

        // GET: InfoFooter
        public ActionResult Index()
        {
            var dataConfigWebHome = _configWebServices.GetListConfigWebByType("LAYOUT");
            return View(dataConfigWebHome);
        }

        public ActionResult InfoContact()
        {
            var dataConfigWebHome = _configWebServices.GetListConfigWebByType("LAYOUT");
            return View(dataConfigWebHome);
        }

        public ActionResult LinkCall()
        {
            var dataConfigWebHome = _configWebServices.GetListConfigWebByType("LAYOUT");
            return View(dataConfigWebHome);
        }



    }
}