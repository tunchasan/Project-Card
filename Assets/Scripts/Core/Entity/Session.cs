using System.Collections.Generic;

namespace ProjectCard.Core.Entity
{
    public class Session : SessionBase
    {
        public override void InitializeSession(IEnumerable<CardBase> cards)
        {
            Cards = cards;
        }
        public override void OverrideSession(List<CardBase> cards)
        {
            cards.Clear();
            Cards = cards;
        }
    }
}