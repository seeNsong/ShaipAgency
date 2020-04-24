CREATE TABLE [dbo].[UserPassbook]
(
	[ApplyNo] CHAR(12) NOT NULL PRIMARY KEY, 
    [ShaipName] NVARCHAR(40) NOT NULL, 
    [Charge] INT NOT NULL, 
    [CDateTime] DATETIME NOT NULL
)

GO

CREATE INDEX [IndexShaipName] ON [dbo].[UserPassbook] (ShaipName)
