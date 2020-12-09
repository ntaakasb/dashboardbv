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
    public partial class NewsCategoryServices : INewsCategory
    {
        private readonly IUnitOfWork _unitOfWork;
        public NewsCategoryServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<NewsCategory> GetListNewsCategory()
        {
            try
            {
                var data = _unitOfWork.NewsCategoryRepository.GetAllData().ToList();
                return data;
            }
            catch (Exception ex)
            {
                OsLog.Error("NewsCategoryService --> GetListNewsCategory ", ex);
                return new List<NewsCategory>();
            }
        }

        public List<NewsCategory> GetListNewsCategoryByName(string newsCategoryName)
        {
            try
            {
                var data = _unitOfWork.NewsCategoryRepository.Filter(c => c.NewsCategoryName == newsCategoryName).ToList();
                return data;
            }
            catch (Exception ex)
            {
                OsLog.Error("NewsCategoryService --> GetListNewsCategoryByName ", ex);
                return new List<NewsCategory>();
            }
        }


    


        public bool AddNewsCategory(NewsCategory newsCategory)
        {
            if (newsCategory == null)
                return false;
            try
            {
                _unitOfWork.NewsCategoryRepository.Insert(newsCategory);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch { }
            return false;
        }


        public bool DeleteNewsCategory(int id)
        {
            NewsCategory _newsCategory = new NewsCategory();
            _newsCategory.id = id;
            try
            {
                NewsCategory newsCategory  = _unitOfWork.NewsCategoryRepository.FindById(_newsCategory.id);
                if (newsCategory == null)
                    return false;
                _unitOfWork.NewsCategoryRepository.Delete(newsCategory);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch (Exception ex){ }
            return false;
        }




    }

    public partial class NewsCategoryServices
    {

    }

}




