using MWCGClasses;
using MWCGClasses.GameObjects;
using MWCGClasses.InGame;
using MWData;
using MWCGClasses.ClientInterface;

namespace MagicWarCG
{
    class Program
    {
        static void Main(string[] args)
        {
            Factory f = new Factory();
            f.InitLibs();

            Library firstLib = new Library();
            for (int i = 0; i < 10; i++)
                firstLib.Cards.Add(f.GetCardById(1));
            firstLib.Shuffle();

            Game game = new Game(1, 1, null, null,f);
            game.Clients.Add(new LocalConsoleClient());
            game.Clients.Add(new LocalConsoleClient());

            Card foot=new Card();
            foot.ManaCost = 1;
            foot.Races = new int[1];
            foot.Races[0] = 1;
            foot.Rarity = RareType.Common;
            foot.Type = CardType.Permanent;

            Unit footman = f.GetObjectById(1)as Unit;
            Card c = f.GetCardById(1);
            Card sp = f.GetCardToPlayer(5,0);

            GameAction.PlayCard(game, c);
            GameAction.PlayCard(game, sp);
        }
    }
}
