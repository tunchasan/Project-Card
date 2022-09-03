using System.Collections.Generic;

namespace ProjectCard.Core.Entity
{
    public interface IDrawable
    {
        public void DrawCertainCards(List<int> cardIds, SessionBase session);
        public void DrawRandomCards(int amount, SessionBase sessionBase);
    }
}