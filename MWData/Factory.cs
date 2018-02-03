using System;
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
            GameObject o = this._objectsLibrary.First(x => x.ObjectNum == id).GetCopy();

            if (o == null) return null;
            o.Id = this._objId++;
            return o;
        }

        public Hero GetHeroByRace(int id)
        {
            Hero h = this.GetObjectById(this._raceLibrary.First(x => x.RaceId == id).HeroId) as Hero;

            if (h == null) return null;
            h.Id = this._objId++;
            return h;
        }

        public Event GetEventById(int effect)
        {
            try
            {
                return this._eventMap[effect];
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
                Card c = this._cardLibrary.First(x => x.CardId == id).GetCopy();

                if (c == null) return null;
                c.Id = this._cardId++;
                return c;
            }
            catch
            {
                return null;
            }
        }

        public GameObject GetObjectToPlayer(int id, Player player)
        {
            GameObject res = this.GetObjectById(id);
            res.Owner = player;
            return res;
        }

        public Card GetCardToPlayer(int id, int playerNum)
        {
            Card res = this.GetCardById(id);
            res.Owner = playerNum;
            return res;
        }

        public void InitLibs()
        {
            this._cardLibrary = DataAccessor.GetCardList();
            this._objectsLibrary = DataAccessor.GetObjectList();
            this._raceLibrary = DataAccessor.GetRaceList();
            this._eventMap = InitEvents();

            foreach (Tuple<int, int?> pair in DataAccessor.GetEventList())
            {
                if (pair.Item2.HasValue)
                    this._objectsLibrary.First(o=>o.ObjectNum==pair.Item1).OnSummon = this._eventMap[pair.Item2.Value];
            }
        }

        private static List<Event> InitEvents()
        {
            List<Event> result = new List<Event> { SpellAction.FireBall, EnterAction.YouthBerserk };
            return result;
        }
    }
}