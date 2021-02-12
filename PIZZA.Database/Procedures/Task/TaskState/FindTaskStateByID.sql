CREATE PROCEDURE [dbo].[FindTaskStateById]
	@ID int
AS
	SELECT * FROM TaskState
		WHERE ID = @ID;
