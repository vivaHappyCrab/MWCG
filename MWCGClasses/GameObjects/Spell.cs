using MWData;
using System;
using System.Collections.Generic;
using System.Text;

namespace MWCGClasses.GameObjects
{
    public class Spell:GameObject
    {
        public Action Effect { get; set; }
        public Spell(int cardback, int effect,int id, string name, string desc, ObjectType otype=ObjectType.spell) : base(cardback,id,otype, name, desc)
        {
            Effect = Factory.getActionById(effect);
        }
    }
}
