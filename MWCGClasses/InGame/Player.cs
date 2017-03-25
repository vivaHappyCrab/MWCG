using System.Collections.Generic;
using MWCGClasses.GameObjects;
using MWCGClasses.Enums;

namespace MWCGClasses.InGame
{
    public class Player
    {
        #region Player(...)

        public Player(int race,Library deck,bool first,int num,Game g)
        {
            Race = race;
            Deck = deck;
            First = first;
            Num = num;
            Field = new BattleField(race,g);
            Hand = new List<Card>();
            Graves = new Graveyard();
        }

        #endregion

        #region Public Methods

        public void Kill(GameObject obj)
        {
            switch(obj.OType){
                case ObjectType.Creature:
                    Field.Units.Remove(obj as Unit);
                    Graves.Graves.Add(obj);
                    break;

                case ObjectType.Support:
                    Field.Supports.Remove(obj as Support);
                    Graves.Graves.Add(obj);
                    break;

                case ObjectType.Hero:
                    //Win_Lose Event
                    break;
            }
        }

        #endregion

        #region Properties

        public int Race { get; set; }

        public Library Deck { get; set; }

        public BattleField Field { get; set; }

        public List<Card> Hand { get; set; }

        public Graveyard Graves { get; set; }

        public bool First { get; set; }

        public int Num { get; set; }

        #endregion
    }
}
