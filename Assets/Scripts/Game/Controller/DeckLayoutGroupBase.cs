using System.Collections.Generic;
using ProjectCard.Core.Entity;
using UnityEngine;

namespace ProjectCard.Game.Controller
{
    public abstract class DeckLayoutGroupBase : MonoBehaviour
    {
        [Header("@Configurations")]
        [SerializeField] private DeckLayoutElementBase layoutElement = null;

        protected List<DeckLayoutElementBase> Elements = new();
        public abstract void Initialize(List<CardBase> elements);
    }
}