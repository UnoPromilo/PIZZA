CREATE TABLE [dbo].[TaskModel]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Deadline] DATETIME NULL, 
    [Priority] INT NOT NULL, 
    [Name] NVARCHAR(256) NOT NULL, 
    [Description] NVARCHAR(MAX) NOT NULL,
)

GO

CREATE INDEX [NameIndex] ON [dbo].[TaskModel] ([Name])

GO

CREATE TRIGGER [dbo].[Trigger_AddFirstTaskState]
    ON [dbo].[TaskModel]
    AFTER INSERT
    AS
    BEGIN
        SET NoCount ON;
        DECLARE @Task int;
        DECLARE insertedCursor CURSOR FOR
        SELECT ID FROM inserted;
        OPEN insertedCursor;
        FETCH NEXT FROM insertedCursor INTO @Task;
        WHILE @@FETCH_STATUS = 0
        BEGIN
            INSERT INTO TaskState
                (Task, NewTaskState, DateTime, Editor, TaskNote) 
                VALUES
                (@Task, 0, GETDATE(), null, null);
            FETCH NEXT FROM insertedCursor INTO @Task;
        END
    END