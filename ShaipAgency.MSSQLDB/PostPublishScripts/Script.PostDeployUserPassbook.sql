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

IF ( SELECT COUNT(*) FROM dbo.UserPassbook WITH(NOLOCK) ) = 0 AND (SELECT COUNT(*) FROM dbo.AspNetUsers WITH(NOLOCK) WHERE Id = 1) > 0
BEGIN

    INSERT INTO dbo.UserPassbook    (RequestNo,       UserID,      Charge,		CDateTime)
    VALUES                          ('202004241215',  1,      300000,    GETDATE()),
                                    ('202004243232',  1,      -2000,    GETDATE()),
                                    ('202004243241',  1,      -5000,    GETDATE()),
                                    ('202004249873',  1,      -4000,    GETDATE())
    
    PRINT 'UserPassbook Data INSERTION COMPLETED'
	
END
ELSE
	PRINT 'THERE IS NO dbo.UserPassbook'