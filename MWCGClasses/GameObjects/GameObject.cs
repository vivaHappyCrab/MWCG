using System;
using System.Collections.Generic;
using System.Text;

namespace MWCGClasses.GameObjects
{
    public class GameObject
    {
        public Card BackCard { get; set; }

        public int ObjectNum { get; set; }

        public GameObject(Card back)
        {
            BackCard = back;
        }
    }
}
