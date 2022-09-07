using UnityEngine;

namespace ProjectCard.Game.Controller
{
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class DeckLayoutElementBase : MonoBehaviour
    {
        [SerializeField] protected SpriteRenderer visual;
        public abstract void Initialize();
        public abstract void Initialize(int id);
        public abstract void UpdateVisual(Color color, float animDuration);
        public abstract void SetSortingOrder(int order);
        public abstract void SetPosition(Vector3 targetPos, bool shouldAnimate = false);
        public abstract void SetRotation(Vector3 targetRot, bool shouldAnimate = false);
    }
}