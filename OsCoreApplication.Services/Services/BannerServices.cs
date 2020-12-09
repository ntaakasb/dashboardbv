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
    public partial class BannerServices : IBanner
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICached _cached;
        public BannerServices(IUnitOfWork unitOfWork, IMapper mapper, ICached cached)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cached = cached;
        }
        public List<BannerModels> GetListBanner()
        {
            try
            {
                var lstData = _cached.Get<List<BannerModels>>(KeyCache.Key_Banner);
                if (lstData != null) return lstData;
                var data = _unitOfWork.BannerRepository.Filter(c => c.id > 0).ToList();
                lstData = _mapper.Map<List<Banner>, List<BannerModels>>(data);
                _cached.Set(CacheMode.Add, KeyCache.Key_Banner, lstData, new TimeSpan(1, 0, 0, 0));
                return lstData;
            }
            catch (Exception ex)
            {
                OsLog.Error("BannerServices --> GetListBanner ", ex);
                return new List<BannerModels>();
            }
        }

        public bool DeleteBanner(int id)
        {
            Banner _banner = new Banner();
            _banner.id = id;
            try
            {
                Banner banner = _unitOfWork.BannerRepository.FindById(_banner.id);
                if (banner == null)
                    return false;
                _unitOfWork.BannerRepository.Delete(banner);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch {}
            return false;
        }

        public bool AddBanner(Banner banner)
        {
            if (banner == null)
                return false;
            try
            {
                _unitOfWork.BannerRepository.Insert(banner);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch { }
            return false;
        }   
    }
    public partial class BannerServices
    {

    }

}
