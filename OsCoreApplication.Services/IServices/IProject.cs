using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OsCoreApplication.DataLayer.EFModel;
using OsCoreApplication.ViewModel.Models;


namespace OsCoreApplication.Services
{
    public interface IProject
    {
        List<ProjectModels> GetListProjectHightlights(int qty);
        List<ProjectModels> GetListProjectRandom(int qty);
        List<ProjectModels> GetAllProject(int pageSize, int pageNumber, out int totalRows);
        List<ProjectModels> GetListProjectByType(int type, int pageSize, int pageNumber, out int totalRows);
        ProjectModels GetProjectDetail(long id);
        bool AddProject(Project project);
        bool DeleteProject(int id);
        bool UpdateSaled(Project c);
        bool UpdateHightLight(Project c);
        bool UpdateShow(Project c);
        bool UpdateProject(Project c);

    }
}
