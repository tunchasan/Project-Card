using DG.Tweening;
using UnityEngine;

namespace ProjectCard.Game.Controller
{
    public class DeckLayoutElement : DeckLayoutElementBase
    {
        private Tween _visualAnimation, _moveAnimation, _rotateAnimation;
        public override void Initialize() { }
        public override void Initialize(int id) { }
        public override void UpdateVisual(Color color, float animDuration)
        {
            _visualAnimation?.Kill();
            _visualAnimation = DOTween.To(() => visual.color, x => visual.color = x, color, animDuration);
        }
        public override void SetSortingOrder(int order)
        {
            visual.sortingOrder = order;
        }
        public override void SetPosition(Vector3 targetPos, bool shouldAnimate = false)
        {
            _moveAnimation?.Kill();

            if (shouldAnimate)
            {
                _moveAnimation = transform.DOLocalMove(targetPos, .25F);
                return;
            }
            
            transform.localPosition = targetPos;
        }
        public override void SetRotation(Vector3 targetRot, bool shouldAnimate = false)
        {
            _rotateAnimation?.Kill();

            if (shouldAnimate)
            {
                _moveAnimation = transform.DOLocalRotate(targetRot, .25F);
                return;
            }
            
            transform.localEulerAngles = targetRot;
        }
    }
}