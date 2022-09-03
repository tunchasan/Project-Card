namespace ProjectCard.Core.Entity
{
    public interface ISortable
    {
        public void StraightSort(SessionBase session);
        public void SameKindSort(SessionBase session);
        public void SmartSort(SessionBase session);
    }
}