using System.Collections.Generic;
using System.Linq;
using ProjectCard.Core.Utilities;

namespace ProjectCard.Core.Entity
{
    public class DeckProvider : DeckProviderBase
    {
        public override IEnumerable<CardBase> RandomCards(int amount, bool useSession)
        {
            Deck.Cards.Shuffle();
            var count = amount > DeckBase.Size ? DeckBase.Size : amount;
            var cards = Deck.Cards.Take(count);

            // Stores the data inside of the Session
            if (useSession)
                Session.InitializeSession(cards.ToList());
            
            return Deck.Cards.Take(count);
        }
    }
}