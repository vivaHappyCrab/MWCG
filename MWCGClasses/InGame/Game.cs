using System.Collections.Generic;
using MWCGClasses.GameObjects;
using MWData;
using MWCGClasses.ClientInterface;
using MWCGClasses.Enums;

namespace MWCGClasses.InGame
{
    public class Game
    {
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
        /// <param name="lib1">Библиотека 1-го игрока</param> todo: заменить просто на список типов карт и их кол-во и на его основе делать библиотеку
        /// <param name="lib2">Библиотека 2-го игрока</param>
        /// <param name="f">Фабрика для генрации игровых объектов</param>
        public Game(int race1, int race2, Library lib1, Library lib2, Factory f)
        {
            Players = new List<Player>();
            Factory = f;
            Players.Add(new Player(race1, lib1, true, 0, this));
            Players.Add(new Player(race2, lib2, false, 1, this));
            foreach (Player p in Players)
                _objects.Add(p.Field.Face.Id, p.Field.Face);
            Clients = new List<IClient>();
        }

        #endregion

        #region public methods

        /// <summary>
        /// Удаление объекта с поля и вызов события 
        /// </summary>
        /// <param name="gameObject">Убиваемый объект</param>
        public void KillObject(GameObject gameObject)
        {
            gameObject.Owner.Kill(gameObject);
            GameAction.OnDeath(this,gameObject);
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
