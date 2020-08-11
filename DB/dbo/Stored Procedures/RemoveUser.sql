
CREATE PROCEDURE [dbo].[RemoveUser]
	@UserId int,
	@Domain nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;

	DELETE FROM dbo.Users
	WHERE UserId = @UserId and Domain = @Domain

END