CREATE PROCEDURE [dbo].[GetLastTaskState]
	@TaskID int
AS
	SELECT TOP(1) t.*, t.ID as Editor, a.FirstName, a.LastName
		FROM TaskState t
		JOIN ApplicationUser a On t.Editor = a.ID
		WHERE Task = @TaskID
		ORDER BY DateTime DESC;
