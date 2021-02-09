CREATE PROCEDURE [dbo].[UpdateSecurityStamp]
	@ID int,
	@SecurityStamp nvarchar(MAX)
AS
	UPDATE [ApplicationUser] SET [SecurityStamp] = @SecurityStamp WHERE [ID] = @ID;
RETURN 0
