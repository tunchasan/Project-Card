using System.Collections.Generic;
using ProjectCard.Core.Utilities;

namespace ProjectCard.Core.Entity
{
    public struct Group
    {
        public SortType Type;
        public int Score;
        public List<CardBase> Cards;

        public void ValidateScore()
        {
            Score = 0;
            
            foreach (var card in Cards)
            {
                Score += card.Score();
            }
        }
    }
}