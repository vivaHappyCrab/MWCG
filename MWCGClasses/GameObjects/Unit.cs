using System;
using MWCGClasses.Enums;
using MWCGClasses.InGame;

namespace MWCGClasses.GameObjects
{
    public class Unit : GameObject
    {
        public int Attack { get; set; }
        public Unit(int cardback, int id, string name, string desc, int attack, int hp) : 
            base(cardback, id, ObjectType.Creature, name, desc)
        {
            this.Attack = attack;
            this.MaxHealth = this.Health = hp;
        }

        public void DealDamage(Game game, GameObject target,int amount)
        {
            if(game==null)
                throw new ArgumentNullException(nameof(game));

            if(target==null)
                return;

            if (amount > this.Attack)
                return;
            if (amount==0)return;

            GameAction.OnObjectDealsDamage(game, this);
            target.TakeDamage(game,amount,DamageType.Physical);
        }
    }
}
