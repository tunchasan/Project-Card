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
            
            DeckProvider.RandomCards(displayCardAmount, Session);
            
            foreach (var card in Session.Data())
            {
                ConditionalDebug.Log(card.ToString());
            }
        }
    }
}