using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using ProjectCard.Core.Entity;
using ProjectCard.Game.SO;
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

        public override void ValidateLayout(List<CardBase> dataset)
        {
            UnityEngine.Debug.LogWarning(dataset.Count);
            for (var i = 0; i < dataset.Count; i++)
            {
                var id = dataset[i].Id;
                Slots[i] = ActiveSlots[id];
                ValidateLayoutElement(id, i, true);
            }
        }

        public override void ValidateLayout()
        {
            if(ActiveSlots.Count <= 1) return;

            var index = 0;
            
            foreach (var elementId in ActiveSlots.Keys)
            {
                ValidateLayoutElement(elementId, index, false);
                index++;
            }
        }

        public override void ValidateLayoutElement(int layoutElementId, int sortingLayer, bool shouldAnimate)
        {
            CalculatePositionAndRotation((float)sortingLayer / (ActiveSlots.Count - 1), 
                out var position, out var rotation);
            ActiveSlots[layoutElementId].SetSortingOrder(sortingLayer);
            ActiveSlots[layoutElementId].SetPosition(position, shouldAnimate);
            ActiveSlots[layoutElementId].SetRotation(rotation, shouldAnimate);
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
            var vertical = angle * spacing / -15F;
            vertical = horizontal > 0F ? -vertical : vertical;
            position = new Vector3(horizontal, vertical, 0);
            rotation = new Vector3(0F, 0F, angle);
        }

        private void AnimateLayoutSpacing(float targetValue, Action onComplete)
        {
            spacing = 0F;

            DOTween.To(() => spacing, x => spacing = x, targetValue, .5F)
                .OnUpdate(ValidateLayout).OnComplete(()=> onComplete?.Invoke());
        }

        private IEnumerator AnimateLayoutTheme(ThemeData theme)
        {
            var waitForSeconds = new WaitForSeconds(.05F);
            var index = 0;

            while (index < ActiveSlots.Count)
            {
                Slots[index].UpdateVisual(theme.cardColor);
                yield return waitForSeconds;
                index++;
            }
        }
        
        #if UNITY_EDITOR
        private void OnValidate() { ValidateLayout(); }
        
        #endif
    }
}