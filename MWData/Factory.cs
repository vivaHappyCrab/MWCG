using System.Collections.Generic;
using System.Linq;
using MWCGClasses;
using MWCGClasses.GameObjects;
using MWCGClasses.InGame;

namespace MWData
{
    public class Factory
    {
        private int _cardId = 1;
        private List<Card> _cardLibrary;
        private List<Event> _eventMap;
        private List<GameObject> _objectsLibrary;
        private int _objId = 1;
        private List<Race> _raceLibrary;

        public GameObject GetObjectById(int id)
        {
            if (id <= 0) return null;
            GameObject o = _objectsLibrary.First(x => x.ObjectNum == id).GetCopy();

            if (o == null) return null;
            o.Id = _objId++;
            return o;
        }

        public Hero GetHeroByRace(int id)
        {
            Hero h = GetObjectById(_raceLibrary.First(x => x.RaceId == id).HeroId) as Hero;

            if (h == null) return null;
            h.Id = _objId++;
            return h;
        }

        public Event GetEventById(int effect)
        {
            try
            {
                return _eventMap[effect];
            }
            catch
            {
                return null;
            }
        }

        public Card GetCardById(int id)
        {
            try
            {
                Card c = _cardLibrary.First(x => x.CardId == id).getCopy();

                if (c == null) return null;
                c.Id = _cardId++;
                return c;
            }
            catch
            {
                return null;
            }
        }

        public GameObject GetObjectToPlayer(int id, Player player)
        {
            GameObject res = GetObjectById(id);
            res.Owner = player;
            return res;
        }

        public Card GetCardToPlayer(int id, int playerNum)
        {
            Card res = GetCardById(id);
            res.Owner = playerNum;
            return res;
        }

        public void InitLibs()
        {
            _cardLibrary = DataAccessor.getCardList();
            _objectsLibrary = DataAccessor.getObjectList();
            _raceLibrary = DataAccessor.getRaceList();
            _eventMap = InitEvents();
        }

        private static List<Event> InitEvents()
        {
            List<Event> result = new List<Event> {SpellAction.FireBall};
            return result;
        }
    }
}