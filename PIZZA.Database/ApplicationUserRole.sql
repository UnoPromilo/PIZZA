CREATE TABLE [dbo].[ApplicationUserRole]
(
	[UserID] INT NOT NULL , 
    [RoleID] INT NOT NULL, 
    PRIMARY KEY ([RoleID], [UserID]), 
    CONSTRAINT [FK_ApplicationUserRole_ToApplicationUser] FOREIGN KEY ([UserID]) REFERENCES [ApplicationUser]([ID]) ON DELETE CASCADE ON UPDATE CASCADE, 
    CONSTRAINT [FK_ApplicationUserRole_ToApplicationRole] FOREIGN KEY ([RoleID]) REFERENCES [ApplicationRole]([ID]) ON DELETE CASCADE ON UPDATE CASCADE
)
