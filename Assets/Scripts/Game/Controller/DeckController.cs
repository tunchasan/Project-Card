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
            
            Sortable.ShuffleSort(displayCardAmount, Session);
            
            foreach (var card in Session.Data())
            {
                ConditionalDebug.Log(card.ToString());
            }
            
            RequestStraightSort(Session);
        }

        public void RequestShuffleSort(SessionBase session)
        {
            Sortable.ShuffleSort(displayCardAmount, session);
        }

        public void RequestStraightSort(SessionBase session)
        {
            Sortable.StraightSort(session);
            
            ConditionalDebug.Log("-----------------------------------------------------------");
            
            foreach (var card in Session.Data())
            {
                ConditionalDebug.Log(card.ToString());
            }
        }

        public void RequestSameKindSort(SessionBase session)
        {
            Sortable.SameKindSort(session);
        }

        public void RequestSmartSort(SessionBase session)
        {
            Sortable.SmartSort(session);
        }
    }
}