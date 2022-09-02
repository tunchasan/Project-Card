using System.Collections.Generic;
using System.Linq;
using ProjectCard.Core.Utilities;

namespace ProjectCard.Core.Entity
{
    public class DeckProvider : DeckProviderBase
    {
        protected override IEnumerable<CardBase> RandomCards(int amount)
        {
            Deck.Cards.Shuffle();
            var count = amount > DeckBase.Size ? DeckBase.Size : amount;
            return Deck.Cards.Take(count);
        }

        public override void DrawCards(int amount, out SessionBase session)
        {
            session = new Session();
            session.InitializeSession(RandomCards(amount));
        }

        public override void DrawCards(int amount, SessionBase session)
        {
            session.OverrideSession(RandomCards(amount).ToList());
        }
    }
}