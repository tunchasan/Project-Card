using System;
using ProjectCard.Game.Managers;
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
        private bool _hasDrawn = false;

        public DynamicElementAnimationBase AnimationController => animationController;

        public void OnMouseDown()
        {
            if(ThemeManager.Instance.OnThemeChancing) return;
            _hasDrawn = true;
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
            if(ThemeManager.Instance.OnThemeChancing || !_hasDrawn) return;

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
            if(ThemeManager.Instance.OnThemeChancing || !_hasDrawn) return;

            OnElementDrop?.Invoke(this, transform.position.x);
            _hasDrawn = false;
        }
    }
}