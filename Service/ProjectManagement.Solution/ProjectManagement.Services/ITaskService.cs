using ProjectManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Services
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
