/*
Script de déploiement pour FantomesDB

Ce code a été généré par un outil.
La modification de ce fichier peut provoquer un comportement incorrect et sera perdue si
le code est régénéré.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "FantomesDB"
:setvar DefaultFilePrefix "FantomesDB"
:setvar DefaultDataPath "C:\Users\r.borremans\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB"
:setvar DefaultLogPath "C:\Users\r.borremans\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB"

GO
:on error exit
GO
/*
Détectez le mode SQLCMD et désactivez l'exécution du script si le mode SQLCMD n'est pas pris en charge.
Pour réactiver le script une fois le mode SQLCMD activé, exécutez ce qui suit :
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'Le mode SQLCMD doit être activé de manière à pouvoir exécuter ce script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
PRINT N'Modification de [dbo].[FSP_Login]...';


GO
ALTER PROCEDURE [dbo].[FSP_Login]
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
GO
PRINT N'Modification de [dbo].[FSP_Register]...';


GO
ALTER PROCEDURE [dbo].[FSP_Register]
@LastName NVARCHAR(50),
@FirstName NVARCHAR(50),
@Email NVARCHAR(200),
@Password NVARCHAR(50) /* Nvarchar à cet endroit mais il sera transformé en BINARY par le hashage */
AS

BEGIN

INSERT INTO [Users] (LastName, FirstName, Email, [Password]) VALUES (@LastName, @FirstName, @Email, HASHBYTES('SHA2_512', [dbo].[FSF_GetPreSalt]() + @Password + [dbo].[FSF_GetPostSalt]()))
RETURN 0

END
GO
/*
Modèle de script de post-déploiement							
--------------------------------------------------------------------------------------
 Ce fichier contient des instructions SQL qui seront ajoutées au script de compilation.		
 Utilisez la syntaxe SQLCMD pour inclure un fichier dans le script de post-déploiement.			
 Exemple :      :r .\monfichier.sql								
 Utilisez la syntaxe SQLCMD pour référencer une variable dans le script de post-déploiement.		
 Exemple :      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

EXEC [FSP_Register] @LastName = 'Renaud' , @Firstname = 'Brigode', @Email = 'r.b@gmail.com' , @Password = '1234test=';
EXEC [FSP_Register] @LastName = 'Jerome' , @Firstname = 'Thunus', @Email = 'j.t@gmail.com' , @Password = '1234test=';
EXEC [FSP_Register] @LastName = 'Romain' , @Firstname = 'Borremans', @Email = 'ro.bo@gmail.com' , @Password = '1234test=';
GO

GO
PRINT N'Mise à jour terminée.';


GO
