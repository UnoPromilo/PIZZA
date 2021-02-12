CREATE PROCEDURE [dbo].[GetTaskStatesForTask]
	@TaskID int
AS
	SELECT * FROM TaskState 
		WHERE Task = @TaskID
		ORDER BY DateTime DESC;
