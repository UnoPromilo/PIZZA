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
