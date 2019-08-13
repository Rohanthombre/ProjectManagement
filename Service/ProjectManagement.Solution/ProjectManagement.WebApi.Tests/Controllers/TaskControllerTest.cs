using System;
//using NUnit.Framework;
using ProjectManagement.DataAccess.Contracts;
using ProjectManagement.Services;
using ProjectManagement.Services.Contracts;
using ProjectManagement.Test.QualityTools;
using ProjectManagement.WebApi.Controllers;
using Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Http.Results;
using ProjectManagement.Models;
using System.Net;

namespace ProjectManagement.WebApi.Tests.Controllers
{
    [TestClass]
    public class TaskControllerTest
    {
        private TaskController Controller;
        private UnityContainer container;

        public TaskControllerTest()
        {
            var context = new ProjectManagementEntitiesFake();

            //container = new UnityContainer();
            //container.RegisterType<IRepositoryDbContext, ProjectManagementEntitiesFake>();
            //container.RegisterType<ITaskService, TaskService>();
            //var taskService = container.Resolve<ITaskService>();

            var taskService = new TaskService(context);

            Controller = new TaskController(taskService);
        }

        [TestMethod]
        public void When_SearchForAllTask_Then_AllTasksReceived()
        {
            // Arrange & Act
            var result = Controller.GetTasks();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void When_SearchForOneTask_Then_OneTaskReceived()
        {
            var taskId = 1;
            // Arrange & Act
            var result = Controller.GetTask(taskId);

            // Assert
            Assert.IsNotNull(result);
            //Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void When_SearchForTaskWhichIsNotAvailable_Then_NoTaskReceived()
        {
            var taskId = 5;
            // Arrange & Act
            var result = Controller.GetTask(taskId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));

        }

        [TestMethod]
        public void When_CreateTaskWithoutParentTask_Then_TaskIsCreated()
        {
            var taskModel = new TaskModel()
            {
                TaskName = "New Task",
                ParentTask = null,
                StartDate = DateTime.Today.AddDays(-15),
                EndDate = DateTime.Today.AddDays(-15),
                Priority = 5,
                Status = "Active"
            };

            // Arrange & Act
            var result = Controller.CreateTask(taskModel);
            var contentResult = result as NegotiatedContentResult<TaskModel>;


            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.OK, contentResult.StatusCode);
            Assert.IsNotNull(contentResult.Content);
        }

        [TestMethod]
        public void When_CreateTaskWithParentTask_Then_TaskIsCreated()
        {
            var taskModel = new TaskModel()
            {
                TaskName = "New Task",
                ParentTask = new ParentTask() { ParentTaskId = 1 },
                StartDate = DateTime.Today.AddDays(-15),
                EndDate = DateTime.Today.AddDays(-15),
                Priority = 5,
                Status = "Active"
            };

            // Arrange & Act
            var result = Controller.CreateTask(taskModel);

            var contentResult = result as NegotiatedContentResult<TaskModel>;


            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.OK, contentResult.StatusCode);
            Assert.IsNotNull(contentResult.Content);
        }

        [TestMethod]
        public void When_EditTaskWithoutParentTask_Then_TaskIsCreated()
        {
            var taskModel = new TaskModel()
            {
                TaskId = 2,
                TaskName = "New Task",
                ParentTask = null,
                StartDate = DateTime.Today.AddDays(-15),
                EndDate = DateTime.Today.AddDays(-15),
                Priority = 5,
                Status = "Active"
            };

            // Arrange & Act
            var result = Controller.EditTask(taskModel);
            var contentResult = result as NegotiatedContentResult<TaskModel>;


            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.OK, contentResult.StatusCode);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(2, contentResult.Content.TaskId);
            Assert.IsNull(contentResult.Content.ParentTask);
        }

        [TestMethod]
        public void When_EditTaskWithParentTask_Then_TaskIsCreated()
        {
            var taskModel = new TaskModel()
            {
                TaskId = 2,
                TaskName = "New Task",
                ParentTask = new ParentTask() { ParentTaskId = 1 },
                StartDate = DateTime.Today.AddDays(-15),
                EndDate = DateTime.Today.AddDays(-15),
                Priority = 5,
                Status = "Active"
            };

            // Arrange & Act
            var result = Controller.EditTask(taskModel);
            var contentResult = result as NegotiatedContentResult<TaskModel>;


            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.OK, contentResult.StatusCode);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(2, contentResult.Content.TaskId);

        }

        [TestMethod]
        public void When_EditTaskAndTaskNotExists_Then_TaskNotFound()
        {
            var taskModel = new TaskModel()
            {
                TaskId = 106,
                TaskName = "New Task",
                ParentTask = new ParentTask() { ParentTaskId = 1 },
                StartDate = DateTime.Today.AddDays(-15),
                EndDate = DateTime.Today.AddDays(-15),
                Priority = 5,
                Status = "Active"
            };

            // Arrange & Act
            var result = Controller.EditTask(taskModel);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));

        }

        [TestMethod]
        public void When_EndTask_Then_TaskIsEnded()
        {
            var taskId = 1;

            // Arrange & Act
            var result = Controller.EndTask(taskId);
            var contentResult = result as NegotiatedContentResult<int>;


            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.OK, contentResult.StatusCode);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content);

        }

        [TestMethod]
        public void When_EndTask_And_TaskNotExists_Then_TaskNotFound()
        {
            var taskId = 106;

            // Arrange & Act
            var result = Controller.EndTask(taskId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));

        }

        [TestMethod]
        public void When_DeleteTask_Then_TaskIsDeleted()
        {
            var taskId = 1;

            // Arrange & Act
            var result = Controller.DeleteTask(taskId);
            var contentResult = result as NegotiatedContentResult<int>;


            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.OK, contentResult.StatusCode);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content);

        }
        [TestMethod]
        public void When_DeleteTask_And_TaskNotExists_Then_TaskNotFound()
        {
            var taskId = 106;

            // Arrange & Act
            var result = Controller.DeleteTask(taskId);
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
