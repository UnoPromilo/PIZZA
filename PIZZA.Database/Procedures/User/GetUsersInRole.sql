CREATE PROCEDURE [dbo].[GetUsersInRole]
	@RoleName nvarchar(256)
AS
BEGIN
	SELECT u.* FROM [ApplicationUser] u 
		INNER JOIN [ApplicationUserRole] ur
			ON ur.[UserID] = u.[ID]
		INNER JOIN [ApplicationRole] r 
			ON r.[ID] = ur.[RoleID]
	WHERE r.[NormalizedName] = UPPER(@RoleName);
END
