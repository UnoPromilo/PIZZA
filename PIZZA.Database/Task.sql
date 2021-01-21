CREATE TABLE [dbo].[Task]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Creator] INT NULL, 
    [Deadline] DATETIME NULL, 
    [Priority] INT NOT NULL, 
    [Name] NVARCHAR(256) NOT NULL, 
    [Description] NVARCHAR(MAX) NOT NULL, 
    [State] INT NOT NULL, 
    CONSTRAINT [FK_Task_ToTEmployee] FOREIGN KEY ([Creator]) REFERENCES [Employee]([ID]) ON DELETE SET NULL ON UPDATE CASCADE
)

GO

CREATE INDEX [CreatorIndex] ON [dbo].[Task] ([Creator])

GO

CREATE INDEX [NameIndex] ON [dbo].[Task] ([Name])
