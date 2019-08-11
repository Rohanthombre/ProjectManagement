using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectManagement.DataAccess;
using ProjectManagement.Models;
using ProjectManagement.Services.Contracts;

namespace ProjectManagement.Services
{
    public class UserService : IUserService
    {
        private ProjectManagementEntities _entities;

        public UserService(ProjectManagementEntities entities)
        {
            this._entities = entities;
        }

        public UserService()
        {
            this._entities = new ProjectManagementEntities();
        }
        public bool CreateUser(UserModel userModel)
        {
            var user = new User()
            {
                User_Id = userModel.UserId,
                Employee_Id = userModel.EmployeeId,
                First_Name = userModel.FirstName,
                Last_Name = userModel.LastName,
                Project_Id = userModel.ProjectId,
                Task_Id = userModel.TaskId
            };
            _entities.Users.Add(user);
            return _entities.SaveChanges() > 0;
        }

        public bool EditUser(UserModel userModel)
        {
            var user = _entities.Users.FirstOrDefault(e => e.User_Id == userModel.UserId);

            if (user != null)
            {
                user.Employee_Id = userModel.EmployeeId;
                user.First_Name = userModel.FirstName;
                user.Last_Name = userModel.LastName;
                user.Project_Id = userModel.ProjectId;
                user.Task_Id = userModel.TaskId;

                return _entities.SaveChanges() > 0;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteUser(int userId)
        {
            var user = _entities.Users.FirstOrDefault(e => e.User_Id == userId);

            if (user != null)
            {
                _entities.Users.Remove(user);
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


        public ICollection<UserModel> GetAllUsers()
        {
            var usersModel = new List<UserModel>();
            foreach (var entityUser in _entities.Users)
            {
                usersModel.Add(
                    new UserModel()
                    {
                        UserId = entityUser.User_Id,
                        EmployeeId = entityUser.Employee_Id,
                        FirstName = entityUser.First_Name,
                        LastName = entityUser.Last_Name,
                        TaskId = entityUser.Task_Id,
                        ProjectId = entityUser.Project_Id
                    });

            }
            return usersModel;
        }

        public UserModel GetUserById(int userId)
        {
            var userModel = new UserModel();
            var entityUser = _entities.Users.FirstOrDefault(e => e.User_Id == userId);
            userModel =
                new UserModel()
                {
                    UserId = entityUser.User_Id,
                    EmployeeId = entityUser.Employee_Id,
                    FirstName = entityUser.First_Name,
                    LastName = entityUser.Last_Name,
                    ProjectId = entityUser.Project_Id,
                    TaskId = entityUser.Task_Id
                };

            return userModel;
        }
    }
}
