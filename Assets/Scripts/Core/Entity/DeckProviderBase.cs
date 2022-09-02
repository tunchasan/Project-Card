namespace ProjectCard.Core.Entity
{
    public abstract class DeckProviderBase
    {
        protected readonly DeckBase Deck = null;
        protected DeckProviderBase()
        {
            Deck = new Deck();
        }
        public abstract void RandomCards(int amount, SessionBase sessionBase);
    }
}