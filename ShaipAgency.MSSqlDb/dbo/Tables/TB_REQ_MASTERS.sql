CREATE TABLE [dbo].[TB_REQ_MASTERS] (
    [RequestNo]            VARCHAR (15)   NOT NULL,
    [RequestCode]          CHAR (2)       NULL,
    [RequestUserId]        INT            NOT NULL,
    [RequestTitle]         NVARCHAR (256) NULL,
    [DetailCount]          INT            NOT NULL,
    [RemainClaims]         INT            NOT NULL,
    [DeliveryGroupNo]      VARCHAR (15)   NOT NULL,
    [IsLocked]             BIT            NOT NULL,
    [CreationDateTime]     DATETIME       NOT NULL,
    [ModificationDateTime] DATETIME       NOT NULL,
    CONSTRAINT [PK_TB_REQ_MASTERS] PRIMARY KEY CLUSTERED ([RequestNo] ASC),
    CONSTRAINT [FK_TB_REQ_MASTERS_TB_STD_REQUEST_CODE_RequestCode] FOREIGN KEY ([RequestCode]) REFERENCES [dbo].[TB_STD_REQUEST_CODE] ([RequestCode])
);


GO
CREATE NONCLUSTERED INDEX [IX_TB_REQ_MASTERS_RequestCode]
    ON [dbo].[TB_REQ_MASTERS]([RequestCode] ASC);

