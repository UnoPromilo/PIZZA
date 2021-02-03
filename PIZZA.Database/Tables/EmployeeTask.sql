CREATE TABLE [dbo].[EmployeeTask]
(
	[Employee] INT NOT NULL, 
    [Task] INT NOT NULL, 
    [TaskRole] INT NOT NULL, 
    PRIMARY KEY ([Task], [Employee]), 
    CONSTRAINT [FK_EmployeeTask_ToEmployee] FOREIGN KEY ([Employee]) REFERENCES [ApplicationUser]([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_EmployeeTask_ToTask] FOREIGN KEY ([Task]) REFERENCES [TaskModel]([ID]) ON DELETE CASCADE ON UPDATE CASCADE
)
