using Moq;
using NSubstitute;
using ProjectManagement.DataAccess;
using ProjectManagement.DataAccess.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;


namespace ProjectManagement.Test.QualityTools
{
    public class ProjectManagementEntitiesFake : IRepositoryDbContext
    {
        public ProjectManagementEntitiesFake()
        {
            this.SetNSubstitute();
            //this.SetMockDbContext();
        }

        public bool ThrowErrorOnNextMethod { get; set; }

        private IDbSet<Task> tasks;

        public IDbSet<Task> Tasks
        {
            get { return tasks; }
            set { tasks = value; }
        }

        private IDbSet<Project> projects;

        public IDbSet<Project> Projects
        {
            get { return projects; }
            set { projects = value; }
        }

        private IDbSet<User> users;

        public IDbSet<User> Users
        {
            get { return users; }
            set { users = value; }
        }

        public IRepositoryDbContext ProjectManagementEdContext { get; set; }

        

        

        private void SetNSubstitute()
        {
            IDbSet<Task> task = NSubstitute.Substitute.For<IDbSet<Task>, IQueryable<Task>>();
            //tasks.Provider = TasksData.AllTaks.Provider;
            task.Provider.Returns(ProjectManagementFakeData.TaskData.AllTasks.Provider);
            task.Expression.Returns(ProjectManagementFakeData.TaskData.AllTasks.Expression);
            task.ElementType.Returns(ProjectManagementFakeData.TaskData.AllTasks.ElementType);
            task.GetEnumerator().Returns(ProjectManagementFakeData.TaskData.AllTasks.GetEnumerator());

            this.Tasks = task;

            IDbSet<Project> project = NSubstitute.Substitute.For<IDbSet<Project>, IQueryable<Project>>();

            project.Provider.Returns(ProjectFakeData.ProjectData.AllProjects.Provider);
            project.Expression.Returns(ProjectFakeData.ProjectData.AllProjects.Expression);
            project.ElementType.Returns(ProjectFakeData.ProjectData.AllProjects.ElementType);
            project.GetEnumerator().Returns(ProjectFakeData.ProjectData.AllProjects.GetEnumerator());

            this.Projects = project;


            IDbSet<User> user = NSubstitute.Substitute.For<IDbSet<User>, IQueryable<User>>();
            //tasks.Provider = TasksData.AllTaks.Provider;
            user.Provider.Returns(UserFakeData.UserData.AllUsers.Provider);
            user.Expression.Returns(UserFakeData.UserData.AllUsers.Expression);
            user.ElementType.Returns(UserFakeData.UserData.AllUsers.ElementType);
            user.GetEnumerator().Returns(UserFakeData.UserData.AllUsers.GetEnumerator());

            this.Users = user;

        }

        //private void SetMockDbContext()
        //{
        //    var mockTask = new Mock<DbSet<Task>>();
        //    mockTask.As<IQueryable<Task>>().Setup(m => m.Provider).Returns(TaskServiceFakeData.TaskData.AllTasks.Provider);
        //    mockTask.As<IQueryable<Task>>().Setup(m => m.Expression).Returns(TaskServiceFakeData.TaskData.AllTasks.Expression);
        //    mockTask.As<IQueryable<Task>>().Setup(m => m.ElementType).Returns(TaskServiceFakeData.TaskData.AllTasks.ElementType);
        //    mockTask.As<IQueryable<Task>>().Setup(m => m.GetEnumerator()).Returns(TaskServiceFakeData.TaskData.AllTasks.GetEnumerator());

        //    var mockProject = new Mock<DbSet<Project>>();
        //    mockTask.As<IQueryable<Project>>().Setup(m => m.Provider).Returns(ProjectServiceFakeData.ProjectData.AllProjects.Provider);
        //    mockTask.As<IQueryable<Project>>().Setup(m => m.Expression).Returns(ProjectServiceFakeData.ProjectData.AllProjects.Expression);
        //    mockTask.As<IQueryable<Project>>().Setup(m => m.ElementType).Returns(ProjectServiceFakeData.ProjectData.AllProjects.ElementType);
        //    mockTask.As<IQueryable<Project>>().Setup(m => m.GetEnumerator()).Returns(ProjectServiceFakeData.ProjectData.AllProjects.GetEnumerator());

        //    var mockUser = new Mock<DbSet<User>>();
        //    mockTask.As<IQueryable<User>>().Setup(m => m.Provider).Returns(UserServiceFakeData.UserData.AllUsers.Provider);
        //    mockTask.As<IQueryable<User>>().Setup(m => m.Expression).Returns(UserServiceFakeData.UserData.AllUsers.Expression);
        //    mockTask.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(UserServiceFakeData.UserData.AllUsers.ElementType);
        //    mockTask.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(UserServiceFakeData.UserData.AllUsers.GetEnumerator());

        //    var mockContext = new Mock<ProjectManagementEntities>();
        //    mockContext.Setup(c => c.Tasks).Returns(mockTask.Object);
        //    mockContext.Setup(c => c.Projects).Returns(mockProject.Object);
        //    mockContext.Setup(c => c.Users).Returns(mockUser.Object);

        //    this.ProjectManagementEdContext = mockContext.Object;
        //}

        public int SaveChanges()
        {
            if (ThrowErrorOnNextMethod)
                throw new Exception("Error");
            return 1;
        }

        public void Dispose()
        {
            Tasks = null;
            Projects = null;
            Users = null;
            return;
        }

        public void SetModifield(object value)
        {
            return;
        }
    }

    
}
