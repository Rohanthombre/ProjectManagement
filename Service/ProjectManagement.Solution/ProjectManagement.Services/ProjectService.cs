using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectManagement.DataAccess;
using ProjectManagement.Models;

namespace ProjectManagement.Services
{
    public class ProjectService : IProjectService
    {
        private ProjectManagementEntities _entities;

        public ProjectService()
        {
            this._entities = new ProjectManagementEntities();
        }
        public bool CreateProject(ProjectModel projectModel)
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
            return _entities.SaveChanges() > 0;
        }

        public bool EditProject(ProjectModel projectModel)
        {
            var project = _entities.Projects.FirstOrDefault(e => e.Project_Id == projectModel.ProjectId);

            if (project != null)
            {
                project.ProjectName = projectModel.ProjectName;
                project.Start_Date = projectModel.StartDate;
                project.End_Date = projectModel.EndDate;
                project.Priority = projectModel.Priority;

                return _entities.SaveChanges() > 0;
            }
            else
            {
                return false;
            }
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
                        StartDate = entityProject.Start_Date,
                        EndDate = entityProject.End_Date,
                        Priority = entityProject.Priority
                    });

            }
            return projectsModel;
        }

        public ProjectModel GetProjectById(int projectId)
        {
            var projectModel = new ProjectModel();
            var entityProject = _entities.Projects.FirstOrDefault(e => e.Project_Id == projectId);
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
    }
}
