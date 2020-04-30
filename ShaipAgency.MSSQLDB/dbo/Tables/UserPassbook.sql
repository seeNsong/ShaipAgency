CREATE TABLE [dbo].[UserPassbook]
(
	[RequestNo] CHAR(14) NOT NULL PRIMARY KEY, 
    [EventCode] CHAR(2) NOT NULL,
    [UserID] INT NOT NULL, 
    [Charge] INT NOT NULL, 
    [CDateTime] DATETIME NOT NULL, 
     
    CONSTRAINT [FK_UserID] FOREIGN KEY (UserID) REFERENCES [AspNetUsers](Id)
    
)

GO

CREATE INDEX [IndexShaipName] ON [dbo].[UserPassbook] ([UserID])
