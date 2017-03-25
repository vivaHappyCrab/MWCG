using MWCGClasses.Enums;

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
    }
}
