CREATE PROCEDURE [dbo].[FindTaskByID]
	@ID int
AS
	SELECT * FROM [TaskModel]
		WHERE [ID] = @ID;
