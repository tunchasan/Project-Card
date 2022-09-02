using System;
using System.Collections.Generic;

namespace ProjectCard.Core.Entity
{
    public class Session : SessionBase
    {
        public override IEnumerable<CardBase> Data()
        {
            if (IsInitialized)
            {
                return Cards;
            }

            throw new NullReferenceException("Session couldn't initialized!");
        }
        public override void InitializeSession(List<CardBase> newCards)
        {
            Cards?.Clear();
            Cards = newCards;
            IsInitialized = true;
            SessionId++;
        }
    }
}