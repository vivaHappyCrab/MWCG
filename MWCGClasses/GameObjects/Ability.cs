using System;
using System.Collections.Generic;
using System.Text;

namespace MWCGClasses.GameObjects
{
    public class Ability:Spell
    {
        public int Cost { get; set; }

        public bool Active { get; set; }

        public Ability(int cardback,int id, Action effect,int cost, bool active) : base(cardback, effect,id,ObjectType.ability)
        {
            Cost = cost;
            Active = active;
        }
    }
}
