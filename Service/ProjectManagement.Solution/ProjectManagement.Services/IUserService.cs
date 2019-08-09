using ProjectManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Services
{
    public interface IUserService : IDisposable
    {
        ICollection<UserModel> GetAllUsers();

        UserModel GetUserById(int userId);

        bool CreateUser(UserModel userModel);

        bool EditUser(UserModel userModel);

        bool DeleteUser(int userId);

    }
}
