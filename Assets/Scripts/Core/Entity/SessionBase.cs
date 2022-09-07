using System;
using System.Collections.Generic;
using ProjectCard.Core.Utilities;

namespace ProjectCard.Core.Entity
{
    public abstract class SessionBase
    {
        protected List<CardBase> Cards = new(DeckBase.Size);
        public bool IsInitialized { protected set; get; }
        public int SessionId { protected set; get; }
        public ErrorCode ErrorCode { protected set; get; } = ErrorCode.None;

        public Action<ErrorCode> OnReceiveError;

        public abstract List<CardBase> Data();
        public abstract void ValidateSession();
        public abstract void ValidateSession(List<CardBase> cards);
        public abstract void ReceiveError(ErrorCode code);
        public abstract bool IsValidSession();
    }
}