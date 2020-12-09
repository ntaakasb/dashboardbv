using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using OsCoreApplication.DataLayer.EFModel;
using OsCoreApplication.DataLayer.Infrastructure;
using OsCoreApplication.Libraries.Memcached;
using OsCoreApplication.Logger;
using OsCoreApplication.ViewModel.Models;

namespace OsCoreApplication.Services
{
    public partial class AccountServices :IAccount
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Account GetUserAccountDetail(string user, string pass)
        {
            try
            {
                var objUser = _unitOfWork.AccountRepository.Find(c => c.Username == user && c.Password == pass);      
                return objUser;
            }
            catch (Exception ex)
            {
                OsLog.Error("AccountServices --> GetUserAccountDetail ", ex);
                return new Account();
            }
        }
    }

    public partial class AccountServices
    {

    }
}
