CREATE TABLE [dbo].[File]
(
	[GUID] VARCHAR(36) NOT NULL PRIMARY KEY, 
    [Owner] INT NULL, 
    [FileName] NVARCHAR(256) NOT NULL, 
    [UploadDate] DATETIME NOT NULL, 
    CONSTRAINT [FK_File_ToOwner] FOREIGN KEY ([Owner]) REFERENCES [ApplicationUser]([ID]) ON DELETE SET NULL ON UPDATE CASCADE
)

GO

CREATE INDEX [OwnerIndex] ON [dbo].[File] ([Owner])
