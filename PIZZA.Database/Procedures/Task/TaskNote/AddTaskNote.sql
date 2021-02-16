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