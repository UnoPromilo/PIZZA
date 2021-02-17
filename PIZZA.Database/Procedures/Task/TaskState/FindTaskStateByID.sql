CREATE PROCEDURE [dbo].[FindTaskStateById]
	@ID int
AS
	SELECT t.*, t.ID as Editor, a.FirstName, a.LastName
		FROM TaskState t
		JOIN ApplicationUser a On t.Editor = a.ID
		WHERE t.ID = @ID;
