using System.Collections.Generic;

namespace ProjectCard.Core.Entity
{
    public abstract class SessionBase
    {
        protected List<CardBase> Cards = new();
        public bool IsInitialized { protected set; get; }
        public int SessionId { protected set; get; }

        public abstract IEnumerable<CardBase> Data();
        public abstract void InitializeSession(List<CardBase> cards);
    }
}