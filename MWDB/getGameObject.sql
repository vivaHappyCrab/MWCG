CREATE PROCEDURE [dbo].[getGameObject]
	@id int = null

AS
	select 
		Id as ObjectNum,
		[type] as OType,
		BackCardId as BackCard,
		Health,
		Cost,
		Name,
		[Description],
		[Default],
		Attack
	from dbo.GameObject
	where @id is null or Id=@id 
RETURN 0
