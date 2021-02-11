CREATE PROCEDURE [dbo].[RemoveUserFromRole]
	@RoleName nvarchar(256),
	@UserID int
AS
BEGIN
	DECLARE @RoleID int = 0;
	DECLARE @NormalizedRoleName nvarchar(256) = UPPER(@RoleName);

	IF EXISTS (SELECT 1 FROM [ApplicationRole] WHERE [NormalizedName] = @NormalizedRoleName)
		BEGIN
			SELECT @RoleID = ID FROM [ApplicationRole] WHERE [NormalizedName] = @NormalizedRoleName;
			DELETE FROM [ApplicationUserRole] WHERE [RoleID] = @RoleID AND [UserID] = @UserID;
		END	
END