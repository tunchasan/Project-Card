using System;
using UnityEngine;

namespace ProjectCard.Game.Controller
{
    public class DeckLayoutDynamicElement : DeckLayoutElement, IMovable
    {
        [SerializeField] protected DynamicElementAnimationBase animationController;
        public Action<DeckLayoutDynamicElement> OnElementSelect;
        public Action<DeckLayoutDynamicElement, float> OnElementDrag;
        public Action<DeckLayoutDynamicElement, float> OnElementDrop;
        private Vector3 _offset = Vector3.zero;

        public DynamicElementAnimationBase AnimationController => animationController;

        public void OnMouseDown()
        {
            _offset = transform.position - GetMouseAsWorldPoint();
            OnElementSelect?.Invoke(this);
        }
        
        private Vector3 GetMouseAsWorldPoint()
        {
            var mousePoint = Input.mousePosition;
            return Camera.ScreenToWorldPoint(mousePoint);

        }
        public void OnMouseDrag()
        {
            var targetTransform = transform;
            var targetPosition = GetMouseAsWorldPoint() + _offset;
            
            #if UNITY_EDITOR
            
            var currentPosition = targetTransform.position;

            if (Mathf.Abs(targetPosition.x) <= 9F && Mathf.Abs(targetPosition.y) <= 5F)
            {
                targetTransform.position = Vector3.Lerp(currentPosition,targetPosition, Time.deltaTime * 20F);
                OnElementDrag?.Invoke(this, targetTransform.position.x);
            }
            
            #endif
            
            #if !UNITY_EDITOR
            
            targetTransform.position = targetPosition;
            OnElementDrag?.Invoke(this, targetTransform.position.x);

            #endif
        }

        public void OnMouseUp()
        {
            OnElementDrop?.Invoke(this, transform.position.x);
        }
    }
}