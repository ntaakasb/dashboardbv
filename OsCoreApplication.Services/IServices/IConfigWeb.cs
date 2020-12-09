using OsCoreApplication.DataLayer.EFModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OsCoreApplication.ViewModel.Models;

namespace OsCoreApplication.Services
{
    public interface IConfigWeb
    {
        List<ConfigWebModels> GetListConfigWebByType(string type);
        bool UpdadeConfig(ConfigWeb c);
    }
}
