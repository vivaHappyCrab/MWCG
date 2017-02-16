using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MWCGClasses.GameObjects;
using MWCGClasses.InGame;
using MWCGClasses;

namespace MWData
{
    public class Factory
    {
        static List<Card> CardLibrary = new List<Card>();
        static List<GameObject> ObjectsLibrary = new List<GameObject>();
        static List<Race> RaceLibrary = new List<Race>();
        static public GameObject getObjectById(int id)
        {
            if (id <= 0) return null;
            return ObjectsLibrary.Where(x => x.ObjectNum == id).First().getCopy();
        }

        internal static Hero GetHeroByRace(int id)
        {
            return getObjectById(
                RaceLibrary.Where(x => x.RaceId == id).First().HeroId)as Hero;
        }

        internal static Event getEventById(int effect)
        {
            throw new NotImplementedException();
        }

        static public Card getCardById(int id)
        {
            return CardLibrary.Where(x => x.CardId == id).First().getCopy();
        }

        static public void InitLibs()
        {
            CardLibrary = DataAccessor.getCardList();
            ObjectsLibrary = DataAccessor.getObjectList();
            RaceLibrary = DataAccessor.getRaceList();
        }
    }
}
