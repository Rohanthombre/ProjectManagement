using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectManagement.DataAccess;
using ProjectManagement.DataAccess.Contracts;
using ProjectManagement.Models;
using ProjectManagement.Services.Contracts;

namespace ProjectManagement.Services
{
    public class ProjectService : IProjectService
    {
        private IRepositoryDbContext _entities;

        public ProjectService(IRepositoryDbContext entities)
        {
            this._entities = entities;
        }

        public ProjectService()
        {
            this._entities = new ProjectManagementEntities();
        }

        public ICollection<ProjectModel> GetAllProjects()
        {
            var projectsModel = new List<ProjectModel>();
            foreach (var entityProject in _entities.Projects)
            {
                projectsModel.Add(
                    new ProjectModel()
                    {
                        ProjectId = entityProject.Project_Id,
                        ProjectName = entityProject.ProjectName,
                        NoOfTasks = entityProject.Tasks.Count,
                        NoOfClosedTasks = entityProject.Tasks.Where(e => e.Status == "Closed").Count(),
                        StartDate = entityProject.Start_Date,
                        EndDate = entityProject.End_Date,
                        Priority = entityProject.Priority
                    }); ;



            }
            return projectsModel;
        }

        public ProjectModel GetProjectById(int projectId)
        {
            var projectModel = new ProjectModel();
            var entityProject = _entities.Projects.FirstOrDefault(e => e.Project_Id == projectId);
            if (entityProject != null)
            {
                projectModel =
                new ProjectModel()
                {
                    ProjectId = entityProject.Project_Id,
                    ProjectName = entityProject.ProjectName,
                    StartDate = entityProject.Start_Date,
                    EndDate = entityProject.End_Date,
                    Priority = entityProject.Priority
                };

                return projectModel;
            }
            return null;
        }


        public bool CreateProject(ProjectModel projectModel)
        {

            var user = _entities.Users.FirstOrDefault(e => e.User_Id == projectModel.UserId);
            if (user != null)
            {
                var project = new Project()
                {
                    Project_Id = projectModel.ProjectId,
                    ProjectName = projectModel.ProjectName,
                    Start_Date = projectModel.StartDate,
                    End_Date = projectModel.EndDate,
                    Priority = projectModel.Priority
                };
                _entities.Projects.Add(project);

                if (_entities.SaveChanges() > 0)
                {
                    user.Project_Id = project.Project_Id;
                    return _entities.SaveChanges() > 0;
                }
            }
            return false;
        }

        public bool EditProject(ProjectModel projectModel)
        {
            var project = _entities.Projects.FirstOrDefault(e => e.Project_Id == projectModel.ProjectId);
            var user = _entities.Users.FirstOrDefault(e => e.User_Id == projectModel.UserId);

            if (project != null && user != null)
            {
                project.ProjectName = projectModel.ProjectName;
                project.Start_Date = projectModel.StartDate;
                project.End_Date = projectModel.EndDate;
                project.Priority = projectModel.Priority;

                if (_entities.SaveChanges() > 0)
                {
                    user.Project_Id = project.Project_Id;
                    return _entities.SaveChanges() > 0;
                }
            }
            return false;
        }

        public bool DeleteProject(int projectId)
        {
            var project = _entities.Projects.FirstOrDefault(e => e.Project_Id == projectId);

            if (project != null)
            {
                _entities.Projects.Remove(project);
                return _entities.SaveChanges() > 0;
            }
            else
            {
                return false;
            }
        }

        public void Dispose()
        {
            _entities.Dispose();
        }



    }
}
