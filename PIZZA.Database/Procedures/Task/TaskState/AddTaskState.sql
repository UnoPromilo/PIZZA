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
