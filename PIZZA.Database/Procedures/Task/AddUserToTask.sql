CREATE PROCEDURE [dbo].[AddUserToTask]
	@Task int,
	@Employee int,
	@Role int
AS
BEGIN
	IF EXISTS(SELECT * FROM TaskModel WHERE ID = @Employee) 
		INSERT INTO EmployeeTask (Employee, Task, TaskRole)
			VALUES (@Employee, @Task, @Role);
	RETURN 0;
END