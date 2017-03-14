using System.Collections.Generic;
using MWCGClasses.GameObjects;

namespace MWCGClasses.InGame
{
    public class Graveyard
    {
        public List<GameObject> Graves { get; set; }
        public List<GameObject> Exiles { get; set; }

        public Graveyard()
        {
            Graves = new List<GameObject>();
            Exiles = new List<GameObject>();
        }
    }
}
