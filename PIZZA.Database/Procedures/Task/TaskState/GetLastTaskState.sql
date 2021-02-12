CREATE PROCEDURE [dbo].[GetLastTaskState]
	@TaskID int
AS
	SELECT TOP(1) * FROM TaskState 
		WHERE Task = @TaskID
		ORDER BY DateTime DESC;
