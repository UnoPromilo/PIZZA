CREATE TABLE [dbo].[ApplicationRole]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(256) NULL, 
    [NormalizedName] NVARCHAR(256) NULL
)

GO

CREATE UNIQUE INDEX [NormalizedNameIndex] ON [dbo].[ApplicationRole] ([NormalizedName])
