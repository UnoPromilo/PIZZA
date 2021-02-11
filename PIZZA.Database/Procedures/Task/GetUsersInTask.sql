CREATE PROCEDURE [dbo].[GetUsersInTask]
	@TaskID int
AS
	SELECT et.TaskRole, e.* FROM [EmployeeTask] as et
		INNER JOIN [Employee] as e ON et.[Employee] = e.[ID] 
		WHERE et.[Task] = @TaskID;
