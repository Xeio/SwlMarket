CREATE TABLE [dbo].[Items]
(
  [Id] INT NOT NULL IDENTITY, 
    [Name] VARCHAR(100) NOT NULL , 
    [ItemCategory] INT NULL, 
    [Rarity] INT NOT NULL, 
    [IsExtraordinary] BIT NULL, 
    PRIMARY KEY ([Id]), 
    UNIQUE ([Name], [Rarity]),
)

GO

CREATE INDEX [IX_Items_Name] ON [dbo].[Items] (Name)
