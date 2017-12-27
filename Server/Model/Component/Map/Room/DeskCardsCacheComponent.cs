using System.Collections.Generic;

namespace Model
{
    public class DeskCardsCacheComponent : Component
    {
        public readonly List<Card> library = new List<Card>();

        public readonly List<Card> LordCards = new List<Card>();

        public int CardsCount { get { return this.library.Count; } }

        public CardsType Rule { get; set; }

        public int MinWeight { get { return (int)this.library[0].CardWeight; } }
    }
}
