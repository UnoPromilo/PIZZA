CREATE PROCEDURE [dbo].[FindRoleById]
	@ID int
AS
	SELECT * FROM [ApplicationRole]
		WHERE [ID] = @ID;
