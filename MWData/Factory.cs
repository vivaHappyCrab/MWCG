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
        static List<Card> CardLibrary = new List<Card>();
        static List<GameObject> ObjectsLibrary = new List<GameObject>();
        static public GameObject getObjectById(int id)
        {
            return ObjectsLibrary.Where(x => x.ObjectNum == id).First();
        }
        static public Card getCardById(int id)
        {
            return CardLibrary.Where(x => x.CardId == id).First();
        }

        static public void InitLibs()
        {
            CardLibrary = DataAccessor.getCardList();
        }
    }
}
