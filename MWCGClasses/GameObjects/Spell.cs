using System;
using System.Collections.Generic;
using System.Text;

namespace MWCGClasses.GameObjects
{
    public class Spell:GameObject
    {
        public Action Effect { get; set; }
        public Spell(int cardback, Action effect,int id,ObjectType otype=ObjectType.spell) : base(cardback,id,otype)
        {
            Effect = effect;
        }
    }
}
