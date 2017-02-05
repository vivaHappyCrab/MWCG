using System;
using System.Collections.Generic;
using System.Text;

namespace MWCGClasses.GameObjects
{
    public class Barrack : GameObject
    {
        public Barrack(Card back,int hp) : base(back)
        {
            Health = hp;
        }
    }
}
