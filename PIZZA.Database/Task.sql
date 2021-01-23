CREATE TABLE [dbo].[Task]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Deadline] DATETIME NULL, 
    [Priority] INT NOT NULL, 
    [Name] NVARCHAR(256) NOT NULL, 
    [Description] NVARCHAR(MAX) NOT NULL,
)

GO

CREATE INDEX [NameIndex] ON [dbo].[Task] ([Name])

GO

CREATE TRIGGER [dbo].[Trigger_AddFirstTaskState]
    ON [dbo].[Task]
    AFTER INSERT
    AS
    BEGIN
        INSERT INTO TaskState
            (Task, NewTaskState, DateTime, Editor, TaskNote) 
            VALUES
            ((SELECT ID FROM inserted), 0, GETDATE(), null, null);
        SET NoCount ON
    END