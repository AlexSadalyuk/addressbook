
/*
	exec [AddUser]
		@Firstname = "sdgsdg",
		@Lastname = "asfafs",
		@Address = "asfsafas",
		@City = "asfasfafs",
		@Country = "afssafasf",
		@Company = "asfsafasf",
		@PhoneNumbers = "{"PhoneId":null,"Number":"26326326","IsActive":false,"UserId":0},{"PhoneId":null,"Number":"26236236236","IsActive":false,"UserId":0}"
		
*/
CREATE PROCEDURE [dbo].[AddUser]
	@Firstname nvarchar(50),
	@Lastname nvarchar(50),
	@Address nvarchar(50),
	@City nvarchar(50),
	@Country nvarchar(50),
	@Company nvarchar(50),
	@Domain nvarchar(50),
	@PhoneNumbers nvarchar(max) = null
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @lastInsertedUserId INT

	BEGIN TRANSACTION
	BEGIN TRY
		
	
		INSERT INTO Users
		VALUES 
		(
			@Firstname,
			@Lastname,
			@Address,
			@City,
			@Country,
			@Company,
			@Domain
		)

		SET @lastInsertedUserId = SCOPE_IDENTITY();

		IF(@PhoneNumbers IS NOT NULL)
		BEGIN


			EXEC dbo.UpdatePhone @PhoneNumbers, @lastInsertedUserId, @Domain

		END

		select * from Users
		where userId = @lastInsertedUserId;

		COMMIT TRANSACTION
	END TRY

	BEGIN CATCH
		ROLLBACK TRANSACTION;
	END CATCH


END