CREATE TABLE [dbo].[Prices]
(
  [Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ItemId] INT NOT NULL, 
    [ExpiresAt] DATETIME NOT NULL, 
    [Marks] INT NOT NULL
)
