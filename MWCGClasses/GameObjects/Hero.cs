using System;
using System.Collections.Generic;
using System.Text;

namespace MWCGClasses.GameObjects
{
    public class Hero : GameObject
    {
        public Hero(int cardback,int id,int hp, Artifact ability) : base(cardback,id,ObjectType.hero)
        {
            Health = hp;
            Default = ability;
            Arts = new Artifact[3];
            Arts[2] = ability;
            Attack = 0;
            CanAttack = false;
        }

        public Artifact[] Arts {get;set;}

        public Artifact Default { get; set; }

        public int Attack { get; set; }

        public bool CanAttack { get; set; }
    }
}
