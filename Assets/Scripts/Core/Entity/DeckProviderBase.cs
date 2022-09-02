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
        protected abstract IEnumerable<CardBase> RandomCards(int amount);
        public abstract void DrawCards(int amount, out SessionBase session);
        public abstract void DrawCards(int amount, SessionBase session);
    }
}