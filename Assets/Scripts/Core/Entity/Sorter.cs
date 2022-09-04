using System.Collections.Generic;
using System.Linq;
using ProjectCard.Core.Utilities;
using Random = System.Random;

namespace ProjectCard.Core.Entity
{
    public static class Sorter
    {
        private static readonly Random Random = new();
        private static readonly CardBase[] TempGroup = new CardBase[DeckBase.Size];
        private static readonly CardBase[] TempUnGroup = new CardBase[DeckBase.Size];
        
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
        public static List<CardBase> SortByStraight(this List<CardBase> cards)
        {
            // Time complexity : average O(nlogn) : worst-case O(n^2) - QuickSort
            cards = cards.OrderBy(card => card.Id).ToList();

            cards.ProcessGroups(SortType.Straight);

            return cards;
        }
        // 7-7-7
        public static List<CardBase> SortByKind(this List<CardBase> cards)
        {
            // Time complexity : average O(nlogn) : worst-case O(n^2) - QuickSort
            cards.Sort((a, b) => a.Kind.CompareTo(b.Kind));

            cards.ProcessGroups(SortType.SameKind);

            return cards;
        }

        private static bool SortCondition(CardBase card1, CardBase card2, SortType sortType)
        {
            return sortType switch
            {
                SortType.Straight => card1.Id - card2.Id == 1,
                SortType.SameKind => card1.Kind == card2.Kind,
                _ => false
            };
        }
        
        private static void ProcessGroups(this List<CardBase> cards, SortType sortType)
        {
            var counter = 1;
            var groupPointer = 0;
            var ungroupPointer = 0;
            var groupContainer = new GroupContainer() {Groups = new List<Group>()};

            for (var i = cards.Count - 1; i >= 0; i--)
            {
                TempUnGroup[ungroupPointer] = cards[i];
                ungroupPointer++;

                if (i != 0 && SortCondition(cards[i], cards[i - 1], sortType))
                {
                    counter++;
                }
                
                else
                {
                    if (counter >= 3)
                    {
                        var group = new Group {Type = sortType, Cards = new List<CardBase>(counter)};
                        
                        for (var j = 0; j < counter; j++)
                        {
                            TempGroup[groupPointer + j] = TempUnGroup[j];
                            group.Cards.Add(TempUnGroup[j]);
                        }
                        
                        groupContainer.Groups.Add(group);

                        cards.RemoveRange(i, counter);
                        ungroupPointer = 0;
                        groupPointer += counter;
                        counter = 1;
                    }

                    else
                    {
                        ungroupPointer = 0;
                        counter = 1;
                    }
                }
            }

            var newGroup = new Group {Type = SortType.None, Cards = new List<CardBase>(cards)};
            groupContainer.Groups.Add(newGroup);

            for (var i = 0; i < groupPointer; i++)
            {
                cards.Insert(0, TempGroup[i]);
            }
        }
        
        // Smart
        public static List<CardBase> SortBySmart(this List<CardBase> cards)
        {
            // TODO
            return null;
        }
    }
}