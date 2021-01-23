CREATE VIEW [dbo].[FileWithTask]
	AS SELECT * FROM [FileTask] INNER JOIN [File] ON [FileTask].[FileGUID] = [File].[GUID];
