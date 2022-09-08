using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using ProjectCard.Core.Entity;
using ProjectCard.Game.Container;
using ProjectCard.Game.Managers;
using ProjectCard.Game.SO;
using ProjectCard.Game.Utilities;
using UnityEngine;

namespace ProjectCard.Game.Controller
{
    public abstract class DeckStandardLayoutGroupBase<T> : DeckLayoutGroupBase<T>
    where T : DeckLayoutElementBase
    {
        public override void PreInitialize()
        {
            LayoutElement = AssetsContainer.Instance.CardPrefab.GetComponent<T>();
            
            for (var i = 0; i < MaxSize; i++)
            {
                var elem = Instantiate(LayoutElement, transform);
                elem.Initialize();
                Slots.Add(elem);
                elem.gameObject.SetActive(false);
            }
        }
        public override void Initialize(List<CardBase> dataset, Action onInitialized)
        {
            ActiveSlots.Clear();
            
            for (var i = 0; i < MaxSize; i++)
            {
                var elem = Slots[i];
                
                if (i < dataset.Count)
                {
                    elem.Initialize(dataset[i].Id);
                    elem.gameObject.SetActive(true);
                    ActiveSlots.Add(dataset[i].Id, elem);
                }
                
                else
                {
                    elem.gameObject.SetActive(false);
                }
            }
            
            AnimateLayoutSpacing(1F, onInitialized);
        }
        protected override float ValidateLayoutOffset()
        {
            return Spacing * Limit * .75F;
        }
        public override void ValidateLayout(List<CardBase> dataset)
        {
            for (var i = 0; i < dataset.Count; i++)
            {
                var id = dataset[i].Id;
                Slots[i] = ActiveSlots[id];
                ValidateLayoutElement(id, i, true);
            }
        }
        public override void ValidateLayout(bool shouldAnimate = false)
        {
            if(ActiveSlots.Count <= 1) return;

            var index = 0;
            
            foreach (var elementId in ActiveSlots.Keys)
            {
                ValidateLayoutElement(elementId, index, shouldAnimate);
                index++;
            }
        }
        public override void ValidateLayoutInOrder(bool shouldAnimate = false)
        {
            for (var i = 0; i < ActiveSlots.Count; i++)
            {
                ValidateLayoutElement(Slots[i], i, shouldAnimate);
            }
        }
        public override void ValidateLayoutElement(T element, int index, bool shouldAnimate = false)
        {
            CalculatePositionAndRotation((float)index / (ActiveSlots.Count - 1), 
                out var position, out var rotation);
            element.SetSortingOrder(index);
            element.SetPosition(position, shouldAnimate);
            element.SetRotation(rotation, shouldAnimate);
        }
        public override void ValidateLayoutElement(int layoutElementId, int sortingLayer, bool shouldAnimate)
        {
            CalculatePositionAndRotation((float)sortingLayer / (ActiveSlots.Count - 1), 
                out var position, out var rotation);
            ActiveSlots[layoutElementId].SetSortingOrder(sortingLayer);
            ActiveSlots[layoutElementId].SetPosition(position, shouldAnimate);
            ActiveSlots[layoutElementId].SetRotation(rotation, shouldAnimate);
        }
        public override void UpdateLayoutSpacingValue(float value)
        {
            Spacing = value;
            ValidateLayoutInOrder();
        }
        public override void UpdateLayoutTheme(ThemeData theme)
        {
            StartCoroutine(AnimateLayoutTheme(theme));
        }
        protected virtual void CalculatePositionAndRotation(float alpha, out Vector3 position, out Vector3 rotation)
        {
            var horizontal = Mathf.Lerp(-ValidateLayoutOffset(), ValidateLayoutOffset(), alpha) * Spacing;
            var angle = Mathf.Lerp(Spring, -Spring, alpha) * (Spacing - .2F);
            var vertical = Mathf.Lerp(angle / -20F, angle / 20F, alpha) * Spacing;
            position = new Vector3(horizontal, vertical, 0);
            rotation = new Vector3(0F, 0F, angle);
        }
        protected virtual void AnimateLayoutSpacing(float targetValue, Action onComplete)
        {
            Spacing = 0F;

            DOTween.To(() => Spacing, x => Spacing = x, targetValue, .5F)
                .OnUpdate(() =>
                {
                    ValidateLayout();
                    UIManager.Instance.UpdateDeckSliderValue(Spacing);
                    
                }).OnComplete(()=> onComplete?.Invoke());
        }
        protected virtual IEnumerator AnimateLayoutTheme(ThemeData theme)
        {
            var isReverse = theme.type == ThemeType.Theme1;
            var alpha = ActiveSlots.Count / (float) MaxSize;
            var animDuration = Mathf.Lerp(.75F, .25F, alpha);
            var intervalDuration = Mathf.Lerp(.1F, .02F,alpha);
            var waitForSeconds = new WaitForSeconds(intervalDuration);
            var index = isReverse ? ActiveSlots.Count - 1 : 0;

            while (index < ActiveSlots.Count && index >= 0)
            {
                Slots[index].UpdateVisual(theme.cardColor, animDuration);
                yield return waitForSeconds;
                index = isReverse ? index - 1 : index + 1;
            }
            
            ThemeManager.OnChangeThemeComplete?.Invoke();
        }
    }
}