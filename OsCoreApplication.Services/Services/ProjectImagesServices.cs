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
    public partial class ProjectImagesServices : IProjectImages
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICached _cached;
        public ProjectImagesServices(IUnitOfWork unitOfWork, ICached cached)
        {
            _unitOfWork = unitOfWork;
            _cached = cached;
        }
        public List<ProjectImage> GetListProjectImage(long ProjectID)
        {
            try
            {
                var lstData = _cached.Get<List<ProjectImage>>(KeyCache.Key_Project_Image);
                if (lstData != null) return lstData;
                lstData = _unitOfWork.ProjectImagesRepository.Filter(c => c.idProject == ProjectID).ToList();
                _cached.Set(CacheMode.Add, KeyCache.Key_Project_Image, lstData, new TimeSpan(1, 0, 0, 0));
                return lstData;
            }
            catch (Exception ex)
            {
                OsLog.Error("ProjectImagesServices --> GetListProjectImage ", ex);
                return new List<ProjectImage>();
            }
        }

        public bool DeleteProjectImage(long id)
        {
            ProjectImage _projectImage = new ProjectImage();
            _projectImage.id = id;
            try
            {
                ProjectImage projectImage = _unitOfWork.ProjectImagesRepository.FindById(_projectImage.id);
                if (projectImage == null)
                    return false;
                _unitOfWork.ProjectImagesRepository.Delete(projectImage);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch { }
            return false;
        }

        public bool AddProjectImage(ProjectImage projectImage)
        {
            if (projectImage == null)
                return false;
            try
            {
                _unitOfWork.ProjectImagesRepository.Insert(projectImage);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch { }
            return false;
        }

    }


    public partial class ProjectImagesServices
    {

    }


}
