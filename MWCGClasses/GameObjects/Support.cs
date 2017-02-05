using System;
using System.Collections.Generic;
using System.Text;

namespace MWCGClasses.GameObjects
{
    public class Support : GameObject
    {
        public Support(Card back,int hp) : base(back)
        {
            Health = hp;
        }
    }
}
