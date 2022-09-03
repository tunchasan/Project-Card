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
            
            Display();
            
            RequestStraightSort();
        }

        public void RequestShuffleSort()
        {
            Sortable.ShuffleSort(displayCardAmount, Session);
        }

        public void RequestStraightSort()
        {
            Sortable.StraightSort(Session);

            Display();
        }

        public void RequestSameKindSort()
        {
            Sortable.SameKindSort(Session);
        }

        public void RequestSmartSort()
        {
            Sortable.SmartSort(Session);
        }

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