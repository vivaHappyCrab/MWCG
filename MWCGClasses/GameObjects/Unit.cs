using System;
using System.Collections.Generic;
using System.Text;

namespace MWCGClasses.GameObjects
{
    public class Unit : GameObject
    {
        public int Attack { get; set; }
        public Unit(int cardback,int id, string name, string desc,int attack,int hp) : base(cardback,id,ObjectType.creature,name,desc)
        {
            Attack = attack;
            MaxHealth = Health = hp;
        }
    }
}
