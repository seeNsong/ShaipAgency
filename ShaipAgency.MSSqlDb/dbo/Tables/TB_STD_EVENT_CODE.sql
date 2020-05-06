CREATE TABLE [dbo].[TB_STD_EVENT_CODE] (
    [EventCode]        CHAR (2)       NOT NULL,
    [EventName]        NVARCHAR (20)  NOT NULL,
    [UseYN]            BIT            NOT NULL,
    [EventDescription] NVARCHAR (256) NULL,
    CONSTRAINT [PK_TB_STD_EVENT_CODE] PRIMARY KEY CLUSTERED ([EventCode] ASC)
);

