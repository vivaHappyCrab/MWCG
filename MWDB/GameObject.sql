CREATE TABLE [dbo].[GameObject]
(
	[Id] INT NOT NULL identity PRIMARY KEY, 
    [BackCardId] INT NOT NULL, 
    [Health] INT NOT NULL, 
    [Cost] INT NULL, 
    [Default] INT NULL, 
    [Attack] INT NULL, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(255) NULL, 
    [type] INT NOT NULL DEFAULT 0 
)
