using System;
using System.Collections.Generic;
using System.Text;

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
            Composition=new Dictionary<int, int>();
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
            if (Composition.ContainsKey(cardId))
                Composition[cardId] += amount;
            else
                Composition.Add(cardId, amount);

            return true;
        }
    }
}
