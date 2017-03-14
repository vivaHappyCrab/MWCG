namespace MWCGClasses
{
    public enum CardType
    {
        Permanent=0,
        Spell=1,
        Instant=2,
        Artefact=3, 
    };

    public enum RareType
    {
        Base=0,
        Common=1,
        Rare=2,
        Elite=3,
        Legendary=4
    };
    public class Card
    {
        public int ManaCost { get; set; }

        public int[] Races { get; set; }

        public CardType Type { get; set; }

        public int EntityId { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public bool Collectable { get; set; }

        public RareType Rarity { get; set; }

        public int CardId { get; set; }

        internal Card GetCopy()
        {
            return MemberwiseClone() as Card;
        }

        public int Owner { get; set; }

        public int Id { get; set; }
    }
}
