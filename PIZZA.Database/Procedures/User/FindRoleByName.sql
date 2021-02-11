CREATE PROCEDURE [dbo].[FindRoleByName]
	@NormalizedRoleName nvarchar(255)
AS
	SELECT * FROM [ApplicationRole]
		WHERE [NormalizedName] = @NormalizedRoleName;

