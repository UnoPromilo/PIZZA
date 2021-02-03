CREATE PROCEDURE [dbo].[CreateUser]
	@UserName nvarchar(256),
	@NormalizedUserName nvarchar(256),
	@Email nvarchar(256),
	@NormalizedEmail nvarchar(256),
	@EmailConfirmed bit = 0,
	@PasswordHash nvarchar(MAX),
	@ForcePasswordChangeWhileNextLogin bit = 0,
	@PhoneNumber nvarchar(50),
	@PhoneNumberConfirmed bit = 0,
	@TwoFactorEnabled bit = 0,
	@FirstName nvarchar(256),
	@LastName nvarchar(256),
	@AddressLine nvarchar(256),
	@PostalCode nvarchar(10),
	@Town nvarchar(50)
AS
BEGIN
	INSERT INTO [ApplicationUser] ([UserName], [NormalizedUserName], [Email],
								   [NormalizedEmail], [EmailConfirmed], [PasswordHash],
								   [ForcePasswordChangeWhileNextLogin], [PhoneNumber], [PhoneNumberConfirmed],
								   [TwoFactorEnabled], [FirstName], [LastName], [AddressLine], [PostalCode], [Town])
								   VALUES
								   (@UserName, @NormalizedUserName, @Email,
								   @NormalizedEmail, @EmailConfirmed, @PasswordHash,
								   @ForcePasswordChangeWhileNextLogin, @PhoneNumber, @PhoneNumberConfirmed,
								   @TwoFactorEnabled, @FirstName, @LastName, @AddressLine, @PostalCode, @Town);
	SELECT CAST(SCOPE_IDENTITY() as int);
END
