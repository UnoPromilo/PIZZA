CREATE PROCEDURE [dbo].[CreateTask]
@Creator int,
@Deadline datetime null,
@Priority int,
@Name nvarchar(256),
@Description nvarchar(MAX),
@Note varchar(MAX)
AS
BEGIN
	DECLARE @TaskID as INT
	INSERT INTO TaskModel(Deadline, Priority, Name, Description)
		VALUES (@Deadline, @Priority, @Name, @Description);
	SET @TaskID = SCOPE_IDENTITY();
	UPDATE TaskNote SET Employee = @Creator, Note = @Note WHERE Task = @TaskID;
	UPDATE TaskState SET Editor = @Creator,
						TaskNote =
							(SELECT TOP(1) ID
								FROM TaskNote
								WHERE Task = @TaskID)
						WHERE Task = @TaskID;

	SELECT @TaskID;
END
