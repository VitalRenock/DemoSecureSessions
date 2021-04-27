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
USE [master];


GO

IF (DB_ID(N'$(DatabaseName)') IS NOT NULL) 
BEGIN
    ALTER DATABASE [$(DatabaseName)]
    SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [$(DatabaseName)];
END

GO
PRINT N'Création de $(DatabaseName)...'
GO
CREATE DATABASE [$(DatabaseName)]
    ON 
    PRIMARY(NAME = [$(DatabaseName)], FILENAME = N'$(DefaultDataPath)$(DefaultFilePrefix)_Primary.mdf')
    LOG ON (NAME = [$(DatabaseName)_log], FILENAME = N'$(DefaultLogPath)$(DefaultFilePrefix)_Primary.ldf') COLLATE SQL_Latin1_General_CP1_CI_AS
GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CLOSE OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
USE [$(DatabaseName)];


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                NUMERIC_ROUNDABORT OFF,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL,
                CURSOR_CLOSE_ON_COMMIT OFF,
                AUTO_CREATE_STATISTICS ON,
                AUTO_SHRINK OFF,
                AUTO_UPDATE_STATISTICS ON,
                RECURSIVE_TRIGGERS OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ALLOW_SNAPSHOT_ISOLATION OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET READ_COMMITTED_SNAPSHOT OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_UPDATE_STATISTICS_ASYNC OFF,
                PAGE_VERIFY NONE,
                DATE_CORRELATION_OPTIMIZATION OFF,
                DISABLE_BROKER,
                PARAMETERIZATION SIMPLE,
                SUPPLEMENTAL_LOGGING OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET TRUSTWORTHY OFF,
        DB_CHAINING OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'Impossible de modifier les paramètres de base de données. Vous devez être administrateur système pour appliquer ces paramètres.';
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET HONOR_BROKER_PRIORITY OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'Impossible de modifier les paramètres de base de données. Vous devez être administrateur système pour appliquer ces paramètres.';
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET TARGET_RECOVERY_TIME = 0 SECONDS 
    WITH ROLLBACK IMMEDIATE;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET FILESTREAM(NON_TRANSACTED_ACCESS = OFF),
                CONTAINMENT = NONE 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CREATE_STATISTICS ON(INCREMENTAL = OFF),
                MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT = OFF,
                DELAYED_DURABILITY = DISABLED 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE (QUERY_CAPTURE_MODE = ALL, DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_PLANS_PER_QUERY = 200, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 367), MAX_STORAGE_SIZE_MB = 100) 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE = OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
    END


GO
IF fulltextserviceproperty(N'IsFulltextInstalled') = 1
    EXECUTE sp_fulltext_database 'enable';


GO
PRINT N'Création de [dbo].[Users]...';


GO
CREATE TABLE [dbo].[Users] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Email]     NVARCHAR (200) NOT NULL,
    [FirstName] NVARCHAR (50)  NOT NULL,
    [LastName]  NVARCHAR (50)  NOT NULL,
    [Password]  BINARY (64)    NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Création de [dbo].[FSF_GetPostSalt]...';


GO
CREATE FUNCTION [dbo].[FSF_GetPostSalt]
(
)
RETURNS NVARCHAR(2048)
AS
BEGIN
RETURN N'kmRYA%ux*y322RzMd_dC#j@8ZsE_FANTOMESLuR&^yV8dg4R_NRDH_?9VrAUWNm&e2e*qJUT5-+ph4t%F2RSj+nnFG4#MLJA3N%Kjgr+vHdy+kYJP-nvr9Qf+4A7S_Zm#%=VT2FS*#B=wFcTL$c%3puT8Tzw!jk84V26rswN?62mEt+Fr$TqA6H=NnwJm6j3V_h-zmxKzVvAej?5QKB7cE*ckPpZh3m29yFz*C*-^6ZHUvHjz8ymuDb7x-#&a#bYe!MdV5mRty6+9BSe_Fc%cJFtBbX!R*JPW+Pvr*xcC-8dFvjv$9NxQG$gewCtFgF4s*pBQfjmr#zmf@^j+8dwwdGg4pVvze=jR?rD9nh%*HL9*9pxQJcW^gbQSuBYs!KXYq2kZ&$JW$#VFeC%pfY_9hxnpHJEj?ABd#5mpn+fu7h3bkRA5n_EzVRYFB483%fP^GkwA&?AP8PkB=ytFSf%Q8KsM*3hXk3Q$5aG7mZJyRLZk+eH6WWnJS$7vu2&+z3q4KqSJMrDLE5*#?4bzCZT7jB-j^pZfNL=4uyTG2&mX@P7q+KZrmrVPFRGg4Q5G&2rE!z6LQReEVBRLvDqD6%7D?WwQL3cAf5qDJXZA9%?MFWwmsEemaXw!YkSwsnQ2kz3KyP8tvAgzCWhzH%=e@wKn?383gNua4gYAsaSPswG92tUx?%u!d!tHEmQxpmpvtBTH@_buQ4GVGG7vLngwD7d6C^8-pSVSzM^Y6mz3LKL!Vy@YsEpM*3LF+9MuEg8Lhfn!=d9Wr&4_CkS+Bum&ELUemYFC=rhNZuzh^PEA6A=-vZ&s5sD_eJhgTQ9QYVZ3?6*7#9%pB6?Dn*5q76jfwJ&d+ZQmsN@_3AyWSUP-NLw$D=9bB7UxPBYN9&DrU72D*TLPH-z8N3%kGmeyDfEbk9Q%+^Mku*BdNXny73_rRXcNU%W=MEV8GxjAkkUY&TAdBQPk?#fBV+XN2na*^Qr=rkS+p=XUfSwdS+WgKkvMs+vYt+YhamrkRtYMkT3yVQsH$eXm#J$6a#%kW@!VthZPQJ@sq#@Xs9v!5LGfe=x3z4@&Zt2kpy-_Gt+Hd=-LRNKEw%%B&BcL_yWC^q*juZR3hfKCQzxa8^uEZ^MZen3u7t^ZFaDYAD8kTfuEwLjV+R!=-Q9jjFMm!d%W#HJmN*WFFL#-!sgr$w&S_V6yJ$nt4e3euKtHcL?5Wq6sm+!gz@tASSE!5FMzAP9bvu4nV7&g56#TFZeLkY9s#AW_3EG8#bGb9bDj&c9%r$F?fK3yf6Szq-N!8!wy6qAX?CNXUTurePNE#pFxBGa^x+L9x66k+D-*qCYk&9NaaVLF%+Pd%8Z*d$as!B4aP@dT*U+HQ9&NMbq4B$@jKzNbgaH=G^E@Pq4JZYr5yr?7j9Azxzg+qaHabt$Ny43npbUXk$Dnrx4C-8y&BsWJEeSHKk#uy-f6xpVMq*hJqH*T!P229y?dXj-t8T#x?#C*cYpS-8uvKvb&VQeun_CV5#Ve3B@agfspWznL-wNMy95yj9MUmmrd&UB*Qrp4cfy*LhnFeUsN8sHh8ahgkPR%ATG%f%umYX!$4j3SAYmJG32%gAKZP3m@5xa5n*Xq=#X*QMnJ!=ux?SAfT%n@eUgJ3y$^vhn@fB@AYr&!fZGJqFkEa#YpW_FGv8tz4%%cxgF=fcWB4jheGj6shee3VfqdPXX*$5v#Uv*w3PNJXq9L*GYkj!PKfk9=_8-@Xn%h+c*Au=x9h8KhQZJ?PjxFCA-hjpLMHJggUwp96DW57M*NZstNAZYy^=D7byZpHjLuXNu%Q!WCwzJdeWuBB*@fBq=MFsbPB!6V2Kn?Bjh_Crn*GWz%ef6h_eQZEV-KR-kFJhe$Zqs3Q8KN9Jb-r&$8u*yPWv#VkQE#hpK3ECsWhh?YHmHqF%u!3Ys%pG%uWzBHJa8g5WLqzsAeKCf6LKmvCz%@wGT6+C*Ma-QY$KXQ2%2F-yuTuC@Ujbv8^jn69tYq5hQ?^yZp532M36LRcJ!EWT8T*Z7TE_YGed@-BFzX-xh3p?$Bkwn2xYn$^y8Lah8';
END
/*
Génération du mot de passe sur le site https://passwordsgenerator.net/
>>> Pourquoi N'' ? > Unicode .. 
*/
GO
PRINT N'Création de [dbo].[FSF_GetPreSalt]...';


GO
CREATE FUNCTION [dbo].[FSF_GetPreSalt]
(
)
RETURNS NVARCHAR(2048)
AS
BEGIN
RETURN N'kmRYA%ux*y322RzMd_dC#j@8ZsE_RZFRqBNjLuR&^yV8dg4R_NRDH_?9VrAUWNm&e2e*qJUT5-+ph4t%F2RSj+nnFG4#MLJA3N%Kjgr+vHdy+kYJP-nvr9Qf+4A7S_Zm#%=VT2FS*#B=wFcTL$c%3puT8Tzw!jk84V26rswN?62mEt+Fr$TqA6H=NnwJm6j3V_h-zmxKzVvAej?5QKB7cE*ckPpZh3m29yFz*C*-^6ZHUvHjz8ymuDb7x-#&a#bYe!MdV5mRty6+9BSe_Fc%cJFtBbX!R*JPW+Pvr*xcC-8dFvjv$9NxQG$gewCtFgF4s*pBQfjmr#zmf@^j+8dwwdGg4pVvze=jR?rD9nh%*HL9*9pxQJcW^gbQSuBYs!KXYq2kZ&$JW$#VFeC%pfY_9hxnpHJEj?ABd#5mpn+fu7h3bkRA5n_EzVRYFB483%fP^GkwA&?AP8PkB=ytFSf%Q8KsM*3hXk3Q$5aG7mZJyRLZk+eH6WWnJS$7vu2&+z3q4KqSJMrDLE5*#?4bzCZT7jB-j^pZfNL=4uyTG2&mX@P7q+KZrmrVPFRGg4Q5G&2rE!z6LQReEVBRLvDqD6%7D?WwQL3cAf5qDJXZA9%?MFWwmsEemaXw!YkSwsnQ2kz3KyP8tvAgzCWhzH%=e@wKn?383gNua4gYAsaSPswG92tUx?%u!d!tHEmQxpmpvtBTH@_buQ4GVGG7vLngwD7d6C^8-pSVSzM^Y6mz3LKL!Vy@YsEpM*3LF+9MuEg8Lhfn!=d9Wr&4_CkS+Bum&ELUemYFC=rhNZuzh^PEA6A=-vZ&s5sD_eJhgTQ9QYVZ3?6*7#9%pB6?Dn*5q76jfwJ&d+ZQmsN@_3AyWSUP-NLw$D=9bB7UxPBYN9&DrU72D*TLPH-z8N3%kGmeyDfEbk9Q%+^Mku*BdNXny73_rRXcNU%W=MEV8GxjAkkUY&TAdBQPk?#fBV+XN2na*^Qr=rkS+p=XUfSwdS+WgKkvMs+vYt+YhamrkRtYMkT3yVQsH$eXm#J$6a#%kW@!VthZPQJ@sq#@Xs9v!5LGfe=x3z4@&Zt2kpy-_Gt+Hd=-LRNKEw%%B&BcL_yWC^q*juZR3hfKCQzxa8^uEZ^MZen3u7t^ZFaDYAD8kTfuEwLjV+R!=-Q9jjFMm!d%W#HJmN*WFFL#-!sgr$w&S_V6yJ$nt4e3euKtHcL?5Wq6sm+!gz@tASSE!5FMzAP9bvu4nV7&g56#TFZeLkY9s#AW_3EG8#bGb9bDj&c9%r$F?fK3yf6Szq-N!8!wy6qAX?CNXUTurePNE#pFxBGa^x+L9x66k+D-*qCYk&9NaaVLF%+Pd%8Z*d$as!B4aP@dT*U+HQ9&NMbq4B$@jKzNbgaH=G^E@Pq4JZYr5yr?7j9Azxzg+qaHabt$Ny43npbUXk$Dnrx4C-8y&BsWJEeSHKk#uy-f6xpVMq*hJqH*T!P229y?dXj-t8T#x?#C*cYpS-8uvKvb&VQeun_CV5#Ve3B@agfspWznL-wNMy95yj9MUmmrd&UB*Qrp4cfy*LhnFeUsN8sHh8ahgkPR%ATG%f%umYX!$4j3SAYmJG32%gAKZP3m@5xa5n*Xq=#X*QMnJ!=ux?SAfT%n@eUgJ3y$^vhn@fB@AYr&!fZGJqFkEa#YpW_FGv8tz4%%cxgF=fcWB4jheGj6shee3VfqdPXX*$5v#Uv*w3PNJXq9L*GYkj!PKfk9=_8-@Xn%h+c*Au=x9h8KhQZJ?PjxFCA-hjpLMHJggUwp96DW57M*NZstNAZYy^=D7byZpHjLuXNu%Q!WCwzJdeWuBB*@fBq=MFsbPB!6V2Kn?Bjh_Crn*GWz%ef6h_eQZEV-KR-kFJhe$Zqs3Q8KN9Jb-r&$8u*yPWv#VkQE#hpK3ECsWhh?YHmHqF%u!3Ys%pG%uWzBHJa8g5WLqzsAeKCf6LKmvCz%@wGT6+C*Ma-QY$KXQ2%2F-yuTuC@Ujbv8^jn69tYq5hQ?^yZp532M36LRcJ!EWT8T*Z7TE_YGed@-BFzX-xh3p?$Bkwn2xYn$^y8Lah8';
END
/*
Génération du mot de passe sur le site https://passwordsgenerator.net/
>>> Pourquoi N'' ? > Unicode .. 
*/
GO
PRINT N'Création de [dbo].[FSP_Login]...';


GO
CREATE PROCEDURE [dbo].[FSP_Login]
	@param1 int = 0,
	@param2 int
AS
	SELECT @param1, @param2
RETURN 0
GO
PRINT N'Création de [dbo].[FSP_Register]...';


GO
CREATE PROCEDURE [dbo].[FSP_Register]
@LastName NVARCHAR(50),
@FirstName NVARCHAR(50),
@Email NVARCHAR(384),
@Passwd NVARCHAR(50) /* Nvarchar à cet endroit mais il sera transformé en BINARY par le hashage */
AS

BEGIN

INSERT INTO [Users] (LastName, FirstName, Email, [Password]) VALUES (@LastName, @FirstName, @Email, HASHBYTES('SHA2_512', [dbo].[FSF_GetPreSalt]() + @Passwd + [dbo].[FSF_GetPostSalt]()))
RETURN 0

END
GO
-- Étape de refactorisation pour mettre à jour le serveur cible avec des journaux de transactions déployés

IF OBJECT_ID(N'dbo.__RefactorLog') IS NULL
BEGIN
    CREATE TABLE [dbo].[__RefactorLog] (OperationKey UNIQUEIDENTIFIER NOT NULL PRIMARY KEY)
    EXEC sp_addextendedproperty N'microsoft_database_tools_support', N'refactoring log', N'schema', N'dbo', N'table', N'__RefactorLog'
END
GO
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '1030fe02-07f1-4ff2-81d5-2221446f4b0b')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('1030fe02-07f1-4ff2-81d5-2221446f4b0b')

GO

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
DECLARE @VarDecimalSupported AS BIT;

SELECT @VarDecimalSupported = 0;

IF ((ServerProperty(N'EngineEdition') = 3)
    AND (((@@microsoftversion / power(2, 24) = 9)
          AND (@@microsoftversion & 0xffff >= 3024))
         OR ((@@microsoftversion / power(2, 24) = 10)
             AND (@@microsoftversion & 0xffff >= 1600))))
    SELECT @VarDecimalSupported = 1;

IF (@VarDecimalSupported > 0)
    BEGIN
        EXECUTE sp_db_vardecimal_storage_format N'$(DatabaseName)', 'ON';
    END


GO
PRINT N'Mise à jour terminée.';


GO
