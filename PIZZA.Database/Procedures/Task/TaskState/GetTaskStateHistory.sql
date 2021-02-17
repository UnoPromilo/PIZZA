CREATE PROCEDURE [dbo].[GetTaskStateHistory]
	@TaskID int
AS
	SELECT t.*, t.ID as Editor, a.FirstName, a.LastName FROM TaskState t
		JOIN ApplicationUser a On t.Editor = a.ID
		WHERE Task = @TaskID
		ORDER BY DateTime DESC;
