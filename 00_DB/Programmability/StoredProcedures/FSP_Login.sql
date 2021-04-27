CREATE PROCEDURE [dbo].[FSP_Login]
@Email NVARCHAR(200),
@Password NVARCHAR(50) /* Nvarchar à cet endroit mais il sera transformé en BINARY par le hashage */
AS

BEGIN

SELECT Id, FirstName, LastName, Email, [Password] 
FROM Users
WHERE Email = @Email 
	  And [Password] = HASHBYTES('SHA2_512', [dbo].[FSF_GetPreSalt]() + @Password + [dbo].[FSF_GetPostSalt]());
RETURN 0

END