using System.Collections.Generic;
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
            Players = new List<Player>();
            Factory = f;
            Library lib=new Library(deck1,this);
            Players.Add(new Player(race1, lib, true, 0, this));

            lib = new Library(deck2, this);
            Players.Add(new Player(race2, lib, false, 1, this));

            foreach (Player p in Players)
                _objects.Add(p.Field.Face.Id, p.Field.Face);
            Clients = new List<IClient>();
        }

        #endregion

        #region public methods

        /// <summary>
        /// Инициализация игры.
        /// </summary>
        /// <param name="startHandSizes">Размер рук, для карт.</param>
        /// <param name="firstPlayer">Номер игрока, который ходит первым.</param>
        public void Start(int[] startHandSizes, int firstPlayer = 0)
        {
            foreach (Player player in Players)
            {
                player.Deck.Shuffle();
            }

            for (int i = 0; i < Players.Count; ++i)
            {
                int count = (startHandSizes!=null && i < startHandSizes.Length) ? startHandSizes[i] : DefaultHandSize;

                for (int j = 0; j < count; ++j)
                {
                    Card card = Players[i].Deck.DrawCard(0);
                    if (card != null)
                        Players[i].Hand.Add(card);
                }
            }
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
            perm.Owner = Players[owner];
            GameAction.OnObjectEnter(this, perm);
            switch (perm.OType)
            {
                case ObjectType.Creature:
                    Players[owner].Field.Units.Add(perm as Unit);
                    break;

                case ObjectType.Support:
                    Players[owner].Field.Supports.Add(perm as Support);
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
            return _objects[targetId];
        }

        /// <summary>
        /// Создание нового объекта
        /// </summary>
        /// <param name="id">ID объекта</param>
        /// <returns>Игровой объект</returns>
        public GameObject CreateObject(int id)
        {
            GameObject obj = Factory.GetObjectById(id);
            _objects.Add(obj.Id, obj);
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
