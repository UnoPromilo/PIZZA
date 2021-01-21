CREATE TABLE [dbo].[Employee]
(
	[ID] INT NOT NULL PRIMARY KEY, 
    [FirstName] NVARCHAR(256) NULL, 
    [LastName] NVARCHAR(256) NULL, 
    [Position] INT NULL, 
    [Address] NVARCHAR(256) NULL, 
    [PostalCode] NVARCHAR(10) NULL, 
    [Town] NVARCHAR(50) NULL, 
    CONSTRAINT [FK_Employee_ToApplicationUser] FOREIGN KEY ([ID]) REFERENCES [ApplicationUser]([ID])
)

GO

CREATE INDEX [NameIndex] ON [dbo].[Employee] ([FirstName], [LastName])
