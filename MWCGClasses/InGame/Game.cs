using System;
using System.Collections.Generic;
using System.Text;
using MWCGClasses.GameObjects;

namespace MWCGClasses.InGame
{
    public class Game
    {
        public Player[] Players { get; set; }

        public Game(int race1,int race2, Library lib1, Library lib2)
        {
            Players = new Player[2];
            Players[0] = new Player(race1,lib1,true,0);
            Players[1] = new Player(race2,lib2,false,1);
        }

        public void AddToBattleField(GameObject perm, int owner)
        {
            foreach (Player p in Players)
            {
                foreach (GameObject obj in p.Field.Units)
                    obj.onEnter(this, perm);
                foreach (GameObject obj in p.Field.Supports)
                    obj.onEnter(this, perm);
            }
            switch (perm.OType) {
                case ObjectType.creature:
                    {
                        Players[owner].Field.Units.Add(perm as Unit);
                        break;
                    }
                case ObjectType.support:
                    {
                        Players[owner].Field.Supports.Add(perm as Support);
                        break;
                    }
                 
            }      
        }
    }
}
