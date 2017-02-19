using System;
using System.Collections.Generic;
using System.Text;
using MWCGClasses.GameObjects;
using MWData;
using MWCGClasses.ClientInterface;

namespace MWCGClasses.InGame
{
    public class Game
    {
        public List<Player> Players { get; set; }

        public Factory Factory { get; set; }

        public List<Client> Clients { get; set; }

        public Game(int race1,int race2, Library lib1, Library lib2, Factory f)
        {
            Players = new List<Player>();
            Players.Add(new Player(race1,lib1,true,0,this));
            Players.Add(new Player(race2,lib2,false,1,this));
            Factory = f;
        }

        public void AddToBattleField(GameObject perm, int owner)
        {
            GameAction.onObjectEnter(this, perm);
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

        public GameObject ObjectById(int targetId)
        {
            foreach(Player p in Players)
            {
                foreach (Unit u in p.Field.Units)
                    if (u.Id == targetId) return u;
                foreach (Support s in p.Field.Supports)
                    if (s.Id == targetId) return s;
                if (p.Field.Face.Id == targetId) return p.Field.Face;
                foreach (Artifact a in p.Field.Face.Arts)
                    if (a.Id == targetId) return a;
                foreach (GameObject o in p.Graves.Graves)
                    if (o.Id == targetId) return o;
            }
            return null;
        }
    }
}
