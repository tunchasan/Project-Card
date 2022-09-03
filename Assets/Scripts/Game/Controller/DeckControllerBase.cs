using ProjectCard.Core.Entity;
using UnityEngine;

namespace ProjectCard.Game.Controller
{
    public abstract class DeckControllerBase : MonoBehaviour
    {
        protected ISortable Sortable = null;
        protected SessionBase Session = null;
        protected virtual void Start()
        {
            Initialize();
        }
        protected abstract void Initialize();
    }
}