using System.Collections.Generic;
using System.Linq;
using ProjectCard.Core.Utilities;
using UnityEngine;
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
        
        // 1-2-3 = Generates sorted List
        public static List<CardBase> SortByStraight(this List<CardBase> cards)
        {
            // Time complexity : average O(nlogn) : worst-case O(n^2) - QuickSort
            cards = cards.OrderBy(card => card.Id).ToList();

            cards.ProcessCards(SortType.Straight);

            return cards;
        }
        
        // 1-2-3 = Generates sorted groupContainer which contains sub groups separately
        public static void SortByStraight(this List<CardBase> cards, out GroupContainer container)
        {
            // Time complexity : average O(nlogn) : worst-case O(n^2) - QuickSort
            cards = cards.OrderBy(card => card.Id).ToList();

            container = cards.ProcessGroups(SortType.Straight);
        }
        
        // 7-7-7 = Generates sorted List
        public static List<CardBase> SortByKind(this List<CardBase> cards)
        {
            // Time complexity : average O(nlogn) : worst-case O(n^2) - QuickSort
            cards.Sort((a, b) => a.Kind.CompareTo(b.Kind));

            cards.ProcessCards(SortType.SameKind);

            return cards;
        }
        
        // 7-7-7 = Generates sorted groupContainer which contains sub groups separately
        public static void SortByKind(this List<CardBase> cards, out GroupContainer container)
        {
            // Time complexity : average O(nlogn) : worst-case O(n^2) - QuickSort
            cards.Sort((a, b) => a.Kind.CompareTo(b.Kind));

            container = cards.ProcessGroups(SortType.SameKind);
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
        
        private static void ProcessCards(this List<CardBase> cards, SortType sortType)
        {
            var counter = 1;
            var groupPointer = 0;
            var ungroupPointer = 0;

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
                        for (var j = 0; j < counter; j++)
                        {
                            TempGroup[groupPointer + j] = TempUnGroup[j];
                        }
                        
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

            for (var i = 0; i < groupPointer; i++)
            {
                cards.Insert(0, TempGroup[i]);
            }
        }
        
        private static GroupContainer ProcessGroups(this List<CardBase> cards, SortType sortType)
        {
            var counter = 1;
            var groupContainer = new GroupContainer() {Groups = new List<Group>()};
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
                        var group = new Group {Type = sortType, Score = -1, Cards = new List<CardBase>(counter)};
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
            newGroup.ValidateScore();
            groupContainer.Groups.Add(newGroup);
            return groupContainer;
        }

        // Smart
        public static List<CardBase> SortBySmart(this List<CardBase> cards)
        {
            SortByStraight(cards, out var straightGroupContainer);
            SortByKind(cards, out var sameKindGroupContainer);
            var groupContainers = new List<GroupContainer>() {straightGroupContainer, sameKindGroupContainer};

            var sameKindGroups = sameKindGroupContainer.Groups;
            var sameKindGroupLength = sameKindGroups.Count;
            var sameKindRemainedGroup = sameKindGroupContainer.GetRemainedGroup();
            for (var i = 0; i < sameKindGroupLength; i++)
            {
                if (sameKindGroups[i].Cards.Count > 3 && sameKindGroups[i].Type != SortType.None)
                {
                    var groupCards = sameKindGroups[i].Cards;
                    for (var j = 0; j < groupCards.Count; j++)
                    {
                        var newList = new List<CardBase>(sameKindRemainedGroup.Cards) {groupCards[j]};
                        SortByStraight(newList, out var newGroupContainer);
                        newGroupContainer.Groups.AddRange(sameKindGroups);
                        newGroupContainer.RemoveGroup(sameKindRemainedGroup);
                        groupContainers.Add(newGroupContainer);
                    }
                }
            }

            var straightGroups = straightGroupContainer.Groups;
            var straightGroupLength = straightGroups.Count;
            var straightRemainedGroup = straightGroupContainer.GetRemainedGroup();
            for (var i = 0; i < straightGroupLength; i++)
            {
                if (straightGroups[i].Cards.Count > 3 && straightGroups[i].Type != SortType.None)
                {
                    var groupCards = straightGroups[i].Cards;
                    for (var j = 0; j < groupCards.Count; j++)
                    {
                        if (j == 0 || j == straightGroupLength - 1)
                        {
                            var newList = new List<CardBase>(straightRemainedGroup.Cards) {groupCards[j]};
                            SortByKind(newList, out var newGroupContainer);
                            newGroupContainer.Groups.AddRange(straightGroups);
                            newGroupContainer.RemoveGroup(straightRemainedGroup);
                            groupContainers.Add(newGroupContainer);
                        }

                        else
                        {
                            if (j >= 3 && straightGroupLength - 1 - j <= 3)
                            {
                                var newList = new List<CardBase>(straightRemainedGroup.Cards) {groupCards[j]};
                                SortByKind(newList, out var newGroupContainer);
                                newGroupContainer.Groups.AddRange(straightGroups);
                                newGroupContainer.RemoveGroup(straightRemainedGroup);
                                groupContainers.Add(newGroupContainer);
                            }
                        }
                    }
                }
            }

            var score = Mathf.Infinity;
            var result = new GroupContainer();

            for (var i = 0; i < groupContainers.Count; i++)
            {
                var newScore = groupContainers[i].GetRemainedGroup().Score;
                if (score > newScore)
                {
                    score = newScore;
                    result = groupContainers[i];
                }
            }

            var resultList = new List<CardBase>();

            foreach (var group in result.Groups)
            {
                if(group.Type != SortType.None)
                    resultList.AddRange(group.Cards);
            }
            
            resultList.AddRange(result.GetRemainedGroup().Cards);
            
            return resultList;
        }
    }
}