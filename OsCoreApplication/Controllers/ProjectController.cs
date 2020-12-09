using OsCoreApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OsCoreApplication.Common;
using OsCoreApplication.ViewModel.Models;

namespace OsCoreApplication.Controllers
{
    public class ProjectController : Controller
    {
        private IProject _projectServices;
        private IProjectImages _projectImageServices;

        public ProjectController( IProject project, IProjectImages projectImages)
        {
            _projectServices = project;
            _projectImageServices = projectImages;
        }


        // GET: Project
        public ActionResult Index(int Page = 1)
        {
            int pageSize = 6;
            int totalRows = 0;
            var listProject = _projectServices.GetAllProject(pageSize, Page, out totalRows);
            Paging p = new Paging();
            p.Page = Page;
            p.Url = "../Project/?Page=";
            p.Node = ClsCommon.SetPage(Page, totalRows, pageSize);
            ViewBag.Paging = p;
            return View(listProject);
        }

        public ActionResult ProjectHeightlights()
        {
            var listProjectHightlights = _projectServices.GetListProjectHightlights(10);
            return View(listProjectHightlights);
        }

        public ActionResult ProjectRandom()
                {
                    var listProjectHightlights = _projectServices.GetListProjectRandom(10);
                    return View(listProjectHightlights);
                }



        public ActionResult ProjectDetail(long id)
        {
            var project = _projectServices.GetProjectDetail(id);
            var projectImages = _projectImageServices.GetListProjectImage(id);
            ViewBag.ProjectImages = projectImages;
            return View(project);
        }


        public ActionResult BinhDuongEast(int Page = 1)
        {
            int pageSize = 6;
            int totalRows = 0;
            var listProject = _projectServices.GetListProjectByType(1,pageSize, Page, out totalRows);
            Paging p = new Paging();
            p.Page = Page;
            p.Url = "../bat-dong-san-dong-binh-duong/";
            p.Node = ClsCommon.SetPage(Page, totalRows, pageSize);
            ViewBag.Paging = p;
            return View(listProject);
        }


        public ActionResult SaleProject(int Page = 1)
        {
            int pageSize = 6;
            int totalRows = 0;
            var listProject = _projectServices.GetListProjectByType(2, pageSize, Page, out totalRows);
            Paging p = new Paging();
            p.Page = Page;
            p.Url = "../bat-dong-san-can-ban/";
            p.Node = ClsCommon.SetPage(Page, totalRows, pageSize);
            ViewBag.Paging = p;
            return View(listProject);
        }

        public ActionResult LeaseProject(int Page = 1)
        {
            int pageSize = 6;
            int totalRows = 0;
            var listProject = _projectServices.GetListProjectByType(3, pageSize, Page, out totalRows);
            Paging p = new Paging();
            p.Page = Page;
            p.Url = "../bat-dong-san-cho-thue/";
            p.Node = ClsCommon.SetPage(Page, totalRows, pageSize);
            ViewBag.Paging = p;
            return View(listProject);
        }







    }
}