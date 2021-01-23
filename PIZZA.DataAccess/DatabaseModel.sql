

/****** Object:  Table [dbo].[ApplicationUser]    Script Date: 22.01.2021 17:46:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationUser](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[NormalizedUserName] [nvarchar](256) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](50) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[FirstName] [nvarchar](256) NULL,
	[LastName] [nvarchar](256) NULL,
	[Position] [int] NULL,
	[Address] [nvarchar](256) NULL,
	[PostalCode] [nvarchar](10) NULL,
	[Town] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[Employee]    Script Date: 22.01.2021 17:46:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Employee]
	AS SELECT [ID], [FirstName], [LastName], [Position], [Email] FROM [ApplicationUser]
GO
/****** Object:  Table [dbo].[File]    Script Date: 22.01.2021 17:46:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[File](
	[GUID] [varchar](36) NOT NULL,
	[Owner] [int] NULL,
	[FileName] [nvarchar](256) NOT NULL,
	[UploadDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[GUID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FileTask]    Script Date: 22.01.2021 17:46:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FileTask](
	[FileGUID] [varchar](36) NOT NULL,
	[TaskID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TaskID] ASC,
	[FileGUID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[FileWithTask]    Script Date: 22.01.2021 17:46:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[FileWithTask]
	AS SELECT * FROM [FileTask] INNER JOIN [File] ON [FileTask].[FileGUID] = [File].[GUID];
GO
/****** Object:  Table [dbo].[EmployeeTask]    Script Date: 22.01.2021 17:46:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeTask](
	[EmployeeID] [int] NOT NULL,
	[TaskID] [int] NOT NULL,
	[TaskRole] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TaskID] ASC,
	[EmployeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Task]    Script Date: 22.01.2021 17:46:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Task](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Deadline] [datetime] NULL,
	[Priority] [int] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaskState]    Script Date: 22.01.2021 17:46:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskState](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Task] [int] NOT NULL,
	[NewTaskState] [int] NOT NULL,
	[DateTime] [datetime] NOT NULL,
	[Editor] [int] NULL,
	[TaskNote] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[TaskWithActualStateAndCreator]    Script Date: 22.01.2021 17:46:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[TaskWithActualStateAndCreator]
	AS SELECT *,
		(SELECT TOP 1 [NewTaskState]
			FROM [TaskState]
			WHERE [Task] = [OTask].[ID])
			as TaskState,
		(SELECT TOP 1 [EmployeeID]
			FROM [EmployeeTask]
			WHERE [TaskID] = [OTask].[ID]
			AND [TaskRole] = 0)
			as TaskCreator
	FROM [Task] as OTask;
GO
/****** Object:  Table [dbo].[ApplicationRole]    Script Date: 22.01.2021 17:46:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationRole](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ApplicationUserRole]    Script Date: 22.01.2021 17:46:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationUserRole](
	[UserID] [int] NOT NULL,
	[RoleID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC,
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaskNote]    Script Date: 22.01.2021 17:46:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskNote](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Task] [int] NOT NULL,
	[Employee] [int] NULL,
	[Note] [varchar](max) NULL,
	[DateTime] [datetime] NOT NULL,
	[ResponseTo] [int] NULL,
	[Deleted] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [NormalizedNameIndex]    Script Date: 22.01.2021 17:46:50 ******/
CREATE UNIQUE NONCLUSTERED INDEX [NormalizedNameIndex] ON [dbo].[ApplicationRole]
(
	[NormalizedName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [NameIndex]    Script Date: 22.01.2021 17:46:50 ******/
CREATE NONCLUSTERED INDEX [NameIndex] ON [dbo].[ApplicationUser]
(
	[FirstName] ASC,
	[LastName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [NormalizedEmailIndex]    Script Date: 22.01.2021 17:46:50 ******/
CREATE UNIQUE NONCLUSTERED INDEX [NormalizedEmailIndex] ON [dbo].[ApplicationUser]
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [NormalizedUserNameInedx]    Script Date: 22.01.2021 17:46:50 ******/
CREATE UNIQUE NONCLUSTERED INDEX [NormalizedUserNameInedx] ON [dbo].[ApplicationUser]
(
	[NormalizedUserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [OwnerIndex]    Script Date: 22.01.2021 17:46:50 ******/
CREATE NONCLUSTERED INDEX [OwnerIndex] ON [dbo].[File]
(
	[Owner] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [NameIndex]    Script Date: 22.01.2021 17:46:50 ******/
CREATE NONCLUSTERED INDEX [NameIndex] ON [dbo].[Task]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ApplicationUserRole]  WITH CHECK ADD  CONSTRAINT [FK_ApplicationUserRole_ToApplicationRole] FOREIGN KEY([RoleID])
REFERENCES [dbo].[ApplicationRole] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ApplicationUserRole] CHECK CONSTRAINT [FK_ApplicationUserRole_ToApplicationRole]
GO
ALTER TABLE [dbo].[ApplicationUserRole]  WITH CHECK ADD  CONSTRAINT [FK_ApplicationUserRole_ToApplicationUser] FOREIGN KEY([UserID])
REFERENCES [dbo].[ApplicationUser] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ApplicationUserRole] CHECK CONSTRAINT [FK_ApplicationUserRole_ToApplicationUser]
GO
ALTER TABLE [dbo].[EmployeeTask]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeTask_ToEmployee] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[ApplicationUser] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[EmployeeTask] CHECK CONSTRAINT [FK_EmployeeTask_ToEmployee]
GO
ALTER TABLE [dbo].[EmployeeTask]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeTask_ToTask] FOREIGN KEY([TaskID])
REFERENCES [dbo].[Task] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[EmployeeTask] CHECK CONSTRAINT [FK_EmployeeTask_ToTask]
GO
ALTER TABLE [dbo].[File]  WITH CHECK ADD  CONSTRAINT [FK_File_ToOwner] FOREIGN KEY([Owner])
REFERENCES [dbo].[ApplicationUser] ([ID])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[File] CHECK CONSTRAINT [FK_File_ToOwner]
GO
ALTER TABLE [dbo].[FileTask]  WITH CHECK ADD  CONSTRAINT [FK_FileTask_ToFile] FOREIGN KEY([FileGUID])
REFERENCES [dbo].[File] ([GUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FileTask] CHECK CONSTRAINT [FK_FileTask_ToFile]
GO
ALTER TABLE [dbo].[FileTask]  WITH CHECK ADD  CONSTRAINT [FK_FileTask_ToTask] FOREIGN KEY([TaskID])
REFERENCES [dbo].[Task] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FileTask] CHECK CONSTRAINT [FK_FileTask_ToTask]
GO
ALTER TABLE [dbo].[TaskNote]  WITH CHECK ADD  CONSTRAINT [FK_TaskNote_ToEmployee] FOREIGN KEY([Employee])
REFERENCES [dbo].[ApplicationUser] ([ID])
GO
ALTER TABLE [dbo].[TaskNote] CHECK CONSTRAINT [FK_TaskNote_ToEmployee]
GO
ALTER TABLE [dbo].[TaskNote]  WITH CHECK ADD  CONSTRAINT [FK_TaskNote_ToTask] FOREIGN KEY([Task])
REFERENCES [dbo].[Task] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TaskNote] CHECK CONSTRAINT [FK_TaskNote_ToTask]
GO
ALTER TABLE [dbo].[TaskNote]  WITH CHECK ADD  CONSTRAINT [FK_TaskNote_ToTaskNote] FOREIGN KEY([ResponseTo])
REFERENCES [dbo].[TaskNote] ([ID])
GO
ALTER TABLE [dbo].[TaskNote] CHECK CONSTRAINT [FK_TaskNote_ToTaskNote]
GO
ALTER TABLE [dbo].[TaskState]  WITH CHECK ADD  CONSTRAINT [FK_TaskState_ToEmployee] FOREIGN KEY([Editor])
REFERENCES [dbo].[ApplicationUser] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TaskState] CHECK CONSTRAINT [FK_TaskState_ToEmployee]
GO
ALTER TABLE [dbo].[TaskState]  WITH CHECK ADD  CONSTRAINT [FK_TaskState_ToTask] FOREIGN KEY([Task])
REFERENCES [dbo].[Task] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TaskState] CHECK CONSTRAINT [FK_TaskState_ToTask]
GO
ALTER TABLE [dbo].[TaskState]  WITH CHECK ADD  CONSTRAINT [FK_TaskState_ToTaskNote] FOREIGN KEY([TaskNote])
REFERENCES [dbo].[TaskNote] ([ID])
GO
ALTER TABLE [dbo].[TaskState] CHECK CONSTRAINT [FK_TaskState_ToTaskNote]
GO
/****** Object:  StoredProcedure [dbo].[CreateTask]    Script Date: 22.01.2021 17:46:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CreateTask](@Creator int, @Deadline datetime null, @Priority int, @Name nvarchar(256), @Description nvarchar(MAX), @Note varchar(MAX))
AS
BEGIN
	DECLARE @TaskID as INT
	INSERT INTO Task(Deadline, Priority, Name, Description)
		VALUES (@Deadline, @Priority, @Name, @Description);
	SET @TaskID = SCOPE_IDENTITY();
	UPDATE TaskState SET Editor = @Creator WHERE ID = @TaskID;
	UPDATE TaskNote SET Employee = @Creator, Note = @Note WHERE ID = @TaskID;
END
GO
/****** Object:  Trigger [dbo].[Trigger_AddFirstTaskState]    Script Date: 22.01.2021 17:46:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [dbo].[Trigger_AddFirstTaskState]
    ON [dbo].[Task]
    AFTER INSERT
    AS
    BEGIN
        INSERT INTO TaskState
            (Task, NewTaskState, DateTime, Editor, TaskNote) 
            VALUES
            ((SELECT ID FROM inserted), 0, GETDATE(), null, null);
        SET NoCount ON
    END
GO
ALTER TABLE [dbo].[Task] ENABLE TRIGGER [Trigger_AddFirstTaskState]
GO
/****** Object:  Trigger [dbo].[Trigger_AddNoteToTask]    Script Date: 22.01.2021 17:46:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [dbo].[Trigger_AddNoteToTask]
    ON [dbo].[TaskState]
    AFTER INSERT
    AS
    BEGIN
        INSERT INTO TaskNote
            (Task, Employee, Note, DateTime, ResponseTo, Deleted) 
            VALUES
            ((SELECT Task FROM inserted), null, null, (SELECT DateTime FROM inserted), null, 0);
        SET NoCount ON
    END
GO
ALTER TABLE [dbo].[TaskState] ENABLE TRIGGER [Trigger_AddNoteToTask]
GO
ALTER DATABASE CURRENT SET READ_WRITE 
GO
