CREATE TABLE [dbo].[TB_REQ_TIMESTAMPS] (
    [Timestamp] DATETIME     NOT NULL,
    [RequestNo] VARCHAR (15) NULL,
    [DetailNo]  INT          NOT NULL,
    [EventCode] CHAR (2)     NULL,
    CONSTRAINT [PK_TB_REQ_TIMESTAMPS] PRIMARY KEY CLUSTERED ([Timestamp] ASC)
);

