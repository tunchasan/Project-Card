using ProjectCard.Core.Utilities;

namespace ProjectCard.Core.Entity
{
    public class Card : CardBase
    {
        public Card(int id) : base(id)
        {
            Kind = (Kind) (Id % 13);
            Suit = (Suit) (Id / 13);
        }

        public override int Score()
        {
            var score = Kind switch
            {
                Kind.Jack => 10,
                Kind.Queen => 10,
                Kind.King => 10,
                _ => (int) Kind + 1
            };

            return score;
        }
    }
}