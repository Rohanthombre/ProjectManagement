using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? EmployeeId { get; set; }

        public int? ProjectId { get; set; }

        public int? TaskId { get; set; }
    }
}
