using System.Collections.Generic;
using System.Linq;
using ProjectCard.Core.Utilities;

namespace ProjectCard.Core.Entity
{
    public class DeckProvider : DeckProviderBase
    {
        public override IEnumerable<CardBase> RandomCards(int amount)
        {
            Deck.Cards.Shuffle();

            var count = amount > DeckBase.Size ? DeckBase.Size : amount;
            
            return Deck.Cards.Take(count).ToArray();
        }
    }
}