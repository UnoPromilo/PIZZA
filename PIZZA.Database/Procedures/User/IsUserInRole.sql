CREATE PROCEDURE [dbo].[IsUserInRole]
	@UserID int,
	@RoleName nvarchar(256)
AS
BEGIN
	DECLARE @NormalizedRoleName nvarchar(256) = UPPER(@RoleName);
	DECLARE @RoleID int;
	SELECT @RoleID = [ID] FROM [ApplicationRole] WHERE [NormalizedName] = @NormalizedRoleName;
	IF EXISTS (SELECT 1 FROM [ApplicationUserRole] WHERE [UserID] = @UserID AND [RoleID] =  @RoleID)
		SELECT 1;
	ELSE 
		SELECT 0;
END