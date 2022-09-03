using System;
using System.Collections.Generic;

namespace ProjectCard.Core.Entity
{
    public static class Sorter
    {
        private static readonly Random Random = new();
        
        // Shuffle
        public static void SortByShuffle(this CardBase[] cards)
        {
            var n = cards.Length;
            while (n > 1) 
            {
                var k = Random.Next(n--);
                var temp = cards[n];
                cards[n] = cards[k];
                cards[k] = temp;
            }
        }
        
        // 1-2-3
        public static void SortByStraight(this List<CardBase> cards)
        {
            // TODO
        }
        
        // 7-7-7
        public static void SortByKind(this List<CardBase> cards)
        {
            // TODO
        }
        
        // Smart
        public static void SortBySmart(this List<CardBase> cards)
        {
            // TODO
        }
    }
}