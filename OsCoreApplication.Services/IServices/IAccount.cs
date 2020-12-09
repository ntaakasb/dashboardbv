using OsCoreApplication.DataLayer.EFModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsCoreApplication.Services
{
    public interface IAccount
    {
        Account GetUserAccountDetail(string user, string pass);
    }
}
