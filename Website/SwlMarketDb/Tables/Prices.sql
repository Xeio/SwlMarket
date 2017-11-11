CREATE TABLE [dbo].[Prices]
(
  [Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ItemId] INT NOT NULL, 
    [Time] DATETIME NOT NULL, 
    [Marks] INT NOT NULL, 
    [ApiKeyId] INT NULL, 
    [ExpiresIn] INT NULL
)
