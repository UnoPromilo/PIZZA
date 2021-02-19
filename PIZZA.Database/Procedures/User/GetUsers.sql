CREATE PROCEDURE [dbo].[GetUsers]
@Keywords AS dbo.SearchKeywordsList READONLY
AS
BEGIN
	SET NoCount ON;

	DECLARE @Tab TABLE (
	[ID] INT NOT NULL, 
    [NormalizedUserName] NVARCHAR(256) NOT NULL,    
    [FirstName] NVARCHAR(256) NULL, 
    [LastName] NVARCHAR(256) NULL);

    INSERT INTO @Tab(ID, NormalizedUserName, FirstName, LastName)
	SELECT ID, NormalizedUserName, FirstName, LastName
		FROM [ApplicationUser]		
		ORDER BY ID;

	DECLARE @keyword nvarchar(MAX);
	DECLARE searchCursor CURSOR FOR 
	SELECT keyword FROM @Keywords;
	OPEN searchCursor;
	FETCH NEXT FROM searchCursor INTO @keyword;
	WHILE @@FETCH_STATUS = 0
	BEGIN

		DELETE FROM @Tab WHERE ID IN(
			SELECT ID FROM @Tab
			WHERE
			FirstName not like '%'+@keyword+'%' AND
			LastName not like '%'+@keyword+'%' AND
			NormalizedUserName not like '%'+@keyword+'%');

		FETCH NEXT FROM searchCursor INTO @keyword;

	END
	SELECT TOP(100) *
		FROM [ApplicationUser] u 
		LEFT JOIN [ApplicationUserRole] ur ON ur.UserID = u.ID
		LEFT JOIN [ApplicationRole] r ON r.ID = ur.RoleID
		WHERE u.ID IN (SELECT ID From @Tab)		     
		ORDER BY UserID;

END

