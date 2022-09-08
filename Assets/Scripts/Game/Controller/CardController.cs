using ProjectCard.Game.Container;
using ProjectCard.Game.Managers;
using UnityEngine;

namespace ProjectCard.Game.Controller
{
    public class CardController : CardControllerBase
    {
        public override void Initialize() { }
        public override void Initialize(int id)
        {
            Camera = Camera.main;
            visual.sprite = AssetsContainer.Instance.GetCardAsset(id);
            visual.color = ThemeManager.Instance.CurrentTheme.cardColor;
            LayoutElementId = id;
        }
    }
}