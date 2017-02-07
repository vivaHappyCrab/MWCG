using System;
using System.Collections.Generic;
using System.Text;
using MWCGClasses.GameObjects;

namespace MWCGClasses.InGame
{
    public class Player
    {
        public int Race { get; set; }

        public Library Deck { get; set; }

        public BattleField Field { get; set; }

        public List<Card> Hand { get; set; }

        public bool First { get; set; }
    }
}
