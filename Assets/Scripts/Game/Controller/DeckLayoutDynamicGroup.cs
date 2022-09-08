using System;
using UnityEngine;

namespace ProjectCard.Game.Controller
{
    public class DeckLayoutDynamicGroup : DeckLayoutDynamicGroupBase
    {
        protected override void SubscribeLayoutElements()
        {
            for (var i = 0; i < MaxSize; i++)
            {
                Slots[i].OnElementSelect += OnAnElementSelected;
                Slots[i].OnElementDrag += OnAnElementDragging;
                Slots[i].OnElementDrop += OnAnElementDropped;
            }
        }
        protected override void UnSubscribeLayoutElements()
        {
            for (var i = 0; i < MaxSize; i++)
            {
                Slots[i].OnElementSelect -= OnAnElementSelected;
                Slots[i].OnElementDrag -= OnAnElementDragging;
                Slots[i].OnElementDrop -= OnAnElementDropped;
            }
        }
        protected override void OnAnElementSelected(DeckLayoutDynamicElement element)
        {
            element.SetSortingOrder(999);
            ActiveSlots.Remove(element.LayoutElementId);
            Slots.Remove(element);
            ValidateLayoutInOrder(true);
        }
        protected override void OnAnElementDragging(DeckLayoutDynamicElement element, float horizontal)
        {
            Slots[LastIndex].AnimationController.StopAnimation("right");
            Slots[LastIndex].AnimationController.StopAnimation("left");
        
            if (LastIndex + 1 < Slots.Count)
            {
                Slots[LastIndex + 1].AnimationController.StopAnimation("right");
                Slots[LastIndex + 1].AnimationController.StopAnimation("left");
            }
                    
            var index = GetClosestIndex(horizontal);
                    
            CalculatePositionAndRotation((float)index / (ActiveSlots.Count - 1), out var position, out var rotation);
            element.SetRotation(rotation, true);
        
            if (index == -1)
            {
                
                Slots[0].AnimationController.PlayAnimation("right");
                LastIndex = 0;
            }
                    
            else if (index == ActiveSlots.Count)
            {
                Slots[index - 1].AnimationController.PlayAnimation("left");
                LastIndex = index - 1;
            }
        
            else
            {
                var targetIndex = Math.Clamp(index + 1, 0, Slots.Count - 1);
                Slots[index].AnimationController.PlayAnimation("left");
                Slots[targetIndex].AnimationController.PlayAnimation("right");
                LastIndex = index;
            }
        }
        protected override void OnAnElementDropped(DeckLayoutDynamicElement element, float horizontal)
        {
            Slots[LastIndex].AnimationController.StopAnimation("right");
            Slots[LastIndex].AnimationController.StopAnimation("left");
        
            if (LastIndex + 1 < Slots.Count)
            {
                Slots[LastIndex + 1].AnimationController.StopAnimation("right");
                Slots[LastIndex + 1].AnimationController.StopAnimation("left");
            }
                    
            var index = GetClosestIndex(horizontal);
        
            if (index == -1)
            {
                Slots.Insert(0, element);
            }
                    
            else if (index == ActiveSlots.Count)
            {
                Slots.Insert(index, element);
            }
        
            else
            {
                var targetIndex = Math.Clamp(index + 1, 0, ActiveSlots.Count);
                Slots.Insert(targetIndex, element);
            }
                    
            ActiveSlots.Add(element.LayoutElementId, element);
            ValidateLayoutInOrder(true);
        }
        protected override int GetClosestIndex(float horizontal)
        {
            var alpha = (horizontal / (Spacing * ValidateLayoutOffset() * transform.localScale.x) + 1) / 2F;
            var closestIndex = Mathf.FloorToInt(alpha * ActiveSlots.Count);
            closestIndex = Math.Clamp(closestIndex, -1, ActiveSlots.Count);
            return closestIndex;
        }
    }
}