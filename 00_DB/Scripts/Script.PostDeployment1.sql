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