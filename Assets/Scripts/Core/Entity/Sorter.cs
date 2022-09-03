using System.Collections.Generic;
using System.Linq;
using Random = System.Random;

namespace ProjectCard.Core.Entity
{
    public static class Sorter
    {
        private static readonly Random Random = new();
        private static readonly List<CardBase> GroupList = new(DeckBase.Size);
        private static readonly List<CardBase> UnGroupList = new(DeckBase.Size);
        
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
        public static List<CardBase> SortByStraight(List<CardBase> cards)
        {
            GroupList.Clear();
            UnGroupList.Clear();
            
            var counter = 1;
            
            cards = cards.OrderBy(card => card.Id).ToList();
            
            for (var i = cards.Count -1; i > 0; i--)
            {
                UnGroupList.Add(cards[i]);

                if (cards[i].Id - cards[i - 1].Id == 1)
                {
                    counter++;
                }

                else
                {
                    if (counter >= 3)
                    {
                        GroupList.AddRange(UnGroupList);
                        cards.RemoveRange(i, counter);
                        UnGroupList.Clear();
                        counter = 1;
                    }

                    else
                    {
                        counter = 1;
                        UnGroupList.Clear();
                    }
                }
            }

            GroupList.Reverse();
            GroupList.AddRange(cards);
            
            return GroupList;
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