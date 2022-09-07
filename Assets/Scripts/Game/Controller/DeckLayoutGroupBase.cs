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

        protected const float Spring = 15;
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
        public abstract void ValidateLayout();
        public abstract void ValidateLayoutElement(int layoutElementId, int sortingLayer, bool shouldAnimate);

        private void OnEnable()
        {
            ThemeManager.OnChangeTheme += UpdateLayoutTheme;
        }
        private void OnDisable()
        {
            ThemeManager.OnChangeTheme -= UpdateLayoutTheme;
        }
    }
}