CREATE PROCEDURE [dbo].[UpdateRole]
	@ID int,
	@Name nvarchar(256),
	@NormalizedName nvarchar(256)
AS
	UPDATE [ApplicationRole] SET
	[Name] = @Name,
	[NormalizedName] = @NormalizedName
	WHERE [ID] = @ID;
