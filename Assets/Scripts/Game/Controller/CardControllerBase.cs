namespace ProjectCard.Game.Controller
{
    public abstract class CardControllerBase : DeckLayoutDynamicElement
    {
        public abstract override void Initialize();
        public abstract override void Initialize(int id);
    }
}