using System.Collections.Generic;

namespace ProjectCard.Core.Entity
{
    public abstract class DeckProviderBase : ISortable, IDrawable
    {
        protected readonly DeckBase Deck = null;
        protected DeckProviderBase()
        {
            Deck = new Deck();
        }

        public abstract void DrawCertainCards(List<int> cardIds, SessionBase session);
        public abstract void DrawRandomCards(int amount, SessionBase sessionBase);
        public abstract void ShuffleSort(SessionBase session);
        public abstract void StraightSort(SessionBase session);
        public abstract void SameKindSort(SessionBase session);
        public abstract void SmartSort(SessionBase session);
    }
}