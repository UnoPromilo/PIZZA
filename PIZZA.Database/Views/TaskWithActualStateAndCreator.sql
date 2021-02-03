CREATE VIEW [dbo].[TaskWithActualStateAndCreator]
	AS SELECT *,
		(SELECT TOP 1 [NewTaskState]
			FROM [TaskState]
			WHERE [Task] = [OTask].[ID])
			as TaskState,
		(SELECT TOP 1 [Employee]
			FROM [EmployeeTask]
			WHERE [Task] = [OTask].[ID]
			AND [TaskRole] = 0)
			as TaskCreator
	FROM [TaskModel] as OTask;
