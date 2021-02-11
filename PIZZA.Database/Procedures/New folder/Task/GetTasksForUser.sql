CREATE PROCEDURE [dbo].[GetTasksForUser]
	@UserID int
AS
	SELECT et.TaskRole, t.* FROM [EmployeeTask] as et
		INNER JOIN [TaskModel] as t ON et.[Task] = t.[ID] 
		WHERE et.[Employee] = @UserID;
