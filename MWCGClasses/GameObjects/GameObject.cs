using System;
using System.Collections.Generic;
using System.Text;
using MWCGClasses.InGame;

namespace MWCGClasses.GameObjects
{
    public class GameObject
    {
        public Card BackCard { get; set; }

        public int ObjectNum { get; set; }
        
        public int Health { get; set; }

        public Player Owner { get; set; }

        public GameObject(Card back)
        {
            BackCard = back;
            Health = -1;
        }
    }
}
