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
            var cards = DeckProvider.RandomCards(displayCardAmount);
            
            foreach (var card in cards)
            {
                ConditionalDebug.Log(card.ToString());
            }
        }
    }
}