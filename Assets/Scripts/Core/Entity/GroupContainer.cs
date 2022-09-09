using System.Collections.Generic;
using System.Linq;
using ProjectCard.Core.Utilities;

namespace ProjectCard.Core.Entity
{
    public struct GroupContainer
    {
        public List<Group> Groups;
        public int Score;

        public List<CardBase> GetAllCards()
        {
            var allCards = new List<CardBase>();
            
            foreach (var @group in Groups.Where(@group => @group.Type != SortType.None))
            {
                allCards.AddRange(@group.Cards);
            }
            
            allCards.AddRange(GetRemainedGroup().Cards);

            return allCards;
        }
        public void ValidateScore()
        {
            Score = 0;
            
            foreach (var card in GetRemainedGroup().Cards)
            {
                Score += card.Score();
            }
        }
        public Group GetRemainedGroup()
        {
            return Groups.Find((group => group.Type == SortType.None));
        }
        public void RemoveGroup(Group member)
        {
            Groups.Remove(member);
        }
        public bool IsValid()
        {
            if (Groups == null) return false;
            
            if (Groups.Count == 0) return false;

            return Groups.Any(group => group.Type != SortType.None);
        }
    }
}