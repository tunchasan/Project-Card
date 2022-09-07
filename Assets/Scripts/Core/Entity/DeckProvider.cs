using System.Collections.Generic;
using System.Linq;
using ProjectCard.Core.Utilities;

namespace ProjectCard.Core.Entity
{
    public class DeckProvider : DeckProviderBase
    {
        public override void DrawCertainCards(List<int> cardIds, SessionBase session)
        {
            var cards = new List<CardBase>(cardIds.Count);
            foreach (var cardId in cardIds)
                cards.Add(Deck.Cards.First(card => card.Id == cardId));
            session.ValidateSession(cards);
        }
        public override void DrawRandomCards(int amount, SessionBase session)
        {
            Deck.Cards.SortByShuffle();
            var count = amount > DeckBase.Size ? DeckBase.Size : amount;
            var cards = Deck.Cards.Take(count);
            // Stores the data inside of the Session
            session.ValidateSession(cards.ToList());
        }
        public override void ShuffleSort(SessionBase session)
        {
            var result = session.Data().SortByShuffle();
            session.ValidateSession(result);
        }
        public override void StraightSort(SessionBase session)
        {
            var result = session.Data().SortByStraight();
            
            if (result.IsValid())
            {
                session.ValidateSession(result.GetAllCards());
            }

            else
            {
                session.ReceiveError(ErrorCode.NoDataReceiveFromStraightRequest);
            }
        }
        public override void SameKindSort(SessionBase session)
        {
            var result = session.Data().SortByKind();

            if (result.IsValid())
            {
                session.ValidateSession(result.GetAllCards());
            }

            else
            {
                session.ReceiveError(ErrorCode.NoDataReceiveFromSameKindRequest);
            }
        }
        public override void SmartSort(SessionBase session)
        {
            var result = session.Data().SortBySmart();
            
            if (result.IsValid())
            {
                session.ValidateSession(result.GetAllCards());
            }

            else
            {
                session.ReceiveError(ErrorCode.NoDataReceiveFromSmartRequest);
            }
        }
    }
}