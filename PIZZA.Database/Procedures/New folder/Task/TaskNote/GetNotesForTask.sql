CREATE PROCEDURE [dbo].[GetNotesForTask]
	@TaskID int
AS
	SELECT * FROM TaskNote WHERE Task = @TaskID;
