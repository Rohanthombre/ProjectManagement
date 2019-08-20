using System;
using System.Net;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectManagement.Models;
using ProjectManagement.Services;
using ProjectManagement.Test.QualityTools;
using ProjectManagement.WebApi.Controllers;
using Unity;

namespace ProjectManagement.WebApi.Tests.Controllers
{
    [TestClass]
    public class ProjectControllerTest
    {
        private ProjectController Controller;
        private UnityContainer container;

        public ProjectControllerTest()
        {
            var context = new ProjectManagementEntitiesFake();
            var taskService = new ProjectService(context);

            Controller = new ProjectController(taskService);
        }

        [TestMethod]
        public void When_SearchForAllProject_Then_AllProjectsReceived()
        {
            // Arrange & Act
            var result = Controller.GetProjects();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void When_SearchForOneProject_Then_OneProjectReceived()
        {
            var taskId = 1;
            // Arrange & Act
            var result = Controller.GetProject(taskId);

            // Assert
            Assert.IsNotNull(result);
            //Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void When_SearchForProjectWhichIsNotAvailable_Then_NoProjectReceived()
        {
            var taskId = 5;
            // Arrange & Act
            var result = Controller.GetProject(taskId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));

        }

        [TestMethod]
        public void When_CreateProject_Then_ProjectIsCreated()
        {
            var taskModel = new ProjectModel()
            {
                ProjectId = 1,
                ProjectName = "New test project",
                StartDate = DateTime.Today.AddDays(-15),
                EndDate = DateTime.Today.AddDays(-15),
                Priority = 5,
                UserId = 1
            };

            // Arrange & Act
            var result = Controller.CreateProject(taskModel);
            var contentResult = result as NegotiatedContentResult<ProjectModel>;


            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.OK, contentResult.StatusCode);
            Assert.IsNotNull(contentResult.Content);
        }

        [TestMethod]
        public void When_EditProject_Then_ProjectIsEdited()
        {
            var projectModel = new ProjectModel()
            {
                ProjectId = 1,
                ProjectName = "Edited test project",
                StartDate = DateTime.Today.AddDays(-15),
                EndDate = DateTime.Today.AddDays(-15),
                Priority = 5,
                UserId = 1
            };

            // Arrange & Act
            var result = Controller.EditProject(projectModel);
            var contentResult = result as NegotiatedContentResult<ProjectModel>;


            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.OK, contentResult.StatusCode);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(projectModel.ProjectName, contentResult.Content.ProjectName);
        }

        [TestMethod]
        public void When_EditProjectAndProjectNotExists_Then_ProjectNotFound()
        {
            var projectModel = new ProjectModel()
            {
                ProjectId = 106,
                ProjectName = "Edited test project",
                StartDate = DateTime.Today.AddDays(-15),
                EndDate = DateTime.Today.AddDays(-15),
                Priority = 5
            };

            // Arrange & Act
            var result = Controller.EditProject(projectModel);
            var contentResult = result as NegotiatedContentResult<ProjectModel>;


            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.NotFound, contentResult.StatusCode);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(projectModel.ProjectName, contentResult.Content.ProjectName);

        }

        [TestMethod]
        public void When_DeleteProject_Then_ProjectIsDeleted()
        {
            var taskId = 1;

            // Arrange & Act
            var result = Controller.DeleteProject(taskId);
            var contentResult = result as NegotiatedContentResult<int>;


            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.OK, contentResult.StatusCode);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content);

        }
        [TestMethod]
        public void When_DeleteProject_And_ProjectNotExists_Then_ProjectNotFound()
        {
            var projectId = 106;

            // Arrange & Act
            var result = Controller.DeleteProject(projectId);
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
