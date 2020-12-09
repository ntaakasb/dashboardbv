using OsCoreApplication.DataLayer.EFModel;
using OsCoreApplication.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsCoreApplication.Services
{
    public interface Inews
    {
        List<NewsModels> GetAllNews(int pageSize, int pageNumber, out int totalRows);

        List<NewsModels> GetNewsRandom(int qty);
        List<News> GetListNewsByNewsCategory(int idNewsCategory);

        NewsModels GetNewsDetail(long id);
        bool AddNews(News news);
        bool DeleteNews(int id);

        bool UpdateNews(News c);

    }
}
