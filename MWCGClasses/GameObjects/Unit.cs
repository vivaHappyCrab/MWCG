using System;
using System.Collections.Generic;
using System.Text;

namespace MWCGClasses.GameObjects
{
    public class Unit : GameObject
    {
        public int Attack { get; set; }
        public Unit(Card back) : base(back)
        {
        }
    }
}
