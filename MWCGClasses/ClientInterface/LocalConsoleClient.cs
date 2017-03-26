using System;
using System.Collections.Generic;
using System.Linq;
using MWCGClasses.GameObjects;
using MWCGClasses.InGame;

namespace MWCGClasses.ClientInterface
{
    /// <summary>
    /// Локальный консольный клиент для игры 1 на 1.
    /// </summary>
    public class LocalConsoleClient : IClient
    {
        #region Fields

        private Game _game = null;
        private readonly int _yourNum;
        private readonly int _enemyNum;

        #endregion

        public LocalConsoleClient(int yourNum)
        {
            this._yourNum = yourNum;
            this._enemyNum = yourNum == 0 ? 1 : 0;
        }

        private void DrawGame()
        {
            #region Draw enemy
            Player enemy = this._game.Players[this._enemyNum];

            Console.WriteLine("Opponent: ID={0}; Health={1}; Hand:{2};", enemy.Field.Face.Id, enemy.Field.Face.Health, enemy.Hand.Count);
            Console.WriteLine("OppManaPool: {5}/{6}; Opponent arts: Right - {0}({1}),Left - {2}({3}),Ability - {4}",enemy.Field.Face.Arts[0]?.Name, enemy.Field.Face.Arts[0]?.Id, 
                                                                                                                    enemy.Field.Face.Arts[1]?.Name, enemy.Field.Face.Arts[1]?.Id,
                                                                                                                    enemy.Field.Face.Arts[2]?.Name, enemy.Mana, enemy.MaxMana);
            Console.WriteLine("Oppdeck has {0} cards, graveyard has {1} cards;", enemy.Deck.Count, enemy.Graves.Graves.Count);

            Console.Write("Enemy supports:");
            foreach (Support support in enemy.Field.Supports)
            {
                Console.Write("{0}({1})H:{2};", support.Name, support.Id,support.Health);
            }
            Console.WriteLine();

            Console.Write("Enemy units:");
            foreach (Unit unit in enemy.Field.Units)
            {
                Console.Write("{0}({1})A/H:({2}/{3});", unit.Name, unit.Id,unit.Attack,unit.Health);
            }
            Console.WriteLine();
            #endregion

            #region Draw self
            Player self = this._game.Players[this._yourNum];

            Console.Write("Self units:");
            foreach (Unit unit in self.Field.Units)
            {
                Console.Write("{0}({1})A/H:({2}/{3});", unit.Name, unit.Id, unit.Attack, unit.Health);
            }
            Console.WriteLine();

            Console.Write("Self supports:");
            foreach (Support support in self.Field.Supports)
            {
                Console.Write("{0}({1})H:{2};", support.Name, support.Id, support.Health);
            }
            Console.WriteLine();

            Console.WriteLine("Deck has {0} cards, graveyard has {1} cards;", self.Deck.Count, self.Graves.Graves.Count);
            Console.WriteLine("ManaPool: {5}/{6}; Arts: Right - {0}({1}),Left - {2}({3}),Ability - {4}", self.Field.Face.Arts[0]?.Name, self.Field.Face.Arts[0]?.Id,
                                                                                                        self.Field.Face.Arts[1]?.Name, self.Field.Face.Arts[1]?.Id,
                                                                                                        self.Field.Face.Arts[2]?.Name, self.Mana, self.MaxMana);
            Console.WriteLine("You: ID={0}; Health={1}; Hand:{2};", self.Field.Face.Id, self.Field.Face.Health, self.Hand.Count);
            #endregion
        }

        public Answer CreateAction(ActionType type, List<int> targets)
        {
            Console.Clear();
            this.DrawGame();
            
            Console.Write("Your actions:");
            if(type==ActionType.HandCard)
                foreach (int target in targets)
                    Console.Write("{1}({0});", target,this._game.Players[this._yourNum].Hand.First(card => card.Id==target)?.Name);
            Console.WriteLine();
            while (true)
            {
                string s = Console.ReadLine();
                if(s=="")
                    return new Answer()
                    {
                        ActionType = ActionType.Skip,
                        Target = 0
                    };
                int n;
                if (!int.TryParse(s, out n)) n = -1;
                if (!targets.Select(x => x == n).Any()) continue;
                Answer a = new Answer
                {
                    ActionType = type,
                    Target = n
                };
                return a;
            }
        }

        public bool GetGameState(Game g)
        {
            this._game = g;
            return true;
        }
    }
}
