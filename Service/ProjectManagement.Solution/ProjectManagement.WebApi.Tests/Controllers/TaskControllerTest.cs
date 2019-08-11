using System;
//using NUnit.Framework;
using ProjectManagement.DataAccess.Contracts;
using ProjectManagement.Services;
using ProjectManagement.Services.Contracts;
using ProjectManagement.Test.QualityTools;
using ProjectManagement.WebApi.Controllers;
using Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;


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
    }
}
