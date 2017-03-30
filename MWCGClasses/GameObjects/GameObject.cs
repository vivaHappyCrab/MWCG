using MWCGClasses.InGame;
using MWCGClasses.Enums;

namespace MWCGClasses.GameObjects
{
    public class GameObject
    {
        #region GameObject(...)

        public GameObject(int cardback, int id, ObjectType otype, string name, string desc)
        {
            this.BackCard = cardback;
            this.MaxHealth = this.Health = -1;
            this.ObjectNum = id;
            this.OType = otype;
            this.Name = name;
            this.Description = desc;
        }

        public GameObject GetCopy()
        {
            return this.MemberwiseClone() as GameObject;
        }

        #endregion

        public virtual void TakeDamage(Game g, int dmg, DamageType type)
        {
            if (dmg > 0)
            {
                this.Health = this.Health - dmg;
                g.ObjectTakesDamage(this);
            }
            if (this.Health <= 0)
                g.KillObject(this);

        }

        #region Events

        /// <summary>
        /// Вызывается при призыве этого объекта из руки.
        /// </summary>
        public Event OnSummon { get; set; }

        /// <summary>
        /// Вызвается при появлении другого нового объекта на поле.
        /// </summary>
        public Event OnEnter { get; set; }

        /// <summary>
        /// Вызвается при смерти другого объекта.
        /// </summary>
        public Event OnPermDeath { get; set; }
        
        /// <summary>
        /// Вызывается при смерти самого объекта.
        /// </summary>
        public Event OnDeath { get; set; }

        public Event OnRemove { get; set; }

        public Event OnTakeDamage { get; set; }

        public Event OnDealDamage { get; set; }

        public Event OnAbilityCastStart { get; set; }

        public Event OnAbilityCastCompleted { get; set; }

        public Event OnSpellCastStart { get; set; }

        public Event OnSpellCastCompleted { get; set; }

        public PlayerEvent OnTurnStart { get; set; }

        public PlayerEvent OnTurnEnd { get; set; }

        public CardEvent OnDrawCard { get; set; }
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

        /// <summary>
        /// Признак возможности объекта сразу дейстовать(атаковать или кастовать способности)
        /// </summary>
        public bool Activated { get; set; } = false;

        /// <summary>
        /// Ключевые слова карты(автоматически дополняются в описание).
        /// </summary>
        public Keywords Keys { get; set; }=Keywords.None;

        #endregion
    }
}
