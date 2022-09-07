using ProjectCard.Game.Container;
using ProjectCard.Game.Managers;

namespace ProjectCard.Game.Controller
{
    public class CardController : CardControllerBase
    {
        public override void Initialize() { }
        public override void Initialize(int id)
        {
            visual.sprite = AssetsContainer.Instance.GetCardAsset(id);
            visual.color = ThemeManager.Instance.CurrentTheme.cardColor;
        }
    }
}