using System;
using System.Collections.Generic;
using System.Text;

namespace MWCGClasses.GameObjects
{
    public class Support : GameObject
    {
        public Support(int cardback,int id, string name, string desc, int hp) : base(cardback,id,ObjectType.support, name, desc)
        {
            Health = hp;
        }
    }
}
