using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MWCGClasses;
using MWCGClasses.GameObjects;
using MWCGClasses.InGame;
using MWData;

namespace MagicWarCG
{
    class Program
    {
        static void Main(string[] args)
        {
            Player p = new Player();
            Card foot=new Card();
            foot.ManaCost = 1;
            foot.Races = new int[1];
            foot.Races[0] = 1;
            foot.Rarity = RareType.Common;
            foot.Type = CardType.Permanent;
            Unit footman = new Unit(foot);
            footman.Attack = 1;
            footman.Health = 1;
            footman.ObjectNum = 1;
            footman.Owner = p;
            Factory.InitLibs();
            Card c = Factory.getCardById(1);
        }
    }
}
