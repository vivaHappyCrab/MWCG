using MWCGClasses;
using MWCGClasses.InGame;
using MWData;
using MWCGClasses.ClientInterface;

namespace MagicWarCG
{
    internal class Program
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

            Card foot = new Card
            {
                ManaCost = 1,
                Races = new int[1],
                Rarity = RareType.Common,
                Type = CardType.Permanent
            };

            foot.Races[0] = 1;

            Card c = f.GetCardById(1);
            Card sp = f.GetCardToPlayer(5,0);

            GameAction.PlayCard(game, c);
            GameAction.PlayCard(game, sp);
        }
    }
}
