
/*
	exec UpdatePhone '[{"PhoneId":null,"Number":"380933513029","IsActive":false,"UserId":0}]', 7
*/

CREATE PROCEDURE [dbo].[UpdatePhone]
	@Numbers nvarchar(max),
	@UserId int,
	@Domain nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;

	
	SET @Numbers = STUFF(@Numbers, 1, 1, '');
	SET @Numbers = STUFF(@Numbers, LEN(@Numbers) , 1, '');

	MERGE Phones AS TARGET
	USING (SELECT * 
		FROM OPENJSON(@Numbers)
		WITH (
			PhoneId int 'strict $.PhoneId',
			Number nvarchar(50) 'strict $.Number'
			)) AS source (PhoneId, Number)
	ON (target.PhoneId = source.PhoneId)
	WHEN MATCHED THEN
		UPDATE 
			SET 
				target.Number = source.Number
	WHEN NOT MATCHED THEN
		INSERT (Number, IsActive, UserId)
		VALUES (source.Number, 1, @UserId);

END