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
        private IConfigWeb _configWebServices;
        private IContact _contatcServices;
        private IProject _projectServices;
        private IImagesUpload _imagesUploadServices;
        private Inews _newService;

        public HomeController(IConfigWeb configWeb, IContact contact, IProject project, Inews inews, IImagesUpload imagesUpload)
        {
            _configWebServices = configWeb;
            _contatcServices = contact;
            _projectServices = project;
            _imagesUploadServices = imagesUpload;
            _projectServices = project; 
            _newService = inews;
        }

        public ActionResult Index()
        {
            var dataConfigWebHome = _configWebServices.GetListConfigWebByType("HOME");
            ViewBag.ConfigWebHome = dataConfigWebHome;
            ViewBag.BinhDuongEast = _projectServices.GetListProjectByType(1, 20, 1, out int a);
            ViewBag.SaleProject = _projectServices.GetListProjectByType(2, 10, 1, out int b);
            ViewBag.LeaseProject = _projectServices.GetListProjectByType(3, 3, 1, out int c);
            ViewBag.ImagesUpload = _imagesUploadServices.GetListImageUpload(10);
            ViewBag.ProjectHightlight = _projectServices.GetListProjectHightlights(10);

            //news list
            int pageSize = 12;
            int totalRows = 0;
            ViewBag.ListNews = _newService.GetAllNews(pageSize, 1, out totalRows);
            //end
            return View();
        }

        public ActionResult About()
        {
            var dataConfigWebHome = _configWebServices.GetListConfigWebByType("HOME");
            ViewBag.ConfigWebHome = dataConfigWebHome;
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult SaveContact(FormCollection frm)
        {
            Contact c = new Contact();
            c.Name = frm["name"];
            c.Email = frm["mail"];
            c.Subject = frm["subjectForm"];
            c.ContentMsg = frm["messageForm"];
            c.DateContact = DateTime.Now;

            try
            {
                _contatcServices.InsertContact(c);
                return Json(new { msg = "Đã gửi liên hệ. Chúng tôi sẽ liên lạc với bạn sớm.", info = 0});
            }
            catch (Exception ex)
            {
                return Json(new { msg = "Không thể gửi liên hệ bây giờ. Vui lòng thử lại sau.", info = -1});
            }       
        }    

    }
}