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
        [Header("@Configurations")]
        [SerializeField] private ThemeData theme1;
        [SerializeField] private ThemeData theme2;

        public ThemeType CurrentTheme { private set; get; } = ThemeType.Theme1;

        public static Action OnChangeTheme;
        
        private void ChangeTheme()
        {
            CurrentTheme = CurrentTheme == ThemeType.Theme1 ? 
                ThemeType.Theme2 : ThemeType.Theme1;
            
            AnimateTheme();
        }

        private void AnimateTheme()
        {
            var initialColor = CurrentTheme == ThemeType.Theme1 ? theme1.color : theme2.color;
            var targetColor = background.color;
            targetColor.a = 0;

            DOTween.To(() => Camera.main.backgroundColor, x => Camera.main.backgroundColor = x, initialColor, 1F);
            
            DOTween.To(() => background.color, x => background.color = x, targetColor, .5F)
                .OnComplete(() =>
                {
                    background.sprite = CurrentTheme == ThemeType.Theme1
                        ? theme1.backgroundAsset
                        : theme2.backgroundAsset;

                    DOTween.To(() => background.color, x => background.color = x, initialColor, .5F)
                        .OnComplete(() => { OnChangeTheme?.Invoke(); });

                    
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