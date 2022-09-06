using System.Collections.Generic;
using NUnit.Framework;
using ProjectCard.Core.Entity;
using UnityEngine;

namespace ProjectCard.Tests
{
    public class SorterTest
    {
        private readonly DeckProviderBase _deckProvider = new DeckProvider();
        private readonly SessionBase _session = new Session();
        
        [Test]
        public void DrawCertainCardsTest()
        {
            var certainCards = new List<int> {26, 1, 17, 29, 0, 15, 42, 3, 13, 2, 16};
            _deckProvider.DrawCertainCards(certainCards, _session);

            for (var i = 0; i < certainCards.Count; i++)
            {
                Assert.AreEqual(certainCards[i], _session.Data()[i].Id);
            }
        }
        
        [Test]
        public void DrawRandomCardsTest()
        {
            _deckProvider.DrawRandomCards(15, _session);

            Assert.AreEqual(15, _session.Data().Count);
        }
        
        [Test]
        public void ShuffleSortTest()
        {
            var certainCards = new List<int> {26, 1, 17, 29, 0, 15, 42, 3, 13, 2, 16};
            _deckProvider.DrawCertainCards(certainCards, _session);
            _deckProvider.ShuffleSort(_session);
            var sortedCards = _session.Data();
            
            Assert.AreEqual(certainCards.Count, sortedCards.Count);

            foreach (var id in certainCards)
            {
                var result = sortedCards.Find(card => card.Id == id);
                Assert.AreEqual(true, result != null);
            }
        }
        
        [Test]
        public void StraightSortTest()
        {
            var certainCards = new List<int> {26, 1, 17, 29, 0, 15, 42, 3, 13, 2, 16};
            var sortedCards = new List<int> {15, 16, 17, 0, 1, 2, 3, 13, 26, 29, 42};
            _deckProvider.DrawCertainCards(certainCards, _session);
            _deckProvider.StraightSort(_session);

            for (var i = 0; i < sortedCards.Count; i++)
            {
                Assert.AreEqual(sortedCards[i], _session.Data()[i].Id);
            }
        }
        
        [Test]
        public void SameKindSortTest()
        {
            var certainCards = new List<int> {26, 1, 17, 29, 0, 15, 42, 3, 13, 2, 16};
            var sortedCards = new List<int> {29, 42, 3, 16, 26, 0, 13, 1, 15, 2, 17};
            _deckProvider.DrawCertainCards(certainCards, _session);
            _deckProvider.SameKindSort(_session);

            for (var i = 0; i < sortedCards.Count; i++)
            {
                Assert.AreEqual(sortedCards[i], _session.Data()[i].Id);
            }
        }
        
        [Test]
        public void SmartSortTest()
        {
            var certainCards = new List<int> {26, 1, 17, 29, 0, 15, 42, 3, 13, 2, 16};
            var sortedCards = new List<int> {29, 42, 3, 15, 16, 17, 0, 1, 2, 13, 26};
            _deckProvider.DrawCertainCards(certainCards, _session);
            _deckProvider.SmartSort(_session);

            for (var i = 0; i < sortedCards.Count; i++)
            {
                Assert.AreEqual(sortedCards[i], _session.Data()[i].Id);
            }
        }
    }
}