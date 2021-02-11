CREATE TABLE [dbo].[TaskState]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Task] INT NOT NULL, 
    [NewTaskState] INT NOT NULL , 
    [DateTime] DATETIME NOT NULL, 
    [Editor] INT NULL, 
    [TaskNote] INT NULL, 
    CONSTRAINT [FK_TaskState_ToTask] FOREIGN KEY ([Task]) REFERENCES [TaskModel]([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_TaskState_ToEmployee] FOREIGN KEY ([Editor]) REFERENCES [ApplicationUser]([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_TaskState_ToTaskNote] FOREIGN KEY ([TaskNote]) REFERENCES [TaskNote]([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION,
)

GO

CREATE TRIGGER [dbo].[Trigger_AddNoteToTask]
    ON [dbo].[TaskState]
    AFTER INSERT
    AS
    BEGIN
        INSERT INTO TaskNote
            (Task, Employee, Note, DateTime, ResponseTo, IsDeleted) 
            VALUES
            ((SELECT Task FROM inserted), null, null, (SELECT DateTime FROM inserted), null, 0);
        SET NoCount ON
    END