using System.Collections.Generic;
using ProjectCard.Core.Utilities;

namespace ProjectCard.Core.Entity
{
    public struct Group
    {
        public SortType Type;
        public List<CardBase> Cards;
    }
}