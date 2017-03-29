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

        /// <summary>
        /// Нанесение урона физической атакой и получение ответа.
        /// </summary>
        /// <param name="game">Игра, в которой находятся объекты.</param>
        /// <param name="target">Цель-получатель урона.</param>
        /// <param name="amount"></param>
        public void DealDamage(Game game, GameObject target,int amount)
        {
            if(game==null)
                throw new ArgumentNullException(nameof(game));

            if(target==null)
                return;

            if (amount > this.Attack)
                return;

            GameAction.OnObjectDealsDamage(game, this);
            target.TakeDamage(game,amount,DamageType.Physical);
            Unit targetUnit=target as Unit;

            if (targetUnit == null || targetUnit.Attack <= 0) return;

            GameAction.OnObjectDealsDamage(game, targetUnit);
            this.TakeDamage(game, targetUnit.Attack,DamageType.Physical);
        }
    }
}
