using System.Linq;

namespace ProjectCard.Core.Entity
{
    public class DeckProvider : DeckProviderBase
    {
        public override void ShuffleSort(int amount, SessionBase session)
        {
            Deck.Cards.SortByShuffle();
            var count = amount > DeckBase.Size ? DeckBase.Size : amount;
            var cards = Deck.Cards.Take(count);
            // Stores the data inside of the Session
            session.ValidateSession(cards.ToList());
        }

        public override void StraightSort(SessionBase session)
        {
            var result = Sorter.SortByStraight(session.Data());
            session.ValidateSession(result);
        }

        public override void SameKindSort(SessionBase session)
        {
            session.Data().SortByKind();
            session.ValidateSession();
        }

        public override void SmartSort(SessionBase session)
        {
            session.Data().SortBySmart();
            session.ValidateSession();
        }
    }
}