CREATE PROCEDURE [dbo].[RemoveTask]
	@ID int
AS
	DELETE FROM [TaskModel] WHERE [TaskModel].ID = @ID;
RETURN 0
