using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using ProjectCard.Core.Entity;
using ProjectCard.Game.Managers;
using ProjectCard.Game.SO;
using ProjectCard.Game.Utilities;
using UnityEngine;

namespace ProjectCard.Game.Controller
{
    public class DeckLayoutGroup : DeckLayoutGroupBase
    {
        public override void PreInitialize()
        {
            for (var i = 0; i < MaxSize; i++)
            {
                var elem = Instantiate(layoutElement, transform);
                elem.Initialize();
                Slots.Add(elem);
                elem.gameObject.SetActive(false);

                elem.OnElementSelect += OnAnElementSelected;
                elem.OnElementDrag += OnAnElementDragging;
                elem.OnElementDrop += OnAnElementDropped;
            }
        }

        private void OnAnElementSelected(DeckLayoutElementBase obj)
        {
            var key = ActiveSlots.First(elem => elem.Value == obj).Key;
            ActiveSlots.Remove(key);
            Slots.Remove(obj);
            ValidateLayout(true);
        }

        private void OnAnElementDragging(DeckLayoutElementBase arg1, Vector3 arg2)
        {
            
        }
        
        private void OnAnElementDropped(DeckLayoutElementBase obj)
        {
            
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

        public override void ValidateLayoutInOrder()
        {
            for (var i = 0; i < ActiveSlots.Count; i++)
            {
                ValidateLayoutElement(Slots[i], i);
            }
        }
        
        public override void ValidateLayoutElement(DeckLayoutElementBase element, int index)
        {
            CalculatePositionAndRotation((float)index / (ActiveSlots.Count - 1), 
                out var position, out var rotation);
            element.SetSortingOrder(index);
            element.SetPosition(position);
            element.SetRotation(rotation);
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
            spacing = value;
            ValidateLayoutInOrder();
        }

        public override void UpdateLayoutTheme(ThemeData theme)
        {
            StartCoroutine(AnimateLayoutTheme(theme));
        }

        private void CalculatePositionAndRotation(float alpha, out Vector3 position, out Vector3 rotation)
        {
            var offset = spacing * Limit * .75F;
            var horizontal = Mathf.Lerp(-offset, offset, alpha) * spacing;
            var angle = Mathf.Lerp(Spring, -Spring, alpha) * (spacing - .2F);
            var vertical = Mathf.Lerp(angle / -20F, angle / 20F, alpha) * spacing;
            position = new Vector3(horizontal, vertical, 0);
            rotation = new Vector3(0F, 0F, angle);
        }

        private void AnimateLayoutSpacing(float targetValue, Action onComplete)
        {
            spacing = 0F;

            DOTween.To(() => spacing, x => spacing = x, targetValue, .5F)
                .OnUpdate(() =>
                {
                    ValidateLayout();
                    UIManager.Instance.UpdateDeckSliderValue(spacing);
                    
                }).OnComplete(()=> onComplete?.Invoke());
        }

        private IEnumerator AnimateLayoutTheme(ThemeData theme)
        {
            var isReverse = theme.type == ThemeType.Theme1;
            var alpha = ActiveSlots.Count / (float) MaxSize;
            var animDuration = Mathf.Lerp(.75F, .25F, alpha);
            var intervalDuration = Mathf.Lerp(.12F, .02F,alpha);
            var waitForSeconds = new WaitForSeconds(intervalDuration);
            var index = isReverse ? ActiveSlots.Count - 1 : 0;

            while (index < ActiveSlots.Count && index >= 0)
            {
                Slots[index].UpdateVisual(theme.cardColor, animDuration);
                yield return waitForSeconds;
                index = isReverse ? index - 1 : index + 1;
            }
        }
        
        #if UNITY_EDITOR
        private void OnValidate() { ValidateLayout(); }
        
        #endif
    }
}