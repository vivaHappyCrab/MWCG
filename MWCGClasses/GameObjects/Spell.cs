using MWCGClasses.Enums;

namespace MWCGClasses.GameObjects
{
    public class Spell:GameObject
    {
        public int Effect { get; set; }
        public Spell(int cardback, int id, int effect, string name, string desc, ObjectType otype=ObjectType.spell) : base(cardback,id,otype, name, desc)
        {
            Effect = effect;
        }
    }
}
