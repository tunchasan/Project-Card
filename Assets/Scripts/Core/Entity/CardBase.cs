using ProjectCard.Core.Utilities;

namespace ProjectCard.Core.Entity
{
    public abstract class CardBase
    {
        public int Id { get; private set; }
        public Kind Kind { get; protected set; }
        public Suit Suit { get; protected set; }
        public CardBase(int id)
        {
            Id = id;
        }
        public abstract int Score();
        public override string ToString()
        {
            var s1 = string.Concat(" || Id : ", Id.ToString());
            var s2 = string.Concat(" || Kind : ", Kind.ToString());
            var s3 = string.Concat(" || Suit : ", Suit.ToString());
            var s4 = string.Concat(" || Score : ", Score().ToString());
            return StringCombiner.Combine(s1, s2, s3, s4);
        }
    }
}