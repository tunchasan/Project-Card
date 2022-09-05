using ProjectCard.Core.Entity;
using ProjectCard.Core.Utilities;
using ProjectCard.Game.Managers;
using UnityEngine;

namespace ProjectCard.Game.Controller
{
    public abstract class DeckControllerBase : MonoBehaviour
    {
        protected DeckProviderBase DeckProvider = null;
        protected SessionBase Session = null;
        protected abstract void Initialize();
        public abstract void RequestSort(SortType sortType);
        protected abstract void DrawCertainCards();
        protected abstract void DrawRandomCards(int cardAmounts);
        protected abstract void Display();

        protected virtual void OnEnable()
        {
            GameManager.OnGameStart += Initialize;
            UIManager.OnSortCardsRequest += RequestSort;
        }
        protected virtual void OnDisable()
        {
            GameManager.OnGameStart -= Initialize;
            UIManager.OnSortCardsRequest -= RequestSort;
        }
    }
}