using System;
using System.Collections.Generic;

namespace MWCGClasses.InGame
{
    /// <summary>
    /// Библиотека игрока.
    /// </summary>
    public class Library
    {
        /// <summary>
        /// Конструктор пустой библиотеки.
        /// </summary>
        public Library()
        {
            Cards = new List<Card>();
        }

        public void Shuffle()
        {
            Random rnd = new Random();
            List<Card> newLib = new List<Card>();
            while(Cards.Count>0)
            {
                int n = rnd.Next(Cards.Count);
                Card c = Cards[n];
                Cards.Remove(c);
                newLib.Add(c);
            }
            Cards = newLib;
        }

        /// <summary>
        /// Список карт.
        /// </summary>
        public List<Card> Cards { get; set; }
    }
}
