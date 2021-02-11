CREATE PROCEDURE [dbo].[FindUserById]
	@ID int
AS
	SELECT * FROM [ApplicationUser]
		WHERE [ID] = @ID;
