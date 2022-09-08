namespace ProjectCard.Game.Controller
{
    public class DynamicElementAnimation : DynamicElementAnimationBase
    {
        public override void PlayAnimation(string key)
        {
            animator.SetBool(key, true);
        }

        public override void StopAnimation(string key)
        {
            animator.SetBool(key, false);
        }
    }
}