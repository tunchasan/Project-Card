using UnityEngine;

namespace ProjectCard.Game.Controller
{
    public abstract class DynamicElementAnimationBase : MonoBehaviour
    {
        [SerializeField] protected Animator animator = null;

        public abstract void PlayAnimation(string key);
        public abstract void StopAnimation(string key);
    }
}