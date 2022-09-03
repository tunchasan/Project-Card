using System.Collections.Generic;

namespace ProjectCard.Core.Entity
{
    public abstract class SessionBase
    {
        protected List<CardBase> Cards = new();
        public bool IsInitialized { protected set; get; }
        public int SessionId { protected set; get; }

        public abstract List<CardBase> Data();
        public abstract void ValidateSession();
        public abstract void ValidateSession(List<CardBase> cards);
    }
}