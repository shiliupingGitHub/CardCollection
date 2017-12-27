using System;

namespace Model
{
    /// <summary>
    /// 牌类
    /// </summary>
    public class Card : IEquatable<Card>
    {
        public Weight CardWeight { get; private set; }
        public Suits CardSuits { get; private set; }

        public Card(Weight weight, Suits suits)
        {
            this.CardWeight = weight;
            this.CardSuits = suits;
        }

        public bool Equals(Card other)
        {
            return this.CardWeight == other.CardWeight && this.CardSuits == other.CardSuits;
        }

        public string GetName()
        {
            return this.CardSuits == Suits.None ? this.CardWeight.ToString() : $"{this.CardSuits.ToString()}{this.CardWeight.ToString()}";
        }
    }
}
