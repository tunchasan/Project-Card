namespace ProjectCard.Core.Entity
{
    public abstract class DeckBase
    {
        public const int Size = 52;
        public CardBase[] Cards { get; private set; } = new CardBase[Size];
        protected DeckBase()
        {
            for (var i = 0; i < Size; i++)
            {
                Cards[i] = new Card(i);
            }
        }
    }
}