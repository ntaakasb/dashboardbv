using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OsCoreApplication.ViewModel.Models;

namespace OsCoreApplication.Controllers
{
    public class PagingController : Controller
    {
        // GET: Paging
        public ActionResult Index(Paging p)
        {  
            return View(p);
        }     
    }
}