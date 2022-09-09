using System;
using System.Collections.Generic;
using System.Linq;
using ProjectCard.Core.Utilities;
using Random = System.Random;

namespace ProjectCard.Core.Entity
{
    /// <summary>
    /// Sorter is a static class that provides functionality to sort algorithms such as Shuffle, Straight, Same Kind, and Smart.
    /// The class aims to compute the sorting results very efficiently, avoiding Garbage Collection by using struct class
    /// and rarely using new keywords and Linq.
    /// </summary>
    public static class Sorter
    {
        private static readonly Random Random = new();
        
        /// <summary>
        /// Places the elements into random indexes of the list.
        /// Complexity : Average O(n) | Worst-Case O(n)
        /// Returns CardBase List
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public static List<CardBase> SortByShuffle(this List<CardBase> cards)
        {
            var n = cards.Count;
            while (n > 1) 
            {
                var k = Random.Next(n--);
                var temp = cards[n];
                cards[n] = cards[k];
                cards[k] = temp;
            }

            return cards;
        }
        
        /// <summary>
        /// Loops the whole list to find possible matching groups that contain
        /// elements with sequential ids and of the same suit type.
        /// Complexity : Average [O(n) + O(nlogn)] | Worst-Case [O(n) + O(n^2)]
        /// Returns GroupContainer.
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public static GroupContainer SortByStraight(this List<CardBase> cards)
        {
            cards = cards.OrderBy(card => card.Id).ToList();

            return cards.ProcessGroups(SortType.Straight);
        }
        
        /// <summary>
        /// Loops the whole list to find possible matching groups that contain
        /// elements with the same kind and different suit types.
        /// Complexity : Average [O(n) + O(nlogn)] | Worst-Case [O(n) + O(n^2)]
        /// Returns GroupContainer.
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public static GroupContainer SortByKind(this List<CardBase> cards)
        {
            cards.Sort((a, b) => a.Kind.CompareTo(b.Kind));

            return cards.ProcessGroups(SortType.SameKind);
        }
        
        /// <summary>
        /// Uses both sorting algorithms, Straight and the Same Kind, to find the possible best result.
        /// Complexity : Average [6*O(n) + 2*O(nlogn)] | Worst-Case [2*O(n) + 3*O(n^2)]]
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public static GroupContainer SortBySmart(this List<CardBase> cards)
        {
            var elements = new List<CardBase>(cards);
            var straightGroupContainer = SortByStraight(elements);
            var sameKindGroupContainer = SortByKind(elements);

            if (!straightGroupContainer.IsValid() || !sameKindGroupContainer.IsValid()) 
                return new GroupContainer();

            var bestResult1 = FindBestResult(sameKindGroupContainer, SortType.Straight);
            var bestResult2 = FindBestResult(straightGroupContainer, SortType.SameKind);

            return bestResult1.Score < bestResult2.Score ? bestResult1 : bestResult2;
        }
        private static bool SortCondition(CardBase card1, CardBase card2, SortType sortType)
        {
            return sortType switch
            {
                SortType.Straight => card1.Id - card2.Id == 1 && card1.Suit == card2.Suit,
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
                        unGroup.Cards.Clear();
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
        private static GroupContainer FindBestResult(GroupContainer processingGroupContainer, SortType sortType)
        {
            var bestResult = new GroupContainer() {Groups = new List<Group>(), Score = 999};
            var groups = processingGroupContainer.Groups;
            var remainedGroup = processingGroupContainer.GetRemainedGroup();

            CardBase lastProcessedCard = null;
            var lastProcessedGroup = new Group();
            
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
                            if (newGroupContainer.Score < bestResult.Score)
                            {
                                lastProcessedCard = groupCards[j];
                                lastProcessedGroup = groups[i];
                                
                                newGroupContainer.Groups.AddRange(groups);
                                newGroupContainer.RemoveGroup(remainedGroup);
                                bestResult = newGroupContainer;
                            }
                        }
                    }
                }
            }

            if(lastProcessedCard != null)
                lastProcessedGroup.Cards.Remove(lastProcessedCard);

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