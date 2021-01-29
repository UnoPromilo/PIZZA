CREATE TABLE [dbo].[ApplicationUser]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserName] NVARCHAR(256) NOT NULL, 
    [NormalizedUserName] NVARCHAR(256) NOT NULL, 
    [Email] NVARCHAR(256) NULL, 
    [NormalizedEmail] NVARCHAR(256) NULL, 
    [EmailConfirmed] BIT NOT NULL DEFAULT 0, 
    [PasswordHash] NVARCHAR(MAX) NULL,
    [ForcePasswordChangeWhileNextLogin] BIT NOT NULL DEFAULT 0, 
    [PhoneNumber] NVARCHAR(50) NULL, 
    [PhoneNumberConfirmed] BIT NOT NULL DEFAULT 0, 
    [TwoFactorEnabled] BIT NOT NULL DEFAULT 0,
    [FirstName] NVARCHAR(256) NULL, 
    [LastName] NVARCHAR(256) NULL, 
    [AddressLine] NVARCHAR(256) NULL, 
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