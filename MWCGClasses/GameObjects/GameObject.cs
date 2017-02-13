using System;
using System.Collections.Generic;
using System.Text;
using MWCGClasses.InGame;

namespace MWCGClasses.GameObjects
{
    public enum ObjectType
    {
        creature=0,
        support=1,
        hero=2,
        spell =3,
        ability=4,
        artifact=5
    }
    public class GameObject
    {
        public int BackCard { get; set; }

        public int ObjectNum { get; set; }
        
        public int Health { get; set; }

        public Player Owner { get; set; }

        public ObjectType OType { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Action onSummon { get; set; }

        public Action onEnter { get; set; }

        public Action onDeath { get; set; }

        public Action onRemove { get; set; }

        public Action onTakeDamage { get; set; }

        public Action onAbilityCastStart { get; set; }
        
        public Action onAbilityCastCompleted { get; set; } 

        public GameObject(int cardback,int id,ObjectType otype,string name, string desc)
        {
            BackCard = cardback;
            Health = -1;
            ObjectNum = id;
            OType = otype;
            Name = name;
            Description = desc;
        }
    }
}
