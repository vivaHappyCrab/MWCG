using System.Linq;
using MWCGClasses.Enums;
using MWCGClasses.GameObjects;
using MWCGClasses.InGame;

namespace MWData
{
    public static class EnterAction
    {
        public static void YouthBerserk(Game g, GameObject obj)
        {
            foreach (Unit unit in obj.Owner.Field.Units.ToList())
                unit.TakeDamage(g,1,DamageType.Magical);
        }
    }
}