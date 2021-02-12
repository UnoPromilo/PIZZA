CREATE PROCEDURE [dbo].[FindTaskByID]
	@ID int
AS
	DECLARE @LastState int = -1;
	SELECT TOP(1) @LastState = NewTaskState FROM TaskState
		WHERE Task = 1
		ORDER BY DateTime DESC;

	DECLARE @Creator int = -1;
	SELECT TOP(1) @LastState = Employee FROM EmployeeTask
		WHERE Task = 1 AND TaskRole = 0;
	
	SELECT *, @LastState as LastState, @Creator as Creator FROM [TaskModel]
			WHERE [ID] =1 ; 
