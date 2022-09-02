using System;

namespace ProjectCard.Core.Utilities
{
    public static class CoreUtils
    {
        public static void Shuffle<T> (this T[] array)
        {
            var n = array.Length;
            while (n > 1) 
            {
                var k = new Random().Next(n--);
                var temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }
    }
}