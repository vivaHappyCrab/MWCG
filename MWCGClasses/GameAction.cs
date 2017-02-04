using System;
using System.Collections.Generic;
using System.Text;
using MWCGClasses.InGame;
using MWCGClasses.GameObjects;

namespace MWCGClasses
{
    public delegate void Event(Game g, GameObject obj);
    class GameAction
    {
        public void PlayCard(Game game, Card card) { }
        
    }
}
