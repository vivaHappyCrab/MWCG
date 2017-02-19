using System;
using System.Collections.Generic;
using System.Text;
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
