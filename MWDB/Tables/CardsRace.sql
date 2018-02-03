CREATE TABLE [dbo].[CardsRace]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [CardId] INT NOT NULL, 
    [RaceId] INT NOT NULL, 
    CONSTRAINT [FK_CardsRace_ToCard] FOREIGN KEY (CardId) REFERENCES [Card]([Id]), 
    CONSTRAINT [FK_CardsRace_ToRace] FOREIGN KEY ([RaceId]) REFERENCES [Races]([Id])
)
