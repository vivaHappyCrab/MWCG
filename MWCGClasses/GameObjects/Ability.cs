using MWCGClasses.Enums;

namespace MWCGClasses.GameObjects
{
    public class Ability:Card
    {
        public bool Active { get; set; }

        public int MaxUsages { get; set; }

        public int Usages { get; set; }
    }
}
