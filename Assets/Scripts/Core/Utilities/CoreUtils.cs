namespace ProjectCard.Core.Utilities
{
    public static class CoreUtils
    {
        public static bool IsFirstIndex(this int index)
        {
            return index == 0;
        }
        
        public static bool IsLastIndex(this int index, int length)
        {
            return index == length - 1;
        }

        public static bool IsInsideOfRange(this int index, int offset, int length)
        {
            return index >= offset && length - 1 - index <= offset;
        }
    }
}