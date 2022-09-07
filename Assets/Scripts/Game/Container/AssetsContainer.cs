using System.Collections.Generic;
using ProjectCard.Core.Entity;
using ProjectCard.Game.Managers;
using ProjectCard.Game.SO;
using ProjectCard.Game.Utilities;
using UnityEngine;

namespace ProjectCard.Game.Container
{
    public class AssetsContainer : Singleton<AssetsContainer>
    {
        private readonly Dictionary<int, Sprite> _cardAssets = new(DeckBase.Size);
        private readonly Dictionary<ThemeType, ThemeData> _themeAssets = new(ThemeManager.Size);

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            LoadCardAssets();

            LoadThemeAssets();
        }

        private void LoadCardAssets()
        {
            for (var i = 0; i < DeckBase.Size; i++)
            {
                _cardAssets.Add(i, Resources.Load<Sprite>($"Cards/Assets/{i}"));
            }
        }

        private void LoadThemeAssets()
        {
            _themeAssets.Add(ThemeType.Theme1, Resources.Load<ThemeData>("Themes/Presets/Theme01"));
            _themeAssets.Add(ThemeType.Theme2, Resources.Load<ThemeData>("Themes/Presets/Theme02"));
        }

        public ThemeData GetThemeAsset(ThemeType type)
        {
            return _themeAssets[type];
        }

        public Sprite GetCardAsset(int id)
        {
            return _cardAssets[id];
        }
    }
}