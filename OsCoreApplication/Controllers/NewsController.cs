using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OsCoreApplication.Common;
using OsCoreApplication.ViewModel.Models;
using OsCoreApplication.Services;

namespace OsCoreApplication.Controllers
{
    public class NewsController : Controller
    {

        private Inews _newsServices;
        public NewsController(Inews news)
        {
            _newsServices = news;
        }

        // GET: News
        public ActionResult Index(int Page = 1)
        {
            int pageSize = 12;
            int totalRows = 0;
            var listNews = _newsServices.GetAllNews(pageSize, Page, out totalRows);
            Paging p = new Paging();
            p.Page = Page;
            p.Url = "../News/?Page=";
            p.Node = ClsCommon.SetPage(Page, totalRows, pageSize);
            ViewBag.Paging = p;
            return View(listNews);

        }


        public ActionResult NewsRandom()
        {
            var listNewsRand = _newsServices.GetNewsRandom(10);
            return View(listNewsRand);
        }



        public ActionResult NewsDetail(long id)
        {
            var news = _newsServices.GetNewsDetail(id);
            return View(news);
        }

    }
}