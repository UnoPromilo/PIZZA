CREATE TABLE [dbo].[Task]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Deadline] DATETIME NULL, 
    [Priority] INT NOT NULL, 
    [Name] NVARCHAR(256) NOT NULL, 
    [Description] NVARCHAR(MAX) NOT NULL, 
    [State] INT NOT NULL, 
)

GO

CREATE INDEX [NameIndex] ON [dbo].[Task] ([Name])
