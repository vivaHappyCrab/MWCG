using System;
using System.Collections.Generic;

namespace MWCGClasses.InGame
{
    /// <summary>
    /// Библиотека игрока.
    /// </summary>
    public class Library
    {
        #region Fields

        /// <summary>
        /// Список карт.
        /// </summary>
        private List<Card> _cards = new List<Card>();

        #endregion

        /// <summary>
        /// Конструктор пустой библиотеки.
        /// </summary>
        public Library(PlayerDeck deck,Game g,int owner)
        {
            if(deck==null)
                return;
            if (g == null)
                throw new ArgumentNullException(nameof(g));

            foreach (KeyValuePair<int, int> pair in deck.Composition)
            {
                for (int i = 0; i < pair.Value; ++i)
                {
                    Card card = g.Factory.GetCardById(pair.Key);
                    card.Owner = owner;
                    this._cards.Add(card);
                }
            }
        }

        /// <summary>
        /// Перемешивание колоды.
        /// </summary>
        public void Shuffle()
        {
            Random rnd = new Random();
            List<Card> newLib = new List<Card>();
            while(this._cards.Count>0)
            {
                int n = rnd.Next(this._cards.Count);
                Card c = this._cards[n];
                this._cards.Remove(c);
                newLib.Add(c);
            }
            this._cards = newLib;
        }

        /// <summary>
        /// Взятие карты из библиотеки.
        /// </summary>
        /// <param name="cardnum">Номер карты в библиотеке.</param>
        /// <returns>Карту из библиотеки с заданным номером. Если взятие карты не возможно - вернёт null.</returns>
        public Card DrawCard(int cardnum)
        {
            if (cardnum >= this._cards.Count) return null;

            Card drawed = this._cards[cardnum];
            this._cards.Remove(drawed);
            return drawed;
        }

        #region Properties

        public int Count => this._cards.Count;

        #endregion
    }
}
