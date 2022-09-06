using System.Collections.Generic;

namespace ProjectCard.Core.Entity
{
    public abstract class DeckBase
    {
        public const int Size = 52;
        public readonly List<CardBase> Cards;
        protected DeckBase()
        {
            Cards = new List<CardBase>(Size);
            
            for (var i = 0; i < Size; i++)
            {
                Cards.Add(new Card(i));
            }
        }
    }
}