﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MWCGClasses.GameObjects
{
    public class Hero : GameObject
    {
        public Artifact[] Arts {get;set;}

        public Artifact Default { get; set; }

        public bool First { get; set; }
    }
}
