using System;
using System.Collections.Generic;
using System.Text;

namespace MWCGClasses.GameObjects
{
    public class Spell:GameObject
    {
        public Action Effect { get; set; }
        public Spell(Card back,Action effect) : base(back)
        {
            Effect = effect;
        }
    }
}
