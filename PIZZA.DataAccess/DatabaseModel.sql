/****** Object:  Table [dbo].[ApplicationUser]    Script Date: 13.02.2021 17:41:15 ******/
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
	[ForcePasswordChangeWhileNextLogin] [bit] NOT NULL,
	[PhoneNumber] [nvarchar](50) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[FirstName] [nvarchar](256) NULL,
	[LastName] [nvarchar](256) NULL,
	[AddressLine] [nvarchar](256) NULL,
	[PostalCode] [nvarchar](10) NULL,
	[Town] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[NormalizedUserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[Employee]    Script Date: 13.02.2021 17:41:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Employee]
	AS SELECT [ID],
			  [UserName],
			  [Email],
			  [PhoneNumber],
			  [FirstName],
			  [LastName],
			  [AddressLine],
			  [PostalCode],
			  [Town]			  
			  FROM [ApplicationUser]
GO
/****** Object:  Table [dbo].[File]    Script Date: 13.02.2021 17:41:15 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FileTask]    Script Date: 13.02.2021 17:41:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FileTask](
	[File] [varchar](36) NOT NULL,
	[Task] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Task] ASC,
	[File] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[FileWithTask]    Script Date: 13.02.2021 17:41:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[FileWithTask]
	AS SELECT * FROM [FileTask] INNER JOIN [File] ON [FileTask].[File] = [File].[GUID];
GO
/****** Object:  Table [dbo].[EmployeeTask]    Script Date: 13.02.2021 17:41:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeTask](
	[Employee] [int] NOT NULL,
	[Task] [int] NOT NULL,
	[TaskRole] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Task] ASC,
	[Employee] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaskModel]    Script Date: 13.02.2021 17:41:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskModel](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Deadline] [datetime] NULL,
	[Priority] [int] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaskState]    Script Date: 13.02.2021 17:41:15 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedTableType [dbo].[SearchKeywordsList]    Script Date: 13.02.2021 17:41:15 ******/
CREATE TYPE [dbo].[SearchKeywordsList] AS TABLE(
	[keyword] [nvarchar](max) NULL
)
GO
/****** Object:  View [dbo].[TaskWithActualStateAndCreator]    Script Date: 13.02.2021 17:41:15 ******/
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
		(SELECT TOP 1 [Employee]
			FROM [EmployeeTask]
			WHERE [Task] = [OTask].[ID]
			AND [TaskRole] = 0)
			as TaskCreator
	FROM [TaskModel] as OTask;
GO
/****** Object:  Table [dbo].[ApplicationRole]    Script Date: 13.02.2021 17:41:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationRole](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[NormalizedName] [nvarchar](256) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[NormalizedName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ApplicationUserRole]    Script Date: 13.02.2021 17:41:15 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaskNote]    Script Date: 13.02.2021 17:41:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskNote](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Task] [int] NOT NULL,
	[Employee] [int] NULL,
	[Note] [nvarchar](max) NULL,
	[DateTime] [datetime] NOT NULL,
	[ResponseTo] [int] NULL,
	[IsDeleted] [bit] NOT NULL DEFAULT 0,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [NormalizedNameIndex]    Script Date: 13.02.2021 17:41:15 ******/
CREATE UNIQUE NONCLUSTERED INDEX [NormalizedNameIndex] ON [dbo].[ApplicationRole]
(
	[NormalizedName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [NameIndex]    Script Date: 13.02.2021 17:41:15 ******/
CREATE NONCLUSTERED INDEX [NameIndex] ON [dbo].[ApplicationUser]
(
	[FirstName] ASC,
	[LastName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [NormalizedEmailIndex]    Script Date: 13.02.2021 17:41:15 ******/
CREATE NONCLUSTERED INDEX [NormalizedEmailIndex] ON [dbo].[ApplicationUser]
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [NormalizedUserNameInedx]    Script Date: 13.02.2021 17:41:15 ******/
CREATE UNIQUE NONCLUSTERED INDEX [NormalizedUserNameInedx] ON [dbo].[ApplicationUser]
(
	[NormalizedUserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [OwnerIndex]    Script Date: 13.02.2021 17:41:15 ******/
CREATE NONCLUSTERED INDEX [OwnerIndex] ON [dbo].[File]
(
	[Owner] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [NameIndex]    Script Date: 13.02.2021 17:41:15 ******/
CREATE NONCLUSTERED INDEX [NameIndex] ON [dbo].[TaskModel]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ApplicationUser] ADD  DEFAULT ((0)) FOR [EmailConfirmed]
GO
ALTER TABLE [dbo].[ApplicationUser] ADD  DEFAULT ((0)) FOR [ForcePasswordChangeWhileNextLogin]
GO
ALTER TABLE [dbo].[ApplicationUser] ADD  DEFAULT ((0)) FOR [PhoneNumberConfirmed]
GO
ALTER TABLE [dbo].[ApplicationUser] ADD  DEFAULT ((0)) FOR [TwoFactorEnabled]
GO
ALTER TABLE [dbo].[TaskNote] ADD  DEFAULT ((0)) FOR [IsDeleted]
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
ALTER TABLE [dbo].[EmployeeTask]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeTask_ToEmployee] FOREIGN KEY([Employee])
REFERENCES [dbo].[ApplicationUser] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[EmployeeTask] CHECK CONSTRAINT [FK_EmployeeTask_ToEmployee]
GO
ALTER TABLE [dbo].[EmployeeTask]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeTask_ToTask] FOREIGN KEY([Task])
REFERENCES [dbo].[TaskModel] ([ID])
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
ALTER TABLE [dbo].[FileTask]  WITH CHECK ADD  CONSTRAINT [FK_FileTask_ToFile] FOREIGN KEY([File])
REFERENCES [dbo].[File] ([GUID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FileTask] CHECK CONSTRAINT [FK_FileTask_ToFile]
GO
ALTER TABLE [dbo].[FileTask]  WITH CHECK ADD  CONSTRAINT [FK_FileTask_ToTask] FOREIGN KEY([Task])
REFERENCES [dbo].[TaskModel] ([ID])
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
REFERENCES [dbo].[TaskModel] ([ID])
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
REFERENCES [dbo].[TaskModel] ([ID])
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
/****** Object:  StoredProcedure [dbo].[AddTaskNote]    Script Date: 13.02.2021 17:41:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddTaskNote]
	@Task int,
	@Employee int,
	@Note nvarchar(MAX),
	@DateTime datetime,
	@ResponseTo int = null	
AS
BEGIN
	IF EXISTS(SELECT * FROM TaskModel WHERE [ID] = @Task)
	BEGIN
		IF @ResponseTo = 0
			SET @ResponseTo = NULL;

		IF @ResponseTo != NULL
		BEGIN
			DECLARE @BaseTaskID int = 0;
			SELECT @BaseTaskID = Task FROM TaskNote WHERE  ID = @ResponseTo;
			IF @BaseTaskID != @Task
				RETURN 0;
		END
		INSERT INTO [TaskNote] (Task, Employee, Note, DateTime, ResponseTo)
		VALUES (@Task, @Employee, @Note, @DateTime, @ResponseTo);
	SELECT CAST(SCOPE_IDENTITY() as int)
	END
END
GO
/****** Object:  StoredProcedure [dbo].[AddTaskState]    Script Date: 13.02.2021 17:41:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddTaskState]
	@Task int,
	@NewTaskState int,
	@DateTime int,
	@Editor int,
	@Note nvarchar(MAX)
AS
BEGIN
	DECLARE @LastState int = -1;
	SELECT TOP(1) @LastState = NewTaskState FROM TaskState 
		WHERE Task = @Task
		ORDER BY DateTime DESC;

	IF(@LastState = @NewTaskState)
		RETURN 0;

	INSERT INTO TaskState (Task, NewTaskState, DateTime, Editor)
		VALUES (@Task, @NewTaskState, @DateTime, @Editor);
	DECLARE @Inserted int = @@RowCount;
		UPDATE TaskNote SET
		Note = @Note
		WHERE Task = @@IDENTITY;
	RETURN @Inserted;
END
GO
/****** Object:  StoredProcedure [dbo].[AddUserToRole]    Script Date: 13.02.2021 17:41:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddUserToRole]
	@RoleName nvarchar(256),
	@UserID int
AS
BEGIN
	DECLARE @RoleID int = 0;
	DECLARE @NormalizedRoleName nvarchar(256) = UPPER(@RoleName);

	IF NOT EXISTS (SELECT 1 FROM [ApplicationRole] WHERE [NormalizedName] = @NormalizedRoleName)
		BEGIN
			INSERT INTO [ApplicationRole] ([Name], [NormalizedName]) VALUES (@RoleName, @NormalizedRoleName);
			SELECT @RoleID = CAST(SCOPE_IDENTITY() as int);
		END
	ELSE
		SELECT @RoleID = ID FROM [ApplicationRole] WHERE [NormalizedName] = @NormalizedRoleName;
	
	IF NOT EXISTS (SELECT 1 FROM ApplicationUserRole WHERE [UserID] = @UserID AND [RoleID] = @RoleID)
		INSERT INTO [ApplicationUserRole] ([UserID], [RoleID]) VALUES (@UserID, @RoleID);
END
GO
/****** Object:  StoredProcedure [dbo].[AddUserToTask]    Script Date: 13.02.2021 17:41:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddUserToTask]
	@Task int,
	@Employee int,
	@Role int
AS
BEGIN
	IF EXISTS(SELECT * FROM TaskModel WHERE ID = @Employee)
	BEGIN
		INSERT INTO EmployeeTask (Employee, Task, TaskRole)
			VALUES (@Employee, @Task, @Role);
		RETURN @@ROWCOUNT;
	END
	RETURN 0;
END
GO
/****** Object:  StoredProcedure [dbo].[CreateRole]    Script Date: 13.02.2021 17:41:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CreateRole]
	@Name nvarchar(256),
	@NormalizedName nvarchar(256)
AS
BEGIN
	INSERT INTO [ApplicationRole] ([Name], [NormalizedName]) 
		VALUES (@Name, @NormalizedName);
	SELECT CAST(SCOPE_IDENTITY() as int)
END
GO
/****** Object:  StoredProcedure [dbo].[CreateTask]    Script Date: 13.02.2021 17:41:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CreateTask]
@Creator int,
@Deadline datetime null,
@Priority int,
@Name nvarchar(256),
@Description nvarchar(MAX),
@Note varchar(MAX)
AS
BEGIN
	DECLARE @TaskID as INT
	INSERT INTO TaskModel(Deadline, Priority, Name, Description)
		VALUES (@Deadline, @Priority, @Name, @Description);
	SET @TaskID = SCOPE_IDENTITY();
	UPDATE TaskNote SET Employee = @Creator, Note = @Note WHERE Task = @TaskID;
	UPDATE TaskState SET Editor = @Creator,
						TaskNote =
							(SELECT TOP(1) ID
								FROM TaskNote
								WHERE Task = @TaskID)
						WHERE Task = @TaskID;

	SELECT @TaskID;
END
GO
/****** Object:  StoredProcedure [dbo].[CreateUser]    Script Date: 13.02.2021 17:41:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CreateUser]
	@UserName nvarchar(256),
	@NormalizedUserName nvarchar(256),
	@Email nvarchar(256),
	@NormalizedEmail nvarchar(256),
	@EmailConfirmed bit = 0,
	@PasswordHash nvarchar(MAX),
	@ForcePasswordChangeWhileNextLogin bit = 0,
	@PhoneNumber nvarchar(50),
	@PhoneNumberConfirmed bit = 0,
	@TwoFactorEnabled bit = 0,
	@SecurityStamp nvarchar(MAX),
	@FirstName nvarchar(256),
	@LastName nvarchar(256),
	@AddressLine nvarchar(256),
	@PostalCode nvarchar(10),
	@Town nvarchar(50)
AS
BEGIN
	INSERT INTO [ApplicationUser] ([UserName], [NormalizedUserName], [Email],
								   [NormalizedEmail], [EmailConfirmed], [PasswordHash],
								   [ForcePasswordChangeWhileNextLogin], [PhoneNumber], [PhoneNumberConfirmed],
								   [TwoFactorEnabled], [SecurityStamp], [FirstName], [LastName], [AddressLine], [PostalCode], [Town])
								   VALUES
								   (@UserName, @NormalizedUserName, @Email,
								   @NormalizedEmail, @EmailConfirmed, @PasswordHash,
								   @ForcePasswordChangeWhileNextLogin, @PhoneNumber, @PhoneNumberConfirmed,
								   @TwoFactorEnabled, @SecurityStamp, @FirstName, @LastName, @AddressLine, @PostalCode, @Town);
	SELECT CAST(SCOPE_IDENTITY() as int);
END
GO
/****** Object:  StoredProcedure [dbo].[FindRoleById]    Script Date: 13.02.2021 17:41:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[FindRoleById]
	@ID int
AS
	SELECT * FROM [ApplicationRole]
		WHERE [ID] = @ID;
GO
/****** Object:  StoredProcedure [dbo].[FindRoleByName]    Script Date: 13.02.2021 17:41:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[FindRoleByName]
	@NormalizedRoleName nvarchar(255)
AS
	SELECT * FROM [ApplicationRole]
		WHERE [NormalizedName] = @NormalizedRoleName;
GO
/****** Object:  StoredProcedure [dbo].[FindTaskByID]    Script Date: 13.02.2021 17:41:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[FindTaskByID]
	@ID int
AS
BEGIN
	DECLARE @LastState int = -1;
	SELECT TOP(1) @LastState = NewTaskState FROM TaskState
		WHERE Task = @ID
		ORDER BY DateTime DESC;

	DECLARE @Creator int = -1;
	SELECT TOP(1) @LastState = Employee FROM EmployeeTask
		WHERE Task = @ID AND TaskRole = 0;
	
	SELECT *, @LastState as LastState, @Creator as Creator FROM [TaskModel]
			WHERE [ID] = @ID ; 
END
GO
/****** Object:  StoredProcedure [dbo].[FindTaskByQuery]    Script Date: 13.02.2021 17:41:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[FindTaskByQuery]
	@Keywords AS dbo.SearchKeywordsList READONLY,
	@ShowFinished AS bit,
	@UserID AS int = 0
AS
BEGIN
	SET NoCount ON;
	DECLARE @AvoidState int = 2;
	IF(@ShowFinished = 1)
		SET @AvoidState = -1;
	
	DECLARE @Keywords2 SearchKeywordsList;
	IF(SELECT COUNT(*) FROM @Keywords) = 0
		INSERT INTO @Keywords2 VALUES
		('');
	ELSE 
		INSERT INTO @Keywords2 SELECT * FROM @Keywords;
		

	DECLARE @Tab AS TABLE(ID int NOT NULL,
						  Name nvarchar(256)  NULL,
						  Priority int  NULL,
						  Description nvarchar(MAX) NULL,
						  Deadline datetime NULL,
						  TaskState int  NULL,
						  TaskCreator int  NULL);
	IF(@UserID = 0 OR @UserID = null)
		INSERT INTO @Tab (ID, Name, Priority, Description, Deadline, TaskState, TaskCreator)
		SELECT t.ID, t.Name, t.Priority, t.Description, t.Deadline,
			(SELECT TOP(1) NewTaskState FROM TaskState 
					WHERE Task = t.ID
					ORDER BY DateTime DESC) as TaskState,
				  (SELECT TOP(1) Employee FROM EmployeeTask
				    WHERE Task = t.ID AND TaskRole = 0) as TaskCreator
			FROM [TaskModel] t
			WHERE 
			@AvoidState != (SELECT TOP(1) NewTaskState FROM TaskState 
					WHERE Task = t.ID
					ORDER BY DateTime DESC) 
			ORDER BY ID;
	ELSE
		INSERT INTO @Tab (ID, Name, Priority, Description, Deadline, TaskState, TaskCreator)
		SELECT t.ID, t.Name, t.Priority, t.Description, t.Deadline, (SELECT TOP(1) NewTaskState FROM TaskState 
				WHERE Task = t.ID
				ORDER BY DateTime DESC) as TaskState,
				(SELECT TOP(1) Employee FROM EmployeeTask
				WHERE Task = t.ID AND TaskRole = 0) as TaskCreator
			FROM [TaskModel] t
			INNER JOIN [EmployeeTask] as et ON et.[Task] = t.[ID] 	
			WHERE 
			@AvoidState != (SELECT TOP(1) NewTaskState FROM TaskState 
					WHERE Task = t.ID
					ORDER BY DateTime DESC) AND
			@UserID IN (SELECT [Employee] FROM EmployeeTask WHERE Task = t.ID)
			ORDER BY ID;

	DECLARE @keyword nvarchar(MAX);
	DECLARE searchCursor CURSOR FOR 
	SELECT keyword FROM @Keywords2;
	OPEN searchCursor;
	FETCH NEXT FROM searchCursor INTO @keyword;
	WHILE @@FETCH_STATUS = 0
	BEGIN
		DELETE FROM @Tab WHERE ID IN(
		SELECT ID FROM @Tab
		WHERE Name not like '%'+@keyword+'%');

		FETCH NEXT FROM searchCursor INTO @keyword;
	END
	SELECT * FROM @Tab;
END
GO
/****** Object:  StoredProcedure [dbo].[FindTaskByQuery]    Script Date: 13.02.2021 17:41:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[FindTaskByQuery]
	@Keywords AS dbo.SearchKeywordsList READONLY,
	@ShowFinished AS bit,
	@UserID AS int = 0
AS
BEGIN
	DECLARE @AvoidState int = 2;
	IF(@ShowFinished = 1)
		SET @AvoidState = -1;
	
	DECLARE @Keywords2 SearchKeywordsList;
	IF(SELECT COUNT(*) FROM @Keywords) = 0
		INSERT INTO @Keywords2 VALUES
		('');
	ELSE 
		INSERT INTO @Keywords2 SELECT * FROM @Keywords;

	IF(@UserID = 0 OR @UserID = null)

		SELECT t.*, (SELECT TOP(1) NewTaskState FROM TaskState 
					WHERE Task = t.ID
					ORDER BY DateTime DESC) as TaskState,
				  (SELECT TOP(1) Employee FROM EmployeeTask
				    WHERE Task = t.ID AND TaskRole = 0) as TaskCreator
			FROM [TaskModel] t
			WHERE t.ID IN (SELECT ID FROM [TaskModel] JOIN @Keywords2 ON [Name] like '%'+keyword+'%') AND 
			@AvoidState != (SELECT TOP(1) NewTaskState FROM TaskState 
					WHERE Task = t.ID
					ORDER BY DateTime DESC) 
			ORDER BY ID;
	ELSE
			SELECT t.*, (SELECT TOP(1) NewTaskState FROM TaskState 
						WHERE Task = t.ID
						ORDER BY DateTime DESC) as TaskState,
						(SELECT TOP(1) Employee FROM EmployeeTask
						WHERE Task = t.ID AND TaskRole = 0) as TaskCreator
			FROM [TaskModel] t
			INNER JOIN [EmployeeTask] as et ON et.[Task] = t.[ID] 
		
			WHERE t.ID IN (SELECT ID FROM [TaskModel] JOIN @Keywords2 ON [Name] like '%'+keyword+'%') AND 
			@AvoidState != (SELECT TOP(1) NewTaskState FROM TaskState 
					WHERE Task = t.ID
					ORDER BY DateTime DESC) AND
			@UserID IN (SELECT [Employee] FROM EmployeeTask WHERE Task = t.ID)
			ORDER BY ID;
END
GO
/****** Object:  StoredProcedure [dbo].[FindTaskStateById]    Script Date: 13.02.2021 17:41:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[FindTaskStateById]
	@ID int
AS
	SELECT * FROM TaskState
		WHERE ID = @ID;
GO
/****** Object:  StoredProcedure [dbo].[FindUserByEmail]    Script Date: 13.02.2021 17:41:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[FindUserByEmail]
	@NormalizedEmail nvarchar(255)
AS
	SELECT * FROM [ApplicationUser]
		WHERE [NormalizedEmail] = @NormalizedEmail;
GO
/****** Object:  StoredProcedure [dbo].[FindUserById]    Script Date: 13.02.2021 17:41:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[FindUserById]
	@ID int
AS
	SELECT * FROM [ApplicationUser]
		WHERE [ID] = @ID;
GO
/****** Object:  StoredProcedure [dbo].[FindUserByName]    Script Date: 13.02.2021 17:41:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[FindUserByName]
	@NormalizedUserName nvarchar(255)
AS
	SELECT * FROM [ApplicationUser]
		WHERE [NormalizedUserName] = @NormalizedUserName;
GO
/****** Object:  StoredProcedure [dbo].[GetLastTaskState]    Script Date: 13.02.2021 17:41:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetLastTaskState]
	@TaskID int
AS
	SELECT TOP(1) * FROM TaskState 
		WHERE Task = @TaskID
		ORDER BY DateTime DESC;
GO
/****** Object:  StoredProcedure [dbo].[GetNotesForTask]    Script Date: 13.02.2021 17:41:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetNotesForTask]
	@TaskID int
AS
	SELECT * FROM TaskNote WHERE Task = @TaskID;
GO
/****** Object:  StoredProcedure [dbo].[GetSecurityStamp]    Script Date: 13.02.2021 17:41:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetSecurityStamp]
	@ID int
AS
	SELECT [SecurityStamp] FROM [ApplicationUser] WHERE [ID] = @ID;
GO
/****** Object:  StoredProcedure [dbo].[GetTaskNote]    Script Date: 13.02.2021 17:41:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetTaskNote]
	@ID int
AS
	SELECT * FROM TaskNote WHERE ID = @ID;
GO
/****** Object:  StoredProcedure [dbo].[GetTasksForUser]    Script Date: 13.02.2021 17:41:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetTasksForUser]
	@UserID int
AS
	SELECT et.TaskRole, t.* FROM [EmployeeTask] as et
		INNER JOIN [TaskModel] as t ON et.[Task] = t.[ID] 
		WHERE et.[Employee] = @UserID;
GO
/****** Object:  StoredProcedure [dbo].[GetTaskState]    Script Date: 13.02.2021 17:41:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetTaskState]
	@ID int
AS
	SELECT * FROM TaskState
		WHERE ID = @ID;
GO
/****** Object:  StoredProcedure [dbo].[GetTaskStateHistory]    Script Date: 13.02.2021 17:41:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetTaskStateHistory]
	@TaskID int
AS
	SELECT * FROM TaskState 
		WHERE Task = @TaskID
		ORDER BY DateTime DESC;
GO
/****** Object:  StoredProcedure [dbo].[GetTaskStatesForTask]    Script Date: 13.02.2021 17:41:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetTaskStatesForTask]
	@TaskID int
AS
	SELECT * FROM TaskState 
		WHERE Task = @TaskID
		ORDER BY DateTime DESC;
GO
/****** Object:  StoredProcedure [dbo].[GetUserRoles]    Script Date: 13.02.2021 17:41:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetUserRoles]
	@UserID int
AS
	SELECT r.[Name] FROM [ApplicationRole] r 
		INNER JOIN [ApplicationUserRole] ur
			ON ur.[RoleID] = r.ID
		WHERE ur.UserID = @UserID;
GO
/****** Object:  StoredProcedure [dbo].[GetUsers]    Script Date: 13.02.2021 17:41:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetUsers]
@Keywords AS dbo.SearchKeywordsList READONLY
AS
BEGIN
	SET NoCount ON;

	DECLARE @Tab TABLE (
	[ID] INT NOT NULL, 
    [NormalizedUserName] NVARCHAR(256) NOT NULL,    
    [FirstName] NVARCHAR(256) NULL, 
    [LastName] NVARCHAR(256) NULL);

    INSERT INTO @Tab(ID, NormalizedUserName, FirstName, LastName)
	SELECT ID, NormalizedUserName, FirstName, LastName
		FROM [ApplicationUser]		
		ORDER BY ID;

	DECLARE @keyword nvarchar(MAX);
	DECLARE searchCursor CURSOR FOR 
	SELECT keyword FROM @Keywords;
	OPEN searchCursor;
	FETCH NEXT FROM searchCursor INTO @keyword;
	WHILE @@FETCH_STATUS = 0
	BEGIN

		DELETE FROM @Tab WHERE ID IN(
			SELECT ID FROM @Tab
			WHERE
			FirstName not like '%'+@keyword+'%' AND
			LastName not like '%'+@keyword+'%' AND
			NormalizedUserName not like '%'+@keyword+'%');

		FETCH NEXT FROM searchCursor INTO @keyword;

	END
	SELECT *
		FROM [ApplicationUser] u 
		LEFT JOIN [ApplicationUserRole] ur ON ur.UserID = u.ID
		LEFT JOIN [ApplicationRole] r ON r.ID = ur.RoleID
		WHERE u.ID IN (SELECT ID From @Tab)		     
		ORDER BY UserID;

END
GO
/****** Object:  StoredProcedure [dbo].[GetUsersInRole]    Script Date: 13.02.2021 17:41:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetUsersInRole]
	@RoleName nvarchar(256)
AS
BEGIN
	SELECT u.* FROM [ApplicationUser] u 
		INNER JOIN [ApplicationUserRole] ur
			ON ur.[UserID] = u.[ID]
		INNER JOIN [ApplicationRole] r 
			ON r.[ID] = ur.[RoleID]
	WHERE r.[NormalizedName] = UPPER(@RoleName);
END
GO
/****** Object:  StoredProcedure [dbo].[GetUsersInTask]    Script Date: 13.02.2021 17:41:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetUsersInTask]
	@TaskID int
AS
	SELECT et.TaskRole, e.* FROM [EmployeeTask] as et
		INNER JOIN [Employee] as e ON et.[Employee] = e.[ID] 
		WHERE et.[Task] = @TaskID;
GO
/****** Object:  StoredProcedure [dbo].[IsUserInRole]    Script Date: 13.02.2021 17:41:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[IsUserInRole]
	@UserID int,
	@RoleName nvarchar(256)
AS
BEGIN
	DECLARE @NormalizedRoleName nvarchar(256) = UPPER(@RoleName);
	DECLARE @RoleID int;
	SELECT @RoleID = [ID] FROM [ApplicationRole] WHERE [NormalizedName] = @NormalizedRoleName;
	IF EXISTS (SELECT 1 FROM [ApplicationUserRole] WHERE [UserID] = @UserID AND [RoleID] =  @RoleID)
		SELECT 1;
	ELSE 
		SELECT 0;
END
GO
/****** Object:  StoredProcedure [dbo].[RemoveTask]    Script Date: 13.02.2021 17:41:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RemoveTask]
	@ID int
AS
	DELETE FROM [TaskModel] WHERE [TaskModel].ID = @ID;
RETURN @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[RemoveTaskNote]    Script Date: 13.02.2021 17:41:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RemoveTaskNote]
	@ID int
AS
	DELETE FROM [TaskNote] WHERE ID = @ID;
RETURN @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[RemoveTaskState]    Script Date: 13.02.2021 17:41:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RemoveTaskState]
	@ID int
AS
	DELETE FROM TaskState WHERE ID = @ID;
RETURN @@RowCount;
GO
/****** Object:  StoredProcedure [dbo].[RemoveUserFromRole]    Script Date: 13.02.2021 17:41:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RemoveUserFromRole]
	@RoleName nvarchar(256),
	@UserID int
AS
BEGIN
	DECLARE @RoleID int = 0;
	DECLARE @NormalizedRoleName nvarchar(256) = UPPER(@RoleName);

	IF EXISTS (SELECT 1 FROM [ApplicationRole] WHERE [NormalizedName] = @NormalizedRoleName)
		BEGIN
			SELECT @RoleID = ID FROM [ApplicationRole] WHERE [NormalizedName] = @NormalizedRoleName;
			DELETE FROM [ApplicationUserRole] WHERE [RoleID] = @RoleID AND [UserID] = @UserID;
		END	
END
GO
/****** Object:  StoredProcedure [dbo].[RemoveUserFromTask]    Script Date: 13.02.2021 17:41:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RemoveUserFromTask]
	@TaskID int,
	@UserID int
AS
	DELETE FROM [EmployeeTask] WHERE Task = @TaskID AND [Employee] = @UserID;
RETURN @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[UpdateRole]    Script Date: 13.02.2021 17:41:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateRole]
	@ID int,
	@Name nvarchar(256),
	@NormalizedName nvarchar(256)
AS
	UPDATE [ApplicationRole] SET
	[Name] = @Name,
	[NormalizedName] = @NormalizedName
	WHERE [ID] = @ID;
GO
/****** Object:  StoredProcedure [dbo].[UpdateSecurityStamp]    Script Date: 13.02.2021 17:41:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateSecurityStamp]
	@ID int,
	@SecurityStamp nvarchar(MAX)
AS
	UPDATE [ApplicationUser] SET [SecurityStamp] = @SecurityStamp WHERE [ID] = @ID;
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[UpdateTask]    Script Date: 13.02.2021 17:41:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateTask]
	@ID int,
	@Deadline datetime null,
	@Priority int,
	@Name nvarchar(256),
	@Description nvarchar(MAX)
AS
	UPDATE [TaskModel] SET
		Deadline = @Deadline,
		Priority = @Priority,
		Name = @Name,
		Description = @Description
		WHERE ID = @ID;
RETURN @@ROWCOUNT;
GO
/****** Object:  StoredProcedure [dbo].[UpdateTaskNote]    Script Date: 13.02.2021 17:41:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateTaskNote]
	@ID int,
	@Note nvarchar(MAX),
	@DateTime datetime,
	@ResponseTo int = null	
AS
	UPDATE TaskNote SET
	Note = @Note,
	DateTime = @DateTime,
	ResponseTo = @ResponseTo
	WHERE ID = @ID;
RETURN @@ROWCOUNT
GO
/****** Object:  StoredProcedure [dbo].[UpdateTaskState]    Script Date: 13.02.2021 17:41:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateTaskState]
	@ID int,
	@NewTaskState int,
	@DateTime int,
	@Editor int
AS
	UPDATE TaskState SET
	NewTaskState = @NewTaskState,
	DateTime = @DateTime,
	Editor = @Editor
	WHERE ID = @ID;
RETURN @@ROWCount;
GO
/****** Object:  StoredProcedure [dbo].[UpdateUser]    Script Date: 13.02.2021 17:41:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateUser]
	@ID int,
	@UserName nvarchar(256),
	@NormalizedUserName nvarchar(256),
	@Email nvarchar(256),
	@NormalizedEmail nvarchar(256),
	@EmailConfirmed bit = 0,
	@PasswordHash nvarchar(MAX),
	@ForcePasswordChangeWhileNextLogin bit = 0,
	@PhoneNumber nvarchar(50),
	@PhoneNumberConfirmed bit = 0,
	@TwoFactorEnabled bit = 0,
	@SecurityStamp nvarchar(MAX),
	@FirstName nvarchar(256),
	@LastName nvarchar(256),
	@AddressLine nvarchar(256),
	@PostalCode nvarchar(10),
	@Town nvarchar(50)
AS
	UPDATE [ApplicationUser] SET
	[UserName] = @UserName,
	[NormalizedUserName] = @NormalizedUserName,
	[Email] = @Email,
	[NormalizedEmail] = @NormalizedEmail,
	[EmailConfirmed] = @EmailConfirmed,
	[PasswordHash] = @PasswordHash,
	[ForcePasswordChangeWhileNextLogin] = @ForcePasswordChangeWhileNextLogin,
	[PhoneNumber] = @PhoneNumber,
	[PhoneNumberConfirmed] = @PhoneNumberConfirmed,
	[TwoFactorEnabled] = @TwoFactorEnabled,
	[SecurityStamp] = @SecurityStamp,
	[FirstName] = @FirstName,
	[LastName] = @LastName,
	[AddressLine] = @AddressLine,
	[PostalCode] = @PostalCode,
	[Town] = @Town
	WHERE [ID] = @ID;
GO
/****** Object:  Trigger [dbo].[Trigger_AddFirstTaskState]    Script Date: 13.02.2021 17:41:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [dbo].[Trigger_AddFirstTaskState]
    ON [dbo].[TaskModel]
    AFTER INSERT
    AS
    BEGIN
        SET NoCount ON;
        DECLARE @Task int;
        DECLARE insertedCursor CURSOR FOR
        SELECT ID FROM inserted;
        FETCH NEXT FROM insertedCursor INTO @Task;
        WHILE @@FETCH_STATUS = 0
        BEGIN
            INSERT INTO TaskState
                (Task, NewTaskState, DateTime, Editor, TaskNote) 
                VALUES
                (@Task, 0, GETDATE(), null, null);
            FETCH NEXT FROM insertedCursor INTO @Task;
        END
    END
GO
ALTER TABLE [dbo].[TaskModel] ENABLE TRIGGER [Trigger_AddFirstTaskState]
GO
/****** Object:  Trigger [dbo].[Trigger_OnDelete]    Script Date: 13.02.2021 17:41:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE TRIGGER [dbo].[Trigger_OnDelete]
    ON [dbo].[TaskNote]
    FOR DELETE
    AS
    BEGIN
        SET NoCount ON;
        DELETE FROM TaskNote WHERE ResponseTo IN (SELECT ID FROM DELETED);
    END
GO
ALTER TABLE [dbo].[TaskNote] ENABLE TRIGGER [Trigger_OnDelete]
GO
/****** Object:  Trigger [dbo].[Trigger_AddNoteToTask]    Script Date: 13.02.2021 17:41:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [dbo].[Trigger_AddNoteToTask]
    ON [dbo].[TaskState]
    AFTER INSERT
    AS
    BEGIN
        SET NoCount ON;
        DECLARE @ID int;
        DECLARE @Task int;
        DECLARE @DateTime datetime;

        DECLARE insertedCursor CURSOR FOR
        SELECT ID, Task, DateTime FROM inserted;

        OPEN insertedCursor;

        FETCH NEXT FROM insertedCursor INTO @ID, @Task, @DateTime;

        WHILE @@FETCH_STATUS = 0

        BEGIN
            INSERT INTO TaskNote
                (Task, Employee, Note, DateTime, ResponseTo, IsDeleted) 
                VALUES
                (@Task, null, null, @DateTime, null, 0);
            
            UPDATE TaskState SET
                TaskNote = SCOPE_IDENTITY()
                WHERE ID = @ID;

            FETCH NEXT FROM insertedCursor INTO @ID, @Task, @DateTime;
        END
    END
GO
ALTER TABLE [dbo].[TaskState] ENABLE TRIGGER [Trigger_AddNoteToTask]
GO
USE [master]
GO
ALTER DATABASE [PIZZA] SET  READ_WRITE 
GO

