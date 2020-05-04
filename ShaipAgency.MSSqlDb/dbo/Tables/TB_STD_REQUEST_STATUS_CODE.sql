CREATE TABLE [dbo].[TB_STD_REQUEST_STATUS_CODE] (
    [RequestStatusCode]    CHAR (2)      NOT NULL,
    [RequestStatusName]    NVARCHAR (20) NOT NULL,
    [UseYN]                BIT           NOT NULL,
    [RequestAlertPriority] INT           NOT NULL,
    [CreationUserId]       INT           NOT NULL,
    [CreationDateTime]     DATETIME      NOT NULL,
    CONSTRAINT [PK_TB_STD_REQUEST_STATUS_CODE] PRIMARY KEY CLUSTERED ([RequestStatusCode] ASC)
);

