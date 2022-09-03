using System.Collections.Generic;
using ProjectCard.Core.Entity;
using ProjectCard.Game.Debug;
using UnityEngine;

namespace ProjectCard.Game.Controller
{
    public class DeckController : DeckControllerBase
    {
        [Header("Configurations")]
        [Range(11, 52)]
        [SerializeField] private int displayCardAmount = 11;

        protected override void Initialize()
        {
            DeckProvider = new DeckProvider();
            Session = new Session();
            
            DrawCertainCards();
        }
        
        [ContextMenu("DrawCertainCards")]
        public void DrawCertainCards()
        {
            var certainCards = new List<int> {26, 1, 17, 29, 0, 15, 42, 3, 13, 2, 16};
            
            DeckProvider.DrawCertainCards(certainCards, Session);
            
            Display();
        }

        [ContextMenu("DrawRandomCards")]
        public void DrawRandomCards()
        {
            DeckProvider.DrawRandomCards(displayCardAmount, Session);
            
            Display();
        }

        [ContextMenu("RequestStraightSort")]
        public void RequestStraightSort()
        {
            DeckProvider.StraightSort(Session);

            Display();
        }

        [ContextMenu("RequestSameKindSort")]
        public void RequestSameKindSort()
        {
            DeckProvider.SameKindSort(Session);
            
            Display();
        }

        [ContextMenu("RequestSmartSort")]
        public void RequestSmartSort()
        {
            DeckProvider.SmartSort(Session);
            
            Display();
        }

        [ContextMenu("Display")]
        public void Display()
        {
            ConditionalDebug.Log("-----------------------------------------------------------");
            
            foreach (var card in Session.Data())
            {
                ConditionalDebug.Log(card.ToString());
            }
        }
    }
}