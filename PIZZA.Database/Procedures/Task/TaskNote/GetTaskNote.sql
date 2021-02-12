CREATE PROCEDURE [dbo].[GetTaskNote]
	@ID int
AS
	SELECT * FROM TaskState WHERE ID = @ID;
