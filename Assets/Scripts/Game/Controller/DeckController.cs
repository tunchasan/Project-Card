using System.Collections.Generic;
using ProjectCard.Core.Entity;
using ProjectCard.Core.Utilities;
using ProjectCard.Game.Debug;
using ProjectCard.Game.Managers;
using ProjectCard.Game.Utilities;

namespace ProjectCard.Game.Controller
{
    public class DeckController : DeckControllerBase
    {
        protected override void Initialize()
        {
            DeckLayoutDynamicGroup = GetComponent<DeckLayoutDynamicGroupBase>();
            DeckProvider = new DeckProvider();
            Session = new Session();
            Session.OnReceiveError += UIManager.Instance.DisplayErrors;
            
            DrawCertainCardsRequest();
            GameManager.Instance.UpdateState(GameState.OnPlay);
        }
        public override void SortCardsRequest(SortType sortType)
        {
            switch (sortType)
            {
                case SortType.Shuffle:
                    DeckProvider.ShuffleSort(Session);
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
            
            if(Session.IsValidSession())
                DeckLayoutDynamicGroup.ValidateLayout(Session.Data());
            else
                ConditionalDebug.LogWarning($"Something went wrong during SortCardsRequest Process! - {Session.ErrorCode}");
        }
        protected override void DrawCertainCardsRequest()
        {
            var certainCards = new List<int> {26, 1, 17, 29, 0, 15, 42, 3, 13, 2, 16};
            
            DeckProvider.DrawCertainCards(certainCards, Session);
            
            if(Session.IsValidSession())
                DeckLayoutDynamicGroup.Initialize(Session.Data(), Display);
            else
                ConditionalDebug.LogWarning($"Something went wrong during DrawCertainCards Process! - {Session.ErrorCode}");
        }
        protected override void DrawRandomCardsRequest(int cardAmounts)
        {
            DeckProvider.DrawRandomCards(cardAmounts, Session);
            
            if(Session.IsValidSession())
                DeckLayoutDynamicGroup.Initialize(Session.Data(), Display);
            else
                ConditionalDebug.LogWarning($"Something went wrong during DrawRandomCards Process! - {Session.ErrorCode}");
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