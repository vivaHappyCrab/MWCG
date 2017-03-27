using System.Collections.Generic;
using MWCGClasses.GameObjects;
using MWCGClasses.Enums;

namespace MWCGClasses.InGame
{
    public class Player
    {
        #region Player(...)

        public Player(int race,Library deck,int num,Game g)
        {
            this.Race = race;
            this.Deck = deck;
            this.Num = num;

            this.Field = new BattleField(race, g) {Face = {Owner = this}};

            this.Hand = new List<Card>();
            this.Graves = new Graveyard();
        }

        #endregion

        #region Public Methods

        public void Kill(GameObject obj)
        {
            switch(obj.OType){
                case ObjectType.Creature:
                    this.Field.Units.Remove(obj as Unit);
                    this.Graves.Graves.Add(obj);
                    break;

                case ObjectType.Support:
                    this.Field.Supports.Remove(obj as Support);
                    this.Graves.Graves.Add(obj);
                    break;

                case ObjectType.Hero:
                    //Win_Lose Event
                    break;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Раса игрока(для енерации карт под неё)
        /// </summary>
        public int Race { get; set; }

        /// <summary>
        /// Колода игрока.
        /// </summary>
        public Library Deck { get; set; }

        /// <summary>
        /// Поле игрока, где расположены герой, юниты, сапорты.
        /// </summary>
        public BattleField Field { get; set; }

        /// <summary>
        /// Рука игрока.
        /// </summary>
        public List<Card> Hand { get; set; }

       /// <summary>
       /// Кладбище игрока.
       /// </summary>
        public Graveyard Graves { get; set; }


        public int Num { get; set; }

        public int Mana { get; set; }

        public int MaxMana { get; set; }

        #endregion
    }
}
