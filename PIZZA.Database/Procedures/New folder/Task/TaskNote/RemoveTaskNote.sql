﻿CREATE PROCEDURE [dbo].[RemoveTaskNote]
	@ID int
AS
	DELETE FROM [TaskNote] WHERE ID = @ID;
RETURN 0