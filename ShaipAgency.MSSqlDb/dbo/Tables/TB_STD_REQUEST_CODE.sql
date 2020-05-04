CREATE TABLE [dbo].[TB_STD_REQUEST_CODE] (
    [RequestCode]      CHAR (2)      NOT NULL,
    [RequestName]      NVARCHAR (20) NOT NULL,
    [UseYN]            BIT           NOT NULL,
    [CreationUserId]   INT           NOT NULL,
    [CreationDateTime] DATETIME      NOT NULL,
    CONSTRAINT [PK_TB_STD_REQUEST_CODE] PRIMARY KEY CLUSTERED ([RequestCode] ASC)
);

