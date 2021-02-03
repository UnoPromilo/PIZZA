﻿CREATE TABLE [dbo].[FileTask]
(
	[File] VARCHAR(36) NOT NULL , 
    [Task] INT NOT NULL, 
    PRIMARY KEY ([Task], [File]), 
    CONSTRAINT [FK_FileTask_ToFile] FOREIGN KEY ([File]) REFERENCES [File]([GUID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_FileTask_ToTask] FOREIGN KEY ([Task]) REFERENCES [TaskModel]([ID]) ON DELETE CASCADE ON UPDATE CASCADE
)