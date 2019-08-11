
using ProjectManagement.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectManagement.Test.QualityTools
{
    public static class ProjectManagementFakeData
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

    public static class ProjectFakeData
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

    public static class UserFakeData
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
