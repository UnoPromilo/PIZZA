CREATE PROCEDURE [dbo].[RemoveUserFromTask]
	@TaskID int,
	@UserID int
AS
	DELETE FROM [EmployeeTask] WHERE Task = @TaskID AND [Employee] = @UserID;
RETURN @@ROWCOUNT
