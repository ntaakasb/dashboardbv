using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using OsCoreApplication.Common;
using OsCoreApplication.DataLayer.EFModel;
using OsCoreApplication.DataLayer.Infrastructure;
using OsCoreApplication.Libraries.Memcached;
using OsCoreApplication.Logger;
using OsCoreApplication.ViewModel.Models;



namespace OsCoreApplication.Services
{
    public class NewsServices : Inews
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICached _cached;
        public NewsServices(IUnitOfWork unitOfWork, IMapper mapper, ICached cached)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cached = cached;
        }

        public List<NewsModels> GetAllNews(int pageSize, int pageNumber, out int totalRows)
        {
            totalRows = 0;
            try
            {
                var lstData = _cached.Get<List<NewsModels>>(KeyCache.Key_News);
                if (lstData != null) return lstData;
                totalRows = _unitOfWork.NewsRepository.GetAllData().Count();

                //Data News
                var dataNews = _unitOfWork.NewsRepository.GetAllData().OrderByDescending(p => p.id).Skip(pageSize * (pageNumber - 1)).Take(pageSize);
                //var listDataNews = _mapper.Map<List<News>, List<NewsModels>>(dataNews);

                //Data News Category
                var dataNewsCategory = _unitOfWork.NewsCategoryRepository.GetAllData();
                //var listDataNewsCategory = _mapper.Map<List<NewsCategory>, List<NewsCategoryModels>>(dataNewsCategory);

                //Join News and News Category
                var q = from n in dataNews
                        join c in dataNewsCategory on n.idNewsCategory equals c.id into x
                        from k in x.DefaultIfEmpty()
                        select new NewsModels
                        {
                            id = n.id,
                            NewsTitle = n.NewsTitle,
                            ShortDescription = n.ShortDescription,
                            Thumb = n.Thumb,
                            DateCreated = n.DateCreated,
                            idNewsCategory = n.idNewsCategory,
                            NewsCategoryName = k.NewsCategoryName
                        };

                lstData = q.ToList();

                _cached.Set(CacheMode.Add, KeyCache.Key_News, lstData, new TimeSpan(1, 0, 0, 0));
                return lstData;
            }
            catch (Exception ex)
            {
                OsLog.Error("NewsServices --> GetAllNews ", ex);
                return new List<NewsModels>();
            }
        }



        public List<NewsModels> GetNewsRandom(int qty)
        {
    
            try
            {
                var lstData = _cached.Get<List<NewsModels>>(KeyCache.Key_News);
                if (lstData != null) return lstData;
        
                //Data News
                var dataNews = _unitOfWork.NewsRepository.GetAllData().OrderBy(r => Guid.NewGuid()).Take(qty);
                //var listDataNews = _mapper.Map<List<News>, List<NewsModels>>(dataNews);

                //Data News Category
                var dataNewsCategory = _unitOfWork.NewsCategoryRepository.GetAllData();
                //var listDataNewsCategory = _mapper.Map<List<NewsCategory>, List<NewsCategoryModels>>(dataNewsCategory);

                //Join News and News Category
                var q = from n in dataNews
                        join c in dataNewsCategory on n.idNewsCategory equals c.id into x
                        from k in x.DefaultIfEmpty()
                        select new NewsModels
                        {
                            id = n.id,
                            NewsTitle = n.NewsTitle,
                            ShortDescription = n.ShortDescription,
                            Thumb = n.Thumb,
                            DateCreated = n.DateCreated,
                            idNewsCategory = n.idNewsCategory,
                            NewsCategoryName = k.NewsCategoryName
                        };

                lstData = q.ToList();

                _cached.Set(CacheMode.Add, KeyCache.Key_News, lstData, new TimeSpan(1, 0, 0, 0));
                return lstData;
            }
            catch (Exception ex)
            {
                OsLog.Error("NewsServices --> GetAllNews ", ex);
                return new List<NewsModels>();
            }
        }


        public NewsModels GetNewsDetail(long id)
        {
            try
            {
                var project = _unitOfWork.NewsRepository.Filter(c => c.id == id);
                //Data News Category
                var dataNewsCategory = _unitOfWork.NewsCategoryRepository.GetAllData();

                var q = (from a in project
                        join b in dataNewsCategory on a.idNewsCategory equals b.id into x
                        from k in x.DefaultIfEmpty()
                        select new NewsModels
                        {
                            id = a.id,
                            NewsTitle = a.NewsTitle,
                            ShortDescription = a.ShortDescription,
                            Thumb = a.Thumb,
                            DateCreated = a.DateCreated,
                            NewsContent = a.NewsContent,
                            idNewsCategory = a.idNewsCategory,
                            NewsCategoryName = k.NewsCategoryName
                        }).FirstOrDefault();

                return q;
            }
            catch (Exception ex)
            {
                OsLog.Error("NewsServices --> GetNewsDetail ", ex);
                return new NewsModels();
            }
        }


        public List<News> GetListNewsByNewsCategory(int idNewsCategory)
        {
            try
            {
                var data = _unitOfWork.NewsRepository.Filter(c => c.idNewsCategory == idNewsCategory).ToList();
                return data;
            }
            catch (Exception ex)
            {
                OsLog.Error("NewsServices --> GetListNewsByNewsCategory ", ex);
                return new List<News>();
            }

        }

        public bool AddNews(News news)
        {
            if (news == null)
                return false;
            try
            {
                 _unitOfWork.NewsRepository.Insert(news);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch { }
            return false;
        }

        public bool DeleteNews(int id)
        {
            News _news = new News();
            _news.id = id;
            try
            {
                News news = _unitOfWork.NewsRepository.FindById(_news.id);
                if (news == null)
                    return false;
                _unitOfWork.NewsRepository.Delete(news);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch { }
            return false;
        }





        public bool UpdateNews(News c)
        {
            try
            {
                News news = _unitOfWork.NewsRepository.FindById(c.id);
                if (news == null)
                    return false;
                news.NewsTitle = c.NewsTitle;
                news.ShortDescription = c.ShortDescription;         
                news.idNewsCategory = c.idNewsCategory;
                news.NewsContent = c.NewsContent;
                news.DateUpdate = DateTime.Now;
                _unitOfWork.NewsRepository.Update(news);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;

        }


















    }


    

    public partial class BannerServices
    {

    }


}
