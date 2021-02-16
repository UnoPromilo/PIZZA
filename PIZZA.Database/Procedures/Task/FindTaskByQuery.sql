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
