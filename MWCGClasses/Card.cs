using MWCGClasses.Enums;

namespace MWCGClasses
{
    public enum RareType
    {
        Base=0,
        Common=1,
        Rare=2,
        Elite=3,
        Legendary=4
    };
    public class Card
    {
        /// <summary>
        ///   Стоимость розыгрыша.
        /// </summary>
        public int ManaCost { get; set; }

        /// <summary>
        /// Расы, для которых доступна карта.
        /// </summary>
        public int[] Races { get; set; }

        /// <summary>
        /// Тип карты
        /// </summary>
        public CardType Type { get; set; }

        /// <summary>
        /// Сущность соотносящаяся с розыгрышем карты.(0=нету)
        /// </summary>
        public int EntityId { get; set; }

        /// <summary>
        /// Описание карты.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Имя карты.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Коллекционируемость карты.
        /// </summary>
        public bool Collectable { get; set; }

        /// <summary>
        /// Редкость.
        /// </summary>
        public RareType Rarity { get; set; }

        /// <summary>
        /// Идентификатор типа карты.
        /// </summary>
        public int CardId { get; set; }
        
        /// <summary>
        /// Владелец карты.
        /// </summary>
        public int Owner { get; set; }

        /// <summary>
        /// Идентификатор карты в игре.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Копирование карты.
        /// </summary>
        /// <returns>Копия карты с уникальным впоследствии.</returns>
        internal Card GetCopy()
        {
            return this.MemberwiseClone() as Card;
        }
    }
}
