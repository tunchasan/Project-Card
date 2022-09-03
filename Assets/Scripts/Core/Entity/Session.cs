using System;
using System.Collections.Generic;

namespace ProjectCard.Core.Entity
{
    public class Session : SessionBase
    {
        public override List<CardBase> Data()
        {
            if (IsInitialized)
            {
                return Cards;
            }

            throw new NullReferenceException("Session couldn't initialized!");
        }
        
        public override void ValidateSession()
        {
            IsInitialized = true;
            SessionId++;
        }
        
        public override void ValidateSession(List<CardBase> newCards)
        {
            Cards = newCards;
            IsInitialized = true;
            SessionId++;
        }
    }
}