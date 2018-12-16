using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MWCGClasses;
using MWCGClasses.GameObjects;
using MWCGClasses.InGame;

namespace MWData
{
    public class LocalFactory : IFactory
    {
        public Card GetCardById(int id)
        {
            throw new NotImplementedException();
        }

        public Card GetCardToPlayer(int id, int playerNum)
        {
            throw new NotImplementedException();
        }

        public Event GetEventById(int effect)
        {
            throw new NotImplementedException();
        }

        public Hero GetHeroByRace(int id)
        {
            throw new NotImplementedException();
        }

        public GameObject GetObjectById(int id)
        {
            throw new NotImplementedException();
        }

        public GameObject GetObjectToPlayer(int id, Player player)
        {
            throw new NotImplementedException();
        }

        public void InitLibs()
        {
            throw new NotImplementedException();
        }
    }
}
