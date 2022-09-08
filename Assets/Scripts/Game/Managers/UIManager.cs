using System;
using DG.Tweening;
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
        [SerializeField] private Slider deckSpacingSlider = null;
        [SerializeField] private TextMeshProUGUI sliderText = null;
        [SerializeField] private TextMeshProUGUI deckText = null;
        [SerializeField] private TextMeshProUGUI errorText = null;
        [SerializeField] private CanvasGroup canvasGroup = null;

        public static Action OnChangeThemeRequest;
        public static Action<SortType> OnSortCardsRequest;
        public static Action OnDrawCertainCardsRequest;
        public static Action<int> OnDrawRandomCardsRequest;
        public static Action<float> OnDeckSpacingSliderValueChanged;

        private void Start()
        {
            deckText.text = $"DECK\n 52/11";
            AnimateCanvasGroup();
            EnableUIElements();
        }

        private void AnimateCanvasGroup(bool isReverse = false)
        {
            DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 
                isReverse ? 0F : 1F, .5F);
        }
        
        public void OnClickChangeThemeButton()
        {
            OnChangeThemeRequest?.Invoke();
            DisableUIElements();
            ResetErrorMessage();
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
        public void OnChangeDeckSliderValue()
        {
            OnDeckSpacingSliderValueChanged?.Invoke(deckSpacingSlider.value);
        }
        public void UpdateDeckSliderValue(float spacing)
        {
            deckSpacingSlider.value = spacing;
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
            deckSpacingSlider.interactable = status;
        }
        public void EnableUIElements()
        {
            ValidateElementsStatus(true);
        }
        public void DisableUIElements()
        {
            ValidateElementsStatus(false);
        }

        private void UpdateErrorMessage(string message)
        {
            errorText.gameObject.SetActive(true);
            errorText.text += message;
        }

        private void ResetErrorMessage()
        {
            errorText.gameObject.SetActive(false);
            errorText.text = "The deck isn't suitable for\n";
        }

        public void DisplayErrors(ErrorCode code)
        {
            switch (code)
            {
                case ErrorCode.None:
                    straightButton.interactable = true;
                    sameKindButton.interactable = true;
                    smartButton.interactable = true;
                    ResetErrorMessage();
                    break;
                case ErrorCode.NoDataReceiveFromStraightRequest:
                    straightButton.interactable = false;
                    UpdateErrorMessage("\n-Straight Sorting");
                    break;
                case ErrorCode.NoDataReceiveFromSameKindRequest:
                    sameKindButton.interactable = false;
                    UpdateErrorMessage("\n-SameKind Sorting");
                    break;
                case ErrorCode.NoDataReceiveFromSmartRequest:
                    smartButton.interactable = false;
                    UpdateErrorMessage("\n-Smart Sorting");
                    break;
            }
        }

        private void OnEnable()
        {
            GameManager.OnGameQuit += DisableUIElements;
            ThemeManager.OnChangeThemeComplete += EnableUIElements;
        }
        private void OnDisable()
        {
            GameManager.OnGameQuit -= DisableUIElements;
            ThemeManager.OnChangeThemeComplete -= EnableUIElements;
        }
    }
}