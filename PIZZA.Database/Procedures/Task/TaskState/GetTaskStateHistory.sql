CREATE PROCEDURE [dbo].[GetTaskStateHistory]
	@TaskID int
AS
	SELECT * FROM TaskState 
		WHERE Task = @TaskID
		ORDER BY DateTime DESC;
