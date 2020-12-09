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
    public partial class ProjectServices:IProject
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICached _cached;
        public ProjectServices(IUnitOfWork unitOfWork, IMapper mapper, ICached cached)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cached = cached;
        }

        public List<ProjectModels>GetAllProject(int pageSize, int pageNumber, out int totalRows)
        {
            totalRows = 0;
            try
            {
                var lstData = _cached.Get<List<ProjectModels>>(KeyCache.Key_Project);
                if (lstData != null) return lstData;
                totalRows = _unitOfWork.ProjectRepository.GetAllData().Count();
                var data = _unitOfWork.ProjectRepository.GetAllData().OrderByDescending(p => p.id).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
                lstData = _mapper.Map<List<Project>, List<ProjectModels>>(data);
                _cached.Set(CacheMode.Add, KeyCache.Key_Project, lstData, new TimeSpan(1, 0, 0, 0));
                return lstData;
            }
            catch (Exception ex)
            {
                OsLog.Error("ProjectServices --> GetAllProject ", ex);
                return new List<ProjectModels>();
            }
        }

        public List<ProjectModels> GetListProjectByType(int type, int pageSize, int pageNumber, out int totalRows)
        {
            totalRows = 0;
            try
            {
                var lstData = _cached.Get<List<ProjectModels>>(KeyCache.Key_Project);
                if (lstData != null) return lstData;
                totalRows = _unitOfWork.ProjectRepository.Filter(c => c.idCategory == type).Count();
                var data = _unitOfWork.ProjectRepository.Filter(c => c.idCategory == type).OrderByDescending(p => p.id).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
                lstData = _mapper.Map<List<Project>, List<ProjectModels>>(data);
                _cached.Set(CacheMode.Add, KeyCache.Key_Project, lstData, new TimeSpan(1, 0, 0, 0));
                return lstData;
            }
            catch (Exception ex)
            {
                OsLog.Error("ProjectServices --> GetAllProject ", ex);
                return new List<ProjectModels>();
            }
        }

       
        public ProjectModels GetProjectDetail(long id)
        {
            try
            {
                var project = _unitOfWork.ProjectRepository.FindById(id);
                var objProject = _mapper.Map<Project, ProjectModels>(project);

                return objProject;
            }
            catch (Exception ex)
            {
                OsLog.Error("OsAccountServices --> GetListAccount ", ex);
                return new ProjectModels();
            }
        }

        public List<ProjectModels> GetListProjectHightlights(int qty)
        {
            try
            {
                var lstData = _cached.Get<List<ProjectModels>>(KeyCache.Key_Project);
                if (lstData != null) return lstData;
                var data = _unitOfWork.ProjectRepository.Filter(c => c.IsHighlights == true && !(c.IsSaled.ToString().ToUpper() == "TRUE")).Take(qty).ToList();
                lstData = _mapper.Map<List<Project>, List<ProjectModels>>(data);
                _cached.Set(CacheMode.Add, KeyCache.Key_Project, lstData, new TimeSpan(1, 0, 0, 0));
                return lstData;
            }
            catch (Exception ex)
            {
                OsLog.Error("ProjectServices --> GetListProjectHightlights ", ex);
                return new List<ProjectModels>();
            }
        }

        public List<ProjectModels> GetListProjectRandom(int qty)
        {
            try
            {
                var lstData = _cached.Get<List<ProjectModels>>(KeyCache.Key_Project);
                if (lstData != null) return lstData;
                var data = _unitOfWork.ProjectRepository.Filter(c => c.IsShow == true).OrderBy(r => Guid.NewGuid()).Take(qty).ToList();
                lstData = _mapper.Map<List<Project>, List<ProjectModels>>(data);
                _cached.Set(CacheMode.Add, KeyCache.Key_Project, lstData, new TimeSpan(1, 0, 0, 0));
                return lstData;
            }
            catch (Exception ex)
            {
                OsLog.Error("ProjectServices --> GetListProjectRandom ", ex);
                return new List<ProjectModels>();
            }
        }

        public bool AddProject(Project project)
        {
            if (project == null)
                return false;
            try
            {
                _unitOfWork.ProjectRepository.Insert(project);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch { }
            return false;

        }

        public bool DeleteProject(int id)
        {
            Project _project = new Project();
            _project.id = id;
            try
            {
                Project project = _unitOfWork.ProjectRepository.FindById(_project.id);
                if (project == null)
                    return false;
                _unitOfWork.ProjectRepository.Delete(project);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch { }
            return false;
        }


        public bool UpdateSaled(Project c)
        {
            try
            {
                Project project = _unitOfWork.ProjectRepository.FindById(c.id);
                if (project == null)
                    return false;

                project.IsSaled = c.IsSaled;
                project.UserUpdated = c.UserUpdated;
                _unitOfWork.ProjectRepository.Update(project);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;
        }


        public bool UpdateHightLight(Project c)
        {
            try
            {
                Project project = _unitOfWork.ProjectRepository.FindById(c.id);
                if (project == null)
                    return false;

                project.IsHighlights = c.IsHighlights;
                project.UserUpdated = c.UserUpdated;
                _unitOfWork.ProjectRepository.Update(project);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;
        }


        public bool UpdateShow(Project c)
        {
            try
            {
                Project project = _unitOfWork.ProjectRepository.FindById(c.id);
                if (project == null)
                    return false;

                project.IsShow = c.IsShow;
                project.UserUpdated = c.UserUpdated;
                _unitOfWork.ProjectRepository.Update(project);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;
        }


        public bool UpdateProject(Project c)
        {
            try
            {
                Project project = _unitOfWork.ProjectRepository.FindById(c.id);
                if (project == null)
                    return false;
                project.idCategory = c.idCategory;
                project.Juridical = c.Juridical;
                project.Price = c.Price;
                project.ProjectName = c.ProjectName;
                project.Acreage = c.Acreage;
                project.Address = c.Address;
                project.Description = c.Description;
                project.Direction = c.Direction;
                project.DateUpdated = DateTime.Now;
                _unitOfWork.ProjectRepository.Update(project);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;

        }




    }

    public partial class ProjectServices
    {

    }


}
