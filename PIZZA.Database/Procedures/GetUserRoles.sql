CREATE PROCEDURE [dbo].[GetUserRoles]
	@UserID int
AS
	SELECT r.[Name] FROM [ApplicationRole] r 
		INNER JOIN [ApplicationUserRole] ur
			ON ur.[RoleID] = r.ID
		WHERE ur.UserID = @UserID;

