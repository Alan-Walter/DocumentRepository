CREATE PROCEDURE [dbo].[AddDocument]
	@authorid int,
	@filename nvarchar(255),
	@filepath nvarchar(255)
AS
INSERT INTO Document(FileName, Date, AuthorId, FilePath)
Values (@filename, GETDATE(), @authorid, @filepath)