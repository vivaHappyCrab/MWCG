using System;
using System.Collections.Generic;
using System.Text;
using MWCGClasses.GameObjects;

namespace MWCGClasses.InGame
{
    public class BattleField
    {
        public Hero Face { get; set; }

        public List<Unit> Units { get; set; }

        public List<Support> Supports { get; set; }
    }
}
