CREATE PROCEDURE [dbo].[GetUsers]
@Keywords AS dbo.SearchKeywordsList READONLY
AS
BEGIN
	SELECT *
		FROM [ApplicationUser] u 
		LEFT JOIN [ApplicationUserRole] ur ON ur.UserID = u.ID
		LEFT JOIN [ApplicationRole] r ON r.ID = ur.RoleID
		WHERE u.ID IN (SELECT ID FROM [ApplicationUser] JOIN @Keywords ON FirstName like '%'+keyword+'%') OR
		      u.ID IN (SELECT ID FROM [ApplicationUser] JOIN @Keywords ON LastName like '%'+keyword+'%') OR
		      u.ID IN (SELECT ID FROM [ApplicationUser] JOIN @Keywords ON NormalizedUserName like '%'+keyword+'%')
		ORDER BY UserID;
END

