﻿using MWCGClasses;
using MWCGClasses.InGame;
using MWData;
using MWCGClasses.ClientInterface;
using MWCGClasses.Enums;

namespace MagicWarCG
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Factory f = new Factory(new LocalAccessor());
            f.InitLibs();

            if (args.Length != 0)
                return;

            #region Create Test decks

            PlayerDeck deck1 = new PlayerDeck();
            deck1.AddCards(1, 4);
            deck1.AddCards(3, 2);
            deck1.AddCards(4, 2);
            deck1.AddCards(5);
            deck1.AddCards(6);

            PlayerDeck deck2 = new PlayerDeck();
            deck2.AddCards(1, 2);
            deck2.AddCards(3, 3);
            deck2.AddCards(4);
            deck2.AddCards(5, 2);
            deck2.AddCards(6, 2);

            #endregion

            Game game = new Game(1, 1, deck1, deck2, f);
            game.Clients.Add(new LocalConsoleClient(0));
            game.Clients.Add(new LocalConsoleClient(1));

            game.Start(null);

            Card foot = new Card
            {
                ManaCost = 1,
                Races = new int[1],
                Rarity = RareType.Common,
                Type = CardType.Permanent
            };

            foot.Races[0] = 1;
            
             
        }
    }
}
