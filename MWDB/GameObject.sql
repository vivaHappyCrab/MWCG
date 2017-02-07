CREATE TABLE [dbo].[GameObject]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [BackCardId] INT NOT NULL, 
    [Health] INT NOT NULL, 
    [Cost] INT NULL, 
    [Default] INT NULL, 
    [Attack] INT NULL
)
