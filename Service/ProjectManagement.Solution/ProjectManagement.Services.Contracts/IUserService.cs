using ProjectManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Services.Contracts
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
