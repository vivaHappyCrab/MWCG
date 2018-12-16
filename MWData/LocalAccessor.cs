using System;
using System.Collections.Generic;
using System.Linq;
using MWCGClasses;
using MWCGClasses.Enums;
using MWCGClasses.GameObjects;

namespace MWData
{
    public class LocalAccessor : IDataAccessor
    {
        #region Cards

        private readonly List<Card> _cards = new List<Card>()
        {
            new Card()
            {
                CardId = 1,
                Collectable = true,
                Description = "Basic human warrior",
                EntityId = 1,
                Id=0,
                ManaCost = 1,
                Name = "Footman",
                Owner = 0,
                Races = new []{1},
                Rarity = RareType.Base,
                Type = CardType.Permanent
            },
                        new Card()
            {
                CardId = 2,
                Collectable = false,
                Description = "Well-known human hero",
                EntityId = -1,
                Id=0,
                ManaCost = 0,
                Name = "Lord Arvis",
                Owner = 0,
                Races = new []{1},
                Rarity = RareType.Base,
                Type = CardType.Artefact
            },
                                    new Card()
            {
                CardId = 3,
                Collectable = true,
                Description = "Deals huge damage",
                EntityId = 3,
                Id=0,
                ManaCost = 3,
                Name = "Fireball",
                Owner = 0,
                Races = new []{1},
                Rarity = RareType.Common,
                Type = CardType.Spell
            },
                                                new Card()
            {
                CardId = 4,
                Collectable = true,
                Description = "Strong human warrior",
                EntityId = 4,
                Id=0,
                ManaCost = 2,
                Name = "Commander",
                Owner = 0,
                Races = new []{1},
                Rarity = RareType.Common,
                Type = CardType.Permanent
            },
                                                            new Card()
            {
                CardId = 5,
                Collectable = true,
                Description = "Crazy unit -be careful.",
                EntityId = 5,
                Id=0,
                ManaCost = 1,
                Name = "Youth Berserk",
                Owner = 0,
                Races = new []{1},
                Rarity = RareType.Base,
                Type = CardType.Permanent
            },
                                                                        new Card()
            {
                CardId = 6,
                Collectable = true,
                Description = "It's good to spend evening on farm.",
                EntityId = 6,
                Id=0,
                ManaCost = 2,
                Name = "Farm",
                Owner = 0,
                Races = new []{1},
                Rarity = RareType.Base,
                Type = CardType.Permanent
            }
        };
        #endregion

        #region Objects

        private readonly List<GameObject> _objects = new List<GameObject>()
        {
            new Unit(1,1,"Footman",null,1,1),
            new Hero(0,2,"Lord Arvis","Increase 1 hp of unit or himself",30,0),
            new Spell(3,3,0,"Fireball","Deal 4 damage to target permanent or hero"),
            new Unit(4,4,"Commander",null,1,4),
            new Unit(5,5,"Youth Berserk","Deal 1 damage to your creatures",2,3),
            new Support(6,6,"Farm","At end of your turn restore 1 health to a random unit",4)
        };
        #endregion

        #region Races

        private readonly List<Race> _races = new List<Race>()
        {
            new Race() {HeroId = 2,Name="Knight",RaceId=1}
        };
        #endregion

        #region EventList
        private readonly List<Tuple<int,int>> _events= new List<Tuple<int, int>>()
        {
            new Tuple<int, int>(5,1)
        };
        #endregion

        public Card GetCard(int id) => this._cards.FirstOrDefault(card => card.CardId == id);

        public List<Card> GetCardList() => this._cards;

        public List<Tuple<int, int>> GetEventList() => this._events;

        public GameObject GetObject(int id) => this._objects.FirstOrDefault(obj => obj.ObjectNum == id);

        public List<GameObject> GetObjectList() => this._objects;

        public Race GetRace(int id) => this._races.FirstOrDefault(race => race.RaceId == id);

        public List<Race> GetRaceList() => this._races;
    }
}
