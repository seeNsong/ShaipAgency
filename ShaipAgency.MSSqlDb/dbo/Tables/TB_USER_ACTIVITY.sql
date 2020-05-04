CREATE TABLE [dbo].[TB_USER_ACTIVITY] (
    [UserId]                      INT      NOT NULL,
    [RequestCount]                INT      NOT NULL,
    [CancellationCount]           INT      NOT NULL,
    [BacktoBackCancellationCount] INT      NOT NULL,
    [Grade]                       INT      NOT NULL,
    [Blocked]                     INT      NOT NULL,
    [SignUpDateTime]              DATETIME NOT NULL,
    [LastSignInDateTime]          DATETIME NOT NULL,
    [WithdrawalDateTime]          DATETIME NOT NULL,
    CONSTRAINT [PK_TB_USER_ACTIVITY] PRIMARY KEY CLUSTERED ([UserId] ASC),
    CONSTRAINT [FK_TB_USER_ACTIVITY_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);

