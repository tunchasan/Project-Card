using System;
using System.Collections.Generic;
using ProjectCard.Core.Utilities;

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
            ErrorCode = ErrorCode.None;
            OnReceiveError?.Invoke(ErrorCode.None);
        }
        
        public override void ValidateSession(List<CardBase> newCards)
        {
            Cards = newCards;
            IsInitialized = true;
            SessionId++;
            ErrorCode = ErrorCode.None;
            OnReceiveError?.Invoke(ErrorCode.None);
        }

        public override void ReceiveError(ErrorCode code)
        {
            ErrorCode = code;
            OnReceiveError?.Invoke(ErrorCode);
        }

        public override bool IsValidSession()
        {
            return IsInitialized && ErrorCode == ErrorCode.None;
        }
    }
}