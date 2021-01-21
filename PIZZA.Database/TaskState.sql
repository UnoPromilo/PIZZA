﻿CREATE TABLE [dbo].[TaskState]
(
	[ID] INT NOT NULL PRIMARY KEY, 
    [Task] INT NOT NULL, 
    [NewTaskState] INT NOT NULL, 
    [DateTime] DATETIME NOT NULL, 
    [Editor] INT NOT NULL, 
    [TaskNote] INT NULL, 
    CONSTRAINT [FK_TaskState_ToTask] FOREIGN KEY ([Task]) REFERENCES [Task]([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_TaskState_ToEmployee] FOREIGN KEY ([Editor]) REFERENCES [Employee]([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_TaskState_ToTaskNote] FOREIGN KEY ([TaskNote]) REFERENCES [TaskNote]([ID]) ON DELETE NO ACTION ON UPDATE NO ACTION,
)
