/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

IF ( SELECT COUNT(*) FROM dbo.UserPassbook WITH(NOLOCK) ) = 0
BEGIN

    INSERT INTO dbo.UserPassbook    (ApplyNo,       ShaipName,      Charge,		CDateTime)
    VALUES                          ('202004241215',  N'김성민',      300000,    GETDATE()),
                                    ('202004243232',  N'김성민',      -2000,    GETDATE()),
                                    ('202004243241',  N'김성민',      -5000,    GETDATE()),
                                    ('202004249873',  N'김성민',      -4000,    GETDATE())
    
    PRINT 'UserPassbook Data INSERTION COMPLETED'
	
END
ELSE
	PRINT 'THERE IS NO dbo.UserPassbook'