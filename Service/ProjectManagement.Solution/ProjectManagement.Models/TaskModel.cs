using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Models
{
    public class TaskModel
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public ParentTask ParentTask { get; set; }
        public ProjectModel Project { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Priority { get; set; }
        public string Status { get; set; }
        public UserModel User { get; set; }

    }

    public class ParentTask
    {
        public int? ParentTaskId { get; set; }
        public string ParentTaskName { get; set; }
    }
}
