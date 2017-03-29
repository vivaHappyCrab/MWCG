using System.Collections.Generic;
using System.Linq;
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

        public List<GameObject> Permanents => new List<GameObject>().Union(this.Units).Union(this.Supports).Union(new List<GameObject>() {this.Face}).ToList();
    }
}
