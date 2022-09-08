using System;
using System.Collections.Generic;
using ProjectCard.Core.Entity;
using ProjectCard.Game.Managers;
using ProjectCard.Game.SO;
using UnityEngine;

namespace ProjectCard.Game.Controller
{
    public abstract class DeckLayoutGroupBase : MonoBehaviour
    {
        [Header("@Configurations")]
        [Range(0.2F, 1F)] [SerializeField] protected float spacing = 1F;

        [SerializeField] protected DeckLayoutElementBase layoutElement = null;

        protected const float Spring = 30;
        protected const float Limit = 8;
        protected const int MaxSize = DeckBase.Size;
        
        protected readonly List<DeckLayoutElementBase> Slots = new();
        protected readonly Dictionary<int,DeckLayoutElementBase> ActiveSlots = new();

        protected void Awake()
        {
            PreInitialize();
        }
        public abstract void PreInitialize();
        public abstract void Initialize(List<CardBase> elements, Action onInitialized);
        public abstract void UpdateLayoutTheme(ThemeData theme);
        public abstract void ValidateLayout(List<CardBase> elements);
        public abstract void ValidateLayout(bool shouldAnimate = false);
        public abstract void ValidateLayoutInOrder();
        public abstract void ValidateLayoutElement(DeckLayoutElementBase element, int index);
        public abstract void ValidateLayoutElement(int layoutElementId, int sortingLayer, bool shouldAnimate);
        public abstract void UpdateLayoutSpacingValue(float value);

        private void OnEnable()
        {
            ThemeManager.OnChangeTheme += UpdateLayoutTheme;
            UIManager.OnDeckSpacingSliderValueChanged += UpdateLayoutSpacingValue;
        }
        private void OnDisable()
        {
            ThemeManager.OnChangeTheme -= UpdateLayoutTheme;
            UIManager.OnDeckSpacingSliderValueChanged -= UpdateLayoutSpacingValue;
        }
    }
}