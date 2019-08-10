using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.DataAccess.Contracts
{
    public interface IProjectManagementDbContext : IDisposable
    {
        IDbSet<Task> Tasks { get; set; }

        IDbSet<Project> Projects { get; set; }

        IDbSet<User> Users { get; set; }

        DbContext ProjectManagementEdContext { get; set; }

        int SaveChanges();
    

        //DbEntityEntry Entry(object value);

            void SetModifield(object value);
    }
}
