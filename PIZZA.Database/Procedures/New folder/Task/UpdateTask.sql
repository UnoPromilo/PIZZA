CREATE PROCEDURE [dbo].[UpdateTask]
	@ID int,
	@Deadline datetime null,
	@Priority int,
	@Name nvarchar(256),
	@Description nvarchar(MAX),
	@Note varchar(MAX)
AS
	UPDATE [TaskModel] SET
		Deadline = @Deadline,
		Priority = @Priority,
		Name = @Name,
		Description = @Description
		WHERE ID = @ID;
RETURN 0
