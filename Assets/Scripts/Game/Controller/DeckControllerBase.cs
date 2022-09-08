using ProjectCard.Core.Entity;
using ProjectCard.Core.Utilities;
using ProjectCard.Game.Managers;
using UnityEngine;

namespace ProjectCard.Game.Controller
{
    [RequireComponent(typeof(DeckLayoutDynamicGroup))]
    public abstract class DeckControllerBase : MonoBehaviour
    {
        protected DeckLayoutDynamicGroupBase DeckLayoutDynamicGroup = null;
        protected DeckProviderBase DeckProvider = null;
        protected SessionBase Session = null;

        protected virtual void Start()
        {
            Initialize();
        }
        protected abstract void Initialize();
        public abstract void SortCardsRequest(SortType sortType);
        protected abstract void DrawCertainCardsRequest();
        protected abstract void DrawRandomCardsRequest(int cardAmounts);
        protected abstract void Display();

        protected virtual void OnEnable()
        {
            UIManager.OnSortCardsRequest += SortCardsRequest;
            UIManager.OnDrawRandomCardsRequest += DrawRandomCardsRequest;
            UIManager.OnDrawCertainCardsRequest += DrawCertainCardsRequest;
        }
        protected virtual void OnDisable()
        {
            UIManager.OnSortCardsRequest -= SortCardsRequest;
            UIManager.OnDrawRandomCardsRequest -= DrawRandomCardsRequest;
            UIManager.OnDrawCertainCardsRequest -= DrawCertainCardsRequest;
        }
    }
}