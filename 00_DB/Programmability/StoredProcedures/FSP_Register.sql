CREATE PROCEDURE [dbo].[FSP_Register]
@LastName NVARCHAR(50),
@FirstName NVARCHAR(50),
@Email NVARCHAR(200),
@Password NVARCHAR(50) /* Nvarchar à cet endroit mais il sera transformé en BINARY par le hashage */
AS

BEGIN

INSERT INTO [Users] (LastName, FirstName, Email, [Password]) VALUES (@LastName, @FirstName, @Email, HASHBYTES('SHA2_512', [dbo].[FSF_GetPreSalt]() + @Password + [dbo].[FSF_GetPostSalt]()))
RETURN 0

END