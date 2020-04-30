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

    INSERT INTO dbo.UserPassbook    (RequestNo,         EventCode,  UserID,     Charge,     CDateTime)
    VALUES                          ('202004241215',                1,          300000,     GETDATE()),
                                    ('202004243232',                1,          -2000,      GETDATE()),
                                    ('202004243241',                1,          -5000,      GETDATE()),
                                    ('202004249873',                1,          -4000,      GETDATE())
    
    PRINT 'UserPassbook Data INSERTION COMPLETED'
	
END
ELSE
	PRINT 'THERE IS NO dbo.UserPassbook'


IF ( SELECT COUNT(*) FROM STD_REQUEST_CODE WITH(NOLOCK) ) = 0 
BEGIN

    INSERT INTO DBO.STD_REQUEST_CODE
    (
        [RequestCode]
        ,[RequestName]
        ,[UseYN]
        ,[UserId]
        ,[CreationDateTime]
    )
    VALUES  
        ('10',	'예치금충전',	true,	1,	'2020-04-30 01:52:16.0000000'),
        ('11',	'예치금환불',	true,	1,	'2020-04-30 02:09:24.0000000'),
        ('20',	'결제대행',	    true,	1,	'2020-04-30 02:35:45.0000000'),
        ('30',	'주문대행',	    true,	1,	'2020-04-30 02:36:11.0000000'),
        ('40',	'배송대행',	    true,	1,	'2020-04-30 02:37:59.0000000'),
        ('50',	'구매대행',	    true,	1,	'2020-04-30 02:36:19.0000000')

    PRINT 'INSRT STD_REQUEST_CODE OK'      
END
ELSE
    PRINT 'INSRT STD_REQUEST_CODE NG'
