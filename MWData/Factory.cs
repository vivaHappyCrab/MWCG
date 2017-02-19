using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MWCGClasses.GameObjects;
using MWCGClasses.InGame;
using MWCGClasses;

namespace MWData
{
    public class Factory
    {
         List<Card> CardLibrary = new List<Card>();
         List<GameObject> ObjectsLibrary = new List<GameObject>();
         List<Race> RaceLibrary = new List<Race>();
         int ObjId = 1;
         int CardId = 1;
         public GameObject getObjectById(int id)
        {
            if (id <= 0) return null;
            GameObject o=ObjectsLibrary.Where(x => x.ObjectNum == id).First().getCopy();
            o.Id = ObjId++;
            return o;
        }

        public Hero GetHeroByRace(int id)
        {
            Hero h=getObjectById(
                RaceLibrary.Where(x => x.RaceId == id).First().HeroId)as Hero;
            h.Id= ObjId++;
            return h;
        }

        Event getEventById(int effect)
        {
            throw new NotImplementedException();
        }

         public Card getCardById(int id)
        {
            Card c= CardLibrary.Where(x => x.CardId == id).First().getCopy();
            c.Id = CardId++;
            return c;
        }

         public void InitLibs()
        {
            CardLibrary = DataAccessor.getCardList();
            ObjectsLibrary = DataAccessor.getObjectList();
            RaceLibrary = DataAccessor.getRaceList();
        }
    }
}
