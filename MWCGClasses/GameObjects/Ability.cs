using MWCGClasses.Enums;

namespace MWCGClasses.GameObjects
{
    public class Ability:Spell
    {
        public int Cost { get; set; }

        public bool Active { get; set; }

        public Ability(int cardback,int id, int effect,int cost, bool active, string name, string desc) : base(cardback, effect, id, name, desc, ObjectType.Ability)
        {
            this.Cost = cost;
            this.Active = active;
        }
    }
}
