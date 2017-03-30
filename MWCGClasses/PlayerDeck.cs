using System.Collections.Generic;

namespace MWCGClasses
{
    /// <summary>
    /// Колода игрока, представленная вне игры.
    /// </summary>
    public class PlayerDeck
    {
         /// <summary>
         /// Состав колоды.
         /// </summary>
         /// <remarks>Ключ - id, значение кол-во карт с этим id.</remarks>
         public Dictionary<int,int> Composition { get; set; }

        /// <summary>
        /// Создание новой колоды.
        /// </summary>
        public PlayerDeck()
        {
            this.Composition=new Dictionary<int, int>();
        } 

        /// <summary>
        /// Добавление карт в колоду.
        /// </summary>
        /// <param name="cardId">Id карты.</param>
        /// <param name="amount">Кол-во карт.</param>
        /// <returns></returns>
        public bool AddCards(int cardId, int amount=1)
        {
            //todo: validation
            if (this.Composition.ContainsKey(cardId))
                this.Composition[cardId] += amount;
            else
                this.Composition.Add(cardId, amount);

            return true;
        }
    }
}
