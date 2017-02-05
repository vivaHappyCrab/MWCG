using System;
using System.Collections.Generic;
using System.Text;

namespace MWCGClasses.GameObjects
{
    public class Ability:Spell
    {
        public int Cost { get; set; }

        public bool Active { get; set; }

        public Ability(Card back, Action effect,int cost, bool active) : base(back, effect)
        {
            Cost = cost;
            Active = active;
        }
    }
}
