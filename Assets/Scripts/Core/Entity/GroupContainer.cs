using System.Collections.Generic;
using ProjectCard.Core.Utilities;

namespace ProjectCard.Core.Entity
{
    public struct GroupContainer
    {
        public List<Group> Groups;
        public Group GetRemainedGroup()
        {
            return Groups.Find((group => group.Type == SortType.None));
        }

        public void RemoveGroup(Group member)
        {
            Groups.Remove(member);
        }
    }
}