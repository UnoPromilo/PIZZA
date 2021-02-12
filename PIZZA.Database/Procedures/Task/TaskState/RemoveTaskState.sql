CREATE PROCEDURE [dbo].[RemoveTaskState]
	@ID int
AS
	DELETE FROM TaskState WHERE ID = @ID;
RETURN @@RowCount;
