CREATE PROCEDURE [dbo].[GetTaskState]
	@ID int
AS
	SELECT * FROM TaskState
		WHERE ID = @ID;
