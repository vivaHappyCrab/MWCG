using MWCGClasses.Enums;
using MWCGClasses.InGame;
using MWData;
using System;
using System.Collections.Generic;
using System.Text;

namespace MWCGClasses.GameObjects
{
    public class Hero : GameObject
    {
        public Hero(int cardback,int id, string name, string desc, int hp, int ability) : 
            base(cardback,id,ObjectType.hero, name, desc)
        {
            MaxHealth = Health = hp;
            Default = ability;
            Arts = new Artifact[3];
            //Arts[2] = Factory.getObjectById(ability)as Artifact; TODO IN GAME INIT
            Attack = 0;
            CanAttack = false;
        }

        public Artifact[] Arts {get;set;}

        public int Default { get; set; }

        public int Attack { get; set; }

        public bool CanAttack { get; set; }
    }
}
