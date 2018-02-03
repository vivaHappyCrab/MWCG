CREATE TABLE [dbo].[Card]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(255) NOT NULL, 
    [ManaCost] INT NOT NULL, 
    [Type] INT NOT NULL, 
    [EntityId] INT NOT NULL, 
    [Collectable] BIT NOT NULL, 
    [Rarity] INT NOT NULL
)
