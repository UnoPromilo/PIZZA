CREATE PROCEDURE [dbo].[AddUserToTask]
	@Task int,
	@Employee int,
	@Role int
AS
BEGIN
	INSERT INTO EmployeeTask (Employee, Task, TaskRole)
	VALUES (@Employee, @Task, @Role);
	RETURN @@ROWCOUNT;
END