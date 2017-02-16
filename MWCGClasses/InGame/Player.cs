using System;
using System.Collections.Generic;
using System.Text;
using MWCGClasses.GameObjects;
using MWData;

namespace MWCGClasses.InGame
{
    public class Player
    {
        public int Race { get; set; }

        public Library Deck { get; set; }

        public BattleField Field { get; set; }

        public List<Card> Hand { get; set; }

        public bool First { get; set; }

        public int Num { get; set; }

        public Player(int race,Library deck,bool first,int num)
        {
            Race = race;
            Deck = deck;
            First = first;
            Num = num;
            Field = new BattleField(race);
            Hand = new List<Card>();
        }
    }
}
