using ProjectManagement.DataAccess;
using ProjectManagement.Models;
using ProjectManagement.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ProjectManagement.Services
{
    public class TaskService : ITaskService
    {
        private ProjectManagementEntities _entities;

        //public TaskServices(TaskManagerEntities entities)
        //{
        //    this._entities = entities;
        //}

        public TaskService()
        {
            this._entities = new ProjectManagementEntities();
        }

        public bool CreateTask(TaskModel taskModel)
        {
            var task = new Task()
            {
                Parent_TaskId = taskModel.ParentTask.ParentTaskId,
                Project_Id = taskModel.Project.ProjectId,
                TaskName = taskModel.TaskName,
                Start_Date = taskModel.StartDate,
                End_Date = taskModel.EndDate,
                Priority = taskModel.Priority,
                Status = taskModel.Status
            };
            _entities.Tasks.Add(task);
            return _entities.SaveChanges() > 0;
        }
        public bool EditTask(TaskModel taskModel)
        {
            var task = _entities.Tasks.FirstOrDefault(e => e.Task_Id == taskModel.TaskId);

            if (task != null)
            {
                task.Parent_TaskId = taskModel.ParentTask.ParentTaskId;
                task.Project_Id = taskModel.Project.ProjectId;
                task.TaskName = taskModel.TaskName;
                task.Start_Date = taskModel.StartDate;
                task.End_Date = taskModel.EndDate;
                task.Priority = taskModel.Priority;
                task.Status = taskModel.Status;

                return _entities.SaveChanges() > 0;
            }
            else
            {
                return false;
            }
        }
        public bool DeleteTask(int taskId)
        {
            var task = _entities.Tasks.FirstOrDefault(e => e.Task_Id == taskId);

            if (task != null)
            {
                _entities.Tasks.Remove(task);
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

        public ICollection<TaskModel> GetAllTasks()
        {
            var tasksModel = new List<TaskModel>();
            foreach (var entityTask in _entities.Tasks)
            {
                var parentTask = _entities.Tasks.FirstOrDefault(e => e.Task_Id == entityTask.Parent_TaskId);

                //var project = _entities.Projects.FirstOrDefault(e => e.Project_Id== entityTask.Project_Id);

                tasksModel.Add(
                    new TaskModel()
                    {
                        TaskId = entityTask.Task_Id,
                        TaskName = entityTask.TaskName,

                        ParentTask = parentTask != null ? new ParentTask()
                        {
                            ParentTaskId = parentTask.Task_Id,
                            ParentTaskName = parentTask.TaskName
                        } : null,

                        Project = entityTask.Project != null ? new ProjectModel()
                        {
                            ProjectId = entityTask.Project.Project_Id,
                            ProjectName = entityTask.Project.ProjectName,
                            StartDate = entityTask.Project.Start_Date,
                            EndDate = entityTask.Project.End_Date,
                            Priority = entityTask.Project.Priority
                        } : null,
                        StartDate = entityTask.Start_Date,
                        EndDate = entityTask.End_Date,
                        Priority = entityTask.Priority,
                        Status = entityTask.Status
                    });

            }
            return tasksModel;
        }

        public TaskModel GetTaskById(int id)
        {
            var taskModel = new TaskModel();

            var output = _entities.Tasks.FirstOrDefault(e => e.Task_Id == id);
            var parentTask = _entities.Tasks.FirstOrDefault(e => e.Task_Id == output.Parent_TaskId);

            taskModel =
                new TaskModel()
                {
                    TaskId = output.Task_Id,
                    TaskName = output.TaskName,
                    ParentTask = parentTask != null ? new ParentTask()
                    {
                        ParentTaskId = parentTask.Task_Id,
                        ParentTaskName = parentTask.TaskName
                    } : null,
                    Project = output.Project != null ? new ProjectModel()
                    {
                        ProjectId = output.Project.Project_Id,
                        ProjectName = output.Project.ProjectName,
                        StartDate = output.Project.Start_Date,
                        EndDate = output.Project.End_Date,
                        Priority = output.Project.Priority
                    } : null,
                    StartDate = output.Start_Date,
                    EndDate = output.End_Date,
                    Priority = output.Priority,
                    Status = output.Status

                };

            return taskModel;

        }

        public bool EndTask(int taskId)
        {
            var task = _entities.Tasks.FirstOrDefault(e => e.Task_Id == taskId);

            if (task != null)
            {
                task.End_Date = DateTime.Now;

                return _entities.SaveChanges() > 0;
            }
            else
            {
                return false;
            }
        }

    }

}
