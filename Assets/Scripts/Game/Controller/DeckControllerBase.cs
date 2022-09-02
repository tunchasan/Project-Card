using ProjectCard.Core.Entity;
using UnityEngine;

namespace ProjectCard.Game.Controller
{
    public abstract class DeckControllerBase : MonoBehaviour
    {
        protected DeckProviderBase DeckProvider = null;
        protected virtual void Awake()
        {
            DeckProvider = new DeckProvider();
        }
        protected virtual void Start()
        {
            Initialize();
        }
        protected abstract void Initialize();
    }
}