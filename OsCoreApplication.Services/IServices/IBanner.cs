using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OsCoreApplication.DataLayer.EFModel;
using OsCoreApplication.ViewModel.Models;

namespace OsCoreApplication.Services
{
    public interface IBanner
    {
        List<BannerModels> GetListBanner();
        bool DeleteBanner(int id);
        bool AddBanner(Banner banner);
    }
}
