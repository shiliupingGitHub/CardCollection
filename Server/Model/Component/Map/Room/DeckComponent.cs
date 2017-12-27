using System.Collections.Generic;

namespace Model
{
    public class DeckComponent : Component
    {
        public readonly List<Card> library = new List<Card>();

        public int CardsCount { get { return this.library.Count; } }
    }
}
