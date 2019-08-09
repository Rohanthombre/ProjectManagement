USE [ProjectManagement]
GO
ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_User_Task1]
GO
ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_User_Project1]
GO
ALTER TABLE [dbo].[Task] DROP CONSTRAINT [FK_Task_Project2]
GO
/****** Object:  Table [dbo].[User]    Script Date: 8/10/2019 12:30:42 AM ******/
DROP TABLE [dbo].[User]
GO
/****** Object:  Table [dbo].[Task]    Script Date: 8/10/2019 12:30:42 AM ******/
DROP TABLE [dbo].[Task]
GO
/****** Object:  Table [dbo].[Project]    Script Date: 8/10/2019 12:30:42 AM ******/
DROP TABLE [dbo].[Project]
GO
/****** Object:  Table [dbo].[Project]    Script Date: 8/10/2019 12:30:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Project](
	[Project_Id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectName] [varchar](80) NULL,
	[Start_Date] [datetime] NULL,
	[End_Date] [datetime] NULL,
	[Priority] [int] NULL,
 CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED 
(
	[Project_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Task]    Script Date: 8/10/2019 12:30:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Task](
	[Task_Id] [int] IDENTITY(1,1) NOT NULL,
	[Parent_TaskId] [int] NULL,
	[Project_Id] [int] NULL,
	[TaskName] [varchar](80) NULL,
	[Start_Date] [datetime] NULL,
	[End_Date] [datetime] NULL,
	[Priority] [int] NULL,
	[Status] [varchar](10) NULL,
 CONSTRAINT [PK_Task] PRIMARY KEY CLUSTERED 
(
	[Task_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 8/10/2019 12:30:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[User_Id] [int] IDENTITY(1,1) NOT NULL,
	[First_Name] [varchar](50) NULL,
	[Last_Name] [varchar](50) NULL,
	[Employee_Id] [int] NULL,
	[Project_Id] [int] NULL,
	[Task_Id] [int] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[User_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FK_Task_Project2] FOREIGN KEY([Project_Id])
REFERENCES [dbo].[Project] ([Project_Id])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FK_Task_Project2]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Project1] FOREIGN KEY([Project_Id])
REFERENCES [dbo].[Project] ([Project_Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Project1]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Task1] FOREIGN KEY([Task_Id])
REFERENCES [dbo].[Task] ([Task_Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Task1]
GO
