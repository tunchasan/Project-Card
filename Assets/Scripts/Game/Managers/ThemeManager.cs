using System;
using DG.Tweening;
using ProjectCard.Game.Container;
using ProjectCard.Game.SO;
using ProjectCard.Game.Utilities;
using UnityEngine;

namespace ProjectCard.Game.Managers
{
    public class ThemeManager : Singleton<ThemeManager>
    {
        [Header("@References")]
        [SerializeField] private SpriteRenderer background;
        [SerializeField] private Camera playerCamera;

        public const int Size = 2; 
        public float AnimationDuration { private set; get; } = 1.2F;
        public ThemeData CurrentTheme { private set; get; } = null;

        public static Action<ThemeData> OnChangeTheme;
        public static Action OnChangeThemeComplete;

        private void Awake()
        {
            CurrentTheme = AssetsContainer.Instance.GetThemeAsset(ThemeType.Theme1);
        }

        private void ChangeTheme()
        {
            CurrentTheme = CurrentTheme.type == ThemeType.Theme1 ? 
                AssetsContainer.Instance.GetThemeAsset(ThemeType.Theme2) : 
                AssetsContainer.Instance.GetThemeAsset(ThemeType.Theme1);
            
            AnimateTheme();
        }

        private void AnimateTheme()
        {
            OnChangeTheme?.Invoke(CurrentTheme);

            var initialColor = CurrentTheme.backgroundColor;
            var targetColor = background.color;
            targetColor.a = 0;

            DOTween.To(() => playerCamera.backgroundColor, x => playerCamera.backgroundColor = x, initialColor, AnimationDuration);
            
            DOTween.To(() => background.color, x => background.color = x, targetColor, AnimationDuration / 2F)
                .OnComplete(() =>
                {
                    background.sprite = CurrentTheme.backgroundAsset;

                    DOTween.To(() => background.color, x => background.color = x, initialColor, AnimationDuration / 2F)
                        .OnComplete(() => { OnChangeThemeComplete?.Invoke(); });
                });
        }
        
        private void OnEnable()
        {
            UIManager.OnChangeThemeRequest += ChangeTheme;
        }
        
        private void OnDisable()
        {
            UIManager.OnChangeThemeRequest -= ChangeTheme;
        }
    }
}