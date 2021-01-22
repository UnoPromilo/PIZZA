CREATE TABLE [dbo].[ApplicationUser]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserName] NVARCHAR(256) NOT NULL, 
    [NormalizedUserName] NVARCHAR(256) NOT NULL, 
    [Email] NVARCHAR(256) NULL, 
    [NormalizedEmail] NVARCHAR(256) NULL, 
    [EmailConfirmed] BIT NOT NULL, 
    [PasswordHash] NVARCHAR(MAX) NULL, 
    [PhoneNumber] NVARCHAR(50) NULL, 
    [PhoneNumberConfirmed] BIT NOT NULL, 
    [TwoFactorEnabled] BIT NOT NULL,
    [FirstName] NVARCHAR(256) NULL, 
    [LastName] NVARCHAR(256) NULL, 
    [Position] INT NULL, 
    [Address] NVARCHAR(256) NULL, 
    [PostalCode] NVARCHAR(10) NULL, 
    [Town] NVARCHAR(50) NULL, 
)

GO

CREATE UNIQUE INDEX [NormalizedUserNameInedx] ON [dbo].[ApplicationUser] ([NormalizedUserName])

GO

CREATE UNIQUE INDEX [NormalizedEmailIndex] ON [dbo].[ApplicationUser] ([Email])

GO

CREATE INDEX [NameIndex] ON [dbo].[ApplicationUser] ([FirstName], [LastName])

GO