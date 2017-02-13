using System;
using System.Collections.Generic;
using System.Text;

namespace MWCGClasses.GameObjects
{
    public class Ability:Spell
    {
        public int Cost { get; set; }

        public bool Active { get; set; }

        public Ability(int cardback,int id, int effect,int cost, bool active, string name, string desc) : base(cardback, effect, id, name, desc, ObjectType.ability)
        {
            Cost = cost;
            Active = active;
        }
    }
}
