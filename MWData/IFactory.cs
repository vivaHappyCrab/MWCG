using MWCGClasses;
using MWCGClasses.GameObjects;
using MWCGClasses.InGame;

namespace MWData
{
    public interface IFactory
    {
        Card GetCardById(int id);
        Card GetCardToPlayer(int id, int playerNum);
        Event GetEventById(int effect);
        Hero GetHeroByRace(int id);
        GameObject GetObjectById(int id);
        GameObject GetObjectToPlayer(int id, Player player);
        void InitLibs();
    }
}