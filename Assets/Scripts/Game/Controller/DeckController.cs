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
            Sortable = new DeckProvider();
            Session = new Session();
            
            RequestShuffleSort();
        }

        [ContextMenu("RequestShuffleSort")]
        public void RequestShuffleSort()
        {
            Sortable.ShuffleSort(displayCardAmount, Session);
            
            Display();
        }

        [ContextMenu("RequestStraightSort")]
        public void RequestStraightSort()
        {
            Sortable.StraightSort(Session);

            Display();
        }

        [ContextMenu("RequestSameKindSort")]
        public void RequestSameKindSort()
        {
            Sortable.SameKindSort(Session);
            
            Display();
        }

        [ContextMenu("RequestSmartSort")]
        public void RequestSmartSort()
        {
            Sortable.SmartSort(Session);
            
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