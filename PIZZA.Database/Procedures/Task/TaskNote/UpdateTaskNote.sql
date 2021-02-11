CREATE PROCEDURE [dbo].[UpdateTaskNote]
	@ID int,
	@Note nvarchar(MAX),
	@DateTime datetime,
	@ResponseTo int = null	
AS
	UPDATE TaskNote SET
	Note = @Note,
	DateTime = @DateTime,
	ResponseTo = @ResponseTo
	WHERE ID = @ID;
RETURN 0
