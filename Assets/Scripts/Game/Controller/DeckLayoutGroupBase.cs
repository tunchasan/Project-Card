using System;
using System.Collections.Generic;
using ProjectCard.Core.Entity;
using ProjectCard.Game.Managers;
using ProjectCard.Game.SO;
using UnityEngine;

namespace ProjectCard.Game.Controller
{
    public abstract class DeckLayoutGroupBase<T> : MonoBehaviour
    where T : DeckLayoutElementBase
    {
        protected float Spacing = 1F;
        protected T LayoutElement = null;
        protected const float Spring = 30;
        protected const float Limit = 8;
        protected const int MaxSize = DeckBase.Size;
        protected readonly List<T> Slots = new();
        protected readonly Dictionary<int,T> ActiveSlots = new();
        
        protected void Awake()
        {
            PreInitialize();
            ValidateLayoutForSafeAreas();
        }
        public abstract void PreInitialize();
        public abstract void Initialize(List<CardBase> elements, Action onInitialized);
        protected virtual void ValidateLayoutForSafeAreas()
        {
            transform.localScale = ((float)Screen.width / Screen.height) * Vector3.one / 1.77F;
        }
        protected abstract float ValidateLayoutOffset();
        public abstract void UpdateLayoutTheme(ThemeData theme);
        public abstract void ValidateLayout(List<CardBase> elements);
        public abstract void ValidateLayout(bool shouldAnimate = false);
        public abstract void ValidateLayoutInOrder(bool shouldAnimate = false);
        public abstract void ValidateLayoutElement(T element, int index, bool shouldAnimate = false);
        public abstract void ValidateLayoutElement(int layoutElementId, int sortingLayer, bool shouldAnimate);
        public abstract void UpdateLayoutSpacingValue(float value);

        protected virtual void OnEnable()
        {
            ThemeManager.OnChangeTheme += UpdateLayoutTheme;
            UIManager.OnDeckSpacingSliderValueChanged += UpdateLayoutSpacingValue;
        }
        protected virtual void OnDisable()
        {
            ThemeManager.OnChangeTheme -= UpdateLayoutTheme;
            UIManager.OnDeckSpacingSliderValueChanged -= UpdateLayoutSpacingValue;
        }
        
        #if UNITY_EDITOR
        private void Update() { ValidateLayoutForSafeAreas(); }
        
        #endif
    }
}