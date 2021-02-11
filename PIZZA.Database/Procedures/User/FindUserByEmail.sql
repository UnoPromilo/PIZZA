CREATE PROCEDURE [dbo].[FindUserByEmail]
	@NormalizedEmail nvarchar(255)
AS
	SELECT * FROM [ApplicationUser]
		WHERE [NormalizedEmail] = @NormalizedEmail;

