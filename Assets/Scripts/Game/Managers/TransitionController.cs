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
            mainCamera.DOOrthoSize(isReverse ? 5F : .01F, 2F).SetEase(Ease.OutExpo);
        }
    }
}