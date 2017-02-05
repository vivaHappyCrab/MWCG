using System;
using System.Collections.Generic;
using System.Text;
using MWCGClasses.InGame;
using MWCGClasses.GameObjects;
using MWData;

namespace MWCGClasses
{
    public delegate void Event(Game g, GameObject obj);
    class GameAction
    {
        public void PlayCard(Game game, Card card) {
            switch (card.Type) { 
                case CardType.Permanent:{
                        
                        break;
                }
            }
        }

        public void onObjectEnter(Game game, GameObject obj) { }

        public void onObjectSummons(Game game, GameObject obj) { }
    }
}
