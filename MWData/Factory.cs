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
        List<Card> CardLibrary;
        List<GameObject> ObjectsLibrary;
        List<Race> RaceLibrary;
        List<Event> EventMap;
        int ObjId = 1;
        int CardId = 1;
        public GameObject getObjectById(int id)
        {
            try
            {
                if (id <= 0) return null;
                GameObject o = ObjectsLibrary.Where(x => x.ObjectNum == id).First().getCopy();
                o.Id = ObjId++;
                return o;
            }
            catch
            {
                return null;
            }
        }

        public Hero GetHeroByRace(int id)
        {
            try
            {
                Hero h = getObjectById(RaceLibrary.Where(x => x.RaceId == id).First().HeroId) as Hero;
                h.Id = ObjId++;
                return h;
            }
            catch
            {
                return null;
            }
        }

        public Event getEventById(int effect)
        {
            try
            {
                return EventMap[effect];
            }
            catch
            {
                return null;
            }
        }

        public Card getCardById(int id)
        {
            try
            {
                Card c = CardLibrary.Where(x => x.CardId == id).First().getCopy();
                c.Id = CardId++;
                return c;
            }
            catch
            {
                return null;
            }
        }

        public void InitLibs()
        {
            CardLibrary = DataAccessor.getCardList();
            ObjectsLibrary = DataAccessor.getObjectList();
            RaceLibrary = DataAccessor.getRaceList();
            EventMap = InitEvents();
        }

        private List<Event> InitEvents()
        {
            List<Event> result = new List<Event>();

            result.Add(SpellAction.FireBall);

            return result;
        }
    }
}
