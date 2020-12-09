using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using OsCoreApplication.Common;
using OsCoreApplication.DataLayer.EFModel;
using OsCoreApplication.DataLayer.Infrastructure;
using OsCoreApplication.Libraries.Memcached;
using OsCoreApplication.Logger;
using OsCoreApplication.ViewModel.Models;

namespace OsCoreApplication.Services
{
    public partial class OsAccountServices : IOsAccount
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICached _cached;
        public OsAccountServices(IUnitOfWork unitOfWork, IMapper mapper, ICached cached)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cached = cached;
        }
        //public List<OsAccountModels> GetListAccount()
        //{
        //    try
        //    {
        //        var lstData = _cached.Get<List<OsAccountModels>>(KeyCache.Key_Account);
        //        if (lstData != null) return lstData;
        //        var data = _unitOfWork.AccountRepository.Filter(c => c.Id > 0).ToList();
        //        lstData = _mapper.Map<List<OS_Account>, List<OsAccountModels>>(data);
        //        _cached.Set(CacheMode.Add, KeyCache.Key_Account, lstData, new TimeSpan(1, 0, 0, 0));
        //        return lstData;
        //    }
        //    catch (Exception ex)
        //    {
        //        OsLog.Error("OsAccountServices --> GetListAccount ", ex);
        //        return new List<OsAccountModels>();
        //    }
        //}

        //public OsAccountModels GetUserAccountDetail(string user, string pass)
        //{
        //    try
        //    {
        //        var objUser = _unitOfWork.AccountRepository.Filter(c => c.Username == user && c.Password == pass && c.IsActive && !c.IsDeleted)
        //            .Select(c => new OsAccountModels
        //            {
        //                Firstname = c.Firstname,
        //                Avatar = c.Avatar,
        //                Username = c.Username,
        //                Id = c.Id,
        //                Email = c.Email,
        //                Lastname = c.Lastname,
        //            }).FirstOrDefault();
        //        return objUser;
        //    }
        //    catch (Exception ex)
        //    {
        //        OsLog.Error("OsAccountServices --> GetListAccount ", ex);
        //        return new OsAccountModels();
        //    }
        //}
    }

    public partial class OsAccountServices
    {

    }
}
