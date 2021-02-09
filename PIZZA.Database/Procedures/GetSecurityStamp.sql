CREATE PROCEDURE [dbo].[GetSecurityStamp]
	@ID int
AS
	SELECT [SecurityStamp] FROM [ApplicationUser] WHERE [ID] = @ID;
