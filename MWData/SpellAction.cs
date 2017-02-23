using MWCGClasses;
using MWCGClasses.GameObjects;
using MWCGClasses.InGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWData
{
    public class SpellAction
    {
        public static void FireBall(Game g, GameObject obj)
        {
            List<int> avtargets = new List<int>();
            foreach(Player p in g.Players)
            {
                foreach (Unit u in p.Field.Units)
                    avtargets.Add(u.Id);
                foreach (Support s in p.Field.Supports)
                    avtargets.Add(s.Id);
                avtargets.Add(p.Field.Face.Id);
            }
            int targetId = g.Clients[obj.Owner.Num].CreateAction(MWCGClasses.ClientInterface.ActionType.FieldObjects,avtargets).Target;
            GameObject target = g.ObjectById(targetId);
            target.TakeDamage(4,g);
        }
    }
}
