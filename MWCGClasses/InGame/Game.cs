using System;
using System.Collections.Generic;
using System.Linq;
using MWCGClasses.ClientInterface;
using MWCGClasses.Enums;
using MWCGClasses.GameObjects;
using MWData;

namespace MWCGClasses.InGame
{
    public class Game
    {
        #region Consts

        /// <summary>
        /// Standart hand size for classic plays.
        /// </summary>
        const int DefaultHandSize = 5;

        #endregion

        #region Fields

        /// <summary>
        /// Список всех игровых объектов в этой игре
        /// </summary>
        private readonly Dictionary<int, GameObject> _objects = new Dictionary<int, GameObject>();

        /// <summary>
        /// Номер хода.
        /// </summary>
        private int _turnCount;

        #endregion

        #region Game(...)

        /// <summary>
        /// Конструктор игры для 2х игроков
        /// </summary>
        /// <param name="race1">Раса 1-го игрока - влияет на героя и артефакт-абилку.</param>
        /// <param name="race2">Раса 2-го игрока - влияет на героя и артефакт-абилку.</param>
        /// <param name="deck1">Библиотека 1-го игрока</param>
        /// <param name="deck2">Библиотека 2-го игрока</param>
        /// <param name="f">Фабрика для генрации игровых объектов</param>
        public Game(int race1, int race2, PlayerDeck deck1, PlayerDeck deck2, Factory f)
        {
            this.Players = new List<Player>();
            this.Factory = f;
            Library lib = new Library(deck1, this, 0);
            this.Players.Add(new Player(race1, lib, 0, this));

            lib = new Library(deck2, this, 1);
            this.Players.Add(new Player(race2, lib, 1, this));

            foreach (Player p in this.Players)
                this._objects.Add(p.Field.Face.Id, p.Field.Face);
            this.Clients = new List<IClient>();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Инициализация игры.
        /// </summary>
        /// <param name="startHandSizes">Размер рук, для карт.</param>
        /// <param name="firstPlayer">Номер игрока, который ходит первым.</param>
        public void Start(int[] startHandSizes, int firstPlayer = 0)
        {
            foreach (Player player in this.Players)
            {
                player.Deck.Shuffle();
            }

            for (int i = 0; i < this.Players.Count; ++i)
            {
                int count = (startHandSizes != null && i < startHandSizes.Length) ? startHandSizes[i] : DefaultHandSize;

                for (int j = 0; j < count; ++j)
                {
                    Card card = this.Players[i].Deck.DrawCard(0);
                    if (card != null)
                        this.Players[i].Hand.Add(card);
                }
            }
            foreach (IClient client in this.Clients)
                client.GetGameState(this);

            this._turnCount = 1;
            this.GameCycle(this.Players[firstPlayer], this._turnCount);
        }

        public void DrawCards(Player player, int amount = 1)
        {
            for (int i = 0; i < amount; ++i)
            {
                Card card = player.Deck.DrawCard(0);
                if (card == null) return;
                player.Hand.Add(card);
                GameAction.OnDrawCard(this, card);
            }
        }

        public void GameCycle(Player player, int turn)
        {
            while (true)//todo: Win-Lose condition
            {
                if (player.MaxMana < 12)
                    player.MaxMana++;
                player.Mana = player.MaxMana;

                GameAction.OnTurnStart(this, player);
                this.DrawCards(player);

                this.MainPhase(player);
                this.AttackPhase(player);

                if (player.Num + 1 == this.Players.Count)
                {
                    player = this.Players[0];
                    turn = ++this._turnCount;
                }
                else
                {
                    player = this.Players[player.Num + 1];
                    turn = this._turnCount;
                }
            }
        }

        /// <summary>
        /// Основная фаза, в которрой доступен розыгрыш заклинаний и каст способностей юнитов, не имеющих статус мгновенной.
        /// </summary>
        /// <param name="player"></param>
        public void MainPhase(Player player)
        {
            bool endPhase = true;

            while (endPhase)
            {
                List<int> targets = player.Hand.Select(card => card.Id).ToList();
                if (!targets.Any()) return;

                Answer ans = this.Clients[player.Num].CreateAction(ActionType.HandCard, targets, "choose card to play");
                switch (ans.ActionType)
                {
                    case ActionType.HandCard:
                        GameAction.PlayCard(this, player.Hand.First(card => card.Id == ans.Target));
                        break;

                    case ActionType.Skip:
                        endPhase = false;
                        break;
                }
            }
        }

        /// <summary>
        /// Фаза выбора атакующих существ.
        /// </summary>
        /// <param name="player">Атакующий игрок.</param>
        public void AttackPhase(Player player)
        {
            bool endPhase = true;

            List<int> targets = new List<int>();
            foreach (Player opp in this.Players.Where(opp => opp != player))
            {
                targets.AddRange(opp.Field.Supports.Select(sup => sup.Id));
                targets.AddRange(opp.Field.Units.Select(unit => unit.Id));
                targets.Add(opp.Field.Face.Id);
            }

            List<int> actors = player.Field.Units.Select(unit => unit.Id).ToList();

            List<Tuple<int, int>> pairs = new List<Tuple<int, int>>();

            if (!actors.Any() || !targets.Any()) return;

            while (endPhase)
            {
                Answer ans = this.Clients[player.Num].CreateAction(ActionType.Attack, actors, targets, "choose pair to attack");
                switch (ans.ActionType)
                {
                    case ActionType.Attack:
                        pairs.Add(ans.Pair);
                        actors.Remove(ans.Pair.Item1);
                        if (!actors.Any())
                            endPhase = false;
                        break;

                    case ActionType.Skip:
                        endPhase = false;
                        break;
                }
            }

            List<Tuple<int, int>> defenders = this.DefendPhase(player, pairs);

            IEnumerable<Tuple<int, List<int>>> combatPairs = TargetList(pairs, defenders);

            //todo: Instant phase

            this.DamagePhase(player, combatPairs);
        }

        /// <summary>
        /// Фаза блокировки атакующих существ противника.
        /// </summary>
        /// <param name="attacker">Атакующий игрок.</param>
        /// <param name="pairs">Пары атакующий-цель.</param>
        /// <returns>Пары защищающий-атакующий.</returns>
        public List<Tuple<int, int>> DefendPhase(Player attacker, List<Tuple<int, int>> pairs)
        {
            List<Tuple<int, int>> result = new List<Tuple<int, int>>();

            foreach (Player player in this.Players)
            {
                if (player == attacker) continue;
                List<int> actors =
                    player.Field.Units.Where(unit => !ListOfPairsContains(pairs, unit.Id)) //Юнит не должен быть в списке атакуемых.
                        .Select(unit => unit.Id)
                        .ToList();
                if (!actors.Any()) continue;

                List<int> targets =
                    pairs.Where(pair => this._objects[pair.Item2].Owner == player)  //Доступные цели выбираются из тех, которые атакуют ваших юнитов.
                        .Select(pair => pair.Item1)
                        .ToList();
                if (!targets.Any()) continue;

                bool endPhase = true;

                while (endPhase)
                {
                    Answer ans = this.Clients[player.Num].CreateAction(ActionType.Attack, actors, targets,
                        "choose pair to defend");

                    switch (ans.ActionType)
                    {
                        case ActionType.Attack:
                            result.Add(ans.Pair);
                            actors.Remove(ans.Pair.Item1);
                            if (!actors.Any())
                                endPhase = false;
                            break;

                        case ActionType.Skip:
                            endPhase = false;
                            break;
                    }
                }
            }
            return result;
        }


        /// <summary>
        /// Удаление объекта с поля и вызов события 
        /// </summary>
        /// <param name="gameObject">Убиваемый объект</param>
        public void KillObject(GameObject gameObject)
        {
            gameObject.Owner.Kill(gameObject);
            GameAction.OnDeath(this, gameObject);
        }

        /// <summary>
        /// Событие нанесения урона объекту
        /// </summary>
        /// <param name="gameObject">Объект, принимающий урон</param>
        public void ObjectTakesDamage(GameObject gameObject)
        {
            //todo
        }

        /// <summary>
        /// Добавление на поля боя объекта
        /// </summary>
        /// <param name="perm">Объект на поле боя</param>
        /// <param name="owner">Владелец объекта</param>
        public void AddToBattleField(GameObject perm, int owner)
        {
            perm.Owner = this.Players[owner];
            GameAction.OnObjectEnter(this, perm);
            switch (perm.OType)
            {
                case ObjectType.Creature:
                    this.Players[owner].Field.Units.Add(perm as Unit);
                    break;

                case ObjectType.Support:
                    this.Players[owner].Field.Supports.Add(perm as Support);
                    break;
            }
        }

        /// <summary>
        /// Получение объекта по его id
        /// </summary>
        /// <param name="targetId">id объекта</param>
        /// <returns></returns>
        public GameObject ObjectById(int targetId)
        {
            return this._objects[targetId];
        }

        /// <summary>
        /// Создание нового объекта
        /// </summary>
        /// <param name="id">ID объекта</param>
        /// <returns>Игровой объект</returns>
        public GameObject CreateObject(int id)
        {
            GameObject obj = this.Factory.GetObjectById(id);
            this._objects.Add(obj.Id, obj);
            return obj;
        }

        #endregion

        #region Private methods

        private static bool ListOfPairsContains(IEnumerable<Tuple<int, int>> list, int target, bool first = false)
        {
            foreach (Tuple<int, int> pair in list)
            {
                if (first)
                {
                    if (target == pair.Item1)
                        return true;
                }
                else
                {
                    if (target == pair.Item2)
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Получение целей для атакующих юнитов.
        /// </summary>
        /// <param name="attackers">Пары атакующий - цель.</param>
        /// <param name="defenders">Пары защитник - атакующий.</param>
        /// <returns>Пары атакующий - список его реальных целей.</returns>
        private static IEnumerable<Tuple<int, List<int>>> TargetList(IEnumerable<Tuple<int, int>> attackers, List<Tuple<int, int>> defenders)
        {
            List<Tuple<int, List<int>>> result = new List<Tuple<int, List<int>>>();
            foreach (Tuple<int, int> item in attackers)
            {
                List<int> targets = defenders.Where(targ => targ.Item2 == item.Item1)
                .Select(targ => targ.Item1)
                .ToList();

                if (!targets.Any())
                    targets.Add(item.Item2);

                result.Add(new Tuple<int, List<int>>(item.Item1, targets));
            }

            return result;
        }

        private void DamagePhase(Player player, IEnumerable<Tuple<int, List<int>>> combatPairs)
        {
            foreach (Tuple<int, List<int>> pair in combatPairs)
            {
                Unit unit = this._objects[pair.Item1] as Unit;
                if (pair.Item2.Count < 2)
                {
                    Unit attacker = (this._objects[pair.Item1] as Unit);
                    attacker?.DealDamage(this, this._objects[pair.Item2[0]], attacker.Attack);
                }
                else if (unit != null && unit.Attack >= pair.Item2.Select(x => this._objects[x].Health).Sum())
                {
                    foreach (int i in pair.Item2)
                    {
                        unit?.DealDamage(this, this._objects[pair.Item2[0]], this._objects[pair.Item2[0]].Health);
                    }
                    //todo:Add original target for trample
                }
                else
                {
                    Answer ans = this.Clients[player.Num].CreateAction(ActionType.Sort, pair.Item2,pair.Item1.ToString());
                    int totalDamage = unit?.Attack ?? 0;
                    foreach (int target in ans.Targets)
                    {
                        GameObject tagetObject = this._objects[target];
                        if (totalDamage <= 0) break;
                        int damage = totalDamage > tagetObject.Health ? tagetObject.Health : totalDamage;
                        unit?.DealDamage(this, tagetObject, damage);
                        totalDamage -= damage;
                    }
                }
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Игроки в игре
        /// </summary>
        public List<Player> Players { get; set; }

        /// <summary>
        /// Фабрика, генерирующая объекты для игры
        /// </summary>
        public Factory Factory { get; set; }

        /// <summary>
        /// Обработчики клиентов игроков
        /// </summary>
        public List<IClient> Clients { get; set; }

        #endregion
    }
}
