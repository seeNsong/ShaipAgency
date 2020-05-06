CREATE TABLE [dbo].[TB_REQ_DETAILS_DEPOSIT] (
    [RequestNo]         VARCHAR (15)  NOT NULL,
    [DepositDate]       DATETIME      NOT NULL,
    [Amount]            INT           NOT NULL,
    [PersonName]        NVARCHAR (20) NULL,
    [AccountNumber]     NVARCHAR (40) NULL,
    [RequestStatusCode] CHAR (2)      NULL,
    CONSTRAINT [PK_TB_REQ_DETAILS_DEPOSIT] PRIMARY KEY CLUSTERED ([RequestNo] ASC),
    CONSTRAINT [FK_TB_REQ_DETAILS_DEPOSIT_TB_REQ_MASTERS_RequestNo] FOREIGN KEY ([RequestNo]) REFERENCES [dbo].[TB_REQ_MASTERS] ([RequestNo]) ON DELETE CASCADE,
    CONSTRAINT [FK_TB_REQ_DETAILS_DEPOSIT_TB_STD_REQUEST_STATUS_CODE_RequestStatusCode] FOREIGN KEY ([RequestStatusCode]) REFERENCES [dbo].[TB_STD_REQUEST_STATUS_CODE] ([RequestStatusCode])
);


GO
CREATE NONCLUSTERED INDEX [IX_TB_REQ_DETAILS_DEPOSIT_RequestStatusCode]
    ON [dbo].[TB_REQ_DETAILS_DEPOSIT]([RequestStatusCode] ASC);

