CREATE PROCEDURE [dbo].[FindUserByName]
	@NormalizedUserName nvarchar(255)
AS
	SELECT * FROM [ApplicationUser]
		WHERE [NormalizedUserName] = @NormalizedUserName;
