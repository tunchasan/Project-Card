using System;
using ProjectCard.Core.Utilities;
using ProjectCard.Game.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectCard.Game.Managers
{
    public class UIManager : Singleton<UIManager>
    {
        [Header("@References")] 
        [SerializeField] private Button theme1Button = null;
        [SerializeField] private Button theme2Button = null;
        [SerializeField] private Button straightButton = null;
        [SerializeField] private Button sameKindButton = null;
        [SerializeField] private Button smartButton = null;
        [SerializeField] private Button shuffleButton = null;
        [SerializeField] private Button certainCardsButton = null;
        [SerializeField] private Button randomCardsButton = null;
        [SerializeField] private Slider randomCardAmountSlider = null;
        [SerializeField] private TextMeshProUGUI sliderTextMesh = null;

        public static Action<ThemeType> OnChangeThemeRequest;
        public static Action<SortType> OnSortCardsRequest;
        public static Action OnDrawCertainCardsRequest;
        public static Action<int> OnDrawRandomCardsRequest;

        public void OnClickTheme1Button()
        {
            OnChangeThemeRequest?.Invoke(ThemeType.Theme1);
        }
        public void OnClickTheme2Button()
        {
            OnChangeThemeRequest?.Invoke(ThemeType.Theme2);
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
        }

        public void OnClickRandomCardsRequest()
        {
            OnDrawRandomCardsRequest?.Invoke((int)randomCardAmountSlider.value);
        }

        public void OnChangeSliderValue()
        {
            sliderTextMesh.text = $"DRAW RANDOM CARDS ({(int)randomCardAmountSlider.value})";
        }

        private void ValidateElementsStatus(bool status)
        {
            theme1Button.interactable = status;
            theme2Button.interactable = status;
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
        }

        private void OnDisable()
        {
            GameManager.OnGamePlay -= EnableUIElements;
            GameManager.OnGameQuit -= DisableUIElements;
        }
    }
}