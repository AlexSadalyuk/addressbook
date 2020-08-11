
CREATE PROCEDURE [dbo].[GetUser]
	@UserId int,
	@Domain nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * FROM dbo.Users
	WHERE UserId = @UserId and Domain = @Domain

END