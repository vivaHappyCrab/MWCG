using System;
using System.Collections.Generic;
using System.Text;

namespace MWCGClasses.GameObjects
{
    public enum ArtType
    {
        Right=0,
        Left=1,
        Ability=2
    };
    public class Artifact:GameObject
    {
        public Artifact(Card back, ArtType type=ArtType.Right, int charges=-1) : base(back)
        {
            Health = charges;
            Type = type;
        }

        public ArtType Type { get; set; }
    }
}
