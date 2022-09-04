using System;
using System.Collections.Generic;
using System.Linq;
using ProjectCard.Core.Utilities;
using Random = System.Random;

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
        
        // 1-2-3 = Generates sorted groupContainer which contains sub groups separately
        public static GroupContainer SortByStraight(this List<CardBase> cards)
        {
            // Time complexity : average O(nlogn) : worst-case O(n^2) - QuickSort
            cards = cards.OrderBy(card => card.Id).ToList();

            return cards.ProcessGroups(SortType.Straight);
        }
        
        // 7-7-7 = Generates sorted groupContainer which contains sub groups separately
        public static GroupContainer SortByKind(this List<CardBase> cards)
        {
            // Time complexity : average O(nlogn) : worst-case O(n^2) - QuickSort
            cards.Sort((a, b) => a.Kind.CompareTo(b.Kind));

            return cards.ProcessGroups(SortType.SameKind);
        }
        
        // Smart
        public static List<CardBase> SortBySmart(this List<CardBase> cards)
        {
            var bestResult = new GroupContainer{Score = 99};
            
            var straightGroupContainer = SortByStraight(cards);
            var sameKindGroupContainer = SortByKind(cards);

            bestResult = FindBestResult(bestResult.Score, sameKindGroupContainer, SortType.Straight);
            bestResult = FindBestResult(bestResult.Score, straightGroupContainer, SortType.SameKind);

            return bestResult.GetAllCards();
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
        
        private static bool SortCondition(int index, int groupLength, SortType sortType)
        {
            return sortType switch
            {
                SortType.Straight => index.IsFirstIndex() || index.IsLastIndex(groupLength) || index.IsInsideOfRange(3, groupLength),
                SortType.SameKind => true,
                _ => false
            };
        }
        
        private static GroupContainer ProcessGroups(this List<CardBase> cards, SortType sortType)
        {
            var counter = 1;
            var groupContainer = new GroupContainer() {Groups = new List<Group>(), Score = 0};
            var unGroup = new Group {Type = SortType.None, Cards = new List<CardBase>()};

            for (var i = cards.Count - 1; i >= 0; i--)
            {
                unGroup.Cards.Add(cards[i]);

                if (i != 0 && SortCondition(cards[i], cards[i - 1], sortType))
                {
                    counter++;
                }
                
                else
                {
                    if (counter >= 3)
                    {
                        unGroup.Cards.Reverse();
                        var group = new Group {Type = sortType, Cards = new List<CardBase>(counter)};
                        group.Cards.AddRange(unGroup.Cards);
                        groupContainer.Groups.Add(group);
                        cards.RemoveRange(i, counter);
                        counter = 1;
                    }

                    else
                    {
                        unGroup.Cards.Clear();
                        counter = 1;
                    }
                }
            }

            var newGroup = new Group {Type = SortType.None, Cards = new List<CardBase>(cards)};
            groupContainer.Groups.Add(newGroup);
            groupContainer.ValidateScore();
            return groupContainer;
        }
        
        private static GroupContainer FindBestResult(int bestScore, GroupContainer processingGroupContainer, SortType sortType)
        {
            var bestResult = new GroupContainer();
            var groups = processingGroupContainer.Groups;
            var remainedGroup = processingGroupContainer.GetRemainedGroup();
            for (var i = 0; i < groups.Count; i++)
            {
                if (groups[i].Cards.Count > 3 && groups[i].Type != SortType.None)
                {
                    var groupCards = groups[i].Cards;
                    var groupLength = groupCards.Count;
                    for (var j = 0; j < groupLength; j++)
                    {
                        if (SortCondition(j, groupLength, sortType))
                        {
                            var newList = new List<CardBase>(remainedGroup.Cards) {groupCards[j]};
                            SortForBestResult(newList, out var newGroupContainer, sortType);
                            if (newGroupContainer.Score < bestScore)
                            {
                                bestScore = newGroupContainer.Score;
                                newGroupContainer.Groups.AddRange(groups);
                                newGroupContainer.RemoveGroup(remainedGroup);
                                bestResult = newGroupContainer;
                            }
                        }
                    }
                }
            }

            return bestResult;
        }
        
        private static void SortForBestResult(List<CardBase> cards, out GroupContainer groupContainer, SortType sortType)
        {
            switch (sortType)
            {
                case SortType.Straight:
                    groupContainer = SortByStraight(cards);
                    break;
                case SortType.SameKind:
                    groupContainer = SortByKind(cards);
                    break;
                default:
                    throw new NotSupportedException($"{sortType} isn't supported by SortBy method!");
            }
        }
    }
}