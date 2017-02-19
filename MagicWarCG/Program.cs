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
            Factory f = new Factory();
            f.InitLibs();
            Game game = new Game(1, 1, null, null,f);
            Card foot=new Card();
            foot.ManaCost = 1;
            foot.Races = new int[1];
            foot.Races[0] = 1;
            foot.Rarity = RareType.Common;
            foot.Type = CardType.Permanent;
            Unit footman = f.getObjectById(1)as Unit;
            Card c = f.getCardById(1);
            GameAction.PlayCard(game, c);
        }
    }
}
