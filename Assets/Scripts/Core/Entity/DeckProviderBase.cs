using System.Collections.Generic;

namespace ProjectCard.Core.Entity
{
    public abstract class DeckProviderBase
    {
        protected readonly DeckBase Deck = null;

        protected DeckProviderBase()
        {
            Deck = new Deck();
        }
        public abstract IEnumerable<CardBase> RandomCards(int amount);
    }
}