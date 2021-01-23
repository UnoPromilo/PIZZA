CREATE PROCEDURE [dbo].[CreateTask](@Creator int, @Deadline datetime null, @Priority int, @Name nvarchar(256), @Description nvarchar(MAX), @Note varchar(MAX))
AS
BEGIN
	DECLARE @TaskID as INT
	INSERT INTO Task(Deadline, Priority, Name, Description)
		VALUES (@Deadline, @Priority, @Name, @Description);
	SET @TaskID = SCOPE_IDENTITY();
	UPDATE TaskState SET Editor = @Creator WHERE ID = @TaskID;
	UPDATE TaskNote SET Employee = @Creator, Note = @Note WHERE ID = @TaskID;
END
