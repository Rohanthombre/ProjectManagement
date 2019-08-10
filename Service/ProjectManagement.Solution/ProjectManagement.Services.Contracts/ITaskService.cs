using ProjectManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Services.Contracts
{
    public interface ITaskService : IDisposable
    {
        ICollection<TaskModel> GetAllTasks();

        TaskModel GetTaskById(int taskId);

        bool CreateTask(TaskModel taskModel);

        bool EditTask(TaskModel taskModel);

        bool DeleteTask(int taskId);

        bool EndTask(int taskId);
    }
}
