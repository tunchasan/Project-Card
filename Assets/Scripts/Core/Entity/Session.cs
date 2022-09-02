using System;
using System.Collections.Generic;

namespace ProjectCard.Core.Entity
{
    public static class Session
    {
        private static List<CardBase> _cards = new();
        public static bool IsInitialized = false;
        public static int SessionId = -1;
        
        public static IEnumerable<CardBase> Cards()
        {
            if (IsInitialized)
            {
                return _cards;
            }

            throw new NullReferenceException("Session couldn't initialized!");
        }
        public static void InitializeSession(List<CardBase> cards)
        {
            _cards.Clear();
            _cards = cards;
            IsInitialized = true;
            SessionId++;
        }
    }
}