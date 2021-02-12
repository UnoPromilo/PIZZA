CREATE TABLE [dbo].[TaskNote]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Task] INT NOT NULL, 
    [Employee] INT NULL, 
    [Note] NVARCHAR(MAX) NULL, 
    [DateTime] DATETIME NOT NULL, 
    [ResponseTo] INT NULL, 
    [IsDeleted] BIT NOT NULL, 
    CONSTRAINT [FK_TaskNote_ToTask] FOREIGN KEY ([Task]) REFERENCES [TaskModel]([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_TaskNote_ToEmployee] FOREIGN KEY ([Employee]) REFERENCES [ApplicationUser]([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION, 
    CONSTRAINT [FK_TaskNote_ToTaskNote] FOREIGN KEY ([ResponseTo]) REFERENCES [TaskNote]([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION

)

GO


CREATE TRIGGER [dbo].[Trigger_OnDelete]
    ON [dbo].[TaskNote]
    FOR DELETE
    AS
    BEGIN
        SET NoCount ON;
        DELETE FROM TaskNote WHERE ResponseTo IN (SELECT ID FROM DELETED);
    END