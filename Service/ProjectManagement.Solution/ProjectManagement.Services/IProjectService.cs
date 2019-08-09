using ProjectManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Services
{
    public interface IProjectService : IDisposable
    {
        ICollection<ProjectModel> GetAllProjects();

        ProjectModel GetProjectById(int projectId);

        bool CreateProject(ProjectModel projectModel);

        bool EditProject(ProjectModel projectModel);

        bool DeleteProject(int projectId);


    }
}
