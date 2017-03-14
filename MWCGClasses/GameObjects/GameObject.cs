using MWCGClasses.InGame;
using MWCGClasses.Enums;

namespace MWCGClasses.GameObjects
{
    public class GameObject
    {
        #region Game(...)
        public GameObject(int cardback, int id, ObjectType otype, string name, string desc)
        {
            BackCard = cardback;
            MaxHealth = Health = -1;
            ObjectNum = id;
            OType = otype;
            Name = name;
            Description = desc;
        }

        public GameObject GetCopy()
        {
            return MemberwiseClone() as GameObject;
        }

        #endregion

        public virtual void TakeDamage(int dmg, Game g)
        {
            if (dmg > 0)
            {
                Health = Health - dmg;
                g.ObjectTakesDamage(this);
            }
            if (Health < 0)
                g.KillObject(this);

        }

        #region Events
        public Event onSummon { get; set; }

        public Event onEnter { get; set; }

        public Event onDeath { get; set; }

        public Event onRemove { get; set; }

        public Event onTakeDamage { get; set; }

        public Event onAbilityCastStart { get; set; }

        public Event onAbilityCastCompleted { get; set; }

        public Event onSpellCastStart { get; set; }

        public Event onSpellCastCompleted { get; set; }
        #endregion

        #region Props
        /// <summary>
        /// Номер объекта в игре
        /// </summary>
        /// <remarks>Уникален для каждого объекта</remarks>
        public int Id { get; set; }

        /// <summary>
        /// id карты отвечающей за создание объекта
        /// </summary>
        public int BackCard { get; set; }

        /// <summary>
        /// id типа объекта
        /// </summary>
        /// <remarks>Уникален для типа объекта</remarks>
        public int ObjectNum { get; set; }

        /// <summary>
        /// Здоровье объекта(прочность)
        /// </summary>
        public int Health { get; set; }

        /// <summary>
        /// Максимальное здоровье объекта
        /// </summary>
        public int MaxHealth { get; set; }

        /// <summary>
        /// Ссылка на игрока владельца объекта //todo: заменить на номер игрока?
        /// </summary>
        public Player Owner { get; set; }

        /// <summary>
        /// Тип объекта
        /// </summary>
        public ObjectType OType { get; set; }

        /// <summary>
        /// Имя объекта
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание объекта
        /// </summary>
        public string Description { get; set; }
        #endregion
    }
}
