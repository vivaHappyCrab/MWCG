using System;
using System.Collections.Generic;
using System.Text;
using MWCGClasses.GameObjects;
using MWData;

namespace MWCGClasses.InGame
{
    public class BattleField
    {
        public Hero Face { get; set; }

        public List<Unit> Units { get; set; }

        public List<Support> Supports { get; set; }

        public BattleField(int heroid)
        {
            Face = Factory.GetHeroByRace(heroid);
            Units = new List<Unit>();
            Supports = new List<Support>();
        }
    }
}
