CREATE PROCEDURE [dbo].[GetTaskNote]
	@ID int
AS
	SELECT * FROM TaskNote WHERE ID = @ID;

