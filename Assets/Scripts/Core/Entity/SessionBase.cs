using System.Collections.Generic;

namespace ProjectCard.Core.Entity
{
    public abstract class SessionBase
    {
        public IEnumerable<CardBase> Cards { protected set; get; }
        public abstract void InitializeSession(IEnumerable<CardBase> cards);
        public abstract void OverrideSession(List<CardBase> cards);
    }
}