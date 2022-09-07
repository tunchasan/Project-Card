using System;
using DG.Tweening;
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
        [Header("@Configurations")]
        [SerializeField] private ThemeData theme1;
        [SerializeField] private ThemeData theme2;

        public ThemeType CurrentTheme { private set; get; } = ThemeType.Theme1;

        public static Action<ThemeData> OnChangeTheme;
        public static Action OnChangeThemeComplete;
        
        private void ChangeTheme()
        {
            CurrentTheme = CurrentTheme == ThemeType.Theme1 ? 
                ThemeType.Theme2 : ThemeType.Theme1;
            
            AnimateTheme();
        }

        private void AnimateTheme()
        {
            var targetTheme = CurrentTheme == ThemeType.Theme1 ? theme1 : theme2;
            OnChangeTheme?.Invoke(targetTheme);

            var initialColor = targetTheme.backgroundColor;
            var targetColor = background.color;
            targetColor.a = 0;

            DOTween.To(() => playerCamera.backgroundColor, x => playerCamera.backgroundColor = x, initialColor, 1F);
            
            DOTween.To(() => background.color, x => background.color = x, targetColor, .5F)
                .OnComplete(() =>
                {
                    background.sprite = targetTheme.backgroundAsset;

                    DOTween.To(() => background.color, x => background.color = x, initialColor, .5F)
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