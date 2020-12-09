using OsCoreApplication.DataLayer.EFModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsCoreApplication.Services
{
    public interface INewsCategory
    {
        List<NewsCategory> GetListNewsCategory();
        List<NewsCategory> GetListNewsCategoryByName(string newsCategoryName);
        bool AddNewsCategory(NewsCategory newsCategory);
        bool DeleteNewsCategory(int id);
    }
}
