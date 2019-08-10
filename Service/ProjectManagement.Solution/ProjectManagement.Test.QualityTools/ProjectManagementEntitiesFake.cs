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
    public class ProjectManagementEntitiesFake 
    {
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

        public DbContext ProjectManagementEdContext { get; set; }


        public ProjectManagementEntitiesFake()
        {
            this.SetNSubstitute();
            this.SetMockDbContext();
        }

        private void SetNSubstitute()
        {
            IDbSet<Task> task = NSubstitute.Substitute.For<IDbSet<Task>, IQueryable<Task>>();
            //tasks.Provider = TasksData.AllTaks.Provider;
            task.Provider.Returns(TaskServiceFakeData.TaskData.AllTasks.Provider);
            task.Expression.Returns(TaskServiceFakeData.TaskData.AllTasks.Expression);
            task.ElementType.Returns(TaskServiceFakeData.TaskData.AllTasks.ElementType);
            task.GetEnumerator().Returns(TaskServiceFakeData.TaskData.AllTasks.GetEnumerator());

            this.Tasks = task;

            IDbSet<Project> project = NSubstitute.Substitute.For<IDbSet<Project>, IQueryable<Project>>();

            project.Provider.Returns(ProjectServiceFakeData.ProjectData.AllProjects.Provider);
            project.Expression.Returns(ProjectServiceFakeData.ProjectData.AllProjects.Expression);
            project.ElementType.Returns(ProjectServiceFakeData.ProjectData.AllProjects.ElementType);
            project.GetEnumerator().Returns(ProjectServiceFakeData.ProjectData.AllProjects.GetEnumerator());

            this.Projects = project;


            IDbSet<User> user = NSubstitute.Substitute.For<IDbSet<User>, IQueryable<User>>();
            //tasks.Provider = TasksData.AllTaks.Provider;
            user.Provider.Returns(UserServiceFakeData.UserData.AllUsers.Provider);
            user.Expression.Returns(UserServiceFakeData.UserData.AllUsers.Expression);
            user.ElementType.Returns(UserServiceFakeData.UserData.AllUsers.ElementType);
            user.GetEnumerator().Returns(UserServiceFakeData.UserData.AllUsers.GetEnumerator());

            this.Users = user;
        }

        private void SetMockDbContext()
        {
            var mockTask = new Mock<DbSet<Task>>();
            mockTask.As<IQueryable<Task>>().Setup(m => m.Provider).Returns(TaskServiceFakeData.TaskData.AllTasks.Provider);
            mockTask.As<IQueryable<Task>>().Setup(m => m.Expression).Returns(TaskServiceFakeData.TaskData.AllTasks.Expression);
            mockTask.As<IQueryable<Task>>().Setup(m => m.ElementType).Returns(TaskServiceFakeData.TaskData.AllTasks.ElementType);
            mockTask.As<IQueryable<Task>>().Setup(m => m.GetEnumerator()).Returns(TaskServiceFakeData.TaskData.AllTasks.GetEnumerator());

            var mockProject = new Mock<DbSet<Project>>();
            mockTask.As<IQueryable<Project>>().Setup(m => m.Provider).Returns(ProjectServiceFakeData.ProjectData.AllProjects.Provider);
            mockTask.As<IQueryable<Project>>().Setup(m => m.Expression).Returns(ProjectServiceFakeData.ProjectData.AllProjects.Expression);
            mockTask.As<IQueryable<Project>>().Setup(m => m.ElementType).Returns(ProjectServiceFakeData.ProjectData.AllProjects.ElementType);
            mockTask.As<IQueryable<Project>>().Setup(m => m.GetEnumerator()).Returns(ProjectServiceFakeData.ProjectData.AllProjects.GetEnumerator());

            var mockUser = new Mock<DbSet<User>>();
            mockTask.As<IQueryable<User>>().Setup(m => m.Provider).Returns(UserServiceFakeData.UserData.AllUsers.Provider);
            mockTask.As<IQueryable<User>>().Setup(m => m.Expression).Returns(UserServiceFakeData.UserData.AllUsers.Expression);
            mockTask.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(UserServiceFakeData.UserData.AllUsers.ElementType);
            mockTask.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(UserServiceFakeData.UserData.AllUsers.GetEnumerator());

            var mockContext = new Mock<ProjectManagementEntities>();
            mockContext.Setup(c => c.Tasks).Returns(mockTask.Object);
            mockContext.Setup(c => c.Projects).Returns(mockProject.Object);
            mockContext.Setup(c => c.Users).Returns(mockUser.Object);

            this.ProjectManagementEdContext = mockContext.Object;
        }


        public int SaveChanges()
        {
            if (ThrowErrorOnNextMethod)
                throw new Exception("Error");
            return 1;
        }

        public void Dispose()
        {
            Tasks = null;

            return;
        }

        public void SetModifield(object value)
        {
            return;
        }
    }

    public static class TaskServiceFakeData
    {
        public static class TaskData
        {
            public static Task Task1 = new Task()
            {
                Task_Id = 1,
                TaskName = "First Task",
                Start_Date = new DateTime(2018, 09, 05),
                End_Date = new DateTime(2018, 09, 05),
                Priority = 1,
                Parent_TaskId = null,
                Project_Id = 1
            };


            public static Task Task2 = new Task()
            {
                Task_Id = 2,
                TaskName = "Second Task",
                Start_Date = new DateTime(2018, 09, 05),
                End_Date = new DateTime(2018, 09, 05),
                Priority = 1,
                Parent_TaskId = 1,
                Project_Id = 1
            };

            public static Task Task3 = new Task()
            {
                Task_Id = 3,
                TaskName = "Third Task",
                Start_Date = new DateTime(2018, 09, 05),
                End_Date = new DateTime(2018, 09, 05),
                Priority = 1,
                Parent_TaskId = 1,
                Project_Id = 2

            };
            public static IQueryable<Task> AllTasks = new List<Task>() { Task1, Task2, Task3 }.AsQueryable();
        }

    }

    public static class ProjectServiceFakeData
    {
        public static class ProjectData
        {
            public static Project Project1 = new Project()
            {
                Project_Id = 1,
                ProjectName = "Project 1",
                Start_Date = new DateTime(2018, 09, 05),
                End_Date = new DateTime(2018, 09, 05),
                Priority = 5
            };

            public static Project Project2 = new Project()
            {
                Project_Id = 2,
                ProjectName = "Project 2",
                Start_Date = new DateTime(2018, 09, 15),
                End_Date = new DateTime(2018, 09, 30),
                Priority = 5
            };
            public static IQueryable<Project> AllProjects = new List<Project>() { Project1, Project2 }.AsQueryable();

        }
    }

    public static class UserServiceFakeData
    {
        public static class UserData
        {
            public static User User1 = new User()
            {
                User_Id = 1,
                Employee_Id = 100,
                First_Name = "Narendra",
                Last_Name = "Modi",
                Project_Id = 1,
                Task_Id = 2
            };

            public static User User2 = new User()
            {
                User_Id = 2,
                Employee_Id = 200,
                First_Name = "Amit",
                Last_Name = "Shah",
                Project_Id = 2,
                Task_Id = 3
            };
            public static IQueryable<User> AllUsers = new List<User>() { User1, User2 }.AsQueryable();

        }
    }
}
