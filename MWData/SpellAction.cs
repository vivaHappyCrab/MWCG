using MWCGClasses.GameObjects;
using MWCGClasses.InGame;
using System.Collections.Generic;
using System.Linq;

namespace MWData
{
    public class SpellAction
    {
        public static void FireBall(Game g, GameObject obj)
        {
            List<int> avtargets = new List<int>();
            foreach(Player p in g.Players)
            {
                avtargets.AddRange(p.Field.Units.Select(u => u.Id));
                avtargets.AddRange(p.Field.Supports.Select(s => s.Id));

                avtargets.Add(p.Field.Face.Id);
            }
            int targetId = g.Clients[obj.Owner.Num].CreateAction(MWCGClasses.ClientInterface.ActionType.FieldObjects,avtargets).Target;
            GameObject target = g.ObjectById(targetId);
            target.TakeDamage(4,g);
        }
    }
}
