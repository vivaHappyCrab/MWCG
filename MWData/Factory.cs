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

        private readonly IDataAccessor _dataAccessor;

        public Factory(IDataAccessor dataAccessor)
        {
            if(dataAccessor==null)
                throw new ArgumentNullException(nameof(dataAccessor));
            this._dataAccessor = dataAccessor;
        }

        public GameObject GetObjectById(int id)
        {
            if (id <= 0) return null;
            GameObject o = this._objectsLibrary.First(x => x.ObjectNum == id).GetCopy();

            if (o == null) return null;
            o.Id = this._objId++;
            return o;
        }

        public Hero GetHeroByRace(int id) => this.GetObjectById(this._raceLibrary.First(x => x.RaceId == id).HeroId) as Hero;

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
            this._cardLibrary = this._dataAccessor.GetCardList();
            this._objectsLibrary = this._dataAccessor.GetObjectList();
            this._raceLibrary = this._dataAccessor.GetRaceList();
            this._eventMap = InitEvents();

            foreach (Tuple<int, int> pair in this._dataAccessor.GetEventList()) {
                this._objectsLibrary.First(o=>o.ObjectNum==pair.Item1).OnSummon = this._eventMap[pair.Item2];
            }
        }

        private static List<Event> InitEvents()
        {
            List<Event> result = new List<Event> { SpellAction.FireBall, EnterAction.YouthBerserk };
            return result;
        }
    }
}