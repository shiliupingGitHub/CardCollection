using System.Collections.Generic;

namespace Model
{
    public class HandCardsComponent : Component
    {
        public readonly List<Card> library = new List<Card>();

        public Identity AccessIdentity { get; set; }

        public bool IsAuto { get; set; }

        public int CardsCount { get { return library.Count; } }
    }
}
