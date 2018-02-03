CREATE PROCEDURE [dbo].[getCard]
	@id int = null

AS
	select 
	Id as CardId,
	[Type],
	Name,
	[Description],
	ManaCost,
	EntityId,
	Collectable,
	Rarity
	from dbo.Card
	where @id is null or Id=@id 
RETURN 0
