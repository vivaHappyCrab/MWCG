using System;
using System.Collections.Generic;
using System.Linq;
using MWCGClasses.GameObjects;
using MWData;
using MWCGClasses.ClientInterface;
using MWCGClasses.Enums;

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
        private int _turnCount = 0;

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
            Library lib = new Library(deck1, this,0);
            this.Players.Add(new Player(race1, lib, 0, this));

            lib = new Library(deck2, this,1);
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

            this._turnCount=1;
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
            while (true)
            {
                if(player.MaxMana<12)
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

        public void MainPhase(Player player)
        {
            bool endPhase = true;

            while (endPhase)
            {
                List<int> targets = player.Hand.Select(card => card.Id).ToList();
                if(!targets.Any())return;

                Answer ans = this.Clients[player.Num].CreateAction(ActionType.HandCard, targets,"choose card to play");
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

            List<Tuple<int,int>> pairs =new List<Tuple<int, int>>();

            if(!actors.Any()||!targets.Any())return;

            while (endPhase)
            {
                Answer ans = this.Clients[player.Num].CreateAction(ActionType.Attack, actors, targets, "choose pair 2 attack");
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
