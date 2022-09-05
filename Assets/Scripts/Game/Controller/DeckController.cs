using System.Collections.Generic;
using ProjectCard.Core.Entity;
using ProjectCard.Core.Utilities;
using ProjectCard.Game.Debug;
using ProjectCard.Game.Managers;

namespace ProjectCard.Game.Controller
{
    public class DeckController : DeckControllerBase
    {
        protected override void Initialize()
        {
            DeckProvider = new DeckProvider();
            Session = new Session();
            
            DrawCertainCards();
            
            // TODO
            // Update the Code
            GameManager.OnGamePlay?.Invoke();
        }
        public override void RequestSort(SortType sortType)
        {
            switch (sortType)
            {
                case SortType.Shuffle:
                    // TODO
                    break;
                case SortType.Straight:
                    DeckProvider.StraightSort(Session);
                    break;
                case SortType.SameKind:
                    DeckProvider.SameKindSort(Session);
                    break;
                case SortType.Smart:
                    DeckProvider.SmartSort(Session);
                    break;
            }
            
            Display();
        }
        protected override void DrawCertainCards()
        {
            var certainCards = new List<int> {26, 1, 17, 29, 0, 15, 42, 3, 13, 2, 16};
            
            DeckProvider.DrawCertainCards(certainCards, Session);
            
            Display();
        }
        protected override void DrawRandomCards(int cardAmounts)
        {
            DeckProvider.DrawRandomCards(cardAmounts, Session);
            
            Display();
        }
        protected override void Display()
        {
            ConditionalDebug.Log("-----------------------------------------------------------");
            
            foreach (var card in Session.Data())
            {
                ConditionalDebug.Log(card.ToString());
            }
        }
    }
}