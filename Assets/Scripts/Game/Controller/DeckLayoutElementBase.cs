using System;
using UnityEngine;

namespace ProjectCard.Game.Controller
{
    public abstract class DeckLayoutElementBase : MonoBehaviour, IMovable
    {
        [SerializeField] protected SpriteRenderer visual;
        public int LayoutElementId { protected set; get; } = 0;

        public abstract void Initialize();
        public abstract void Initialize(int id);
        public abstract void UpdateVisual(Color color, float animDuration);
        public abstract void SetSortingOrder(int order);
        public abstract void SetPosition(Vector3 targetPos, bool shouldAnimate = false);
        public abstract void SetRotation(Vector3 targetRot, bool shouldAnimate = false);
        
        #region MoveSystem

        public Action<DeckLayoutElementBase> OnElementSelect;
        public Action<DeckLayoutElementBase, float> OnElementDrag;
        public Action<DeckLayoutElementBase, float> OnElementDrop;
        public abstract void OnMouseDown();
        public abstract void OnMouseDrag();
        public abstract void OnMouseUp();
        
        #endregion
    }
}