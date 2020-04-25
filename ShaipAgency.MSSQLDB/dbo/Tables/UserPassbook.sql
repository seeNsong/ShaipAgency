CREATE TABLE [dbo].[UserPassbook]
(
	[RequestNo] CHAR(12) NOT NULL PRIMARY KEY, 
    [UserID] INT NOT NULL, 
    [Charge] INT NOT NULL, 
    [CDateTime] DATETIME NOT NULL, 
    CONSTRAINT [FK_UserID] FOREIGN KEY (UserID) REFERENCES [AspNetUsers](Id)
    
)

GO

CREATE INDEX [IndexShaipName] ON [dbo].[UserPassbook] ([UserID])
