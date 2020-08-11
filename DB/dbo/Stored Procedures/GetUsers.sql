
CREATE PROCEDURE [dbo].[GetUsers]
	@Domain nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * FROM dbo.Users
	where Domain = @Domain
END