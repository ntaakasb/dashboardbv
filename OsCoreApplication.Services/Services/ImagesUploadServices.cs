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

namespace OsCoreApplication.Services
{
    public partial class ImagesUploadServices : IImagesUpload
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICached _cached;
        public ImagesUploadServices(IUnitOfWork unitOfWork, ICached cached)
        {
            _unitOfWork = unitOfWork;
            _cached = cached;
        }


        public List<ImagesUpload> GetListImageUpload(int qty)
        {
            try
            {
                var lstData = _cached.Get<List<ImagesUpload>>(KeyCache.Key_Images_Upload);
                if (lstData != null) return lstData;
                lstData = _unitOfWork.ImagesUploadRepository.GetAllData().Take(qty).OrderBy(o => o.id).ToList();
                _cached.Set(CacheMode.Add, KeyCache.Key_Images_Upload, lstData, new TimeSpan(1, 0, 0, 0));
                return lstData;
            }
            catch (Exception ex)
            {
                OsLog.Error("ImagesUploadServices --> GetListImageUpload ", ex);
                return new List<ImagesUpload>();
            }
        }



        public bool AddImagesUpload(ImagesUpload imagesUpload)
        {
            if (imagesUpload == null)
                return false;
            try
            {
                _unitOfWork.ImagesUploadRepository.Insert(imagesUpload);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch { }
            return false;
        }






    }



    public partial class ImagesUploadServices
    {


    }

}
