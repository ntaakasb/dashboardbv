using OsCoreApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OsCoreApplication.Controllers
{
    public class SliderController : Controller
    {
        private IBanner _bannerServices;

        public SliderController(IBanner banner)
        {
            _bannerServices = banner;
        }

        // GET: Slider
        public ActionResult Index()
        {
            var listBanner = _bannerServices.GetListBanner();
            return View(listBanner);
        }
    }
}