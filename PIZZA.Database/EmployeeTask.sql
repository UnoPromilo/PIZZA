CREATE TABLE [dbo].[EmployeeTask]
(
	[EmployeeID] INT NOT NULL, 
    [TaskID] INT NOT NULL, 
    [TaskRole] INT NOT NULL, 
    PRIMARY KEY ([TaskID], [EmployeeID]), 
    CONSTRAINT [FK_EmployeeTask_ToEmployee] FOREIGN KEY ([EmployeeID]) REFERENCES [ApplicationUser]([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_EmployeeTask_ToTask] FOREIGN KEY ([TaskID]) REFERENCES [Task]([ID]) ON DELETE CASCADE ON UPDATE CASCADE
)
