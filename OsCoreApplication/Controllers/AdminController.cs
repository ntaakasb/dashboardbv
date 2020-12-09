using OsCoreApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using OsCoreApplication.DataLayer.EFModel;
using System.IO;
using OsCoreApplication.ViewModel.Models;
using OsCoreApplication.Common;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace OsCoreApplication.Controllers
{
    public class AdminController : Controller
    {
        private IConfigWeb _configWebServices;
        private IBanner _bannerServices;
        private IContact _contactServices;
        private IProject _projectServices;
        private Inews _newsServices;
        private INewsCategory _newsCategoryServices;
        private IProjectImages _projectImagesServices;
        private IImagesUpload _imagesUploadServices;

        public AdminController(IConfigWeb configWeb, IBanner banner, IContact contact,
            IProject project, Inews news, INewsCategory newsCategory, IProjectImages projectImages,
            IImagesUpload imagesUpload
            )
        {
            _configWebServices = configWeb;
            _bannerServices = banner;
            _contactServices = contact;
            _projectServices = project;
            _newsServices = news;
            _newsCategoryServices = newsCategory;
            _projectImagesServices = projectImages;
            _imagesUploadServices = imagesUpload;
      
        }

        // GET: Admin
        public ActionResult Index()
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login");
            return View();
        }

        public ActionResult Logout()
        {
            Session["UserID"] = null;
            return RedirectToAction("Login");
        }

        public ViewResult Login()
        {
            return View();
        }
        public ActionResult WebConfig()
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login");
            var config = _configWebServices.GetListConfigWebByType("");       
            return View(config);
        }
        public ActionResult BannerManage()
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login");

            var banner = _bannerServices.GetListBanner();
            return View(banner);
        }



        [HttpPost]
        [ValidateInput(false)]
        public JsonResult UpdateConfig(string keyConfig, string valueConfig)
        {
            valueConfig = ClsCommon.ValidateRss(valueConfig);
            try
            {
                ConfigWeb config = new ConfigWeb();
                config.id = long.Parse(keyConfig);
                config.ValueConfig = valueConfig;
                config.DateUpdated = DateTime.Now;
                var c = _configWebServices.UpdadeConfig(config);
                if(c)
                    return Json(new { rel = 1 });
            }
            catch {}
            return Json(new { rel = 0 });
        }
        public JsonResult DeleteBanner(int id)
        {
            try
            {
                var c =  _bannerServices.DeleteBanner(id);
                if(c)
                    return Json(new { rel = 1 });
            }
            catch { }
            return Json(new { rel = 0 });

        }

        [HttpPost]
        public ActionResult UploadBanner()
        {   
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var imageFile = System.Web.HttpContext.Current.Request.Files["HelpSectionImages"];
                if (imageFile != null)
                {
                    if(imageFile.ContentLength > 1048575) //1MB = 1 * 1024 * 1024 - 1 = 1048575
                    {
                        return Json(new { rel = -2 }); //File is too large.
                    }    
                    string imageFileName = System.IO.Path.GetFileName(imageFile.FileName);
                    string pathSaveImage = System.IO.Path.Combine(
                                           Server.MapPath("~/Content/Images/Banner"), imageFileName);
                    try
                    {        
                        // file is uploaded
                        imageFile.SaveAs(pathSaveImage);
                    }
                    catch { 
                        return Json(new { rel = -1 }); //Upload Error
                    }
                    //Save to Database
                    //using (MemoryStream ms = new MemoryStream())
                    //{
                    //    imageFile.InputStream.CopyTo(ms);
                    //    byte[] array = ms.GetBuffer();
                    //}

                    Banner banner = new Banner();
                    banner.ImageUrl = "/Content/Images/Banner/" + imageFileName;
                    banner.IsShow = true;
                    banner.DateCreated = DateTime.Now;
                    banner.UserCreated = 1;

                    var ck = AddBanner(banner);
                    if(ck)
                        return Json(new { rel = 1 });                  
                    //return RedirectToAction("actionname", "controller name");
                }
            }
            // after successfully uploading redirect the user
            //return RedirectToAction("actionname", "controller name");

            return Json(new {rel = 0 });
        }



        [HttpPost]
        public ActionResult UploadProjectImage()
        {
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var projectID = System.Web.HttpContext.Current.Request["ProjectID"];
                var imageFile = System.Web.HttpContext.Current.Request.Files["HelpSectionImages"];
                if (imageFile != null)
                {
                    if (imageFile.ContentLength > 1048575) //1MB = 1 * 1024 * 1024 - 1 = 1048575
                    {
                        return Json(new { rel = -2 }); //File is too large.
                    }
                    string imageFileName = System.IO.Path.GetFileName(imageFile.FileName);
                    string pathSaveImage = System.IO.Path.Combine(
                                           Server.MapPath("~/Content/Images/ProjectImage"), imageFileName);
                    try
                    {
                        // file is uploaded
                        imageFile.SaveAs(pathSaveImage);
                    }
                    catch
                    {
                        return Json(new { rel = -1 }); //Upload Error
                    }
                    //Save to Database
                    //using (MemoryStream ms = new MemoryStream())
                    //{
                    //    imageFile.InputStream.CopyTo(ms);
                    //    byte[] array = ms.GetBuffer();
                    //}

                    ProjectImage projectImage = new ProjectImage();
                    projectImage.ImageUrl = "/Content/Images/ProjectImage/" + imageFileName;
                    projectImage.idProject = long.Parse(projectID);
                    projectImage.DateCreate = DateTime.Now;

                    var ck = AddProjectImage(projectImage);
                    if (ck)
                        return Json(new { rel = 1 });
                    //return RedirectToAction("actionname", "controller name");
                }
            }
            // after successfully uploading redirect the user
            //return RedirectToAction("actionname", "controller name");

            return Json(new { rel = 0 });
        }

        public bool AddProjectImage(ProjectImage projectImage)
        {
            if (projectImage == null)
                return false;
            try
            {
                _projectImagesServices.AddProjectImage(projectImage);
                return true;
            }
            catch { }
            return false;
        }





        public bool AddBanner(Banner banner)
        {
            if(banner == null)
                return false;
            try
            {
                _bannerServices.AddBanner(banner);
                return true;
            }
            catch {}
            return false;
        }

        public ActionResult ContactManage(int Page = 1)
        {
            int pageSize = 20;
            int totalRows = 0;
            var listProject = _contactServices.GetAllContact(pageSize, Page, out totalRows);
            Paging p = new Paging();
            p.Page = Page;
            p.Url = "/Admin/ContactManage/?Page=";
            p.Node = ClsCommon.SetPage(Page, totalRows, pageSize);
            ViewBag.Paging = p;
            return View(listProject);
        }

        public JsonResult DeleteContact(int id)
        {
            try
            {
                var c = _contactServices.DeleteContact(id);
                if (c)
                    return Json(new { rel = 1 });
            }
            catch { }
            return Json(new { rel = 0 });

        }

        public ActionResult ProjectManage(int Page = 1)
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login");

            int pageSize = 6;
            int totalRows = 0;
            var listProject = _projectServices.GetAllProject(pageSize, Page, out totalRows);
            Paging p = new Paging();
            p.Page = Page;
            p.Url = "/Admin/ProjectManage/?Page=";
            p.Node = ClsCommon.SetPage(Page, totalRows, pageSize);
            ViewBag.Paging = p;
            return View(listProject);
        }


        public ActionResult AddProject()
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login");

            return View();
        }



        public ActionResult NewsManage(int Page = 1)
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login");

            int pageSize = 10;
            int totalRows = 0;
            var listNews = _newsServices.GetAllNews(pageSize, Page, out totalRows);
            Paging p = new Paging();
            p.Page = Page;
            p.Url = "/Admin/NewsManage/?Page=";
            p.Node = ClsCommon.SetPage(Page, totalRows, pageSize);
            ViewBag.Paging = p;
            return View(listNews);
        }


        public ActionResult AddNews()
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login");

            return View();
        }



        [HttpPost]
        public ActionResult UploadThunbNews()
        {
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var imageFile = System.Web.HttpContext.Current.Request.Files["HelpSectionImages"];
                if (imageFile != null)
                {
                    if (imageFile.ContentLength > 512000) //1MB = 1 * 1024 * 1024 - 1 = 1048575
                    {
                        return Json(new { rel = -2 }); //File is too large.
                    }
                    string imageFileName = System.IO.Path.GetFileName(imageFile.FileName);
                    string pathSaveImage = System.IO.Path.Combine(
                                           Server.MapPath("~/Content/Images/ThumbNews"), imageFileName);
                    try
                    {
                        // file is uploaded
                        imageFile.SaveAs(pathSaveImage);
                        return Json(new { rel = 1, urlThumbNews = "/Content/Images/ThumbNews/" + imageFileName});
                        }
                    catch
                    {
                        return Json(new { rel = -1 }); //Upload Error
                    }               
                }
            }
            // after successfully uploading redirect the user
            //return RedirectToAction("actionname", "controller name");

            return Json(new { rel = 0 });
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddNews(string NewsTitle, string NewsType, string ShortDescription, string Thumb, string NewsContent)
        {
            NewsContent = ClsCommon.ValidateRss(NewsContent);

            int idNewsCategory = 0;
            int.TryParse(NewsType, out idNewsCategory);

            News news = new News();
            news.NewsTitle = NewsTitle;
            news.idNewsCategory = idNewsCategory;
            news.Thumb = Thumb;
            news.IsShow = true;
            news.ShortDescription = ShortDescription;
            news.NewsContent = NewsContent;
            news.DateCreated = DateTime.Now;
            news.UserCreated = 1;
            try
            {
                var ck = _newsServices.AddNews(news);
                if (ck)
                    return Json(new { rel = 1 });
            }
            catch { }

            return Json(new { rel = 0 });

        }



        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditNews(long NewsID, string NewsTitle, string NewsType, string ShortDescription, string NewsContent)
        {
            NewsContent = ClsCommon.ValidateRss(NewsContent);

            int idNewsCategory = 0;
            int.TryParse(NewsType, out idNewsCategory);

            News news = new News();
            news.id = NewsID;
            news.NewsTitle = NewsTitle;
            news.idNewsCategory = idNewsCategory;
            news.ShortDescription = ShortDescription;
            news.NewsContent = NewsContent;
            try
            {
                var ck = _newsServices.UpdateNews(news);
                if (ck)
                    return Json(new { rel = 1 });
            }
            catch { }

            return Json(new { rel = 0 });

        }








        [HttpPost]
        public ActionResult UploadThunbProject()
        {
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var imageFile = System.Web.HttpContext.Current.Request.Files["HelpSectionImages"];
                if (imageFile != null)
                {
                    if (imageFile.ContentLength > 512000) //1MB = 1 * 1024 * 1024 - 1 = 1048575
                    {
                        return Json(new { rel = -2 }); //File is too large.
                    }
                    string imageFileName = System.IO.Path.GetFileName(imageFile.FileName);
                    string pathSaveImage = System.IO.Path.Combine(
                                           Server.MapPath("~/Content/Images/ThumbProject"), imageFileName);
                    try
                    {
                        // file is uploaded
                        imageFile.SaveAs(pathSaveImage);
                        return Json(new { rel = 1, urlThumbProject = "/Content/Images/ThumbProject/" + imageFileName });
                    }
                    catch
                    {
                        return Json(new { rel = -1 }); //Upload Error
                    }
                }
            }
            // after successfully uploading redirect the user
            //return RedirectToAction("actionname", "controller name");

            return Json(new { rel = 0 });
        }




        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddProject(string ProjectName, int ProjectCategory, string ProjectAddress, int  ProjectType, string ProjectWidth,           
                                    string ProjectArea, string ProjectHeight, string ProjectDirection, string ProjectPrice, string ProjectJuridical,
                                    int IsShow, int IsHightLight, string ProjectContent, string UrlThumbProject
                                    )
        {

            ProjectContent = ClsCommon.ValidateRss(ProjectContent);

            Project project = new Project();
            project.idCategory = ProjectCategory;
            project.idType = ProjectType;
            project.IsHighlights = IsHightLight == 1;
            project.IsShow = IsShow == 1;
            project.Juridical = ProjectJuridical;
            project.NumberOfFloors = "";
            project.NumerOfBuilding = "";
            project.Price = ProjectPrice;
            project.ProjectName = ProjectName;
            project.Thumbnail = UrlThumbProject;
            project.Type = ""; //Loại hình
            project.UserCreated = 1;
            project.Acreage = ProjectArea;
            project.Address = ProjectAddress;
            project.DateCreated = DateTime.Now;
            project.Description = ProjectContent;
            project.Direction = ProjectDirection;

            try
            {
                var ck = _projectServices.AddProject(project);
                if (ck)
                    return Json(new { rel = 1 });
            }
            catch { }

            return Json(new { rel = 0 });

        }



        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditProject(long ProjectID, string ProjectName, int ProjectCategory, string ProjectAddress, string ProjectWidth,
                                   string ProjectArea, string ProjectHeight, string ProjectDirection, string ProjectPrice,
                                   string ProjectJuridical, string ProjectContent)
        {
            ProjectContent = ClsCommon.ValidateRss(ProjectContent);

            Project project = new Project();
            project.idCategory = ProjectCategory;
            project.Juridical = ProjectJuridical;
            project.Price = ProjectPrice;
            project.ProjectName = ProjectName;
            project.Acreage = ProjectArea;
            project.Address = ProjectAddress;
            project.Description = ProjectContent;
            project.Direction = ProjectDirection;
            project.Width = ProjectWidth;
            project.Height = ProjectHeight;
            project.id = ProjectID;


            try
            {
                var ck = _projectServices.UpdateProject(project);
                if (ck)
                    return Json(new { rel = 1 });
            }
            catch { }

            return Json(new { rel = 0 });

        }



        public JsonResult DeleteNews(int id)
        {
            try
            {
                var c = _newsServices.DeleteNews(id);
                if (c)
                    return Json(new { rel = 1 });
            }
            catch { }
            return Json(new { rel = 0 });

        }


        public ActionResult NewsCategoryManage()
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login");

            var listNewsCategory = _newsCategoryServices.GetListNewsCategory();
             return View(listNewsCategory);
        }

        public JsonResult AddNewsCategory(string vNewsCategoryName)
        {
            var ck = _newsCategoryServices.GetListNewsCategoryByName(vNewsCategoryName);
            if(ck.Count > 0)
                return Json(new { rel = 2 }); //Name is Exist

            NewsCategory newsCategory = new NewsCategory();
            newsCategory.NewsCategoryName = vNewsCategoryName;
            newsCategory.DateCreated = DateTime.Now;
            newsCategory.UserCreated = 1;

            bool b = _newsCategoryServices.AddNewsCategory(newsCategory);
            if(b)
                return Json(new { rel = 1 }); //OK
            return Json(new { rel = 0 }); //Error
        }



        public JsonResult DeleteNewsCategory(int id)
        {
            try
            {
                var listNews = _newsServices.GetListNewsByNewsCategory(id);
                if (listNews.Count <= 0)
                {
                    var c = _newsCategoryServices.DeleteNewsCategory(id);
                    if (c)
                        return Json(new { rel = 1 });
                }
                return Json(new { rel = 2 });
            }
            catch { }
            return Json(new { rel = 0 });

        }

        public JsonResult DeleteProject(int id)
        {
            try
            {
                var c = _projectServices.DeleteProject(id);
                if (c)
                    return Json(new { rel = 1 });
            }
            catch { }
            return Json(new { rel = 0 });

        }


        public JsonResult UpdateSaled(string ids, string vals)
        {
            bool val = false;
            if (vals == "1")
                val = true;
            try
            {
                Project project = new Project();
                project.id = int.Parse(ids);
                project.IsSaled = val;
                project.DateUpdated = DateTime.Now;
                var c = _projectServices.UpdateSaled(project);
                if (c)
                    return Json(new { rel = 1 });
            }
            catch { }
            return Json(new { rel = 0 });

        }


        public JsonResult UpdateHightLight(string ids, string vals)
        {
            bool val = false;
            if (vals == "1")
                val = true;
            try
            {
                Project project = new Project();
                project.id = int.Parse(ids);
                project.IsHighlights = val;
                project.DateUpdated = DateTime.Now;
                var c = _projectServices.UpdateHightLight(project);
                if (c)
                    return Json(new { rel = 1 });
            }
            catch { }
            return Json(new { rel = 0 });

        }

        public JsonResult UpdateShow(string ids, string vals)
        {
            bool val = false;
            if (vals == "1")
                val = true;
            try
            {
                Project project = new Project();
                project.id = int.Parse(ids);
                project.IsShow = val;
                project.DateUpdated = DateTime.Now;
                var c = _projectServices.UpdateShow(project);
                if (c)
                    return Json(new { rel = 1 });
            }
            catch { }
            return Json(new { rel = 0 });

        }



        public ActionResult EditProject(string ProjectID)
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login");

            if(ProjectID == "" || ProjectID == null)
                return RedirectToAction("ProjectManage");


            long projectID = 0;
            try
            {
                projectID = long.Parse(ProjectID);
            }
            catch { }

            ProjectModels project = _projectServices.GetProjectDetail(projectID);
            if(project == null)
                return RedirectToAction("ProjectManage");

            return View(project);
        }





        public ActionResult EditNews(string NewsID)
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login");

            if (NewsID == "" || NewsID == null)
                return RedirectToAction("NewsManage");


            long newsID = 0;
            try
            {
                newsID = long.Parse(NewsID);
            }
            catch { }

            NewsModels news = _newsServices.GetNewsDetail(newsID);
            if (news == null)
                return RedirectToAction("NewsManage");

            return View(news);
        }



        public ActionResult ProjectImages(string ProjectID)
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login");

            if (ProjectID == "" || ProjectID == null)
                return RedirectToAction("NewsManage");


            long projectID = 0;
            try
            {
                projectID = long.Parse(ProjectID);
            }
            catch { }

            List<ProjectImage> projectImages = _projectImagesServices.GetListProjectImage(projectID);
            if (projectImages == null)
                return RedirectToAction("NewsManage");

            ViewBag.ProjectID = ProjectID;
            return View(projectImages);
        }


        public JsonResult DeleteProjectImage(long id)
        {
            try
            {
                var c = _projectImagesServices.DeleteProjectImage(id);
                if (c)
                    return Json(new { rel = 1 });
            }
            catch { }
            return Json(new { rel = 0 });

        }





        public ActionResult ImportImages()
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login");

            return View();
        }

        [HttpPost]
        public ActionResult ImportImages(IEnumerable<HttpPostedFileBase> FileImages)
        {
            if (FileImages != null && FileImages.Count() > 0)
            {
                Random r = new Random();
                foreach (HttpPostedFileBase file in FileImages)
                {
                    if (file != null)
                    {

                        Stream stream = file.InputStream;
                        int rd = r.Next(1000000, 9999999);
                        var InputFileName = DateTime.Now.Ticks.ToString() + "_" + rd.ToString() + "_" + Path.GetFileName(file.FileName);

                        using (var image = System.Drawing.Image.FromStream(stream))
                        {
                            int newWidth = 400; // New Width of Image in Pixel  
                            int newHeight = 300; // New Height of Image in Pixel  
                            var thumbImg = new Bitmap(newWidth, newHeight);
                            var thumbGraph = Graphics.FromImage(thumbImg);
                            thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                            thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                            thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            var imgRectangle = new Rectangle(0, 0, newWidth, newHeight);
                            thumbGraph.DrawImage(image, imgRectangle);

                            var ServerSavePath = Path.Combine(Server.MapPath("~/Content/Images/ImagesUpload/") + InputFileName);

                            thumbImg.Save(ServerSavePath, image.RawFormat);

                            ImagesUpload imagesUpload = new ImagesUpload();
                            imagesUpload.FileName = InputFileName;
                            imagesUpload.Url = "/Content/Images/ImagesUpload/" + InputFileName;
                            imagesUpload.DateCreated = DateTime.Now;
                            imagesUpload.UserCreated = 1;

                            _imagesUploadServices.AddImagesUpload(imagesUpload);

                        }
                        ViewBag.UploadStatus = FileImages.Count().ToString() + " đã được tải lên.";
                    }

                }
            }
            else
            {
                ViewBag.UploadStatus = "Không có hình ảnh nào được chọn.";
            }    

            return View();
        }


        [HttpPost]
        public ActionResult UploadImportImages()
        {
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var imageFile = System.Web.HttpContext.Current.Request.Files; //.Files["HelpSectionImages"];
                //if (imageFile != null)
                //{
                //    if (imageFile.ContentLength > 512000) //1MB = 1 * 1024 * 1024 - 1 = 1048575
                //    {
                //        return Json(new { rel = -2 }); //File is too large.
                //    }
                //    string imageFileName = System.IO.Path.GetFileName(imageFile.FileName);
                //    string pathSaveImage = System.IO.Path.Combine(
                //                           Server.MapPath("~/Content/Images/ThumbProject"), imageFileName);
                //    try
                //    {
                //        // file is uploaded
                //        imageFile.SaveAs(pathSaveImage);
                //        return Json(new { rel = 1, urlThumbProject = "/Content/Images/ThumbProject/" + imageFileName });
                //    }
                //    catch
                //    {
                //        return Json(new { rel = -1 }); //Upload Error
                //    }
                //}
            }
            // after successfully uploading redirect the user
            //return RedirectToAction("actionname", "controller name");

            return Json(new { rel = 0 });
        }




        public ActionResult EditContentAbout()
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login");


            var config = _configWebServices.GetListConfigWebByType("");
            if (config == null || config.Count == 0)
                return RedirectToAction("WebConfig");

            return View(config);
        }



        public ActionResult AddImagesProject(string ProjectID)
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login");

            if (ProjectID == "" || ProjectID == null)
                return RedirectToAction("ProjectManage");

            ViewBag.ProjectID = ProjectID;
            return View();
        }





        [HttpPost]
        public ActionResult AddImagesProject(string ProjectID, IEnumerable<HttpPostedFileBase> FileImages)
        {
            if (FileImages != null && FileImages.Count() > 0)
            {
                Random r = new Random();
                foreach (HttpPostedFileBase file in FileImages)
                {
                    if (file != null)
                    {

                        Stream stream = file.InputStream;
                        int rd = r.Next(1000000, 9999999);
                        var InputFileName = DateTime.Now.Ticks.ToString() + "_" + rd.ToString() + "_" + Path.GetFileName(file.FileName);

                        string pathSaveImage = System.IO.Path.Combine(
                                               Server.MapPath("~/Content/Images/ProjectImage"), InputFileName);
                        try
                        {
                            // file is uploaded
                            file.SaveAs(pathSaveImage);

                            ProjectImage projectImage = new ProjectImage();
                            projectImage.ImageUrl = "/Content/Images/ProjectImage/" + InputFileName;
                            projectImage.idProject = long.Parse(ProjectID);
                            projectImage.DateCreate = DateTime.Now;

                            var ck = AddProjectImage(projectImage);

                        }
                        catch
                        {
                            
                        }
                    }

                }
            }
            else
            {
                ViewBag.UploadStatus = "Không có hình ảnh nào được chọn.";
            }

            return RedirectToAction("ProjectImages", new { ProjectID = ProjectID});
        }
















    }
}