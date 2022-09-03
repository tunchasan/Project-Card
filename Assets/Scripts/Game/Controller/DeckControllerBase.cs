using ProjectCard.Core.Entity;
using UnityEngine;

namespace ProjectCard.Game.Controller
{
    public abstract class DeckControllerBase : MonoBehaviour
    {
        protected DeckProvider DeckProvider = null;
        protected SessionBase Session = null;
        protected virtual void Start()
        {
            Initialize();
        }
        protected abstract void Initialize();
    }
}