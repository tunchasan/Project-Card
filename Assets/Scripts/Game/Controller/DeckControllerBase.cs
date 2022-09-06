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
        public abstract void SortCardsRequest(SortType sortType);
        protected abstract void DrawCertainCardsRequest();
        protected abstract void DrawRandomCardsRequest(int cardAmounts);
        protected abstract void Display();

        protected virtual void OnEnable()
        {
            GameManager.OnGameStart += Initialize;
            UIManager.OnSortCardsRequest += SortCardsRequest;
            UIManager.OnDrawRandomCardsRequest += DrawRandomCardsRequest;
            UIManager.OnDrawCertainCardsRequest += DrawCertainCardsRequest;
        }
        protected virtual void OnDisable()
        {
            GameManager.OnGameStart -= Initialize;
            UIManager.OnSortCardsRequest -= SortCardsRequest;
            UIManager.OnDrawRandomCardsRequest -= DrawRandomCardsRequest;
            UIManager.OnDrawCertainCardsRequest -= DrawCertainCardsRequest;
        }
    }
}