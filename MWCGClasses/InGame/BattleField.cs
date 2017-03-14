using System.Collections.Generic;
using MWCGClasses.GameObjects;

namespace MWCGClasses.InGame
{
    public class BattleField
    {
        public Hero Face { get; set; }

        public List<Unit> Units { get; set; }

        public List<Support> Supports { get; set; }

        public BattleField(int heroid,Game g)
        {
            Face = g.Factory.GetHeroByRace(heroid);
            Units = new List<Unit>();
            Supports = new List<Support>();
        }
    }
}
