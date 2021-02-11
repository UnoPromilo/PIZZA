CREATE PROCEDURE [dbo].[AddUserToRole]
	@RoleName nvarchar(256),
	@UserID int
AS
BEGIN
	DECLARE @RoleID int = 0;
	DECLARE @NormalizedRoleName nvarchar(256) = UPPER(@RoleName);

	IF NOT EXISTS (SELECT 1 FROM [ApplicationRole] WHERE [NormalizedName] = @NormalizedRoleName)
		BEGIN
			INSERT INTO [ApplicationRole] ([Name], [NormalizedName]) VALUES (@RoleName, @NormalizedRoleName);
			SELECT @RoleID = CAST(SCOPE_IDENTITY() as int);
		END
	ELSE
		SELECT @RoleID = ID FROM [ApplicationRole] WHERE [NormalizedName] = @NormalizedRoleName;
	
	IF NOT EXISTS (SELECT 1 FROM ApplicationUserRole WHERE [UserID] = @UserID AND [RoleID] = @RoleID)
		INSERT INTO [ApplicationUserRole] ([UserID], [RoleID]) VALUES (@UserID, @RoleID);
END
