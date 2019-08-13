using System;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectManagement.Models;
using ProjectManagement.Services;
using ProjectManagement.Test.QualityTools;
using ProjectManagement.WebApi.Controllers;
using Unity;
using System.Net;

namespace ProjectManagement.WebApi.Tests.Controllers
{
    [TestClass]
    public class UserControllerTest
    {
        private UserController Controller;
        private UnityContainer container;

        public UserControllerTest()
        {
            var context = new ProjectManagementEntitiesFake();
            var taskService = new UserService(context);

            Controller = new UserController(taskService);
        }

        [TestMethod]
        public void When_SearchForAllUser_Then_AllUsersReceived()
        {
            // Arrange & Act
            var result = Controller.GetUsers();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void When_SearchForOneUser_Then_OneUserReceived()
        {
            var taskId = 1;
            // Arrange & Act
            var result = Controller.GetUser(taskId);

            // Assert
            Assert.IsNotNull(result);
            //Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void When_SearchForUserWhichIsNotAvailable_Then_NoUserReceived()
        {
            var taskId = 5;
            // Arrange & Act
            var result = Controller.GetUser(taskId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));

        }

        [TestMethod]
        public void When_CreateUser_Then_UserIsCreated()
        {
            var userModel = new UserModel()
            {
                UserId = 3,
                EmployeeId = 500,
                FirstName ="Test user",
                LastName = "Test user",
                ProjectId = 1,
                TaskId = 1,
            };

            // Arrange & Act
            var result = Controller.CreateUser(userModel);
            var contentResult = result as NegotiatedContentResult<UserModel>;


            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.OK, contentResult.StatusCode);
            Assert.IsNotNull(contentResult.Content);
        }

        [TestMethod]
        public void When_CreateUser_WithoutProjectAndTask_Then_UserIsCreated()
        {
            var userModel = new UserModel()
            {
                UserId = 3,
                EmployeeId = 500,
                FirstName = "Test user",
                LastName = "Test user",
                ProjectId = null,
                TaskId = null,
            };

            // Arrange & Act
            var result = Controller.CreateUser(userModel);
            var contentResult = result as NegotiatedContentResult<UserModel>;


            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.OK, contentResult.StatusCode);
            Assert.IsNotNull(contentResult.Content);
        }

        [TestMethod]
        public void When_EditUser_Then_UserIsEdited()
        {
            var userModel = new UserModel()
            {
                UserId = 1,
                EmployeeId = 500,
                FirstName = "Edit Test user",
                LastName = "Test user",
                ProjectId = 1,
                TaskId = 1,
            };

            // Arrange & Act
            var result = Controller.EditUser(userModel);
            var contentResult = result as NegotiatedContentResult<UserModel>;


            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.OK, contentResult.StatusCode);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(userModel.FirstName, contentResult.Content.FirstName);
        }

        [TestMethod]
        public void When_EditUser_WithoutProjectAndTask_Then_UserIsEdited()
        {
            var userModel = new UserModel()
            {
                UserId = 1,
                EmployeeId = 500,
                FirstName = "Edit Test user",
                LastName = "Test user",
                ProjectId = null,
                TaskId = null,
            };

            // Arrange & Act
            var result = Controller.EditUser(userModel);
            var contentResult = result as NegotiatedContentResult<UserModel>;


            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.OK, contentResult.StatusCode);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(userModel.FirstName, contentResult.Content.FirstName);
        }

        [TestMethod]
        public void When_EditUserAndUserNotExists_Then_UserNotFound()
        {
            var userModel = new UserModel()
            {
                UserId = 106,
                EmployeeId = 500,
                FirstName = "Edit Test user",
                LastName = "Test user",
                ProjectId = null,
                TaskId = null,
            };

            // Arrange & Act
            var result = Controller.EditUser(userModel);
            var contentResult = result as NegotiatedContentResult<UserModel>;


            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.NotFound, contentResult.StatusCode);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(userModel.FirstName, contentResult.Content.FirstName);
        }

        [TestMethod]
        public void When_DeleteUser_Then_UserIsDeleted()
        {
            var taskId = 1;

            // Arrange & Act
            var result = Controller.DeleteUser(taskId);
            var contentResult = result as NegotiatedContentResult<int>;


            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.OK, contentResult.StatusCode);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content);

        }
        [TestMethod]
        public void When_DeleteUser_And_UserNotExists_Then_UserNotFound()
        {
            var userId = 106;

            // Arrange & Act
            var result = Controller.DeleteUser(userId);
            var contentResult = result as NegotiatedContentResult<int>;


            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.NotFound, contentResult.StatusCode);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(106, contentResult.Content);

        }
    }
}
