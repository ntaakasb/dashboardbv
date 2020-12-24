using System;
using System.Collections.Generic;
using System.Web.Mvc;
using OsCoreApplication.DataLayer.EFModel;
using OsCoreApplication.Services;
using OsCoreApplication.ViewModel.Models;

namespace OsCoreApplication.Controllers
{
    public class HomeController : Controller
    {
        private IServices _services;
        public HomeController(IServices services)
        {
            _services = services;
        }
        public ActionResult Index()
        {
            DateTime? date = null;
            ViewBag.ThongKe = _services.getThongKeTuan();
            ViewBag.DetailInWeek = _services.getDetailBenhNhanTheoTuan(null, null);
            ViewBag.ThongKeTheoKhoa = _services.getThongKeTheoKhoa(null, null);
            //end


            return View();
        }

       

    }
}