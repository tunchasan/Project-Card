using UnityEngine;

namespace ProjectCard.Game.Controller
{
    public abstract class DeckLayoutElementBase : MonoBehaviour
    {
        [SerializeField] protected SpriteRenderer visual;
        public int LayoutElementId { protected set; get; } = 0;

        public abstract void Initialize();
        public abstract void Initialize(int id);
        public abstract void UpdateVisual(Color color, float animDuration);
        public abstract void SetSortingOrder(int order);
        public abstract void SetPosition(Vector3 targetPos, bool shouldAnimate = false);
        public abstract void SetRotation(Vector3 targetRot, bool shouldAnimate = false);
    }
}
