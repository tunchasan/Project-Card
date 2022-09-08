namespace ProjectCard.Game.Controller
{
    public abstract class DeckLayoutDynamicGroupBase : DeckStandardLayoutGroupBase<DeckLayoutDynamicElement>
    {
        protected int LastIndex = 0;

        public override void PreInitialize()
        {
            base.PreInitialize();
            SubscribeLayoutElements();
        }
        protected abstract void SubscribeLayoutElements();
        protected abstract void UnSubscribeLayoutElements();
        protected abstract void OnAnElementSelected(DeckLayoutDynamicElement element);
        protected abstract void OnAnElementDragging(DeckLayoutDynamicElement element, float horizontal);
        protected abstract void OnAnElementDropped(DeckLayoutDynamicElement element, float horizontal);
        protected abstract int GetClosestIndex(float horizontal);
        protected override void OnDisable()
        {
            base.OnDisable();
            UnSubscribeLayoutElements();
        }
    }
}