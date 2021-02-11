CREATE PROCEDURE [dbo].[UpdateUser]
	@ID int,
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
	@SecurityStamp nvarchar(MAX),
	@FirstName nvarchar(256),
	@LastName nvarchar(256),
	@AddressLine nvarchar(256),
	@PostalCode nvarchar(10),
	@Town nvarchar(50)
AS
	UPDATE [ApplicationUser] SET
	[UserName] = @UserName,
	[NormalizedUserName] = @NormalizedUserName,
	[Email] = @Email,
	[NormalizedEmail] = @NormalizedEmail,
	[EmailConfirmed] = @EmailConfirmed,
	[PasswordHash] = @PasswordHash,
	[ForcePasswordChangeWhileNextLogin] = @ForcePasswordChangeWhileNextLogin,
	[PhoneNumber] = @PhoneNumber,
	[PhoneNumberConfirmed] = @PhoneNumberConfirmed,
	[TwoFactorEnabled] = @TwoFactorEnabled,
	[SecurityStamp] = @SecurityStamp,
	[FirstName] = @FirstName,
	[LastName] = @LastName,
	[AddressLine] = @AddressLine,
	[PostalCode] = @PostalCode,
	[Town] = @Town
	WHERE [ID] = @ID;

