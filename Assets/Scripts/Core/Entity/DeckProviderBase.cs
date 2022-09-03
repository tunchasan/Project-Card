namespace ProjectCard.Core.Entity
{
    public abstract class DeckProviderBase : ISortable
    {
        protected readonly DeckBase Deck = null;
        protected DeckProviderBase()
        {
            Deck = new Deck();
        }
        public abstract void ShuffleSort(int amount, SessionBase sessionBase);
        public abstract void StraightSort(SessionBase session);
        public abstract void SameKindSort(SessionBase session);
        public abstract void SmartSort(SessionBase session);
    }
}