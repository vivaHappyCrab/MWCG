using System.Collections.Generic;
using MWCGClasses.GameObjects;

namespace MWCGClasses.InGame
{
    public class BattleField
    {
        public Hero Face { get; set; }

        public List<Unit> Units { get; set; } = new List<Unit>();

        public List<Support> Supports { get; set; } = new List<Support>();

        public BattleField(int heroid,Game g)
        {
            this.Face = g.Factory.GetHeroByRace(heroid);
        }
    }
}
