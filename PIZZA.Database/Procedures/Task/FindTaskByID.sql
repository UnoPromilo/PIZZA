﻿CREATE PROCEDURE [dbo].[FindTaskByID]
	@ID int
AS
BEGIN
	DECLARE @LastState int = -1;
	SELECT TOP(1) @LastState = NewTaskState FROM TaskState
		WHERE Task = @ID
		ORDER BY DateTime DESC;

	DECLARE @Creator int = -1;
	SELECT TOP(1) @LastState = Employee FROM EmployeeTask
		WHERE Task = @ID AND TaskRole = 0;
	
	SELECT *, @LastState as LastState, @Creator as Creator FROM [TaskModel]
			WHERE [ID] = @ID ; 
END