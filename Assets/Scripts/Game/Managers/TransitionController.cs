using DG.Tweening;
using UnityEngine;

namespace ProjectCard.Game.Managers
{
    public class TransitionController : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera = null;
        [SerializeField] private bool playOnAwake = false;

        private void Awake()
        {
            if (playOnAwake)
            {
                PlayTransitionAnimation(true);
            }
        }

        public void PlayTransitionAnimation(bool isReverse = false)
        {
            var duration = isReverse ? 2F : 1F;
            mainCamera.DOOrthoSize(isReverse ? 5F : .01F, duration)
                .SetEase(isReverse ? Ease.OutExpo : Ease.InExpo);
        }
    }
}