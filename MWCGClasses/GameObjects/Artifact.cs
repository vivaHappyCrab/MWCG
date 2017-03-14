﻿using MWCGClasses.Enums;

namespace MWCGClasses.GameObjects
{
    public class Artifact:GameObject
    {
        public Artifact(int cardback,int id, string name, string desc, ArtType type=ArtType.Right, int charges=-1) : 
            base(cardback,id,ObjectType.artifact, name, desc)
        {
            Health = charges;
            Type = type;
        }

        public ArtType Type { get; set; }
    }
}
