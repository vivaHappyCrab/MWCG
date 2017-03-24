﻿using System;
using System.Collections.Generic;
using MWData;

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
        public Library(PlayerDeck deck,Game g)
        {
            if(deck==null)
                return;
            if (g == null)
                throw new ArgumentNullException(nameof(g));

            foreach (KeyValuePair<int, int> pair in deck.Composition)
            {
                for(int i=0;i<pair.Value;++i)
                    this._cards.Add(g.Factory.GetCardById(pair.Key));
            }
        }

        /// <summary>
        /// Перемешивание колоды.
        /// </summary>
        public void Shuffle()
        {
            Random rnd = new Random();
            List<Card> newLib = new List<Card>();
            while(_cards.Count>0)
            {
                int n = rnd.Next(_cards.Count);
                Card c = _cards[n];
                _cards.Remove(c);
                newLib.Add(c);
            }
            _cards = newLib;
        }

        /// <summary>
        /// Взятие карты из библиотеки.
        /// </summary>
        /// <param name="cardnum">Номер карты в библиотеке.</param>
        /// <returns>Карту из библиотеки с заданным номером. Если взятие карты не возможно - вернёт null.</returns>
        public Card DrawCard(int cardnum)
        {
            if (cardnum >= _cards.Count) return null;

            Card drawed = _cards[cardnum];
            _cards.Remove(drawed);
            return drawed;
        }
    }
}
