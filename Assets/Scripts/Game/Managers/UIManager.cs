using System;
using ProjectCard.Core.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectCard.Game.Managers
{
    public class UIManager : Singleton<UIManager>
    {
        [Header("@References")] 
        [SerializeField] private Button changeThemeButton = null;
        [SerializeField] private Button straightButton = null;
        [SerializeField] private Button sameKindButton = null;
        [SerializeField] private Button smartButton = null;
        [SerializeField] private Button shuffleButton = null;
        [SerializeField] private Button certainCardsButton = null;
        [SerializeField] private Button randomCardsButton = null;
        [SerializeField] private Slider randomCardAmountSlider = null;
        [SerializeField] private TextMeshProUGUI sliderText = null;
        [SerializeField] private TextMeshProUGUI deckText = null;

        public static Action OnChangeThemeRequest;
        public static Action<SortType> OnSortCardsRequest;
        public static Action OnDrawCertainCardsRequest;
        public static Action<int> OnDrawRandomCardsRequest;

        public void OnClickChangeThemeButton()
        {
            OnChangeThemeRequest?.Invoke();
            DisableUIElements();
        }
        public void OnClickStraightButton()
        {
            OnSortCardsRequest?.Invoke(SortType.Straight);
        }
        public void OnClickSameKindButton()
        {
            OnSortCardsRequest?.Invoke(SortType.SameKind);
        }
        public void OnClickSmartButton()
        {
            OnSortCardsRequest?.Invoke(SortType.Smart);
        }
        public void OnClickShuffleButton()
        {
            OnSortCardsRequest?.Invoke(SortType.Shuffle);
        }
        public void OnClickCertainCardsButton()
        {
            OnDrawCertainCardsRequest?.Invoke();
            deckText.text = $"DECK\n 52/11";
        }
        public void OnClickRandomCardsRequest()
        {
            var value = (int) randomCardAmountSlider.value;
            OnDrawRandomCardsRequest?.Invoke(value);
            deckText.text = $"DECK\n 52/{(int)randomCardAmountSlider.value}";
        }
        public void OnChangeSliderValue()
        {
            sliderText.text = $"DRAW RANDOM CARDS ({(int)randomCardAmountSlider.value})";
        }
        private void ValidateElementsStatus(bool status)
        {
            changeThemeButton.interactable = status;
            straightButton.interactable = status;
            sameKindButton.interactable = status;
            smartButton.interactable = status;
            shuffleButton.interactable = status;
            certainCardsButton.interactable = status;
            randomCardsButton.interactable = status;
            randomCardAmountSlider.interactable = status;
        }
        public void EnableUIElements()
        {
            ValidateElementsStatus(true);
        }
        public void DisableUIElements()
        {
            ValidateElementsStatus(false);
        }

        private void OnEnable()
        {
            GameManager.OnGamePlay += EnableUIElements;
            GameManager.OnGameQuit += DisableUIElements;
            ThemeManager.OnChangeThemeComplete += EnableUIElements;
        }
        private void OnDisable()
        {
            GameManager.OnGamePlay -= EnableUIElements;
            GameManager.OnGameQuit -= DisableUIElements;
            ThemeManager.OnChangeThemeComplete -= EnableUIElements;
        }
    }
}