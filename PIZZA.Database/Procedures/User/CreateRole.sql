CREATE PROCEDURE [dbo].[CreateRole]
	@Name nvarchar(256),
	@NormalizedName nvarchar(256)
AS
BEGIN
	INSERT INTO [ApplicationRole] ([Name], [NormalizedName]) 
		VALUES (@Name, @NormalizedName);
	SELECT CAST(SCOPE_IDENTITY() as int)
END