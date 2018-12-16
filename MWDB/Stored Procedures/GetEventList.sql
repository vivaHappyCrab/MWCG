CREATE PROCEDURE [dbo].[getEventList]
AS
	SELECT  gobj.Id as Id
			,gobj.EnterBattle as EnterEvent
	FROM dbo.GameObject gobj
	WHERE gobj.EnterBattle is not null
RETURN 0
