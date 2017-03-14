using MWCGClasses.Enums;

namespace MWCGClasses.GameObjects
{
    public class Support : GameObject
    {
        public Support(int cardback, int id, string name, string desc, int hp) :
            base(cardback, id, ObjectType.Support, name, desc)
        {
            MaxHealth = Health = hp;
        }
    }
}