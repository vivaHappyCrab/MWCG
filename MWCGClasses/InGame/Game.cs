using System;
using System.Collections.Generic;
using System.Text;

namespace MWCGClasses.InGame
{
    public class Game
    {
        public Player[] Players { get; set; }

        public Game(int race1,int race2, Library lib1, Library lib2)
        {
            Players = new Player[2];
            Players[0] = new Player();
            Players[1] = new Player();
        }
    }
}
