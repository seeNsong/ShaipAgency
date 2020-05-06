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


IF( SELECT COUNT(*) FROM AspNetUsers WITH(NOLOCK) WHERE ID = 1) = 0
BEGIN

    SET IDENTITY_INSERT AspNetUsers ON

    INSERT INTO AspNetUsers
    (
        Id
        ,UserName
        ,NormalizedUserName
        ,Email
        ,NormalizedEmail
        ,EmailConfirmed
        ,PasswordHash
        ,SecurityStamp
        ,ConcurrencyStamp
        ,PhoneNumber
        ,PhoneNumberConfirmed
        ,TwoFactorEnabled
        ,LockoutEnd
        ,LockoutEnabled
        ,AccessFailedCount
        ,ShaipName
        ,IsWithdraw
    )
    VALUES
    
    (1,	N'test@test.com',	N'TEST@TEST.COM',	N'test@test.com',	N'TEST@TEST.COM',	1,	N'AQAAAAEAACcQAAAAEO+qNxWRM6U+VyJ0vrwadSkOOFyDF64zN5qihhNdx9+mRkckMlQeFhQTUQeHSB0rHA==',	N'JI4NFK26IIDHZ4OP2RSWO3IJKVRT37SE',	N'f803bf7d-02f5-4f9b-a5ca-4da0d22ba46f',	NULL,	0,	0,	NULL,	1,	0,	N'김성민	', 0)
    ,(2,	N'user1@test.com',	N'USER1@TEST.COM',	N'user1@test.com',	N'USER1@TEST.COM',	1,	N'AQAAAAEAACcQAAAAELsghEJjM1FoqB5E9VHgvSBXarZM0AH1fNUKI7kK2RdaL8mfv3UWUo8tw042HdVpXA==',	N'UUFJQOBJAFTE2542IHBLGBDV7I3HXIBC',	N'5f027ddb-1fdd-424c-8ddd-eb58eed50ab4',	NULL,	0,	0,	NULL,	1,	0,	N'김성민	', 0)
    
    PRINT 'INSRT AspNetUsers OK'      

    SET IDENTITY_INSERT AspNetUsers OFF
END
ELSE
    PRINT 'INSRT AspNetUsers NG'      


IF ( SELECT COUNT(*) FROM TB_STD_REQUEST_CODE WITH(NOLOCK) ) = 0 
BEGIN

    INSERT INTO DBO.TB_STD_REQUEST_CODE
    (
        [RequestCode]
        ,[RequestName]
        ,[UseYN]
        ,[CreationUserId]
        ,[CreationDateTime]
    )
    VALUES  
        ('10',	N'예치금충전',	1,	1,	'2020-04-30 01:52:16.001'),
        ('11',	N'예치금환불',	1,	1,	'2020-04-30 02:09:24.001'),
        ('20',	N'결제대행',	    1,	1,	'2020-04-30 02:35:45.001'),
        ('30',	N'주문대행',	    1,	1,	'2020-04-30 02:36:11.001'),
        ('40',	N'배송대행',	    1,	1,	'2020-04-30 02:37:59.001'),
        ('50',	N'구매대행',	    1,	1,	'2020-04-30 02:36:19.001')

    PRINT 'INSRT TB_STD_REQUEST_CODE OK'      
END
ELSE
    PRINT 'INSRT TB_STD_REQUEST_CODE NG'

IF ( SELECT COUNT(*) FROM TB_STD_REQUEST_STATUS_CODE WITH(NOLOCK) ) = 0 
BEGIN

    INSERT INTO DBO.TB_STD_REQUEST_STATUS_CODE
    (
        RequestStatusCode
        ,RequestStatusName
        ,UseYN
        ,RequestAlertPriority
        ,CreationUserId
        ,CreationDateTime
    )
    VALUES  
            ('00',N'대기','1','0','1','2020-05-01 04:32:13.000')
            ,('01',N'의뢰접수대기','1','0','1','2020-05-01 04:35:29.000')
            ,('10',N'입금확인중','1','0','1','2020-05-01 04:44:35.000')
            ,('11',N'충전완료','1','1','1','2020-05-01 05:00:04.000')
            ,('12',N'출금처리중','1','0','1','2020-05-01 05:00:14.000')
            ,('13',N'환불완료','1','1','1','2020-05-01 05:00:20.000')
            ,('20',N'이용료산출중','1','0','1','2020-05-01 05:00:27.000')
            ,('21',N'상품문의중','1','0','1','2020-05-01 05:00:38.000')
            ,('22',N'이용료산출완료','1','1','1','2020-05-01 05:00:46.000')
            ,('23',N'주문처리중','1','0','1','2020-05-01 05:23:15.000')
            ,('24',N'결제처리중','1','0','1','2020-05-01 05:23:27.000')
            ,('25',N'결제완료','1','0','1','2020-05-01 05:23:33.000')
            ,('26',N'주문완료','1','1','1','2020-05-01 05:23:45.000')
            ,('50',N'현지배송대기','0','0','1','2020-05-01 05:24:01.000')
            ,('51',N'현지배송중','1','0','1','2020-05-01 05:25:25.000')
            ,('52',N'현지배송완료','1','1','1','2020-05-01 05:26:18.000')
            ,('60',N'물류센터입고대기','1','0','1','2020-05-01 05:26:45.000')
            ,('61',N'부분입고','1','0','1','2020-05-01 05:27:00.000')
            ,('62',N'전체입고','1','1','1','2020-05-01 05:30:38.000')
            ,('63',N'검수완료','1','1','1','2020-05-01 05:31:04.000')
            ,('64',N'배송요금결제대기','1','1','1','2020-05-01 05:31:14.000')
            ,('65',N'출고대기','1','0','1','2020-05-01 05:31:25.000')
            ,('66',N'출고완료','1','0','1','2020-05-01 05:31:41.000')
            ,('85',N'국내배송완료','1','0','1','2020-05-01 05:31:50.000')
            ,('96',N'오류입고','1','2','1','2020-05-01 05:30:53.000')
            ,('97',N'처리불가','1','2','1','2020-05-01 05:31:56.000')
            ,('98',N'클레임','1','2','1','2020-05-01 05:32:02.000')
            ,('99',N'취소','1','0','1','2020-05-01 05:32:09.000')

    PRINT 'INSRT TB_STD_REQUEST_STATUS_CODE OK'      
END
ELSE
    PRINT 'INSRT TB_STD_REQUEST_STATUS_CODE NG'

IF (SELECT COUNT (*) FROM TB_STD_REQUEST_STATUS_ROUTE_INFO WITH(NOLOCK)) = 0
BEGIN

    INSERT INTO TB_STD_REQUEST_STATUS_ROUTE_INFO
    (
        RequestCode
        ,EventCode
        ,NextStatusCode
        ,CreationUserId
        ,CreationDateTime
    )
    VALUES  
        ('10','10','01','1','2020-05-05 01:23:08.000')
        ,('10','11','99','1','2020-05-05 01:24:43.000')
        ,('10','12','01','1','2020-05-05 01:25:21.000')
        ,('10','15','99','1','2020-05-05 05:09:32.000')
        ,('10','16','10','1','2020-05-05 05:47:20.000')
        ,('10','21','11','1','2020-05-05 05:58:18.000')
        ,('11','10','01','1','2020-05-05 01:23:08.000')
        ,('11','11','99','1','2020-05-05 01:24:43.000')
        ,('11','12','01','1','2020-05-05 01:25:21.000')
        ,('11','15','99','1','2020-05-05 05:09:32.000')
        ,('11','16','12','1','2020-05-05 05:47:20.000')
        ,('11','21','13','1','2020-05-05 05:58:18.000')


    PRINT 'INSERT TB_STD_REQUEST_STATUS_ROUTE_INFO OK'
END
ELSE
    PRINT 'INSRT TB_STD_REQUEST_STATUS_ROUTE_INFO NG'

IF (SELECT COUNT(*) FROM TB_STD_EVENT_CODE WITH(NOLOCK)) = 0
BEGIN
    INSERT INTO TB_STD_EVENT_CODE
    (
        EventCode
        ,EventName
        ,UseYN
        ,EventDescription
    )
    VALUES
        ('00', N'회원가입',1,   N'회원 최초 가입')
        ,('01', N'회원탈퇴',1,   N'회원 탈퇴')
        ,('02', N'로그인',1,   N'유저 로그인')
        ,('03', N'로그아웃',1,   N'유저 로그아웃')

        ,('10', N'의뢰등록(이용자)',1,   N'이용자가 의뢰를 등록함')
        ,('11',N'의뢰취소(이용자)',  1,   N'이용자가 의뢰를 취소함')
        ,('12',N'의뢰수정(이용자)',  1,   N'이용자가 의뢰를 수정함')
        ,('13',N'의뢰삭제(이용자)',  1,   N'의뢰가 접수되기 전에 삭제')
        ,('15',N'의뢰취소(샤입)',  1,   N'샤입이 의뢰를 취소함')
        ,('16',N'의뢰접수(샤입)',  1,   N'샤입이 의뢰를 접수함')
        
        ,('20',N'입금 확인',      1,    N'예치금 입금 확인')
        ,('21',N'예치금 충전',    1,    N'입금된 금액의 예치금을 이용자의 계정에 반영')
        ,('22',N'예치금 출금 이체',1,   N'환불 요청한 예치금을 출금 이체')
        ,('23',N'예치금 차감'    ,1,   N'이용자 예치금 차감')
    
    

END;