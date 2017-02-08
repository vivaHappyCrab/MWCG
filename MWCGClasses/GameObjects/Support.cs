using System;
using System.Collections.Generic;
using System.Text;

namespace MWCGClasses.GameObjects
{
    public class Support : GameObject
    {
        public Support(int cardback,int id, int hp) : base(cardback,id,ObjectType.support)
        {
            Health = hp;
        }
    }
}
