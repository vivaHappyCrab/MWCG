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
        public void TakeDamage(int dmg,Game g)
        {
            if (dmg > 0) {
                Health = Health - dmg;
                g.ObjectTakesDamage(this);
            }
            if (Health < 0)
                g.KillObject(this);

        }
        #region Props
        public int Id { get; set; }

        public int BackCard { get; set; }

        public int ObjectNum { get; set; }
        
        public int Health { get; set; }

        public int MaxHealth { get; set; }

        public Player Owner { get; set; }

        public ObjectType OType { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        #endregion
        #region Events
        public Event onSummon { get; set; }

        public Event onEnter { get; set; }

        public Event onDeath { get; set; }

        public Event onRemove { get; set; }

        public Event onTakeDamage { get; set; }

        public Event onAbilityCastStart { get; set; }
        
        public Event onAbilityCastCompleted { get; set; }
        #endregion

        public GameObject(int cardback,int id,ObjectType otype,string name, string desc)
        {
            BackCard = cardback;
            MaxHealth=Health = -1;
            ObjectNum = id;
            OType = otype;
            Name = name;
            Description = desc;
        }
        internal GameObject getCopy()
        {
            return MemberwiseClone() as GameObject;
        }
    }
}
