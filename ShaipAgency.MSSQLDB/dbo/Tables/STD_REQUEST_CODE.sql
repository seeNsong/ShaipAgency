CREATE TABLE [dbo].[STD_REQUEST_CODE]
(
	[RequestCode] VARCHAR(14) NOT NULL PRIMARY KEY, 
    [RequestName] NVARCHAR(20) NULL, 
    [UseYN] VARCHAR NULL, 
    [CreationUserId] INT NULL, 
    [CreationDateTime] DATETIME NULL
)
