
CREATE PROCEDURE [dbo].[UpdateUser]
	@UserId int,
	@Firstname nvarchar(50),
	@Lastname nvarchar(50),
	@Address nvarchar(50),
	@City nvarchar(50),
	@Country nvarchar(50),
	@Company nvarchar(50),
	@PhoneNumbers nvarchar(max) = null
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE dbo.Users
	SET 
		Lastname = @Lastname,
		Firstname = @Firstname,
		Address = @Address,
		City = @City,
		Country = @Country,
		Company = @Company
	WHERE UserId = @UserId

	IF(@PhoneNumbers IS NOT NULL) 
	BEGIN
		EXEC dbo.UpdatePhone @PhoneNumbers, @UserId
	END
END