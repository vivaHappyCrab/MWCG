using System;
using System.Collections.Generic;
using System.Text;

namespace MWCGClasses.GameObjects
{
    public class Hero : GameObject
    {
        public Hero(Card back,int hp, Artifact ability) : base(back)
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

        public bool First { get; set; }

        public int Attack { get; set; }

        public bool CanAttack { get; set; }
    }
}
