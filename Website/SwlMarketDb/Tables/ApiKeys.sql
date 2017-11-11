CREATE TABLE [dbo].[ApiKeys]
(
  [Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Key] VARCHAR(150) NOT NULL, 
    [Active] BIT NOT NULL, 
    [Name] VARCHAR(50) NULL
)

GO

CREATE INDEX [IX_ApiKeys_Key] ON [dbo].[ApiKeys] ([Key])
